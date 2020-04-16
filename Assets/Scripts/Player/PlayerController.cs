using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Animator anim;
    private Rigidbody2D rb;
    [SerializeField]
    private float jumpForce = 200f;
    [SerializeField]
    private bool inGround = false;
    public Transform handLeft;
    public Transform handRight;
    [SerializeField]
    private float runSpeed = 10f;
    public bool haveWeapon = false;
    private Vector3 cursor;

    // Start is called before the first frame update
    private void Start()
    {
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    private void Update()
    {
        float axisX = Input.GetAxis("Horizontal");
        rb.velocity = new Vector2(runSpeed * axisX, rb.velocity.y);

        if (Input.GetButtonDown("Jump") && inGround)
        {
            rb.AddForce(new Vector2(0, jumpForce), ForceMode2D.Impulse);
        }

        //set animations
        setAnimations(axisX);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        inGround = collision.gameObject.layer == LayerMask.NameToLayer("Ground");
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        inGround = !(collision.gameObject.layer == LayerMask.NameToLayer("Ground"));
    }

    
    private void OnTriggerEnter2D(Collider2D collision)
    {
        //get a new weapon
        if (collision.tag == "Weapon")
        {
            WeaponController weapon = collision.gameObject.GetComponent<WeaponController>();
            if (weapon)
            {
                weapon.setWeapon(this);
            }
        }
    }

    private void setAnimations(float axisX)
    {
        //jump
        anim.SetBool("inGround", inGround);
        //run
        anim.SetFloat("speed", Mathf.Abs(axisX));
        //look
        if (!haveWeapon)
        {
            if (axisX < 0)
            {
                transform.localScale = new Vector3(0.3f, 0.3f, 0.3f);
            }
            else if (axisX > 0)
            {
                transform.localScale = new Vector3(-0.3f, 0.3f, 0.3f);
            }
        }
        else
        {
            cursor = Camera.main.ScreenToWorldPoint(new Vector3(
                Input.mousePosition.x,
                Input.mousePosition.y,
                -Camera.main.transform.position.z
                ));
            if (cursor.x < transform.position.x)
            {
                transform.localScale = new Vector3(0.3f, 0.3f, 0.3f);
            }
            else if (cursor.x > transform.position.x)
            {
                transform.localScale = new Vector3(-0.3f, 0.3f, 0.3f);
            }
        }
    }
}
