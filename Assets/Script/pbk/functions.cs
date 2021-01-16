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
    public static float CalculSpriteHorizontalSize(GameObject argObj)
    {
        return argObj.GetComponent<SpriteRenderer>().sprite.bounds.size.x * argObj.GetComponent<Transform>().localScale.x;
    }
    public static float CalculSpriteVerticalSize(GameObject argObj)
    {
        return argObj.GetComponent<SpriteRenderer>().sprite.bounds.size.y * argObj.GetComponent<Transform>().localScale.y;
    }
    /*public static void Singleton<T>(T argInstance)
    {
        if (argInstance != null)
        {
         //   Destroy(gameObject);
            return;
        }
<<<<<<< HEAD
        argInstance = this;
        DontDestroyOnLoad(argInstance);
    }*/
=======
        //argInstance = this;
        //DontDestroyOnLoad(argInstance);
    }
>>>>>>> feature/QuestUpdate
}
