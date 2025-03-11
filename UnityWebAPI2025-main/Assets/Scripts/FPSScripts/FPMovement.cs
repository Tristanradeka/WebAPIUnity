using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Rendering;
using UnityEngine.SceneManagement;

//Automatically adds CharacterController component to GameObject
[RequireComponent(typeof(CharacterController))]

public class FPMovement : MonoBehaviour
{
    public float speed = 10;
    //public float crouchSpeed = 5;
    float h, v;
    public float gravity = -9.8f;
    public float jumpStrength = 10f;
    public float velocity;
    public float gravityMultiplier = 3f;
    float height;
    public AudioSource walk;
    public AudioSource jump;
    public Transform player;
    

    //Crouch values 
    [SerializeField]private float crouchHeight = 0.5f;
    [SerializeField] private float standingHeight = 2.0f;
    [SerializeField] private float timeToCrouch = 0.25f;
    [SerializeField] private Vector3 crouchCenter = new Vector3(0, 0.5f, 0);
    [SerializeField] private Vector3 standingCenter = new Vector3(0, 0, 0);
    public bool isCrouching;
    public Vector3 moveRope;

    //Double jump value
    public int jumpCtr = 0;

    public CharacterController controller;
    // Start is called before the first frame update
    void Start()
    {
        controller = GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {
        float moveX = h * speed;
        float moveZ = v * speed;
        Vector3 movement = new Vector3(moveX,0,moveZ);

        movement = Vector3.ClampMagnitude(movement, speed);

        moveRope = movement;

        if (isGrounded() && velocity < 0)
        {
            velocity = -1;
            //Destroy(walk);
        }
        else
        {
            velocity += gravity * gravityMultiplier * Time.deltaTime;
            //walk.Play();
        }

        movement.y = velocity;

        movement *= Time.deltaTime;
        movement = transform.TransformDirection(movement);
        controller.Move(movement);

       
    }

    public void MoveInput(InputAction.CallbackContext context)
    {
        h = context.ReadValue<Vector2>().x;
        v = context.ReadValue<Vector2>().y;
        if (h == 0 && v == 0)
        {
            walk.Stop();   
        }
    }

    public void Jump(InputAction.CallbackContext context)
    {
        if (!isGrounded())
        {
            return;
        }

        if (isGrounded())
        {
            jumpCtr = 0;
        }

        if (context.performed)
        {
            velocity += jumpStrength;
            jumpCtr+=1;
        }
        

    }

    public void Crouch(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            StartCoroutine(Crouch());
            Debug.Log("Crouch call");
            
        }
    }

    

    public bool isGrounded()
    {
        return controller.isGrounded;
    }

    IEnumerator Crouch()
    {
        float timeElapsed = 0;
        float targetHeight = isCrouching? standingHeight : crouchHeight;
        float currentHeight = controller.height;
        Vector3 targetCenter = isCrouching? standingCenter: crouchCenter;
        Vector3 currentCenter = controller.center;

        while(timeElapsed < timeToCrouch) 
        {
            controller.height = Mathf.Lerp(currentHeight, targetHeight, timeElapsed/timeToCrouch);
            controller.center = Vector3.Lerp(currentCenter, targetCenter, timeElapsed / timeToCrouch);
            timeElapsed += Time.deltaTime;
            yield return null;
        }

        controller.height = targetHeight;
        controller.center = targetCenter;

        isCrouching = !isCrouching;
    }
}
