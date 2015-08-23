using UnityEngine;
using System.Collections;

public class Floor : MonoBehaviour
{
    public MomBehavior Mom;
    public GameObject NormalFloor;
    public GameObject LavaFloor;

    public MeshRenderer Renderer;

    void Awake()
    {
        Mom.OnEnterRoom += ChangeToNormalFloor;
        Mom.OnLeaveRoom += ChangeToLavaFloor;
    }

    private void ChangeToNormalFloor()
    {
        NormalFloor.SetActive(true);
        LavaFloor.SetActive(false);
        gameObject.tag = "Floor"; // Might not be necessary since the player is most likely "dead" if he touches a non-lava floor
    }

    private void ChangeToLavaFloor()
    {
        NormalFloor.SetActive(false);
        LavaFloor.SetActive(true);
        gameObject.tag = "Lava"; // Might not be necessary since the player is most likely "dead" if he touches a non-lava floor
    }

    void OnDestroy()
    {
        Mom.OnEnterRoom -= ChangeToNormalFloor;
        Mom.OnLeaveRoom -= ChangeToLavaFloor;
    }
}
