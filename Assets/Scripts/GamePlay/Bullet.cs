using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] protected Rigidbody2D rb;
    [SerializeField] private float speed = 10;
    [SerializeField] private float timer = 3;
    [SerializeField] private float deltaAngle;
    public float forward = 1;

    private bool _haveDirection;
    protected Vector3 _direction;

    private void Update()
    {
        CheckDestroy();
    }

    protected void CheckDestroy()
    {
        timer -= Time.deltaTime;
        if (timer < 0) Destroy(gameObject);
    }

    void FixedUpdate()
    {
        MoveBullet();
    }

    protected virtual void MoveBullet()
    {
        if (_haveDirection)
        {
            rb.AddForce(_direction * speed);
            return;
        }

        rb.AddForce(Vector2.left * forward * speed);
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag("Player") || col.CompareTag("Bullet")) return;
        Debug.Log(col.gameObject.transform);
        Destroy(gameObject);
    }

    public void SetDirection(Vector2 direction)
    {
        _haveDirection = true;
        _direction = direction;

        Vector3 eulerAngles = transform.eulerAngles;
        eulerAngles.z = Vector2.SignedAngle(Vector2.left, direction) + deltaAngle;
        transform.eulerAngles = eulerAngles;
    }
}