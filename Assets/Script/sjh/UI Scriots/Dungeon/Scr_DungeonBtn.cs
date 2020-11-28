using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Scr_DungeonBtn : MonoBehaviour
{
    //-----------------------던전 부분
    [SerializeField] private Button[] DungeonButton = new Button[21];
    [SerializeField] private Text[] T_DungeonText = new Text[10];

    private int m_iFloor;
    private int m_iStage;
    private string m_sButtonName;
    private bool m_bOnClick;
    private bool[][] m_bStage = new bool[5][];

    public string sButtonName { get => m_sButtonName; set => m_sButtonName = value; }
    public bool sOnClick { get => m_bOnClick; set => m_bOnClick = value; }

    //----------------------퀘스트 부분
    [SerializeField] private Button[] QuestButton = new Button[5];   //퀘스트 표지판 UI
    [SerializeField] private Text[] T_QuestText = new Text[3];       //퀘스트 텍스트
    [SerializeField] private Text[] T_QuestSelect = new Text[3]; //퀘스트 수락 버튼 텍스트
    [SerializeField] private Image Img_Quest; //퀘스트 이미지
    //

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
                    SetActive(0, 1, 2, 0, 0);
                    break;
                case "Btn_DungeonEnter":
                    SetActive(0, 3, 9, 1, 2);
                    break;
                case "Btn_DungeonExit":
                    SetActive(0, 0, 0, 1, 2);
                    break;
                case "Btn_DungeonFloor1":
                    m_iFloor = 0;
                    SetActive(0, 10, 15, 3, 9);
                    StageText();
                    break;
                case "Btn_DungeonFloor2":
                    m_iFloor = 1;
                    SetActive(0, 10, 15, 3, 9);
                    StageText();
                    break;
                case "Btn_DungeonFloor3":
                    m_iFloor = 2;
                    SetActive(0, 10, 17, 3, 9);
                    StageText();
                    break;
                case "Btn_DungeonFloor4":
                    m_iFloor = 3;
                    SetActive(0, 10, 17, 3, 9);
                    StageText();
                    break;
                case "Btn_DungeonFloor5":
                    m_iFloor = 4;
                    SetActive(0, 10, 20, 3, 9);
                    StageText();
                    break;
                case "Btn_DungeonFloor6":
                    m_iFloor = 5;
                    SetActive(0, 10, 20, 3, 9);
                    StageText();
                    break;
                case "Btn_DungeonExitFloor":
                    SetActive(0, 1, 2, 3, 9);
                    break;
                case "Btn_DungeonExitStage": //던전 끝
                    SetActive(0, 3, 9, 10, 20);
                    break;
                case "Btn_QuestSign":
                    SetActive(1, 1, 4, 0, 0);
                    T_QuestText[0].gameObject.SetActive(true);
                    T_QuestText[1].gameObject.SetActive(true);
                    T_QuestText[2].gameObject.SetActive(true);
                    T_QuestSelect[0].gameObject.SetActive(true);
                    T_QuestSelect[1].gameObject.SetActive(true);
                    T_QuestSelect[2].gameObject.SetActive(true);
                    Img_Quest.gameObject.SetActive(true);
                    break;
                case "Btn_QuestExit":
                    SetActive(1, 0, 0, 1, 4);
                    T_QuestText[0].gameObject.SetActive(false);
                    T_QuestText[1].gameObject.SetActive(false);
                    T_QuestText[2].gameObject.SetActive(false);
                    T_QuestSelect[0].gameObject.SetActive(false);
                    T_QuestSelect[1].gameObject.SetActive(false);
                    T_QuestSelect[2].gameObject.SetActive(false);
                    Img_Quest.gameObject.SetActive(false);
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

    private void SetActive(int iKindOf, int iOnmin, int iOnmax, int iOffmin, int iOffmax)
    {
        switch (iKindOf)
        {
            case 0: //던전
                for (int i = iOnmin; i <= iOnmax; i++)
                    DungeonButton[i].gameObject.SetActive(true);
                for (int i = iOffmin; i <= iOffmax; i++)
                    DungeonButton[i].gameObject.SetActive(false);
                break;
            case 1: //퀘스트
                for (int i = iOnmin; i <= iOnmax; i++)
                    QuestButton[i].gameObject.SetActive(true);
                for (int i = iOffmin; i <= iOffmax; i++)
                    QuestButton[i].gameObject.SetActive(false);
                break;
        }
    }
}