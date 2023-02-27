using System;
using TMPro;
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
    [SerializeField] private bool _isJumping;

    [SerializeField] private LayerMask groundLayers;
    [SerializeField] private Transform groundCheck;

    [SerializeField] private GameObject bulletPrefab;

    // Start is called before the first frame update
    void Start()
    {
        GameManager gameManager = FindObjectOfType<GameManager>();
    }

    // Update is called once per frame
    void Update()
    {
        if (_isJumping) CheckGround();
    }

    private void FixedUpdate()
    {
        Move();
        if (Input.GetMouseButtonDown(1))
        {
            Gun();
        }
    }

    void Move()
    {
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
        rigidbody2D.AddForce(moveVector);


        if (Input.GetAxis("Jump") > 0 && !_isJumping)
        {
            anim.SetBool("isJump", true);
            rigidbody2D.AddForce(new Vector2(0, jumpSpeed), ForceMode2D.Impulse);
            _isJumping = true;
        }

        if (_isJumping)
        {
            anim.SetFloat("jump", rigidbody2D.velocity.y);
        }
    }

    void Die()
    {
        anim.SetTrigger("die");
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag("DeadTouch"))
        {
            Die();
        }

        if (col.CompareTag("Climbable"))
        {
            _isClimbable = true;
            rigidbody2D.gravityScale = 0;
            anim.SetBool("climbing", true);
        }
    }

    void OnTriggerExit2D(Collider2D col)
    {
        Debug.Log($"EXIT {col.tag} ===== {col.CompareTag("Climbable")}");
        if (col.CompareTag("Climbable"))
        {
            _isClimbable = false;
            rigidbody2D.gravityScale = 1;
            anim.SetBool("climbing", false);
        }
    }

    private void CheckGround()
    {
        var hit = Physics2D.Raycast(groundCheck.position, Vector2.down, Mathf.Infinity, groundLayers);
        Debug.Log(hit);
        if (hit.distance < 0.14)
        {
            _isJumping = false;
            anim.SetBool("isJump", false);
        }
    }

    private void Gun()
    {
        var bullet = Instantiate(bulletPrefab, transform.position, Quaternion.identity);
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Vector3 direction = transform.TransformDirection(Vector3.down) * 5;
        Gizmos.DrawRay(groundCheck.position, direction);
    }
}
