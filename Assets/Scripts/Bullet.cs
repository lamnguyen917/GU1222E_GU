using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private float speed = 10;
    [SerializeField] private float timer = 3;
    public float forward = 1;

    private void Update()
    {
        timer -= Time.deltaTime;
        if (timer < 0) Destroy(gameObject);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        rb.AddForce(Vector2.left * forward * speed);
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag("Player") || col.CompareTag("Bullet")) return;
        Debug.Log(col.gameObject.transform);
        Destroy(gameObject);
    }
}