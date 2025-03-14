using TMPro;
using UnityEngine;

public class SendPlayerData : MonoBehaviour
{
    public TMP_InputField screenName;
    public TMP_InputField firstName;
    public TMP_InputField lastName;
    public TMP_InputField numberOfGamesPlayed;
    public TMP_InputField score;
    public PostData post;
    public PlayerScore playerScore;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        post = FindAnyObjectByType<PostData>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SendData()
    {
        if(firstName.text != "" && screenName.text != "" && lastName.text != "" && numberOfGamesPlayed.text != "" && score.text != "")
        {
            post.SetupPlayerData(screenName.text, firstName.text, lastName.text, numberOfGamesPlayed.text, score.text);
        }
    }

    public void SendDataUpdate()
    {
        if (firstName.text != "" && screenName.text != "" && lastName.text != "" && numberOfGamesPlayed.text != "" && score.text != "")
        {
            post.SetupPlayerDataUpdate(screenName.text, firstName.text, lastName.text, numberOfGamesPlayed.text, score.text);
        }
    }

    public void SendDataEndGame()
    {
        if (firstName.text != "" && screenName.text != "" && lastName.text != "" && numberOfGamesPlayed.text != "")
        {
            string score = playerScore.score.ToString();
            post.SetupPlayerDataEndGame(screenName.text, firstName.text, lastName.text, numberOfGamesPlayed.text, score);
        }
    }
}
