using TMPro;
using UnityEngine;

public class SendPlayerData : MonoBehaviour
{
    public TMP_InputField screenName;
    public TMP_InputField firstName;
    public TMP_InputField lastName;
    public TMP_InputField dateStarted;
    public TMP_InputField score;
    public PostData post;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SendData()
    {
        if(firstName.text != "" && screenName.text != "" && lastName.text != "" && dateStarted.text != "" && score.text != "")
        {
            post.SetupPlayerData(screenName.text, firstName.text, lastName.text, dateStarted.text, score.text);
        }
    }

    public void SendDataUpdate()
    {
        if (firstName.text != "" && screenName.text != "" && lastName.text != "" && dateStarted.text != "" && score.text != "")
        {
            post.SetupPlayerDataUpdate(screenName.text, firstName.text, lastName.text, dateStarted.text, score.text);
        }
    }
}
