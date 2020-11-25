using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BtnManager : MonoBehaviour
{
    // Start is called before the first frame update
    public void GameStart()//게임시작 버튼 클릭
    {
        Application.LoadLevel(1);
    }
    public void GameEnd()//게임종료 버튼 클릭
    {
        Application.Quit();
    }

}
