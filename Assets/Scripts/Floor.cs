using UnityEngine;
using System.Collections;

[RequireComponent(typeof(MeshRenderer))]
public class Floor : MonoBehaviour
{
    public MomBehavior Mom;
    public Material NormalMaterial;
    public Material LavaMaterial;

    public float PillowWaitTime = 2f;

    private MeshRenderer _renderer;

    private GameObject lostPillows;

    void Awake()
    {
        _renderer = GetComponent<MeshRenderer>();
        Mom.OnEnterRoom += ChangeToNormalFloor;
        Mom.OnLeaveRoom += ChangeToLavaFloor;
        lostPillows = transform.GetChild(0).gameObject;
    }

    private void ChangeToNormalFloor()
    {
        _renderer.material = NormalMaterial;
        gameObject.tag = "Floor"; // Might not be necessary since the player is most likely "dead" if he touches a non-lava floor
        lostPillows.SetActive(true);
    }

    private void ChangeToLavaFloor()
    {
        _renderer.material = LavaMaterial;
        gameObject.tag = "Lava"; // Might not be necessary since the player is most likely "dead" if he touches a non-lava floor
        lostPillows.SetActive(false);
    }

    void OnDestroy()
    {
        Mom.OnEnterRoom -= ChangeToNormalFloor;
        Mom.OnLeaveRoom -= ChangeToLavaFloor;
    }

    void OnCollisionEnter(Collision other) {
        if (other.gameObject.tag == "Pillow") {
            other.gameObject.GetComponent<Pillow>().IsPickable = false;
            other.gameObject.GetComponent<Pillow>().IsLost = true;
            StartCoroutine( MakePillowDisappear(other.transform) );
        }
    }

    IEnumerator MakePillowDisappear( Transform pillow ) {
        yield return new WaitForSeconds(PillowWaitTime);
        pillow.transform.parent = lostPillows.transform;
    }
}
