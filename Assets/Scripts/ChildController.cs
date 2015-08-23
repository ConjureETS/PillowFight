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
        InputManager.Instance.PushActiveContext("Awake", (int)PlayerNumber);
        InputManager.Instance.AddCallback((int)PlayerNumber, HandlePlayerAxis);
        InputManager.Instance.AddCallback((int)PlayerNumber, HandlePlayerButtons);

        _child = GetComponent<Child>();
        _autoTarget = GetComponent<AutoTarget>();
        _child.Index = (int)PlayerNumber;
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
            //Transform target = _autoTarget.GetTarget(new Vector3(xLookingValue, 0, zLookingValue));
			Transform target = _autoTarget.GetTarget(xLookingValue, zLookingValue);
            
            _child.target = target;
            if (_child.target != null) {
                transform.LookAt(_child.target);
            }
            else {
                transform.eulerAngles = new Vector3(
                    transform.eulerAngles.x, 
                    Mathf.Atan2(xLookingValue, zLookingValue) * Mathf.Rad2Deg -90, // -90 to correct forward facing angle... 
                    transform.eulerAngles.z);
            }
        }
        else {
            _child.target = null; // no auto targeting when not actively pressing the joystick in a direction

            // if player is not look with the right joystick, then face the direction we're going
            // if left joystick is used, else we don't change the facing direction
            if (xValue != 0 || zValue!= 0) {
                transform.eulerAngles = new Vector3(
                    transform.eulerAngles.x,
                    Mathf.Atan2(xValue, zValue) * Mathf.Rad2Deg - 90,
                    transform.eulerAngles.z);
            }
        }

        if (input.Ranges.ContainsKey("Throw")) {
            _child.Throw();
        }
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
            Debug.Log("SLEEPING");
            InputManager.Instance.PushActiveContext("Sleeping", (int)PlayerNumber);
        }
        else if (input.Actions.Contains("WakeUp"))
        {
            Debug.Log("AWAKE");
            _child.WakeUp();
            InputManager.Instance.PushActiveContext("Awake", (int)PlayerNumber);
        }
    }

}
