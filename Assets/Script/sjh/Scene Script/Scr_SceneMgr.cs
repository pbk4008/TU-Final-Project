using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Scr_SceneMgr : MonoBehaviour
{
    private void Click_StageBtn() //Cvs_StageDungeonButton의 Btn_StageN_* 버튼을 누르면 
    {
        SceneManager.LoadScene("Duengeon"); //Scene2로 이동한다.
    }
}
