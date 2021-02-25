using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneMove : MonoBehaviour
{
    public void GameStart()
    {
        SceneManager.LoadScene(1);
    }
    public void GameEnd()
    {
        Application.Quit();
    }
    void Update()
    {
        
        if (Input.anyKeyDown&&SceneManager.GetActiveScene().buildIndex != 0)
            SceneManager.LoadScene(0);
    }
}
