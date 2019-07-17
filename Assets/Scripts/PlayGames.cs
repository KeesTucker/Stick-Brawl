using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GooglePlayGames;
using GooglePlayGames.BasicApi;

public class PlayGames : MonoBehaviour
{
    void Start()
    {
        PlayGamesPlatform.Activate();
        Social.localUser.Authenticate((bool success) => {
            if (success)
            {
                ((GooglePlayGames.PlayGamesPlatform)Social.Active).SetGravityForPopups(Gravity.TOP);
            }
        });
    }

    public void WinFirstGame()
    {
        PlayGamesPlatform.Instance.UnlockAchievement(
        "CgkI4qGctKYXEAIQAQ", (bool success) => {
            // handle success or failure
        });
    }

    public void Level1()
    {
        PlayGamesPlatform.Instance.UnlockAchievement(
        "CgkI4qGctKYXEAIQAg", (bool success) => {
            // handle success or failure
        });
    }

    public void Level5()
    {
        PlayGamesPlatform.Instance.UnlockAchievement(
        "CgkI4qGctKYXEAIQAw", (bool success) => {
            // handle success or failure
        });
    }

    public void Level15()
    {
        PlayGamesPlatform.Instance.UnlockAchievement(
        "CgkI4qGctKYXEAIQBA", (bool success) => {
            // handle success or failure
        });
    }

    public void Level21()
    {
        PlayGamesPlatform.Instance.UnlockAchievement(
        "CgkI4qGctKYXEAIQBQ", (bool success) => {
            // handle success or failure
        });
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
