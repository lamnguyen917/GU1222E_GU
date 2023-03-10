using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shuriken : Bullet
{
    private void Update()
    {
        transform.Rotate(Vector3.back);
        CheckDestroy();
    }
}