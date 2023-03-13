using UnityEngine;

[CreateAssetMenu(fileName = "Level Data", menuName = "Custom Data/Level Data", order = 1)]
public class LevelData : ScriptableObject
{
    public int id = 1;
    public string levelName = "New Level";
    public Vector3 playerPosition;
    public GameObject defaultBullet;

    public GameObject enemyPrefab;
    public Vector3 spawnPosition;
    public float spawnRate;
}