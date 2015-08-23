using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;
using System;
using MenusHandler;
using InputHandler;

public class MomBehavior : MonoBehaviour
{
    public Action OnEnterRoom;
    public Action OnLeaveRoom;

    public Text WarningText;
    public float MinTriggerTime = 60f;
    public float MaxTriggerTime = 90f;
    public float WarningHeadsupTime = 5f;
    public float MotherStayTime = 2f;
    public Door RoomDoor;

    public Child[] Children;

    public enum State { Away, Warning, InRoom }

    private State _currentState;

    private float _elapsedTime = 0f;

    private float _nextTriggerTime;

    private bool _gameOver = false;

    public bool IsInRoom
    {
        get { return _currentState == State.InRoom; }
    }

    void Awake()
    {
        _nextTriggerTime = GetNextTriggerTime();
    }

    void Update()
    {
        if (_gameOver) return;

        // When the mom hasn't been triggered for a while, it can appear anytime between 2 borders

        _elapsedTime += Time.deltaTime;

        switch (_currentState)
        {
            case State.Away:
                if (_elapsedTime >= _nextTriggerTime - WarningHeadsupTime && _elapsedTime < _nextTriggerTime)
                {
                    SetState(State.Warning);
                }
                break;
            case State.Warning:
                if (_elapsedTime >= _nextTriggerTime)
                {
                    SetState(State.InRoom);
                }
                break;
            case State.InRoom:
                if (_elapsedTime >= MotherStayTime)
                {
                    SetState(State.Away);
                }

                CheckIfSleeping();
                break;
        }
    }

    private void SetState(State newState)
    {
        switch (newState)
        {
            case State.Away:
                RoomDoor.Close(OnLeaveRoom);
                _elapsedTime = 0f;
                break;
            case State.Warning:
                // Temporary
                WarningText.gameObject.SetActive(true);

                RoomDoor.Open();
                break;
            case State.InRoom:
                // Temporary
                WarningText.gameObject.SetActive(false);
                _nextTriggerTime = GetNextTriggerTime();

                _elapsedTime = 0f;

                if (OnEnterRoom != null)
                {
                    OnEnterRoom();
                }
                break;
        }

        _currentState = newState;
    }

    public State GetState(){
        return _currentState;
    }

    private void CheckIfSleeping()
    {
        List<Child> safeChildren = new List<Child>();

        foreach (Child child in Children)
        {
            if (child == null) continue;

            Debug.Log(child.IsSleeping);

            if (child.IsSleeping)
            {
                safeChildren.Add(child);
            }
            else
            {
                Debug.Log("Player " + child.Index + " has been spotted by mom.");

                // TODO: Visual animation that the player lost (lasso?)

				child.NumZ = 4;
                Destroy(child.gameObject);
            }
        }

        if (safeChildren.Count == 0)
        {
            Debug.Log("Mom wins!");

            MenusManager.Instance.ShowMenu("MomWinsMenu");

            _gameOver = true;
        }
        else if (safeChildren.Count == 1)
        {
            Debug.Log("Player " + safeChildren[0].Index + " wins!");

            PlayerWinsMenu menu = (PlayerWinsMenu)MenusManager.Instance.ShowMenu("PlayerWinsMenu");
            menu.SetPlayerIndex(safeChildren[0].Index);

            _gameOver = true;
        }
    }

    private float GetNextTriggerTime()
    {
        return UnityEngine.Random.Range(MinTriggerTime, MaxTriggerTime);
    }
}
