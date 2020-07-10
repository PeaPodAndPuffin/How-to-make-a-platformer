using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Warlock : Enemy
{

    public GameObject fireBall;
    public float timeBetweenShots;
    float nextShotTime;
    public Transform shotPoint;

    private void Update()
    {
        if (Time.time > nextShotTime)
        {
            Instantiate(fireBall, shotPoint.position, shotPoint.rotation);
            nextShotTime = Time.time + timeBetweenShots;
        }
    }

}
