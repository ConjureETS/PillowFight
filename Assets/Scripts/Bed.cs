using UnityEngine;
using System.Collections;

public class Bed : MonoBehaviour
{
    // Values to balance out in playtesting
    public float MinSpawnDelay = 7f;
    public float MaxSpawnDelay = 15f;

    public Pillow PillowObject;
    public Vector3 RelativePosition = new Vector3(1.6f, 0.5f, 0f);

    private bool _isTaken;
    private Pillow _currentPillow;
    private float _elapsedTime;

    private float _nextSpawnDelay;

    private GameObject _actionButtonUI;

    public bool IsTaken
    {
        get { return _isTaken; }
    }

    void Awake()
    {
        SpawnPillow();

        _nextSpawnDelay = GetNextSpawnDelay();

        _actionButtonUI = transform.GetChild(0).gameObject;
    }

    void Update()
    {
        if (_currentPillow == null)
        {
            _elapsedTime += Time.deltaTime;

            if (_elapsedTime >= _nextSpawnDelay)
            {
                _elapsedTime = 0f;
                SpawnPillow();
                _nextSpawnDelay = GetNextSpawnDelay();
            }
        }
        else if (_currentPillow.IsOwned)
        {
            _currentPillow = null;
            _elapsedTime = 0f;
        }

    }

    private void SpawnPillow()
    {
        _currentPillow = Instantiate(PillowObject, transform.position, PillowObject.transform.rotation) as Pillow;

        Vector3 rot = _currentPillow.transform.eulerAngles;
        rot.y = transform.eulerAngles.y - 90f;

        _currentPillow.transform.eulerAngles = rot;

        Vector3 pos = new Vector3();

        if (Mathf.Approximately(transform.eulerAngles.y, 0f))
        {
            pos = RelativePosition;
        }
        else if (Mathf.Approximately(transform.eulerAngles.y, 90f))
        {
            pos = new Vector3(-RelativePosition.z, RelativePosition.y, -RelativePosition.x);
        }
        else if (Mathf.Approximately(transform.eulerAngles.y, 180f))
        {
            pos = new Vector3(-RelativePosition.x, RelativePosition.y, -RelativePosition.z);
        }
        else if (Mathf.Approximately(transform.eulerAngles.y, 270f))
        {
            pos = new Vector3(RelativePosition.z, RelativePosition.y, RelativePosition.x);
        }

        _currentPillow.transform.position += pos;
    }

    private float GetNextSpawnDelay()
    {
        return UnityEngine.Random.Range(MinSpawnDelay, MaxSpawnDelay);
    }

    public void Take()
    {
        _isTaken = true;
    }

    public void Leave()
    {
        _isTaken = false;
    }

    /*
    void OnCollisionEnter(Collision col)
    {
        // TODO: Check if the pillow is owned (otherwise it means the collision is only a player walking by)
        if (col.gameObject.tag == "Pillow" && _currentPillow == null)
        {
            _currentPillow = col.gameObject.GetComponent<Pillow>();
        }
    }
    */

    void OnCollisionExit(Collision col)
    {

        if (col.gameObject.tag == "Player")
        {
            _actionButtonUI.SetActive(false);
        }
    }

    
    void OnCollisionStay(Collision col)
    {
        if (col.gameObject.tag == "Player")
        {
            if (GameManager.Instance.GetMomState() == MomBehavior.State.Warning && col.gameObject.GetComponent<Child>().GetBed() ) {

                // show the button for sleeping when is on her way to the room
                if (!_actionButtonUI.activeSelf) {
                    _actionButtonUI.SetActive(true);
                }
            }
            else {
                if (_actionButtonUI.activeSelf) {
                    _actionButtonUI.SetActive(false);
                }
            }
            
        }
    }
}
