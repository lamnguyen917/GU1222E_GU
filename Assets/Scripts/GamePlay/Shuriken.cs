using UnityEngine;

public class Shuriken : Bullet
{
    private void Update()
    {
        transform.Rotate(Vector3.back);
        CheckDestroy();
    }
}