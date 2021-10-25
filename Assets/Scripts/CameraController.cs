using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{

    public Transform tracker;
    public float moveRadius;
    public float innerRadius;
    public float speed;

    private bool outside = false;

    // Update is called once per frame
    void FixedUpdate()
    {
        float dist = Vector2.Distance(transform.position, tracker.position);
        if (dist > moveRadius)
        {
            transform.position = Vector2.MoveTowards(transform.position, tracker.position, speed * dist * Time.fixedDeltaTime);
            transform.position += Vector3.forward * -10f;
            outside = true;
        }
        else if (outside && dist > innerRadius)
        {
            transform.position = Vector2.MoveTowards(transform.position, tracker.position, speed * dist * Time.fixedDeltaTime);
            transform.position += Vector3.forward * -10f;
        }
        else
        {
            outside = false;
        }
    }
}
