using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class MomBehavior : MonoBehaviour
{
    public Child[] Children;

    public Text WarningText;
    public float MinTriggerTime = 60f;
    public float MaxTriggerTime = 90f;
    public float WarningHeadsupTime = 5f;

    private float _elapsedTime = 0f;

    private float _nextTriggerTime;

    void Awake()
    {
        _nextTriggerTime = GetNextTriggerTime();
        Debug.Log("NextTrigger: " + _nextTriggerTime);
    }

    void Update()
    {
        // When the mom hasn't been triggered for a while, it can appear anytime between 2 borders

        _elapsedTime += Time.deltaTime;

        if (_elapsedTime >= _nextTriggerTime - WarningHeadsupTime && _elapsedTime < _nextTriggerTime)
        {
            WarningText.gameObject.SetActive(true);
        }
        else if (_elapsedTime >= _nextTriggerTime)
        {
            WarningText.gameObject.SetActive(false);
            _nextTriggerTime = GetNextTriggerTime();

            foreach (Child child in Children)
            {
                if (!child.IsSleeping)
                {
                    // TODO: Do something (end the game? kill the player? make him lose 1 life? etc.)

                    Debug.Log("Child " + child.Index + " got found by Mommy.");
                }
            }

            _elapsedTime = 0f;
        }
    }

    private float GetNextTriggerTime()
    {
        return UnityEngine.Random.Range(MinTriggerTime, MaxTriggerTime);
    }
}
