using UnityEngine;
using System.Collections;

public class Floor : MonoBehaviour
{
    public MomBehavior Mom;
    public Material NormalMaterial;
    public Material LavaMaterial;

    public MeshRenderer Renderer;

    void Awake()
    {
        Mom.OnEnterRoom += ChangeToNormalFloor;
        Mom.OnLeaveRoom += ChangeToLavaFloor;
    }

    private void ChangeToNormalFloor()
    {
        Renderer.material = NormalMaterial;
        gameObject.tag = "Floor"; // Might not be necessary since the player is most likely "dead" if he touches a non-lava floor
    }

    private void ChangeToLavaFloor()
    {
        Renderer.material = LavaMaterial;
        gameObject.tag = "Lava"; // Might not be necessary since the player is most likely "dead" if he touches a non-lava floor
    }

    void OnDestroy()
    {
        Mom.OnEnterRoom -= ChangeToNormalFloor;
        Mom.OnLeaveRoom -= ChangeToLavaFloor;
    }
}
