using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Patrol : Enemy
{

    public Transform[] patrolPoints;
    public float speed;
    int currentPointIndex;

    float waitTime;
    public float startWaitTime;

    Animator anim;

    private void Start()
    {
        transform.position = patrolPoints[0].position;
        transform.rotation = patrolPoints[0].rotation;
        waitTime = startWaitTime;
        anim = GetComponent<Animator>();
    }

    private void Update()
    {
        transform.position = Vector2.MoveTowards(transform.position, patrolPoints[currentPointIndex].position, speed * Time.deltaTime);
        if (transform.position == patrolPoints[currentPointIndex].position)
        {
            anim.SetBool("isRunning", false);
            transform.rotation = patrolPoints[currentPointIndex].rotation;
            if (waitTime <= 0)
            {
                if (currentPointIndex + 1 < patrolPoints.Length)
                {
                    currentPointIndex++;
                } else {
                    currentPointIndex = 0;
                }
                waitTime = startWaitTime;
            } else {
                waitTime -= Time.deltaTime;
            }

        } else {
            anim.SetBool("isRunning", true);
        }
    }

}
