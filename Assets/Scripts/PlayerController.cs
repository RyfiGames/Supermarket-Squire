using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    public float speed;
    public float speedTileSpeed;
    public float expanderForce;
    public SpriteRenderer spriteRenderer;
    public ParticleSystem creamPS;
    public Transform[] psPos;
    public Sprite[] playerSprites;

    private float whippedCream;

    private Vector3 pointerDir;
    private Camera cam;
    private Rigidbody2D rb;

    // Start is called before the first frame update
    void Start()
    {
        cam = Camera.main;
        rb = GetComponent<Rigidbody2D>();
    }

    private void move()
    {
        Vector3 mousePos = cam.ScreenToWorldPoint(Input.mousePosition);
        mousePos.z = transform.position.z;
        pointerDir = (mousePos - transform.position).normalized;
        float pointerAngle = Vector3.Angle(-transform.up, pointerDir);
        pointerAngle = pointerDir.x > 0 ? pointerAngle : 360 - pointerAngle;
        int dirID = Mathf.RoundToInt(pointerAngle / 45f) % 8;
        spriteRenderer.sprite = playerSprites[dirID];
        creamPS.transform.position = psPos[dirID].position;
        creamPS.transform.eulerAngles = new Vector3(-pointerAngle + 90f, 90f, 0f);
        if (Input.GetMouseButtonDown(0) && whippedCream > 0f)
        {
            creamPS.Play();
        }
        if (Input.GetMouseButtonUp(0) || (creamPS.isPlaying && whippedCream < 0f))
        {
            creamPS.Stop();
        }
        if (Input.GetMouseButton(0) && whippedCream > 0f)
        {
            rb.AddForce(-pointerDir * speed * Time.deltaTime);
            whippedCream -= Time.deltaTime;
            GameManager.one.creamBar.setPercent(whippedCream / 3f);
        }
        if (rb.velocity.magnitude < 0.5f && whippedCream < 3f)
        {
            whippedCream += Time.deltaTime * 5f;
            GameManager.one.creamBar.setPercent(whippedCream / 3f);
        }
        if (rb.velocity.magnitude > 30f)
        {
            rb.velocity = rb.velocity.normalized * 25f;
        }
    }

    void OnCollisionEnter2D(Collision2D coll)
    {
        if (coll.gameObject.tag == "Expander Circle")
        {
            Vector3 dir = (coll.transform.position - transform.position).normalized;
            rb.AddForce(-dir * expanderForce);
            if (whippedCream < 3f)
            {
                whippedCream += Time.deltaTime;
                GameManager.one.creamBar.setPercent(whippedCream / 3f);
            }
        }
    }

    void OnTriggerStay2D(Collider2D other)
    {
        if (other.tag == "SpeedTile")
        {
            Vector3 collPos = other.ClosestPoint(transform.position);
            collPos -= (transform.position - collPos).normalized / 10f;
            Vector2 dir = GameManager.one.GetTileDirection(collPos);
            rb.AddForce(dir * speedTileSpeed * Time.fixedDeltaTime);
            if (whippedCream < 3f)
            {
                whippedCream += Time.deltaTime;
                GameManager.one.creamBar.setPercent(whippedCream / 3f);
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (GameManager.one.playing)
            move();
    }
}
