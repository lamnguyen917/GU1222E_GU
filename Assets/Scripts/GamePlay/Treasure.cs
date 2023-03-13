using UnityEngine;

public class Treasure : MonoBehaviour
{
    [SerializeField] private GameObject particleObject;

    public void Open()
    {
        particleObject.SetActive(true);
        PlayerPrefs.SetFloat("open_treasure", Time.time);
    }
}
