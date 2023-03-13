using UnityEngine;

public class ProjectileBullet : Bullet
{
    private void OnEnable()
    {
        rb.AddForce(_direction * 40, ForceMode2D.Impulse);
    }

    void Update()
    {
        rb.AddForce(Vector2.down * 15);
        UpdateRotation();
    }

    public void SetStartDirection(Vector2 direction)
    {
        _direction = direction;
    }

    protected override void MoveBullet()
    {
        // không làm gì ở đây cả, chỉ de move bullet o script Bullet no khong hoat dọng
    }

    void UpdateRotation()
    {
        Vector3 eulerAngles = transform.eulerAngles;
        eulerAngles.z = Vector2.SignedAngle(Vector2.left, rb.velocity);
        transform.eulerAngles = eulerAngles;
    }
}
