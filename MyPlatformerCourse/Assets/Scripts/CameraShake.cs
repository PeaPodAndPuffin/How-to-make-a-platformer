using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraShake : MonoBehaviour
{

    Animator anim;

    private void Start()
    {
        anim = GetComponent<Animator>();
    }

    public void Shake() {
        anim.SetTrigger("shake");
    }

}
