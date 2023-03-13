using UnityEngine;
using UnityEngine.Serialization;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private SpriteRenderer playerSprite;
    [SerializeField] private Animator anim;
    [SerializeField] private float speed = 10;
    [SerializeField] private float jumpSpeed = 10;
    [SerializeField] private Rigidbody2D rg2d;
    [SerializeField] private float climbingSpeed = 10;

    private bool _isClimbable;
    [SerializeField] private bool _isJumping;

    [SerializeField] private LayerMask groundLayers;
    [SerializeField] private Transform groundCheck;

    [SerializeField] private GameObject bulletPrefab;

    [SerializeField] private AudioSource sound;
    [SerializeField] private AudioSource gunSound;

    [SerializeField] private AudioClip runSfx;
    [SerializeField] private AudioClip jumpSfx;
    [SerializeField] private AudioClip gunSfx;

    [SerializeField] private float fireRate = 0.5f;

    private float fireTimer = 0;

    [SerializeField] private Transform bulletSpawnPosition;

    [SerializeField] private int exp;
    [SerializeField] private int level;

    void Start()
    {
        LoadDefaultData();
        GameManager.Instance.TestFunctionFromGameObject();
        Debug.Log(DataManager.Instance.Gold);
    }

    // Update is called once per frame
    void Update()
    {
        if (_isJumping) CheckGround();
        fireTimer -= Time.deltaTime;

        Debug.DrawRay(transform.position, Vector3.down * 10, Color.green);
    }

    private void FixedUpdate()
    {
        Move();
        if (Input.GetAxis("Fire1") > 0)
        {
            Gun();
        }

        if (Input.GetAxis("Fire2") > 0)
        {
            ShootDirect();
        }
    }

    void LoadDefaultData()
    {
        var data = LevelManager.Instance.data;
        transform.position = data.playerPosition;
        bulletPrefab = data.defaultBullet;
        bulletPrefab.SetActive(false);
        exp = PlayerPrefs.GetInt("exp", 0);
        level = PlayerPrefs.GetInt("level", 0);
        GameManager.Instance.UpdatePlayerState(exp, level);
    }

    void Move()
    {
        var x = Input.GetAxis("Horizontal");
        var y = Input.GetAxis("Vertical");

        Vector3 scale = transform.localScale;
        if (x > 0) scale.x = 1;
        if (x < 0) scale.x = -1;
        transform.localScale = scale;

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
            rg2d.velocity = new Vector2(0, y * climbingSpeed);
        }

        anim.SetFloat("run", Mathf.Abs(x));
        Vector2 moveVector = new Vector2();
        moveVector.x = x * speed;
        rg2d.AddForce(moveVector);


        if (Input.GetAxis("Jump") > 0 && !_isJumping)
        {
            anim.SetBool("isJump", true);
            rg2d.AddForce(new Vector2(0, jumpSpeed), ForceMode2D.Impulse);
            _isJumping = true;
            PlaySfx(jumpSfx, false);
        }

        if (_isJumping)
        {
            anim.SetFloat("jump", rg2d.velocity.y);
        }

        CheckRunSound(Mathf.Abs(x) > 0.1f);
    }

    void CheckRunSound(bool isRunning)
    {
        if (_isJumping) return;
        if (!isRunning)
        {
            StopSfx();
        }
        else
        {
            PlaySfx(runSfx);
        }
    }

    void StopSfx()
    {
        sound.Stop();
    }

    void PlaySfx(AudioClip clip, bool loop = true)
    {
        if (sound.clip == clip && sound.isPlaying) return;
        sound.loop = loop;
        sound.clip = clip;
        sound.Play();
    }

    void Die()
    {
        anim.SetTrigger("die");
        NewEventDispatcher.Instance.PostEEvent(EventType.PlayerDie);
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
            rg2d.gravityScale = 0;
            anim.SetBool("climbing", true);
        }

        if (col.CompareTag("Treasure"))
        {
            Treasure treasure = col.gameObject.GetComponent<Treasure>();
            treasure.Open();
        }
    }

    void OnTriggerExit2D(Collider2D col)
    {
        Debug.Log($"EXIT {col.tag} ===== {col.CompareTag("Climbable")}");
        if (col.CompareTag("Climbable"))
        {
            _isClimbable = false;
            rg2d.gravityScale = 1;
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
        if (fireTimer > 0) return;
        fireTimer = fireRate;
        var bulletObject = Instantiate(bulletPrefab, bulletSpawnPosition.position, Quaternion.identity);
        Bullet bullet = bulletObject.GetComponent<Bullet>();
        bullet.forward = -transform.localScale.x;
        bulletObject.SetActive(true);
        gunSound.Play();
    }

    public void ShootDirect()
    {
        if (fireTimer > 0) return;
        fireTimer = fireRate;
        var bulletObject = Instantiate(bulletPrefab, bulletSpawnPosition.position, Quaternion.identity);
        Bullet bullet = bulletObject.GetComponent<Bullet>();
        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            mousePosition.z = 0;
        if (bullet is ProjectileBullet)
        {
            var projectile = bullet as ProjectileBullet;
            projectile.SetStartDirection((mousePosition - bulletSpawnPosition.position).normalized);
        }
        else
        {
            bullet.SetDirection((mousePosition - bulletSpawnPosition.position).normalized);
        }

        bulletObject.SetActive(true);
        gunSound.Play();
    }

    public void GainExp(int amount)
    {
        exp += amount;
        if (exp > 100) LevelUp();
        GameManager.Instance.UpdatePlayerState(exp, level);
        PlayerPrefs.SetInt("exp", exp);
        PlayerPrefs.SetInt("level", level);
    }

    void LevelUp()
    {
        exp -= 100;
        level += 1;
    }
}