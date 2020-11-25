using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Scr_QuestScript : MonoBehaviour
{
    private string m_iGrade;     //퀘스트 등급

<<<<<<< HEAD
    private int m_iRandomNum;              //확률지정
=======
    private int m_iRandomNum;//확률지정
>>>>>>> feature/QuestSign

    private int[] m_iCurrentMonsterCount = new int[3]; //퀘스트 받고 나서의 플레이어의 현재 잡은 몬스터 값
    private int[] m_iCurrentBossCount = new int[3];      //퀘스트 받고 나서의 플레이어의 현재 잡은 보스 값
    private int[] m_iCurrentEtcItemCount = new int[3];  //퀘스트 받고 나서의 플레이어의 현재 모은 기타아이템 값
    private int[] m_iQuest = new int[3];        //퀘스트 종류 배열
    private int[] m_iGoalCount = new int[3]; //퀘스트 목표 점수 배열

    private bool[] m_bLockQuest = new bool[3]; //퀘스트 선택하면 다시 돌리지 못하게 하는 변수

<<<<<<< HEAD
    [SerializeField] private GameObject Cvs_QuestText; //퀘스트 표지판 UI
=======
    [SerializeField] private GameObject Cvs_QuestText;   //퀘스트 표지판 UI
>>>>>>> feature/QuestSign
    [SerializeField] private Text[] T_Q = new Text[3];       //퀘스트 텍스트
    [SerializeField] private Text[] T_Select = new Text[3]; //퀘스트 수락 버튼 텍스트 

    private void Start()
    {
        for (int i = 0; i < 3; i++)
        {
            m_bLockQuest[i] = true; //참으로 바꿈 (처음엔 퀘스트 선택안하면 돌아감)
        }
        SettingQuest(); //퀘스트 셋팅
    }

    public void Click_QuestSign() //퀘스트 표지판 누르면
    {
        Cvs_QuestText.SetActive(true); //퀘스트 표지판 UI 띄우기
    }

    public void Click_ExitText() //퀘스트 표지판 닫기를 누르면
    {
        Cvs_QuestText.SetActive(false); //퀘스트 표지판 UI 접기
    }

    public void Click_Q1Select() //첫번 째 퀘스트를 수락하면
    {
        m_bLockQuest[0] = false; //퀘스트가 다시 지정되지 못하게 하기
        T_Select[0].GetComponent<Text>().text = " 진 행 중 ";
    }

    public void Click_Q2Select() //두번 째 퀘스트를 수락하면
    {
        m_bLockQuest[1] = false; //퀘스트가 다시 지정되지 못하게 하기
        T_Select[1].GetComponent<Text>().text = " 진 행 중 ";
    }

    public void Click_Q3Select() //세번 째 퀘스트를 수락하면
    {
        m_bLockQuest[2] = false; //퀘스트가 다시 지정되지 못하게 하기
        T_Select[2].GetComponent<Text>().text = " 진 행 중 ";
    }

    public void Click_ClearDungeon() //던전을 클리어 하면
    {
        SettingQuest(); //퀘스트 다시 셋팅
    }

    private void SettingQuest() //퀘스트 셋팅
    {
        for (int i = 0; i < 3; i++) //3번 반복
        {
<<<<<<< HEAD
            if (m_bLockQuest[i] == true) //퀘스트를 수락 안했다면
            {
                m_iRandomNum = Random.Range(1, 101); //1~100까지 랜덤 값 부여
                if (m_iRandomNum > 1 && m_iRandomNum <= 60)             //확률에 따라 등급 지정
=======
            if (m_bLockQuest[i] == true) //퀘스트 바꾸는게 가능하다면
            {
                m_iRandomNum = Random.Range(1, 101);                       //1~100의 랜덤 값 지정으로 등급 정하기
                if (m_iRandomNum > 1 && m_iRandomNum <= 60)
>>>>>>> feature/QuestSign
                    m_iGrade = "Normal";
                else if (m_iRandomNum > 60 && m_iRandomNum <= 90)
                    m_iGrade = "Special";
                else if (m_iRandomNum > 90 && m_iRandomNum <= 100)
                    m_iGrade = "Epic";

<<<<<<< HEAD
                m_iQuest[i] = Random.Range(1, 4);  //1~3 랜덤값 부여로 퀘스트 종류 정하기
                                                                   //1 = 보스 몬스터, //2 = 일반 몬스터, //3 = 기타 아이템

                switch (m_iQuest[i]) //퀘스트 종류 값에 따라
                {
                    case 1:
                        if (m_iGrade == "Normal")   //등급에 따라 목표 값 설정
=======
                m_iQuest[i] = Random.Range(1, 4); //1~3의 랜덤 값으로 퀘스트 종류 정하기
                                                                  //1 = 보스 몬스터, 2 = 일반 몬스터, 3 = 기타 아이템 

                switch (m_iQuest[i]) //퀘스트 종류에 따라
                {
                    case 1: // 1 = 보스 몬스터
                        if (m_iGrade == "Normal")         //등급에 따라 목표 달성량이 달라짐
>>>>>>> feature/QuestSign
                            m_iGoalCount[i] = 1;
                        else if (m_iGrade == "Special")
                            m_iGoalCount[i] = 3;
                        else if (m_iGrade == "Epic")
<<<<<<< HEAD
                            m_iGoalCount[i] = 5;       //퀘스트 텍스트 
                        T_Q[i].GetComponent<Text>().text = "[" + m_iGrade + "]" + "보스 몬스터 " + m_iGoalCount[i] + "마리 잡아오기\n"
                            + "진행도 : ( " + m_iCurrentBossCount[i] + " / " + m_iGoalCount[i] + ")";
                        break;
                    case 2:
                        if (m_iGrade == "Normal")
=======
                            m_iGoalCount[i] = 5;
                        T_Q[i].GetComponent<Text>().text = "[" + m_iGrade + "]" + "보스 몬스터 " + m_iGoalCount[i] + "마리 잡아오기\n" //퀘스트 목표 텍스트
                            + "진행도 : ( " + m_iCurrentBossCount[i] + " / " + m_iGoalCount[i] + ")";
                        break;
                    case 2: // 2 = 일반 몬스터
                        if (m_iGrade == "Normal")       //등급에 따라 목표 달성량이 달라짐
>>>>>>> feature/QuestSign
                            m_iGoalCount[i] = 5;
                        else if (m_iGrade == "Special")
                            m_iGoalCount[i] = 15;
                        else if (m_iGrade == "Epic")
<<<<<<< HEAD
                            m_iGoalCount[i] = 30;
                        T_Q[i].GetComponent<Text>().text = "[" + m_iGrade + "]" + "일반 몬스터 " + m_iGoalCount[i] + "마리 잡아오기\n"
                            + "진행도 : ( " + m_iCurrentMonsterCount[i] + " / " + m_iGoalCount[i] + ")";
                        break;
                    case 3:
                        if (m_iGrade == "Normal")
=======
                            m_iGoalCount[i] = 30; 
                        T_Q[i].GetComponent<Text>().text = "[" + m_iGrade + "]" + "일반 몬스터 " + m_iGoalCount[i] + "마리 잡아오기\n" //퀘스트 목표 텍스트
                            + "진행도 : ( " + m_iCurrentMonsterCount[i] + " / " + m_iGoalCount[i] + ")";
                        break;
                    case 3: // 3 = 기타 아이템
                        if (m_iGrade == "Normal")       //등급에 따라 목표 달성량이 달라짐
>>>>>>> feature/QuestSign
                            m_iGoalCount[i] = 30;
                        else if (m_iGrade == "Special")
                            m_iGoalCount[i] = 50;
                        else if (m_iGrade == "Epic")
                            m_iGoalCount[i] = 100;
<<<<<<< HEAD
                        T_Q[i].GetComponent<Text>().text = "[" + m_iGrade + "]" + "기타 아이템 " + m_iGoalCount[i] + "개 모아오기\n"
=======
                        T_Q[i].GetComponent<Text>().text = "[" + m_iGrade + "]" + "기타 아이템 " + m_iGoalCount[i] + "개 모아오기\n" //퀘스트 목표 텍스트
>>>>>>> feature/QuestSign
                            + "진행도 : ( " + m_iCurrentEtcItemCount[i] + " / " + m_iGoalCount[i] + ")";
                        break;
                }
            }
        }
    }
}