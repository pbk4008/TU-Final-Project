using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BtnManager : MonoBehaviour
{
    // Start is called before the first frame update
    public void GameStart()
    {
        Application.LoadLevel(1);
    }
    public void GameEnd()
    {
        Application.Quit();
    }
}
