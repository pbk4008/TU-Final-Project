using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Scr_DungeonBtn : MonoBehaviour
{
    //----------------------던전 부분
    [SerializeField] private Button[] DungeonButton = new Button[21];
    [SerializeField] private Text[] T_DungeonText = new Text[10];

    private int m_iFloor;
    private int m_iStage;
    private string m_sButtonName;
    private bool m_bOnClick;
    private bool[][] m_bStage = new bool[6][];

    public string sButtonName { get => m_sButtonName; set => m_sButtonName = value; }
    public bool sOnClick { get => m_bOnClick; set => m_bOnClick = value; }

    //----------------------퀘스트 부분
    [SerializeField] private Button[] QuestButton = new Button[5];   //퀘스트 표지판 UI
    [SerializeField] private Text[] T_QuestText = new Text[3];       //퀘스트 텍스트
    [SerializeField] private Text[] T_QuestSelect = new Text[3]; //퀘스트 수락 버튼 텍스트
    [SerializeField] private Image Img_Quest; //퀘스트 이미지

    private string m_iGrade;     //퀘스트 등급
    private bool[] m_bLockQuest = new bool[3]; //퀘스트 선택하면 다시 돌리지 못하게 하는 변수
    private int m_iRandomNum;//확률지정

    private int[] m_iCurrentMonsterCount = new int[3]; //퀘스트 받고 나서의 플레이어의 현재 잡은 몬스터 값
    private int[] m_iCurrentBossCount = new int[3];      //퀘스트 받고 나서의 플레이어의 현재 잡은 보스 값
    private int[] m_iCurrentEtcItemCount = new int[3];  //퀘스트 받고 나서의 플레이어의 현재 모은 기타아이템 값
    private int[] m_iQuest = new int[3];        //퀘스트 종류 배열
    private int[] m_iGoalCount = new int[3]; //퀘스트 목표 점수 배열

    //----------------------도박장 부분
    [SerializeField] private Button[] GambleButton = new Button[7]; 
    [SerializeField] private Player m_Player;
    [SerializeField] private Text T_GamblePlayerMoney;
    [SerializeField] private Text T_GambleReady;
    [SerializeField] private Text T_GambleResult;
    [SerializeField] private Image Img_GambleEnter; //도박장 이미지
    [SerializeField] private Image Img_GambleMenu; //도박장 이미지
    [SerializeField] private Image Img_GambleResult; //도박장 이미지
    [SerializeField] private InputField Input_GambleMoney;

    private string m_sPlayerSelect;
    private int m_iPlayerSelect;
    private int m_iPay;

    //---------------------집 부분
    [SerializeField] private Button[] HouseButton = new Button[6];
    [SerializeField] private Image Img_HouseMenu;
    [SerializeField] private Image Img_HouseHeal;
    [SerializeField] private Text T_HouseHeal;

    //---------------------플레이어UI 부분
    [SerializeField] private Button[] PlayerButton = new Button[2];
    [SerializeField] private Image Img_PlayerStat;
    [SerializeField] private Text T_PlayerUI;

    private void Update()
    {
        SetDungeonButton();
    }

    private void Start()
    {
        //던전 스테이지 막기
        m_bStage[0] = new bool[] { true, false, false, false, false };
        m_bStage[1] = new bool[] { false, false, false, false, false };
        m_bStage[2] = new bool[] { false, false, false, false, false, false, false };
        m_bStage[3] = new bool[] { false, false, false, false, false, false, false };
        m_bStage[4] = new bool[] { false, false, false, false, false, false, false, false, false, false };
        m_bStage[5] = new bool[] { false, false, false, false, false, false, false, false, false, false };

        //퀘스트
        for (int i = 0; i < 3; i++)
        {
            m_bLockQuest[i] = true; //참으로 바꿈 (처음엔 퀘스트 선택안하면 돌아감)
        }
        SettingQuest();
    }

    private void SetDungeonButton()
    {
        if (sOnClick)
        {
            switch (m_sButtonName)
            {
                case "Btn_DungeonNPC": //던전 시작
                    SetActive(0, 1, 2, 0, 0);
                    ActiveButton(0);
                    break;
                case "Btn_DungeonEnter":
                    SetActive(0, 3, 9, 1, 2);
                    break;
                case "Btn_DungeonExit":
                    SetActive(0, 0, 0, 1, 2);
                    ActiveButton(1);
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
                case "Btn_QuestSign"://퀘스트 시작
                    SetActive(1, 1, 4, 0, 0);
                    T_QuestText[0].gameObject.SetActive(true);
                    T_QuestText[1].gameObject.SetActive(true);
                    T_QuestText[2].gameObject.SetActive(true);
                    T_QuestSelect[0].gameObject.SetActive(true);
                    T_QuestSelect[1].gameObject.SetActive(true);
                    T_QuestSelect[2].gameObject.SetActive(true);
                    Img_Quest.gameObject.SetActive(true);
                    ActiveButton(0);
                    break;
                case "Btn_Quest1Select":
                    m_bLockQuest[0] = false; //퀘스트가 다시 지정되지 못하게 하기
                    T_QuestSelect[0].GetComponent<Text>().text = " 진 행 중 ";
                    break;
                case "Btn_Quest2Select":
                    m_bLockQuest[1] = false; //퀘스트가 다시 지정되지 못하게 하기
                    T_QuestSelect[1].GetComponent<Text>().text = " 진 행 중 ";
                    break;
                case "Btn_Quest3Select":
                    m_bLockQuest[2] = false; //퀘스트가 다시 지정되지 못하게 하기
                    T_QuestSelect[2].GetComponent<Text>().text = " 진 행 중 ";
                    break;
                case "Btn_QuestExit": //퀘스트 끝
                    SetActive(1, 0, 0, 1, 4);
                    T_QuestText[0].gameObject.SetActive(false);
                    T_QuestText[1].gameObject.SetActive(false);
                    T_QuestText[2].gameObject.SetActive(false);
                    T_QuestSelect[0].gameObject.SetActive(false);
                    T_QuestSelect[1].gameObject.SetActive(false);
                    T_QuestSelect[2].gameObject.SetActive(false);
                    Img_Quest.gameObject.SetActive(false);
                    ActiveButton(1);
                    break;
                case "Btn_GambleNPC": //도박장 시작
                    SetActive(2, 1, 3, 0, 0);
                    Img_GambleEnter.gameObject.SetActive(true);
                    Input_GambleMoney.gameObject.SetActive(true);
                    T_GamblePlayerMoney.gameObject.SetActive(true);
                    T_GamblePlayerMoney.GetComponent<Text>().text = "소지금 : " + m_Player.IMoney.ToString();
                    m_iPay = 0;
                    ActiveButton(0);
                    break;
                case "Btn_ExitGamble":
                    SetActive(2, 0, 0, 1, 3);
                    Img_GambleEnter.gameObject.SetActive(false);
                    Input_GambleMoney.gameObject.SetActive(false);
                    T_GamblePlayerMoney.gameObject.SetActive(false);
                    ActiveButton(1);
                    break;
                case "Btn_GambleRight":
                    SetActive(2, 4, 5, 1, 3);
                    Img_GambleMenu.gameObject.SetActive(true);
                    T_GambleReady.gameObject.SetActive(true);
                    m_iPlayerSelect = 1;
                    GambleReady();
                    break;
                case "Btn_GambleLeft":
                    SetActive(2, 4, 5, 1, 3);
                    Img_GambleMenu.gameObject.SetActive(true);
                    T_GambleReady.gameObject.SetActive(true);
                    m_iPlayerSelect = 2;
                    GambleReady();
                    break;
                case "Btn_GambleSelect":
                    SetActive(2, 6, 6, 4, 5);
                    Img_GambleResult.gameObject.SetActive(true);
                    T_GambleResult.gameObject.SetActive(true);
                    GambleResult();
                    break;
                case "Btn_GambleReadyExit":
                    SetActive(2, 1, 3, 4, 5);
                    Img_GambleMenu.gameObject.SetActive(false);
                    T_GambleReady.gameObject.SetActive(false);
                    break;
                case "Btn_GambleResult": //도박장 끝
                    SetActive(2, 0, 0, 6, 6);
                    Img_GambleEnter.gameObject.SetActive(false);
                    Img_GambleMenu.gameObject.SetActive(false);
                    Img_GambleResult.gameObject.SetActive(false);
                    Input_GambleMoney.gameObject.SetActive(false);
                    T_GamblePlayerMoney.gameObject.SetActive(false);
                    T_GambleReady.gameObject.SetActive(false);
                    T_GambleResult.gameObject.SetActive(false);
                    ActiveButton(1);
                    break;
                case "Btn_EnterHouse": //집 시작
                    SetActive(3, 1, 4, 0, 0);
                    Img_HouseMenu.gameObject.SetActive(true);
                    ActiveButton(0);
                    break;
                case "Btn_HouseHeal":
                    SetActive(3, 5, 5, 1, 4);
                    Img_HouseMenu.gameObject.SetActive(false);
                    Img_HouseHeal.gameObject.SetActive(true);
                    T_HouseHeal.gameObject.SetActive(true);
                    Heal();
                    break;
                case "Btn_HouseReinforce":
                    break;
                case "Btn_HouseWereHouse":
                    break;
                case "Btn_HouseMenuExit":
                    SetActive(3, 0, 0, 1, 4);
                    Img_HouseMenu.gameObject.SetActive(false);
                    ActiveButton(1);
                    break;
                case "Btn_HouseHealExit": //집 끝
                    SetActive(3, 0, 0, 5, 5);
                    Img_HouseHeal.gameObject.SetActive(false);
                    T_HouseHeal.gameObject.SetActive(false);
                    ActiveButton(1);
                    break;
                case "Btn_PlayerUI":
                    SetActive(4, 1, 1, 0, 0);
                    Img_PlayerStat.gameObject.SetActive(true);
                    T_PlayerUI.gameObject.SetActive(true);
                    PrintPlayerInfo();
                    ActiveButton(0);
                    break;
                case "Btn_PlayerUIExit":
                    SetActive(4, 0, 0, 1, 1);
                    Img_PlayerStat.gameObject.SetActive(false);
                    T_PlayerUI.gameObject.SetActive(false);
                    ActiveButton(1);
                    break;
            }
            sOnClick = false;
        }
    }

    //공통 부분
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
            case 2: //도박장
                for (int i = iOnmin; i <= iOnmax; i++)
                    GambleButton[i].gameObject.SetActive(true);
                for (int i = iOffmin; i <= iOffmax; i++)
                    GambleButton[i].gameObject.SetActive(false);
                break;
            case 3: //집
                for (int i = iOnmin; i <= iOnmax; i++)
                    HouseButton[i].gameObject.SetActive(true);
                for (int i = iOffmin; i <= iOffmax; i++)
                    HouseButton[i].gameObject.SetActive(false);
                break;
            case 4: //플레이어UI
                for (int i = iOnmin; i <= iOnmax; i++)
                    PlayerButton[i].gameObject.SetActive(true);
                for (int i = iOffmin; i <= iOffmax; i++)
                    PlayerButton[i].gameObject.SetActive(false);
                break;
        }
    }

    private void ActiveButton(int Active)
    {
        switch(Active)
        {
            case 0:
                DungeonButton[0].gameObject.SetActive(false);
                QuestButton[0].gameObject.SetActive(false);
                GambleButton[0].gameObject.SetActive(false);
                HouseButton[0].gameObject.SetActive(false);
                PlayerButton[0].gameObject.SetActive(false);
                break;
            case 1:
                DungeonButton[0].gameObject.SetActive(true);
                QuestButton[0].gameObject.SetActive(true);
                GambleButton[0].gameObject.SetActive(true);
                HouseButton[0].gameObject.SetActive(true);
                PlayerButton[0].gameObject.SetActive(true);
                break;
        }
    }

    //던전 스크립트
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

    //퀘스트 스크립트
    private void SettingQuest() //퀘스트 셋팅
    {
        for (int i = 0; i < 3; i++) //3번 반복
        {
            if (m_bLockQuest[i] == true) //퀘스트 바꾸는게 가능하다면
            {
                m_iRandomNum = Random.Range(1, 101);                       //1~100의 랜덤 값 지정으로 등급 정하기
                if (m_iRandomNum > 1 && m_iRandomNum <= 60)
                    m_iGrade = "Normal";
                else if (m_iRandomNum > 60 && m_iRandomNum <= 90)
                    m_iGrade = "Special";
                else if (m_iRandomNum > 90 && m_iRandomNum <= 100)
                    m_iGrade = "Epic";

                m_iQuest[i] = Random.Range(1, 4);  //1~3 랜덤값 부여로 퀘스트 종류 정하기
                                                   //1 = 보스 몬스터, //2 = 일반 몬스터, //3 = 기타 아이템

                switch (m_iQuest[i]) //퀘스트 종류에 따라
                {
                    case 1: // 1 = 보스 몬스터
                        if (m_iGrade == "Normal")         //등급에 따라 목표 달성량이 달라짐
                            m_iGoalCount[i] = 1;
                        else if (m_iGrade == "Special")
                            m_iGoalCount[i] = 3;
                        else if (m_iGrade == "Epic")
                            m_iGoalCount[i] = 5;
                        T_QuestText[i].GetComponent<Text>().text = "[" + m_iGrade + "]" + "보스 몬스터 " + m_iGoalCount[i] + "마리 잡아오기\n" //퀘스트 목표 텍스트
                            + "진행도 : ( " + m_iCurrentBossCount[i] + " / " + m_iGoalCount[i] + ")";
                        break;
                    case 2: // 2 = 일반 몬스터
                        if (m_iGrade == "Normal")       //등급에 따라 목표 달성량이 달라짐
                            m_iGoalCount[i] = 5;
                        else if (m_iGrade == "Special")
                            m_iGoalCount[i] = 15;
                        else if (m_iGrade == "Epic")
                            m_iGoalCount[i] = 30;
                        T_QuestText[i].GetComponent<Text>().text = "[" + m_iGrade + "]" + "일반 몬스터 " + m_iGoalCount[i] + "마리 잡아오기\n" //퀘스트 목표 텍스트
                            + "진행도 : ( " + m_iCurrentMonsterCount[i] + " / " + m_iGoalCount[i] + ")";
                        break;
                    case 3: // 3 = 기타 아이템
                        if (m_iGrade == "Normal")       //등급에 따라 목표 달성량이 달라짐
                            m_iGoalCount[i] = 30;
                        else if (m_iGrade == "Special")
                            m_iGoalCount[i] = 50;
                        else if (m_iGrade == "Epic")
                            m_iGoalCount[i] = 100;
                        T_QuestText[i].GetComponent<Text>().text = "[" + m_iGrade + "]" + "기타 아이템 " + m_iGoalCount[i] + "개 모아오기\n" //퀘스트 목표 텍스트
                            + "진행도 : ( " + m_iCurrentEtcItemCount[i] + " / " + m_iGoalCount[i] + ")";
                        break;
                }
            }
        }
    }

    //도박장 스크립트
    private void SettingMoney()
    {
        string sMoney = Input_GambleMoney.text;
        m_iPay = int.Parse(sMoney);
    }

    private void GambleReady()
    {
        switch (m_iPlayerSelect)
        {
            case 1:
                m_sPlayerSelect = "오른쪽";
                break;
            case 2:
                m_sPlayerSelect = "왼쪽";
                break;
        }

        T_GambleReady.GetComponent<Text>().text = "소지금 : " + m_Player.IMoney.ToString() + "\n"
            + "거신 돈 : " + m_iPay + "\n"
            + "선택한 곳 : " + m_sPlayerSelect + "\n\n"
            + "오른쪽과 왼쪽 중 하나를 선택해 고르면 건 돈의 2배!!\n"
            + "단 틀릴 경우 거신돈 전부 잃습니다.\n"
            + "진행하시겠습니까?";
    }

    private void GambleResult()
    {
        if (m_iPay < 0)
        {
            T_GambleResult.GetComponent<Text>().text = "음수를 넣을 수는 없습니다!!\n"
               + "건 돈을 음수가 되지않게 걸어주세요!!\n\n"
               + "조건\n"
               + "건 돈은 양수가 되어야합니다.";
        }
        else if (m_iPay > m_Player.IMoney)
            T_GambleResult.GetComponent<Text>().text = "소지금이 건 돈보다 적습니다!!\n"
                + "건 돈을 소지금 보다 적게 적어주세요!!\n\n"
                + "조건\n"
                + "소지금 > 건 돈";
        else
        {
            int RandomNum = Random.Range(1, 3);
            if (RandomNum == m_iPlayerSelect)
            {
                m_Player.IMoney += m_iPay;
                T_GambleResult.GetComponent<Text>().text = "축하합니다!!\n"
                    + m_iPay + "원을 걸어" + m_iPay * 2 + "원을 얻었습니다.\n\n"
                    + "도박 후 소지금 : " + m_Player.IMoney;
            }
            else
            {
                m_Player.IMoney -= m_iPay;
                T_GambleResult.GetComponent<Text>().text = "실패!!\n"
                    + m_iPay + "원 전부 잃었습니다.\n\n"
                    + "도박 후 소지금 : " + m_Player.IMoney;
            }
        }
    }

    //집 스크립트
    private void Heal()
    {
        int iBeforeHp =m_Player.IHp;  //이전 채력 = 플레이어 현재 채력
        m_Player.IHp = m_Player.Info.IMaxHp;       //플레이어 체력을 최대 채력으로 올림
        T_HouseHeal.GetComponent<Text>().text = "회복 전 채력 : " + iBeforeHp.ToString() + "\n\n"
            + "회복 후 채력 : " + m_Player.IHp;
    }

    //플레이어UI 스크립트
    private void PrintPlayerInfo()
    {
        T_PlayerUI.GetComponent<Text>().text = "이름 : " + m_Player.Info.SName + "\n"
            + "레벨 : " + m_Player.Info.ILevel.ToString() + "\n"
            + "물리 공격력 : " + m_Player.Info.IAtk.ToString() + "\n"
            + "마법 공격력 : " + m_Player.Info.IMatk.ToString() + "\n"
            + "체력 / 최대체력 : " + m_Player.Info.ICurrentHp.ToString() + " / " + m_Player.Info.IMaxHp.ToString() + "\n"
            + "공격 속도 : " + m_Player.Info.IAtkSpeed.ToString() + "\n"
            + "방어력 : " + m_Player.Info.IDef.ToString() + "\n"
            + "크리티컬 데미지 : " + m_Player.Info.FCriDmg.ToString() + "\n"
            + "--------------플레이어 스텟--------------\n"
            + "힘 : " + m_Player.Stat.IPow.ToString() + "\n"
            + "민첩 : " + m_Player.Stat.IDex.ToString() + "\n"
            + "지능 : " + m_Player.Stat.IInt.ToString();
    }
}