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
        // No keyboard code (only xbox controllers)
        if (this == null || !_child.Mom.StartGame) return;

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

        if (input.Ranges.ContainsKey("LookLeft"))
        {
            xLookingValue = -input.Ranges["LookLeft"];
        }
        else if (input.Ranges.ContainsKey("LookRight"))
        {
            xLookingValue = input.Ranges["LookRight"];
        }

        float zLookingValue = 0f;

        if (input.Ranges.ContainsKey("LookForward"))
        {
            zLookingValue = input.Ranges["LookForward"];
        }
        else if (input.Ranges.ContainsKey("LookBackward"))
        {
            zLookingValue = -input.Ranges["LookBackward"];
        }

        if (xLookingValue != 0 || zLookingValue != 0)
        {

            transform.eulerAngles = new Vector3(
                    transform.eulerAngles.x,
                    Mathf.Atan2(xLookingValue, zLookingValue) * Mathf.Rad2Deg - 90, // -90 to correct forward facing angle... 
                    transform.eulerAngles.z);

        }
        else
        {

            // if player is not look with the right joystick, then face the direction we're going
            // if left joystick is used, else we don't change the facing direction
            if (xValue != 0 || zValue != 0)
            {
                transform.eulerAngles = new Vector3(
                    transform.eulerAngles.x,
                    Mathf.Atan2(xValue, zValue) * Mathf.Rad2Deg - 90,
                    transform.eulerAngles.z);
            }
        }

        if (input.Ranges.ContainsKey("Throw"))
            _child.Throw();

        if (input.Actions.Contains("Hit"))
            _child.Swing();






        // Keyboard + mouse code (for the fourth player)
        /*if (this == null || !_child.Mom.StartGame) return;

        float xValue = 0f;
        float zValue = 0f;
        float xLookingValue = 0f;
        float zLookingValue = 0f;

        bool throwPressed = false;
        bool hitPressed = false;

        if (input.PlayerIndex == 3)
        {
            if (Input.GetKey(KeyCode.A))
            {
                xValue = -1f;
            }
            else if (Input.GetKey(KeyCode.D))
            {
                xValue = 1f;
            }

            if (Input.GetKey(KeyCode.W))
            {
                zValue = 1f;
            }
            else if (Input.GetKey(KeyCode.S))
            {
                zValue = -1f;
            }

            Vector3 mousePos = Input.mousePosition;

            mousePos.z = Vector3.Distance(new Vector3(0f, transform.position.y, 0f), Camera.main.transform.position);

            Vector3 mouseWorldPos = Camera.main.ScreenToWorldPoint(mousePos);

            Vector3 relAxis = (new Vector3(mouseWorldPos.x, transform.position.y, mouseWorldPos.z) - transform.position).normalized;

            xLookingValue = relAxis.x;
            zLookingValue = relAxis.z;

            // targeting

            if (xLookingValue != 0 || zLookingValue != 0)
            {

                transform.eulerAngles = new Vector3(
                        transform.eulerAngles.x,
                        Mathf.Atan2(xLookingValue, zLookingValue) * Mathf.Rad2Deg, // -90 to correct forward facing angle... 
                        transform.eulerAngles.z);

            }
            else
            {

                // if player is not look with the right joystick, then face the direction we're going
                // if left joystick is used, else we don't change the facing direction
                if (xValue != 0 || zValue != 0)
                {
                    transform.eulerAngles = new Vector3(
                        transform.eulerAngles.x,
                        Mathf.Atan2(xValue, zValue) * Mathf.Rad2Deg,
                        transform.eulerAngles.z);
                }
            }

            throwPressed = Input.GetKeyDown(KeyCode.Mouse0);
        }
        else
        {
            if (input.Ranges.ContainsKey("MoveLeft"))
            {
                xValue = -input.Ranges["MoveLeft"];
            }
            else if (input.Ranges.ContainsKey("MoveRight"))
            {
                xValue = input.Ranges["MoveRight"];
            }

            if (input.Ranges.ContainsKey("MoveForward"))
            {
                zValue = input.Ranges["MoveForward"];
            }
            else if (input.Ranges.ContainsKey("MoveBackward"))
            {
                zValue = -input.Ranges["MoveBackward"];
            }

            if (input.Ranges.ContainsKey("LookLeft"))
            {
                xLookingValue = -input.Ranges["LookLeft"];
            }
            else if (input.Ranges.ContainsKey("LookRight"))
            {
                xLookingValue = input.Ranges["LookRight"];
            }

            if (input.Ranges.ContainsKey("LookForward"))
            {
                zLookingValue = input.Ranges["LookForward"];
            }
            else if (input.Ranges.ContainsKey("LookBackward"))
            {
                zLookingValue = -input.Ranges["LookBackward"];
            }

            // targeting

            if (xLookingValue != 0 || zLookingValue != 0)
            {

                transform.eulerAngles = new Vector3(
                        transform.eulerAngles.x,
                        Mathf.Atan2(xLookingValue, zLookingValue) * Mathf.Rad2Deg - 90, // -90 to correct forward facing angle... 
                        transform.eulerAngles.z);

            }
            else
            {

                // if player is not look with the right joystick, then face the direction we're going
                // if left joystick is used, else we don't change the facing direction
                if (xValue != 0 || zValue != 0)
                {
                    transform.eulerAngles = new Vector3(
                        transform.eulerAngles.x,
                        Mathf.Atan2(xValue, zValue) * Mathf.Rad2Deg - 90,
                        transform.eulerAngles.z);
                }
            }

            throwPressed = input.Ranges.ContainsKey("Throw");
            hitPressed = input.Actions.Contains("Hit");
        }

        _child.Move(xValue, zValue);

        if (throwPressed)
            _child.Throw();

        if (hitPressed)
			_child.Swing();*/
    }

    private void HandlePlayerButtons(MappedInput input)
    {
        // No keyboard code (only xbox controllers)
        /*
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
        }*/

        if (this == null) return;

        if (input.PlayerIndex == 3)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                _child.Jump();
            }

            if (Input.GetKeyDown(KeyCode.X) && !_child.IsSleeping && _child.Sleep())
            {
                Debug.Log("SLEEPING");
                InputManager.Instance.PushActiveContext("Sleeping", (int)PlayerNumber);
            }
            else if (Input.GetKeyDown(KeyCode.X) && _child.IsSleeping)
            {
                Debug.Log("AWAKE");
                _child.WakeUp();
                InputManager.Instance.PushActiveContext("Awake", (int)PlayerNumber);
            }
        }
        else
        {
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

}
