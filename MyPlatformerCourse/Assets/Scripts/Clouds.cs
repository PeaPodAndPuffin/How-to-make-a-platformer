using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Clouds : MonoBehaviour
{
    private float speed;
    public float minSpeed;
    public float maxSpeed;
    public float minX;
    public float maxX;

    private void Start()
    {
        speed = Random.Range(minSpeed, maxSpeed);
    }

    private void Update()
    {
        transform.Translate(Vector2.right * speed * Time.deltaTime);

        if (transform.position.x > maxX) {
            Vector2 newPos = new Vector2(minX, transform.position.y);
            transform.position = newPos;
        }
    }
}
