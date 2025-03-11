using UnityEngine;
using Mirror;
using System;
using Telepathy;

public class GameTimer : NetworkBehaviour
{
    [SyncVar] public float timeRemaining = 60.0f;


    // Update is called once per frame
    void Update()
    {
        if (!isServer) return;

        timeRemaining -= Time.deltaTime;

        if (timeRemaining <= 0)
        {
            //End Game
            EndGame();
        }
    }

    [Server]
    private void EndGame()
    {
        Time.timeScale = 0;
        RPCShowWin();
    }

    [ClientRpc]
    void RPCShowWin()
    {
        //Debug.Log( itWins? "It wins":"Survivors win");
        DisplayTimerForPlayer localPlayer = FindFirstObjectByType<DisplayTimerForPlayer>();
        localPlayer.DisplayMenu();
    }
}
