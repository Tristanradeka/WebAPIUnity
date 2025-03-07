using UnityEngine;
using Mirror;
using System;

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
        int frozenCount = 0;

        PlayerTag[] players = FindObjectsByType<PlayerTag>(FindObjectsSortMode.None);

        foreach (var player in players)
        {
            if (player.isFrozen)
            {
                frozenCount++;
            }
        }

        if (frozenCount == players.Length - 1)
        {
            //'It' Player wins
            RPCShowWin(true);
        }
        else
        {
            //Survivors win
            RPCShowWin(false);
        }
    }

    [ClientRpc]
    void RPCShowWin(bool itWins)
    {
        Debug.Log( itWins? "It wins":"Survivors win");
    }
}
