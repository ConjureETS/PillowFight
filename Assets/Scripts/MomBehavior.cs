using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;
using System;
using MenusHandler;
using InputHandler;

public class MomBehavior : MonoBehaviour
{
    public Action OnWarning;
    public Action OnEnterRoom;
    public Action OnLeaveRoom;

    public Text WarningText;
    public float MinTriggerTime = 60f;
    public float MaxTriggerTime = 90f;
    public float WarningHeadsupTime = 5f;
    public float MotherStayTime = 2f;

    public Child[] Children;

    private float _elapsedTime = 0f;

    private float _nextTriggerTime;

    private bool _isInRoom;

    private bool _gameOver = false;

    public bool IsInRoom
    {
        get { return _isInRoom; }
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

        if (_elapsedTime >= _nextTriggerTime - WarningHeadsupTime && _elapsedTime < _nextTriggerTime)
        {
            WarningText.gameObject.SetActive(true);

            if (OnWarning != null)
            {
                OnWarning();
            }
        }
        else if (_elapsedTime >= _nextTriggerTime)
        {
            WarningText.gameObject.SetActive(false);
            _nextTriggerTime = GetNextTriggerTime();

            _elapsedTime = 0f;

            StartCoroutine(StayInRoom());
        }

        if (_isInRoom)
        {
            List<Child> safeChildren = new List<Child>();

            foreach (Child child in Children)
            {
                if (child == null) continue;

                if (child.IsSleeping)
                {
                    safeChildren.Add(child);
                }
                else
                {
                    Debug.Log("Player " + child.Index + " has been spotted by mom.");

                    // TODO: Visual animation that the player lost (lasso?)

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
    }

    private IEnumerator StayInRoom()
    {
        if (OnEnterRoom != null)
        {
            OnEnterRoom();
        }

        _isInRoom = true;

        yield return new WaitForSeconds(MotherStayTime);

        _isInRoom = false;

        if (OnLeaveRoom != null)
        {
            OnLeaveRoom();
        }
    }

    private float GetNextTriggerTime()
    {
        return UnityEngine.Random.Range(MinTriggerTime, MaxTriggerTime);
    }
}
