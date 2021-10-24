using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClapperController : MonoBehaviour
{

    public float clapSpeed;

    public Rigidbody2D leftPaddle;
    public Rigidbody2D rightPaddle;

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
            leftPaddle.AddForce(-leftPaddle.transform.right * clapSpeed);
            rightPaddle.AddForce(rightPaddle.transform.right * clapSpeed);
            // leftPaddle.AddTorque(-clapSpeed);
            // rightPaddle.AddTorque(clapSpeed);
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
                leftPaddle.AddForce(leftPaddle.transform.right * clapSpeed);
                rightPaddle.AddForce(-rightPaddle.transform.right * clapSpeed);
                triggered = false;
                cooldownTimer = 1f;
            }
        }
        if (cooldownTimer > 0f)
        {
            cooldownTimer -= Time.deltaTime;
        }
    }
}
