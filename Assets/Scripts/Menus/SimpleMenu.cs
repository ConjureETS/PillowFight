using UnityEngine;
using System.Collections;
using InputHandler;
using MenusHandler;

public class SimpleMenu : Menu
{
	public int NextLevel;

    private bool _loadingNextLevel = false;

    void Start()
    {
        for (int i = 0; i < 4; i++)
        {
            InputManager.Instance.PushActiveContext("MainMenu", i);
            InputManager.Instance.AddCallback(i, HandleMenuInput);
        }
    }

    private void HandleMenuInput(MappedInput input)
    {
        if (this == null || _loadingNextLevel || !gameObject.activeSelf) return;

        if (input.Actions.Contains("PlayGame"))
        {
            Application.LoadLevel(NextLevel);
            _loadingNextLevel = true;
        }
        else if (input.Actions.Contains("ShowCredits"))
        {
            MenusManager.Instance.ShowMenu("CreditsMenu");
        }
    }
}
