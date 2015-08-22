using UnityEngine;
using System.Collections;

public class Bed : MonoBehaviour
{
    private bool _isTaken;

    public bool IsTaken
    {
        get { return _isTaken; }
    }

    public void Take()
    {
        _isTaken = true;
    }

    public void Leave()
    {
        _isTaken = false;
    }
}
