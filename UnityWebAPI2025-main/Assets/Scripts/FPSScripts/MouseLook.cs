using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class MouseLook : MonoBehaviour
{
    public enum RotationAxis
    {
        MouseXAndY = 0,
        MouseX = 1,
        MouseY = 2
    }

    public RotationAxis axis = RotationAxis.MouseXAndY;

    public float sensitivityX = 5f;
    public float sensitivityY = 5f;
    public float maxVerticalRotation = 45f;
    public float minVerticalRotation = -45f;

    public float mouseX;
    public float mouseY;
    private float verticalRotation = 0;
    

    // Start is called before the first frame update
    void Start()
    {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;   
        Rigidbody rb = GetComponent<Rigidbody>();

        if (rb != null)
        {
            rb.freezeRotation = true;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (axis == RotationAxis.MouseX)
        {
            //Rotation X
            transform.Rotate(0, mouseX * sensitivityX * Time.deltaTime,0);
        }
        else if (axis == RotationAxis.MouseY)
        {
            //Rotation Y
           verticalRotation -= mouseY * sensitivityY * Time.deltaTime;
           verticalRotation = Mathf.Clamp(verticalRotation, minVerticalRotation, maxVerticalRotation);

           float horizontalRotation = transform.localEulerAngles.y;

           transform.localEulerAngles = new Vector3(verticalRotation, horizontalRotation, 0);
        }
        else
        {
            //Rotation X and Y simultaneous
            verticalRotation -= mouseY * sensitivityY;
            verticalRotation = Mathf.Clamp(verticalRotation, minVerticalRotation, maxVerticalRotation);

            float deltaRotation = mouseX * sensitivityX;
            float horizontalRotation = transform.localEulerAngles.y + deltaRotation;

            transform.localEulerAngles = new Vector3(verticalRotation, horizontalRotation, 0);
        }
    }

    public void LookValues(InputAction.CallbackContext context)
    {
        //Debug.Log(context.ReadValue<Vector2>());
        mouseX = context.ReadValue<Vector2>().x;
        mouseY = context.ReadValue<Vector2>().y;    
    }
}
