using System;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private SpriteRenderer playerSprite;
    [SerializeField] private Animator anim;
    [SerializeField] private float speed = 10;
    [SerializeField] private float jumpSpeed = 10;
    [SerializeField] private Rigidbody2D rigidbody2D;
    [SerializeField] private float climbingSpeed = 10;

    private bool _isClimbable;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        // CheckJump();
    }

    private void FixedUpdate()
    {
        Move();
    }

    void Move()
    {
        Vector3 position = transform.position;
        var x = Input.GetAxis("Horizontal");
        var y = Input.GetAxis("Vertical");

        if (x > 0)
        {
            playerSprite.flipX = false;
        }

        if (x < 0)
        {
            playerSprite.flipX = true;
        }

        Debug.Log($"_isClimbable && x * y == 0 {_isClimbable && x * y == 0}, {_isClimbable} {x * y == 0}");
        if (_isClimbable && x == 0 && y == 0)
        {
            anim.speed = 0;
        }
        else
        {
            anim.speed = 1;
        }

        if (_isClimbable && Mathf.Abs(y) > 0)
        {
            rigidbody2D.velocity = new Vector2(0, y * climbingSpeed);
        }

        anim.SetFloat("run", Mathf.Abs(x));

        Vector2 moveVector = new Vector2();
        moveVector.x = x * speed;

        if (Input.GetAxis("Jump") > 0)
        {
            moveVector.y = jumpSpeed;
        }

        rigidbody2D.AddForce(moveVector);
    }

    void Die()
    {
        anim.SetBool("die", true);
    }

    // private void OnCollisionEnter2D(Collision2D col)
    // {
    //     Debug.Log($"ENTER {col.gameObject.name}");
    // }
    //
    // private void OnCollisionExit2D(Collision2D other)
    // {
    //     Debug.Log($"EXIT {other.gameObject.name}");
    // }
    //
    // private void OnCollisionStay2D(Collision2D collision)
    // {
    //     Debug.Log($"STAY {collision.gameObject.name}");
    // }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag("DeadTouch"))
        {
            Die();
        }

        Debug.Log($"ENTER {col.tag} ===== {col.CompareTag("Climbable")}");
        if (col.CompareTag("Climbable"))
        {
            _isClimbable = true;
            rigidbody2D.gravityScale = 0;
            anim.SetBool("climbing", true);
            Debug.Log(_isClimbable + "<<<<<<<<<<<<<<<<<d");
        }
    }

    void OnTriggerExit2D(Collider2D col)
    {
        Debug.Log($"EXIT {col.tag} ===== {col.CompareTag("Climbable")}");
        if (col.CompareTag("Climbable"))
        {
            _isClimbable = false;
            Debug.Log(_isClimbable + "<<<<<<<<<<<<<<<<<d");
            rigidbody2D.gravityScale = 1;
            anim.SetBool("climbing", false);
        }
    }

    // private void OnTriggerStay2D(Collider2D col)
    // {
    // }
}
