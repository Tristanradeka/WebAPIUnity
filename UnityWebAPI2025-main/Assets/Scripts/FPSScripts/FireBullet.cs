using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class FireBullet : MonoBehaviour
{
    public GameObject bulletPrefab;
    public MouseLook playerLook;
    [SerializeField] private float bulletSpeed = 10.0f;
    public Transform gunTip;
    [SerializeField] private float cooldownLength = 1.75f;
    public bool canShoot = true;
    public PlayerScore playerScore; // This player's score reference

    public void Shoot(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            if (canShoot)
            {
                // Create bullet at gunTip position
                GameObject bullet = Instantiate(bulletPrefab, gunTip.position, gunTip.rotation);
                BulletScript bulletScript = bullet.GetComponent<BulletScript>();
                if (bulletScript != null)
                {
                    bulletScript.Initialize(playerScore); // Assign this player's score
                }

                // Adjust the bullet's rotation to be horizontal
                bullet.transform.rotation = Quaternion.LookRotation(gunTip.forward) * Quaternion.Euler(90, 0, 0);

                // Get Rigidbody of the bullet
                Rigidbody bulletRB = bullet.GetComponent<Rigidbody>();

                if (bulletRB != null)
                {
                    // Apply force in the shooting direction
                    bulletRB.AddForce(gunTip.forward * bulletSpeed, ForceMode.Impulse);
                }
                StartCoroutine(ShotCooldown());
            }
        }
    }

    IEnumerator ShotCooldown()
    {
        canShoot = false;
        yield return new WaitForSeconds(cooldownLength);
        canShoot = true;
    }
}