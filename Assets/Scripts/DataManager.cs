using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataManager
{
    public static DataManager Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = new DataManager();
            }

            return _instance;
        }
    }

    private static DataManager _instance;

    private DataManager()
    {
    }

    public int Gold = 10;
}