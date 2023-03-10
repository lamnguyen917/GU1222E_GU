using System;
using System.Collections;
using TMPro;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    public static LevelManager Instance;
    public LevelData data;
    [SerializeField] private TMP_Text levelNameText;
    [SerializeField] private GameObject check;
    private float _timer;

    private void Awake()
    {
        Instance = this;
    }

    void Start()
    {
        LoadLevel();
        StartCoroutine(SpawnEnemyCoroutine(data.spawnRate));
    }

    void LoadLevel()
    {
        levelNameText.text = data.levelName;
    }

    IEnumerator SpawnEnemyCoroutine(float waitTime)
    {
        while (true)
        {
            yield return new WaitForSeconds(waitTime);
            Instantiate(data.enemyPrefab, data.spawnPosition, Quaternion.identity);
        }
    }
}