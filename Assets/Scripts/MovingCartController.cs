using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingCartController : MonoBehaviour
{
    public bool move = true;
    public float speed;
    public bool direction;
    public Transform cart;
    public Transform point1;
    public Transform point2;

    public SpriteRenderer spriteRenderer;
    public Sprite[] sprites;

    void Start()
    {
        spriteRenderer.sprite = sprites[Random.Range(0, sprites.Length)];
    }

    // Update is called once per frame
    void Update()
    {
        if (direction)
        {
            cart.position = Vector3.MoveTowards(cart.position, point1.position, speed * Time.deltaTime);
            float dist = Vector3.Distance(cart.position, point1.position);
            if (dist < 0.01f)
            {
                direction = !direction;
            }
        }
        else
        {
            cart.position = Vector3.MoveTowards(cart.position, point2.position, speed * Time.deltaTime);
            float dist = Vector3.Distance(cart.position, point2.position);
            if (dist < 0.01f)
            {
                direction = !direction;
            }
        }
    }
}
