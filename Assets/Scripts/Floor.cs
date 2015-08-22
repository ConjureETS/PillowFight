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
    }

    private void ChangeToLavaFloor()
    {
        _renderer.material = LavaMaterial;
    }

    void OnDestroy()
    {
        Mom.OnEnterRoom -= ChangeToNormalFloor;
        Mom.OnLeaveRoom -= ChangeToLavaFloor;
    }
}
