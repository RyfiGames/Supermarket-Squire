using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExpanderController : MonoBehaviour
{

    public float expandSpeed;
    public Transform circle;
    public Animator anim;

    private bool triggered;
    private float resetTimer;
    private float cooldownTimer;

    // Start is called before the first frame update
    void Start()
    {

    }

    void OnTriggerStay2D(Collider2D other)
    {
        if (other.tag == "Player" && !triggered && cooldownTimer <= 0)
        {
            triggered = true;
            resetTimer = 5f;
            anim.SetTrigger("Fall");
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (resetTimer > 0f)
        {
            resetTimer -= Time.deltaTime;
            if (resetTimer <= 0)
            {
                triggered = false;
                cooldownTimer = 1f;
                anim.SetTrigger("Unfall");
            }
        }
        if (cooldownTimer > 0f)
        {
            cooldownTimer -= Time.deltaTime;
            if (circle.localScale.x > 0.1f)
            {
                circle.localScale = Vector3.zero;
            }
        }
        if (triggered && circle.localScale.x < 1f)
        {
            circle.localScale += Vector3.one;
        }
    }
}
