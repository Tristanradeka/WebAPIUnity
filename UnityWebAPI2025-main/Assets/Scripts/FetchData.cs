using JetBrains.Annotations;
using Newtonsoft.Json;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using TMPro;
using System.Text;
using System;
using UnityEngine.UI;

public class FetchData : MonoBehaviour
{
    string serverUrl = "http://localhost:3000/player";
    string serverUrl2 = "http://localhost:3000/deletePlayer";
    string serverUrl3 = "http://localhost:3000/topTen";

    List<PlayerData> playerList;
    PlayerData player;
    public GameObject playerData;
    public GameObject showAllMenu;
    public GameObject top10Menu;
     
    public GameObject deleteSuccess;

    public TMP_Text[] topTenTextObjs;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        //StartFetch();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public IEnumerator GetData()
    {
        using (UnityWebRequest request = UnityWebRequest.Get(serverUrl))
        {
            yield return request.SendWebRequest();

            if (request.result == UnityWebRequest.Result.Success)
            {
                string json = request.downloadHandler.text;
                Debug.Log($"Received data: {json}");

                // Deserialize the JSON response into a list of PlayerData objects
                playerList = JsonConvert.DeserializeObject<List<PlayerData>>(json);


                if (playerList == null)
                {
                    Debug.LogError("playerList is null. JSON might not be deserializing correctly.");
                    yield break;
                }
                foreach (var player in playerList)
                {
                    // Create a new GameObject for the text
                    GameObject textObj = new GameObject("PlayerText");

                    // Set the parent to ensure it's inside the VerticalLayoutGroup
                    textObj.transform.SetParent(showAllMenu.transform, false);

                    // Add RectTransform for UI positioning
                    RectTransform rectTransform = textObj.AddComponent<RectTransform>();

                    // Use TextMeshProUGUI instead of TMP_Text
                    TextMeshProUGUI tmpText = textObj.AddComponent<TextMeshProUGUI>();


                    // Set properties for visibility
                    tmpText.fontSize = 24;
                    tmpText.alignment = TextAlignmentOptions.Center;
                    tmpText.enableAutoSizing = true;

                    // Assign player data to the text
                    tmpText.text = $"{player.screenName} {player.firstName} {player.lastName} {player.numberOfGamesPlayed} {player.score}";
                }
            }
            else
            {
                Debug.LogError($"Error fetching data: {request.error}");
            }
        }
    }

    public IEnumerator GetTopTen()
    {
        using (UnityWebRequest request = UnityWebRequest.Get(serverUrl3))
        {
            yield return request.SendWebRequest();

            if (request.result == UnityWebRequest.Result.Success)
            {
                string json = request.downloadHandler.text;
                Debug.Log($"Received JSON: {json}");

                playerList = JsonConvert.DeserializeObject<List<PlayerData>>(json);

                if (playerList == null || playerList.Count == 0)
                {
                    Debug.LogError("No players received.");
                    yield break;
                }

                Debug.Log($"Displaying {playerList.Count} players");

                int ctr = 0;
                // Display top players
                foreach (var player in playerList)
                {
                    Debug.Log($"Displaying: {player.screenName} - {player.score}");

                    topTenTextObjs[ctr].text += $"{player.screenName} - {player.score}";

                    ctr++;
                }


                //// Force UI update to refresh layout
                //LayoutRebuilder.ForceRebuildLayoutImmediate(top10Menu.GetComponent<RectTransform>());
            }
            else
            {
                Debug.LogError($"Error fetching top 10 players: {request.error}");
            }
        }
    }


