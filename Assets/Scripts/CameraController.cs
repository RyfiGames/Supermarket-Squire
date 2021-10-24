using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{

    public Transform tracker;
    public float moveRadius;
    public float speed;

    // Update is called once per frame
    void Update()
    {
        float dist = Vector2.Distance(transform.position, tracker.position);
        if (dist > moveRadius)
        {
            transform.position = Vector2.MoveTowards(transform.position, tracker.position, speed * dist * Time.deltaTime);
            transform.position += Vector3.forward * -10f;
        }
    }
}
