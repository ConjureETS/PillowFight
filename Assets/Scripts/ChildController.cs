using UnityEngine;
using System.Collections;
using InputHandler;

[RequireComponent(typeof(Child))]
public class ChildController : MonoBehaviour
{
    public enum Player { One, Two, Three, Four }

    public Player PlayerNumber;

    private Child _child;

    void Awake()
    {
        InputManager.Instance.PushActiveContext("Gameplay", (int)PlayerNumber);
        InputManager.Instance.AddCallback((int)PlayerNumber, HandlePlayerInput);

        _child = GetComponent<Child>();
    }

    private void HandlePlayerInput(MappedInput input)
    {
        if (this == null) return;

        float xValue = 0f;

        if (input.Ranges.ContainsKey("MoveLeft"))
        {
            xValue = -input.Ranges["MoveLeft"];
        }
        else if (input.Ranges.ContainsKey("MoveRight"))
        {
            xValue = input.Ranges["MoveRight"];
        }

        float zValue = 0f;

        if (input.Ranges.ContainsKey("MoveForward"))
        {
            zValue = input.Ranges["MoveForward"];
        }
        else if (input.Ranges.ContainsKey("MoveBackward"))
        {
            zValue = -input.Ranges["MoveBackward"];
        }

        _child.Move(xValue, zValue);
    }

}
