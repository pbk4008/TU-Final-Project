using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Scr_DungeonBtn : MonoBehaviour
{
    //던전 부분
    [SerializeField] private Text[] T_DungeonText = new Text[10];
    [SerializeField] private Button[] DungeonButton = new Button[21];

    private int m_iFloor;
    private int m_iStage;

    private string m_sButtonName;

    private bool m_bOnClick;
    private bool[][] m_bStage = new bool[5][];

    //던전 버튼 get set 부분
    public string sButtonName { get => m_sButtonName; set => m_sButtonName = value; }
    public bool sOnClick { get => m_bOnClick; set => m_bOnClick = value; }

    private void Update()
    {
        SetDungeonButton();
    }

    private void Start()
    {
        m_bStage[0] = new bool[] { true, false, false, false, false };
        m_bStage[1] = new bool[] { false, false, false, false, false };
        m_bStage[2] = new bool[] { false, false, false, false, false, false, false };
        m_bStage[3] = new bool[] { false, false, false, false, false, false, false };
        m_bStage[4] = new bool[] { false, false, false, false, false, false, false, false, false, false };
        m_bStage[5] = new bool[] { false, false, false, false, false, false, false, false, false, false };
    }

    private void SetDungeonButton()
    {
        if (sOnClick)
        {
            switch (m_sButtonName)
            {
                case "Btn_DungeonNPC": //던전 시작
                    SetActive(1, 2, 0, 0);
                    break;
                case "Btn_DungeonEnter":
                    SetActive(3, 9, 1, 2);
                    break;
                case "Btn_DungeonExit":
                    SetActive(0, 0, 1, 2);
                    break;
                case "Btn_DungeonFloor1":
                    m_iFloor = 0;
                    SetActive(10, 15, 3, 9);
                    StageText();
                    break;
                case "Btn_DungeonFloor2":
                    m_iFloor = 1;
                    SetActive(10, 15, 3, 9);
                    StageText();
                    break;
                case "Btn_DungeonFloor3":
                    m_iFloor = 2;
                    SetActive(10, 17, 3, 9);
                    StageText();
                    break;
                case "Btn_DungeonFloor4":
                    m_iFloor = 3;
                    SetActive(10, 17, 3, 9);
                    StageText();
                    break;
                case "Btn_DungeonFloor5":
                    m_iFloor = 4;
                    SetActive(10, 20, 3, 9);
                    StageText();
                    break;
                case "Btn_DungeonFloor6":
                    m_iFloor = 5;
                    SetActive(10, 20, 3, 9);
                    StageText();
                    break;
                case "Btn_DungeonExitFloor":
                    SetActive(1, 2, 3, 9);
                    break;
                case "Btn_DungeonExitStage": //던전 끝
                    SetActive(3, 9, 10, 20);
                    break;
            }
            sOnClick = false;
        }
    }

    private void StageText()
    {
        for(int i=0;i<10;i++)
        {
            int iStage = 1 + i;
            int iFloor = m_iFloor + 1;
            T_DungeonText[i].GetComponent<Text>().text = iFloor + " - " + iStage;
        }
    }

    private void Click_StageButton()
    {
        string sStageName = "";

        for (int i=0; i<10;i++)
        {
            int iNumber = i + 1;
            sStageName = "Btn_DungeonStageN_" + iNumber;
            if(sButtonName == sStageName)
            {
                m_iStage = i;
            }
        }

        if (m_bStage[m_iFloor][m_iStage] == true)
            SceneManager.LoadScene("Duengeon"); //Scene2로 이동한다.
        else
            Debug.Log("잠겨있음");
    }

    private void SetActive(int iOnmin, int iOnmax, int iOffmin, int iOffmax)
    {
        for (int i = iOnmin; i <= iOnmax; i++)
            DungeonButton[i].gameObject.SetActive(true);
        for (int i = iOffmin; i <= iOffmax; i++)
            DungeonButton[i].gameObject.SetActive(false);
    }
}