using UnityEngine;
using System.Collections;

[RequireComponent(typeof(MeshRenderer))]
public class Floor : MonoBehaviour
{
    public MomBehavior Mom;
    public Material NormalMaterial;
    public Material LavaMaterial;

    private MeshRenderer _renderer;

    void Awake()
    {
        _renderer = GetComponent<MeshRenderer>();
        Mom.OnEnterRoom += ChangeToNormalFloor;
        Mom.OnLeaveRoom += ChangeToLavaFloor;
    }

    private void ChangeToNormalFloor()
    {
        _renderer.material = NormalMaterial;
        gameObject.tag = "Floor"; // Might not be necessary since the player is most likely "dead" if he touches a non-lava floor
    }

    private void ChangeToLavaFloor()
    {
        _renderer.material = LavaMaterial;
        gameObject.tag = "Lava"; // Might not be necessary since the player is most likely "dead" if he touches a non-lava floor
    }

    void OnDestroy()
    {
        Mom.OnEnterRoom -= ChangeToNormalFloor;
        Mom.OnLeaveRoom -= ChangeToLavaFloor;
    }
}
