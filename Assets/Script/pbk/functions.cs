using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class functions : MonoBehaviour
{
     public static bool Percentage(int argPercentage)
    {
        bool res;
        int randomNum = Random.Range(1, 100);
        if (randomNum <= argPercentage)
            res = true;
        else
            res = false;
        return res;
    }
    public static void Singleton<T>(T argInstance)
    {
        if (argInstance != null)
        {
            Destroy(gameObject);
            return;
        }
        argInstance = this;
        DontDestroyOnLoad(argInstance);
    }
}
