﻿using UnityEngine;
using System.Collections;
using MenusHandler;
using UnityEngine.UI;

public class PlayerWinsMenu : Menu
{
    public Text Message;

    public override void Open()
    {
        base.Open();

        GameManager.Instance.PushMenuContext();

        Time.timeScale = 0f;
    }

    public override void Close()
    {
        base.Close();

        GameManager.Instance.PopMenuContext();

        Time.timeScale = 1f;
    }

    public void SetPlayerIndex(int index)
    {
        Message.text = "Player " + (index + 1) + " Wins!";
    }

    public void OnRestartClick()
    {
        Time.timeScale = 1f;
        Application.LoadLevel(Application.loadedLevel);
    }

    public void OnExitClick()
    {
        Application.Quit();
    }
}
