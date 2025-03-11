using System.Collections;
using UnityEngine;

public class BulletScript : MonoBehaviour
{
    public PlayerScore playerScore; // Reference to the player's score
    private bool hasScored = false; // Prevents multiple score additions

    public void Initialize(PlayerScore score)
    {
        playerScore = score; // Assign the shooter's PlayerScore
    }

    void Start()
    {
        StartCoroutine(BulletCooldown());
    }

    IEnumerator BulletCooldown()
    {
        yield return new WaitForSeconds(3);
        Destroy(gameObject); // Destroy the actual bullet object
    }

    private void OnTriggerEnter(Collider other)
    {
        if (hasScored || playerScore == null) return; // Prevent multiple score additions

        int scoreToAdd = 0;

        if (other.gameObject.CompareTag("Outer")) scoreToAdd = 10;
        if (other.gameObject.CompareTag("Inner1")) scoreToAdd = 20;
        if (other.gameObject.CompareTag("Inner2")) scoreToAdd = 30;
        if (other.gameObject.CompareTag("Inner3")) scoreToAdd = 40;
        if (other.gameObject.CompareTag("Inner4")) scoreToAdd = 50;
        if (other.gameObject.CompareTag("Center")) scoreToAdd = 100;

        if (scoreToAdd > 0)
        {
            playerScore.score += scoreToAdd; // Update only the shooting player's score
            hasScored = true;
            Destroy(gameObject);
        }
    }
}