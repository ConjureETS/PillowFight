using UnityEngine;
using System.Collections;
using InputHandler;
using MenusHandler;

public class SimpleMenu2 : Menu
{
	public int NextLevel;

    private bool _loadingNextLevel = false;

    void Update()
    {
        if (Input.anyKeyDown && !_loadingNextLevel)
        {
            Application.LoadLevel(NextLevel);
            _loadingNextLevel = true;
        }
    }
}
