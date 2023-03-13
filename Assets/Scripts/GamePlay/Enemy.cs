using System;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    private void Start()
    {
        NewEventDispatcher.Instance.AddEventLister(EventType.PlayerDie, Laugh);
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag("Bullet"))
        {
            Destroy(gameObject);
            GameManager.Instance.player.GainExp(10);
            PlayerPrefs.SetString("last_action",
                $"Kill {name} by {col.gameObject.name} at position {JsonUtility.ToJson(transform.position)}");
            PlayerPrefs.SetString("last_kill_position", JsonUtility.ToJson(transform.position));
        }
    }

    private void Update()
    {
        Debug.DrawLine(transform.position, GameManager.Instance.player.transform.position);
    }

    public void Laugh()
    {
        Debug.Log("Thả HAHAHAHAHAHAAHA bởi " + name);
    }
}
