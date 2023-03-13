using System;
using UnityEngine;

public class MenuManager : MonoBehaviour
{
    public static MenuManager Instance;

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        NewEventDispatcher.Instance.AddEventLister(EventType.PlayerDie, ShowGameOverMenu);
    }

    public void ShowGameOverMenu()
    {
        Debug.Log("Hiển thị menu kết thúc game");
    }
}