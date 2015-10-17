using UnityEngine;
using System.Collections;
using MenusHandler;
using UnityEngine.UI;
using InputHandler;

public class CreditsMenu : Menu
{
    void Start()
    {
        for (int i = 0; i < 4; i++)
        {
            InputManager.Instance.PushActiveContext("MainMenu", i);
            InputManager.Instance.AddCallback(i, HandleCreditsMenuInput);
        }
    }

    private void HandleCreditsMenuInput(MappedInput input)
    {
        if (this == null || !gameObject.activeSelf) return;

        if (input.Actions.Contains("BackCreditsMenu"))
        {
            MenusManager.Instance.ShowMenu("SimpleMenu");
        }
    }
}
