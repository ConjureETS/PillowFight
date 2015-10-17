using UnityEngine;
using System.Collections;

public class StartScript : MonoBehaviour
{
    void Start()
    {
        MenusHandler.MenusManager.Instance.ShowMenu("SimpleMenu");
    }
}
