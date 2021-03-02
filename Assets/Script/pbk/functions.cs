using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

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
    public static bool CriticalPercentage()
    {
        bool res;
        int randomNum = Random.Range(1, 21);
        if (randomNum <= 3)
            res = true;
        else
            res = false;
        return res;
    }
    public static float CalculSpriteHorizontalSize(GameObject argObj)
    {
        if (argObj.GetComponent<SpriteRenderer>() != null)
            return argObj.GetComponent<SpriteRenderer>().sprite.bounds.size.x * argObj.transform.localScale.x;
        else
        {
            return argObj.GetComponent<Image>().sprite.bounds.size.x * argObj.GetComponent<RectTransform>().localScale.x*100;
        }
    }
    public static float CalculSpriteVerticalSize(GameObject argObj)
    {
        if (argObj.GetComponent<SpriteRenderer>() != null)
            return argObj.GetComponent<SpriteRenderer>().sprite.bounds.size.y * argObj.transform.localScale.y;
        else
        {
            return argObj.GetComponent<Image>().sprite.bounds.size.y * argObj.GetComponent<RectTransform>().localScale.y*100;
        }
    }
    public static string CodetoString(List<char> argList)
    {
        if (argList == null)
            return null;
        string tmpCode = null;

        for (int i = 0; i < argList.Count; i++)
        { 
            
            tmpCode += argList[i];
        }
        return tmpCode;
    }
    
}
