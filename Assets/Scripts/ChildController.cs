using UnityEngine;
using System.Collections;
using InputHandler;

[RequireComponent(typeof(Child))]
[RequireComponent(typeof(AutoTarget))]
public class ChildController : MonoBehaviour
{
    public enum Player { One, Two, Three, Four }

    public Player PlayerNumber;

    private Child _child;
    private AutoTarget _autoTarget;

    void Awake()
    {
        InputManager.Instance.PushActiveContext("Gameplay", (int)PlayerNumber);
        InputManager.Instance.AddCallback((int)PlayerNumber, HandlePlayerAxis);
        InputManager.Instance.AddCallback((int)PlayerNumber, HandlePlayerButtons);

        _child = GetComponent<Child>();
        _autoTarget = GetComponent<AutoTarget>();
    }

    private void HandlePlayerAxis(MappedInput input)
    {
        if (this == null) return;

        // movement 

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
        
        // targeting

        float xLookingValue = 0f;

        if (input.Ranges.ContainsKey("LookLeft")) {
            xLookingValue = -input.Ranges["LookLeft"];
        }
        else if (input.Ranges.ContainsKey("LookRight")) {
            xLookingValue = input.Ranges["LookRight"];
        }

        float zLookingValue = 0f;

        if (input.Ranges.ContainsKey("LookForward")) {
            zLookingValue = input.Ranges["LookForward"];
        }
        else if (input.Ranges.ContainsKey("LookBackward")) {
            zLookingValue = -input.Ranges["LookBackward"];
        }

        if (xLookingValue != 0 || zLookingValue != 0) {
            //transform.rotation = new Quaternion(0, 1, 0, Mathf.Atan2(zLookingValue, xLookingValue));
            transform.eulerAngles = new Vector3(transform.eulerAngles.x, Mathf.Atan2(xLookingValue, zLookingValue) * Mathf.Rad2Deg, transform.eulerAngles.z);
            _child.target = _autoTarget.GetTarget(new Vector3(xLookingValue, 0, zLookingValue));
        }
    }

    private void HandlePlayerButtons(MappedInput input)
    {
        if (this == null) return;

        if (input.Actions.Contains("Jump"))
        {
            _child.Jump();
        }
    }

}
