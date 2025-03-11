using UnityEngine;
using TMPro;
using Unity.VisualScripting;

public class PlayerScore : MonoBehaviour
{
    public TMP_Text scoreText;
    public int score;

    public void Start()
    {
        scoreText = GameObject.FindWithTag("ScoreText").GetComponentInChildren<TMP_Text>();
    }

    // Update is called once per frame
    void Update()
    {
        scoreText.text = $"Score: {score}";
    }
}