    public IEnumerator GetDataByID(string json, string playerid = "")
    {
        string url = serverUrl + "/" + playerid;
        Debug.Log(url);
        byte[] jsonToSend = Encoding.UTF8.GetBytes(json);
        UnityWebRequest request = new UnityWebRequest(url, "GET");
        request.uploadHandler = new UploadHandlerRaw(jsonToSend);
        request.downloadHandler = new DownloadHandlerBuffer();
        request.SetRequestHeader("Content-Type", "application/json");

        yield return request.SendWebRequest();

        if (request.result == UnityWebRequest.Result.Success)
        {
            string response = request.downloadHandler.text;
            Debug.Log($"Success: {response}");

            //Extract playerid
            string newPlayerId = ExtractPlayerId(response);
            if (!string.IsNullOrEmpty(newPlayerId))
            {
                Debug.Log("PlayerID: " + newPlayerId);
            }
            GetPlayer(ExtractPlayerData(response));
            yield return null;
        }
        else
        {
            //Handles Error
            Debug.Log("Error: " + request.error);
            yield return null;
        }
    }

    public IEnumerator DeleteDataByScreenName(string screenName)
    {
        if (string.IsNullOrEmpty(serverUrl2))
        {
            Debug.LogError("serverUrl2 is null or empty!");
            yield break;
        }

        string url = serverUrl2 + "/" + screenName;
        Debug.Log("Deleting player at: " + url);

        using (UnityWebRequest request = UnityWebRequest.Delete(url))
        {
            request.SetRequestHeader("Content-Type", "application/json");

            yield return request.SendWebRequest();

            if (request.result == UnityWebRequest.Result.Success)
            {
                Debug.Log($"Success!");
                deleteSuccess.SetActive(true);
            }
            else
            {
                Debug.LogError("Error: " + request.error);
            }
        }
    }

    public void StartFetch()
    {
        StartCoroutine(GetData());
    }

    public void StartFetch2()
    {
        StartCoroutine(GetTopTen());
    }

    public void SetupPlayerSearchData(string screenName, string playerid)
    {
        player = new PlayerData();

        player.screenName = screenName;


        string json = JsonUtility.ToJson(player);
        Debug.Log(json);
        StartCoroutine(GetDataByID(json, playerid));

    }

    public void SetupPlayerDeleteData(string screenName)
    {
        player = new PlayerData();
        player.screenName = screenName;

        string json = JsonUtility.ToJson(player);
        Debug.Log(json);

        StartCoroutine(DeleteDataByScreenName(screenName)); 
    }



    public void GetPlayer(string[] data)
    {
        playerData.transform.GetChild(0).GetComponent<TMP_Text>().text = data[0];
        playerData.transform.GetChild(1).GetComponent<TMP_Text>().text = data[1];
        playerData.transform.GetChild(2).GetComponent<TMP_Text>().text = data[2];
        playerData.transform.GetChild(3).GetComponent<TMP_Text>().text = data[3];
        playerData.transform.GetChild(4).GetComponent<TMP_Text>().text = data[4];
    }

    string ExtractPlayerId(string jsonResponse)
    {
        int index = jsonResponse.IndexOf("\"playerid\":\"") + 12;
        if (index < 12) return "";
        int endIndex = jsonResponse.IndexOf("\"", index);
        return jsonResponse.Substring(index, endIndex - index);

    }

    string[] ExtractPlayerData(string jsonResponse)
    {
        try
        {
            // Deserialize JSON response into a PlayerData object
            PlayerData playerData = JsonConvert.DeserializeObject<PlayerData>(jsonResponse);

            // Ensure the array is properly sized before assignment
            string[] data = new string[5];

            // Assign extracted values
            data[0] = playerData.screenName;
            data[1] = playerData.firstName;
            data[2] = playerData.lastName;
            data[3] = playerData.numberOfGamesPlayed;
            data[4] = playerData.score;

            return data;
        }
        catch (Exception e)
        {
            Debug.LogError("Error extracting player data: " + e.Message);
            return new string[0]; // Return an empty array on failure
        }
    }
}

public class PlayerData
{
    public string screenName;
    public string firstName;
    public string lastName;
    public string numberOfGamesPlayed;
    public string score;
}
