using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;
using System;

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

    private float _elapsedTime = 0f;

    private float _nextTriggerTime;

    private bool _isInRoom;

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
