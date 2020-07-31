using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayGames : MonoBehaviour
{
    void Start()
    {
    }

    public void WinFirstGame()
    {
    }

    public void Level1()
    {
    }

    public void Level5()
    {
    }

    public void Level15()
    {
    }

    public void Level21()
    {
    }

    public void Kill()
    {
        if (PlayerPrefs.HasKey("Kills"))
        {
            PlayerPrefs.SetInt("Kills", PlayerPrefs.GetInt("Kills") + 1);
        }
        else
        {
            PlayerPrefs.SetInt("Kills", 1);
        }

        Social.ReportScore(PlayerPrefs.GetInt("Kills"), "CgkI4qGctKYXEAIQBg", (bool success) => {
            // handle success or failure
        });
    }

    public void Game()
    {
        if (PlayerPrefs.HasKey("GamesPlayed"))
        {
            PlayerPrefs.SetInt("GamesPlayed", PlayerPrefs.GetInt("GamesPlayed") + 1);
        }
        else
        {
            PlayerPrefs.SetInt("GamesPlayed", 1);
        }

        Social.ReportScore(PlayerPrefs.GetInt("GamesPlayed"), "CgkI4qGctKYXEAIQBw", (bool success) => {
            // handle success or failure
        });
    }
}
