using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private SpriteRenderer playerSprite;
    [SerializeField] private Animator anim;
    [SerializeField] private float speed = 10;
    [SerializeField] private float jumpSpeed = 10;

    private bool moveRight = true;
    private int direction = 1;

    private bool isJumping;
    private float jumpTime;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        Move();
        CheckJump();
    }

    void CheckJump()
    {
        if (isJumping)
        {
            if (jumpTime > 0)
            {
                jumpTime -= Time.deltaTime;
            }
            else
            {
                anim.SetBool("jump", false);
                isJumping = false;
            }
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            Debug.Log("PRESS JUMP DOWN");
            Jump();
        }
    }

    void Jump()
    {
        anim.SetBool("jump", true);
        jumpTime = 1;
        isJumping = true;
    }

    void Move()
    {
        Vector3 position = transform.position;
        if (direction == 1 && position.x > 10)
        {
            SetDirection(-1);
        }

        if (direction == -1 && position.x < -4)
        {
            SetDirection(1);
        }

        // if (!isJumping && position.y > -0.355)
        // {
        //     position.y -= Time.deltaTime;
        // }

        if (isJumping)
        {
            position.y += jumpSpeed * Time.deltaTime;
        }

        position.x += direction * speed * Time.deltaTime;
        transform.position = position;
    }

    private void SetDirection(int dir)
    {
        direction = dir;
        playerSprite.flipX = dir == -1;
    }
}
