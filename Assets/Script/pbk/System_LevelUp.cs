using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class System_LevelUp : MonoBehaviour//LevelUp 및 스텟 관련 클래스
{
    private Player m_Player; //플레이어 정보를 가져올 변수 
    GameObject[] LevelUpMgr = new GameObject[2]; //던전 씬에 있는 캔버스 2개

    private string m_sButtonName; //버튼 이름
    private bool m_bOnClick; //버튼을 클릭했는가
    public string sButtonName { get => m_sButtonName; set => m_sButtonName = value; }
    public bool bOnClick { get => m_bOnClick; set => m_bOnClick = value; }

    void Start()
    {
        m_Player = GetComponent<Player>();//has-a 관계로 인한 Player Object에 붙힘
    }

    public void CalculStat()//스텟 계산
    {
        m_Player.getInfo().setMaxHp(ref m_Player.getInfo(),m_Player.getStat().IPow * 3 + 300);
        m_Player.getInfo().setCurrentHp(ref m_Player.getInfo(), m_Player.getInfo().IMaxHp);
        m_Player.getInfo().setAtkSpeed(ref m_Player.getInfo(), m_Player.getStat().IDex / 15 + 1);
        if (m_Player.getInfo().IAtkSpeed >= 10)
            m_Player.getInfo().setAtkSpeed(ref m_Player.getInfo(), 10);
        m_Player.getInfo().setAtk(ref m_Player.getInfo(), m_Player.getStat().IPow/2+3);
        m_Player.getInfo().setMatk(ref m_Player.getInfo(), m_Player.getStat().IInt+3);
        m_Player.getInfo().setCriDmg(ref m_Player.getInfo(), m_Player.getStat().IDex / 3 * 0.02f + 1.1f);
        
        //argInfo.IDef = 방어구
    }
    void Update()
    {
        CalculStat(); //스텟 계산
        if (bOnClick) //버튼을 클릭했으면
        {
            switch (sButtonName) //버튼 이름에 따라 실행문 실행
            {
                case "Button":
                    Debug.Log("ㅎㅇ");
                    m_Player.BLive = false;
                    SceneManager.LoadScene(1);
                    break;
                case "Btn_LevelUp":
                    m_Player.FExp = 121; //경험치 조정
                    break;
                case "Btn_Pow":
                    if(m_Player.getStat().m_iStat > 0)
                        LevelUp(1); //Pow 스텟 올리기
                    break;
                case "Btn_Int":
                    if (m_Player.getStat().m_iStat > 0)
                        LevelUp(2); //Int 스텟 올리기
                    break;
                case "Btn_Dex":
                    if (m_Player.getStat().m_iStat > 0)
                        LevelUp(3); //Dex 스텟 올리기
                    break;
                case "Btn_LevelUpEnd": //레벨업 관련 정리
                    m_Player.getStat().setStat(ref m_Player.getStat(), 3); //스텟 초기화
                    for (int i = 6; i < 10; i++)
                        LevelUpMgr[0].transform.GetChild(i).gameObject.SetActive(false); // 레벨업에 쓴 UI들 비활성화
                    LevelUpMgr[1].transform.GetChild(0).gameObject.SetActive(false);  // 레벨업에 쓴 UI들 비활성화
                    SceneManager.LoadScene(1); //로비씬으로 이동
                    break;
            }
            bOnClick = false; //버튼 클릭 끝
        }
        if (m_Player.FExp >= 100) //경험치량이 100 이상이면
            LevelUp(0); // 레벨업 실행
    }
    private void LevelUp(int iStat) //레벨업
    {
        switch (iStat) //iStat 값에 따라
        {
            case 0: // 레벨업 UI 띄우기
                m_Player.FExp -= 100; //경험치량 100 깎음
                m_Player.getInfo().setLevel(ref m_Player.getInfo(), m_Player.getInfo().ILevel + 1); //레벨 1업

                LevelUpMgr[0] = GameObject.Find("Cvs_Button"); //캔버스 지정
                for(int i = 6;i<10; i++)
                    LevelUpMgr[0].transform.GetChild(i).gameObject.SetActive(true); //레벨업 UI 띄우기
                LevelUpMgr[1] = GameObject.Find("Cvs_LevelUpText"); //캔버스 지정
                LevelUpMgr[1].transform.GetChild(0).gameObject.SetActive(true); //레벨업 UI 띄우기
                break;
            case 1: //Pow 스텟 올리기
                m_Player.getStat().setPow(ref m_Player.getStat(), m_Player.getStat().IPow + 1);
                m_Player.getStat().setStat(ref m_Player.getStat(), m_Player.getStat().IStat - 1);
                break;
            case 2: //Int 스텟 올리기
                m_Player.getStat().setInt(ref m_Player.getStat(), m_Player.getStat().IInt + 1);
                m_Player.getStat().setStat(ref m_Player.getStat(), m_Player.getStat().IStat - 1);
                break;
            case 3: //Dex 스텟 올리기
                m_Player.getStat().setDex(ref m_Player.getStat(), m_Player.getStat().IDex + 1);
                m_Player.getStat().setStat(ref m_Player.getStat(), m_Player.getStat().IStat - 1);
                break;
        }

        //레벨업 텍스트UI 띄우기
        LevelUpMgr[1].transform.GetChild(0).GetComponent<Text>().text = "원하는 스텟을 선택해 주세요!\n\n"
        + "남은 스텟 포인트 : " + m_Player.getStat().IStat + "\n\n\n"
        + "현재 Pow : " + m_Player.getStat().IPow + "                 현재 Int : " + m_Player.getStat().IInt + "                 현재 Dex : " + m_Player.getStat().IDex;

        if (m_Player.getStat().IStat == 0) //스텟 포인트를 모두 사용해 스텟 포인트가 0이 되었다면
        {
            LevelUpMgr[1].transform.GetChild(1).gameObject.SetActive(true); //완료 버튼을 띄운다.
        }
    }
}
