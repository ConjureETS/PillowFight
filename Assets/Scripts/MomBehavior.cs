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
    public AudioSource MomEnterSound;

    public Child[] Children;

    public enum State { Away, Warning, InRoom }

    private State _currentState;

    private float _elapsedTime = 0f;

    private float _nextTriggerTime;

    private bool _gameOver = false;

    private List<Child> _aliveChildren;

    public bool IsInRoom
    {
        get { return _currentState == State.InRoom; }
    }

    private bool _startGame;

    public bool StartGame
    {
        get { return _startGame; }
        set { _startGame = value; }
    }
    

    void Awake()
    {
        _nextTriggerTime = GetNextTriggerTime();

        _aliveChildren = new List<Child>();

        foreach (Child child in Children)
        {
            child.OnDied += OnChildDied;
            _aliveChildren.Add(child);
        }
    }

    private void OnChildDied(Child child)
    {
        if (_gameOver) return;

        _aliveChildren.Remove(child);
        Destroy(child.gameObject);

        if (_aliveChildren.Count == 1)
        {
            MusicManager.Instance.PlayVictoryMusic();

            StartCoroutine("PlayerWins", child);
        }
    }

    void Update()
    {
        if (_gameOver || !_startGame) return;

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

                RoomDoor.Open(MomEnterSound.Play);

                if (OnEnterRoom != null)
                {
                    OnEnterRoom();
                }
                break;
            case State.InRoom:
                // Temporary
                WarningText.gameObject.SetActive(false);
                _nextTriggerTime = GetNextTriggerTime();

                _elapsedTime = 0f;

                break;
        }

        _currentState = newState;
    }

    public State GetState(){
        return _currentState;
    }

    private void CheckIfSleeping()
    {
        if (_gameOver) return;

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
                _aliveChildren.Remove(child);
                Destroy(child.gameObject);
            }
        }

        if (safeChildren.Count == 0)
        {
            Debug.Log("Mom wins!");

            MusicManager.Instance.PlayDefeatMusic();

            StartCoroutine(MomWins());

            _gameOver = true;
        }
        else if (safeChildren.Count == 1)
        {
            _gameOver = true;

            Debug.Log("Player " + safeChildren[0].Index + " wins!");

            PlayerWinsMenu menu = (PlayerWinsMenu)MenusManager.Instance.ShowMenu("PlayerWinsMenu");
            menu.SetPlayerIndex(safeChildren[0].Index);

            MusicManager.Instance.PlayVictoryMusic();

            _gameOver = true;
        }
    }

    private IEnumerator MomWins()
    {
        yield return new WaitForSeconds(1.5f);

        MenusManager.Instance.ShowMenu("MomWinsMenu");
    }

    private IEnumerator PlayerWins()
    {
        yield return new WaitForSeconds(1.5f);

        PlayerWinsMenu menu = (PlayerWinsMenu)MenusHandler.MenusManager.Instance.ShowMenu("PlayerWinsMenu");
        menu.SetPlayerIndex(_aliveChildren[0].Index);
    }

    private float GetNextTriggerTime()
    {
        return UnityEngine.Random.Range(MinTriggerTime, MaxTriggerTime);
    }

    void OnDestroy()
    {
        foreach (Child child in Children)
        {
            child.OnDied -= OnChildDied;
        }
    }
}
