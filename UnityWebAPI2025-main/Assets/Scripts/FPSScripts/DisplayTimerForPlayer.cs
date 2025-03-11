using UnityEngine;
using TMPro;
public class DisplayTimerForPlayer : MonoBehaviour
{
    public GameTimer timerScript;
    public TMP_Text timerText;
    public GameObject submitMenu;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        timerScript = FindAnyObjectByType<GameTimer>();       
    }

    // Update is called once per frame
    void Update()
    {
        timerText.text = $"Time: 00:00:{(int)timerScript.timeRemaining}";
    }

    public void DisplayMenu()
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        submitMenu.SetActive(true);
    }
}
