using System;
using UnityEngine;
using UnityEngine.SceneManagement;

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

    public void BackToMain()
    {
        SceneManager.LoadScene("0-Menu");
    }
}