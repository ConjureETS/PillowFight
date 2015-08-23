using UnityEngine;
using System.Collections;

public class MecanimContainer : MonoBehaviour
{
    public Child ImmediateParent;

    // For Mecanim
    public void throw_pillow()
    {
        ImmediateParent.ThrowMecanimPillow();
    }
}
