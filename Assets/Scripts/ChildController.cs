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
        InputManager.Instance.PushActiveContext("Awake", (int)PlayerNumber);
        InputManager.Instance.AddCallback((int)PlayerNumber, HandlePlayerAxis);
        InputManager.Instance.AddCallback((int)PlayerNumber, HandlePlayerButtons);

        _child = GetComponent<Child>();
    }

    private void HandlePlayerAxis(MappedInput input)
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

    private void HandlePlayerButtons(MappedInput input)
    {
        if (this == null) return;

        if (input.Actions.Contains("Jump"))
        {
            _child.Jump();
        }

        if (input.Actions.Contains("Sleep") && _child.Sleep())
        {
            InputManager.Instance.PushActiveContext("Sleeping", (int)PlayerNumber);
        }
        else if (input.Actions.Contains("WakeUp"))
        {
            _child.WakeUp();
            InputManager.Instance.PushActiveContext("Awake", (int)PlayerNumber);
        }
    }

}
