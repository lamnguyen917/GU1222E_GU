using TMPro;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    public TMP_Text scoreText;
    public PlayerController player;
    [SerializeField] private TMP_Text status;
    [SerializeField] private TMP_Text lastAction;
    [SerializeField] private GameObject lastKill;

    private Transform tr;

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        var openTime = PlayerPrefs.GetFloat("open_treasure", 0);
        if (openTime == 0)
        {
            status.text = "Treasure chest is not open";
        }
        else
        {
            status.text = $"Treasure chest is open at {openTime}";
        }

        lastAction.text = PlayerPrefs.GetString("last_action", "");
        var lastPosString = PlayerPrefs.GetString("last_kill_position", "{}");
        lastKill.transform.position = JsonUtility.FromJson<Vector3>(lastPosString);

        NewEventDispatcher.Instance.AddEventLister(EventType.PlayerDie, GameOver);
    }

    public void UpdatePlayerState(int exp, int level)
    {
        scoreText.text = $"{exp} - {level}";
    }

    public void TestFunctionFromGameObject()
    {
        Debug.Log("TestFunctionFromGameManager: " + name);
    }

    public void GameOver()
    {
        Debug.Log("Xử lý liên quan đến game khi kết thúc");
    }
}