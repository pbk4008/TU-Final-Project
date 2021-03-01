using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneMove : MonoBehaviour
{

    Scene m_Scene;
    private void Start()
    {
        m_Scene = SceneManager.GetActiveScene();
    }
    public void GameStart()
    {
        if (null != GameObject.Find("Obj_UIMgr"))
        {
            Destroy(GameObject.Find("Player"));
            Destroy(GameObject.Find("InvenCanvas"));
            Destroy(GameObject.Find("Cvs_UI"));
            Destroy(GameObject.Find("Obj_UIMgr"));
            Application.LoadLevel("Lobby");
        }
        else
            Application.LoadLevel("Lobby");
       
    }
    public void GameEnd()
    {
        Application.Quit();
    }
    public void Continue()
    {
        SceneManager.LoadScene(1);
    }
    void Update()
    {
        if (m_Scene.name == "Title")
            if (null != GameObject.Find("Obj_UIMgr"))
            {

                if (null != GameObject.Find("Cvs_UI"))
                {
                    GameObject.Find("Canvas").transform.GetChild(2).gameObject.SetActive(true);
                    GameObject.Find("Cvs_UI").GetComponent<LobbyUI>().CoroutineOn();
                }
            }
        if (Input.anyKeyDown&&SceneManager.GetActiveScene().buildIndex != 0)
            SceneManager.LoadScene(0);
    }
}
