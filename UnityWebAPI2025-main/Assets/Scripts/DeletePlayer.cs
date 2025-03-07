using TMPro;
using UnityEngine;


public class DeletePlayer : MonoBehaviour
{
    public TMP_InputField screenName;
    public FetchData fetch;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
    public void SearchForPlayer()
    {
        if (screenName.text == "")
        {
            Debug.LogError("ScreenName or PlayerID is empty!");
            return;
        }

        Debug.Log($"Deleting player: {screenName.text}");
        fetch.SetupPlayerDeleteData(screenName.text);
    }



}
