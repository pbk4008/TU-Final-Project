using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Scr_SceneMgr : MonoBehaviour
{
    private void Click_StageBtn()
    {
        SceneManager.LoadScene("scene2");
    }
}
