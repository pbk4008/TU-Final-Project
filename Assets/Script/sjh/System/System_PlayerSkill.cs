using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using enums;

public class System_PlayerSkill : MonoBehaviour
{
    Player m_Player; //플레이어 정보를 가져올 변수 
    Monster m_Monster; //몬스터 정보를 가져올 변수
    GameObject[] DuengeonCvs = new GameObject[2]; //던전 씬에 있는 캔버스 2개
    private string m_sButtonName; //버튼 이름
    private bool m_bOnClick; //버튼을 클릭했는가
    private int m_iturn; //현재 턴
    private int[] m_Cooltime = new int[5]; //쿨타임
    private float m_fDamage; //데미지
     
    public string sButtonName { get => m_sButtonName; set => m_sButtonName = value; }
    public bool bOnClick { get => m_bOnClick; set => m_bOnClick = value; }

    private void Start()
    {
        DuengeonCvs[0] = GameObject.Find("Cvs_Button"); //캔버스 지정
        DuengeonCvs[1] = GameObject.Find("Cvs_LevelUpText"); //캔버스 지정
        m_iturn = 1; //전투가 일어나면 현재턴을 1로 함
    }
    private void Update()
    {
        m_Player = GameObject.FindWithTag("Player").GetComponent<Player>(); //플레이어 스크립트 가져오기
        if (bOnClick) //버튼을 클릭했으면
        {
            switch (sButtonName) //버튼 이름에 따라 실행문 실행
            {
                case "Btn_PlayerSkill": //플레이어 스킬 UI 띄우기
                    PlayerSkillSet(PLAYERSKILL.START);
                    break;
                case "Btn_JamJam": //잼잼펀치
                    PlayerSkillSet(PLAYERSKILL.JAMJAM);
                    break;
                case "Btn_Maha": //마하 펀치
                    PlayerSkillSet(PLAYERSKILL.MAHA);
                    break;
                case "Btn_Buff": //곰돌이 왕가의 축복
                    PlayerSkillSet(PLAYERSKILL.BUFF);
                    break;
                case "Btn_Debuff": //버려진 아들의 원한
                    PlayerSkillSet(PLAYERSKILL.DEBUFF);
                    break;
                case "Btn_Uche": //우최 펀치
                    PlayerSkillSet(PLAYERSKILL.UCHE);
                    break;
                case "Btn_PlayerSkillEnd": //플레이어 스킬 정리
                    PlayerSkillSet(PLAYERSKILL.END);
                    break;
                case "Btn_SkillErrorExit":
                    DuengeonCvs[1].transform.GetChild(4).gameObject.SetActive(false); //플레이어 스킬 UI 띄우기
                    DuengeonCvs[1].transform.GetChild(5).gameObject.SetActive(false); //플레이어 스킬 UI 띄우기
                    DuengeonCvs[1].transform.GetChild(6).gameObject.SetActive(false); //플레이어 스킬 UI 띄우기
                    break;
                case "Btn_Turn": //턴 증가
                    m_iturn++;
                    for (int i = 0; i < m_Cooltime.Length; i++)
                    {
                        m_Cooltime[i]--; //쿨타임 감소
                        if (m_Cooltime[i] < 0)
                            m_Cooltime[i] = 0;
                    }
                    break;
                case "Btn_LevelPlus": //플레이어 레벨 증가
<<<<<<< HEAD
                    m_Player.getInfo().setLevel(ref m_Player.getInfo(), m_Player.getInfo().ILevel + 10);
=======
                    m_Player.getInfo().setLevel(ref m_Player.getInfo(), m_Player.Info.ILevel + 10);
>>>>>>> 4db89e11781eea41b3b39dc0ae779c1a64e1d179
                    break;
            }
            bOnClick = false; //버튼 클릭 끝
        }
    }

    void PlayerSkillSet(PLAYERSKILL ePlayerSkill) //스킬 셋팅
    {
        if (m_iturn != 1) //현재 턴이 첫번째 턴이라면
        {
            switch (ePlayerSkill)
            {
                case PLAYERSKILL.START:
                    for (int i = 11; i <= 16; i++)
                        DuengeonCvs[0].transform.GetChild(i).gameObject.SetActive(true); //플레이어 스킬 UI 띄우기
                    DuengeonCvs[1].transform.GetChild(2).gameObject.SetActive(true); //플레이어 스킬 UI 띄우기
                    DuengeonCvs[1].transform.GetChild(3).gameObject.SetActive(true); //플레이어 스킬 UI 띄우기
                    break;
                case PLAYERSKILL.JAMJAM: //잼잼펀치

<<<<<<< HEAD
                    if (m_Player.getInfo().ILevel >= 1 && m_Cooltime[0] == 0)
=======
                    if (m_Player.Info.ILevel >= 1 && m_Cooltime[0] == 0)
>>>>>>> 4db89e11781eea41b3b39dc0ae779c1a64e1d179
                        JamJam();
                    else
                        SkillError(PLAYERSKILL.JAMJAM);
                    break;
                case PLAYERSKILL.MAHA: //마하펀치
<<<<<<< HEAD
                    if (m_Player.getInfo().ILevel >= 5 && m_Cooltime[1] == 0)
=======
                    if (m_Player.Info.ILevel >= 5 && m_Cooltime[1] == 0)
>>>>>>> 4db89e11781eea41b3b39dc0ae779c1a64e1d179
                        Maha();
                    else
                        SkillError(PLAYERSKILL.MAHA);
                    break;
                case PLAYERSKILL.BUFF: //곰돌이 왕가의 축복
<<<<<<< HEAD
                    if (m_Player.getInfo().ILevel >= 15 && m_Cooltime[2] == 0)
=======
                    if (m_Player.Info.ILevel >= 15 && m_Cooltime[2] == 0)
>>>>>>> 4db89e11781eea41b3b39dc0ae779c1a64e1d179
                        Buff();
                    else
                        SkillError(PLAYERSKILL.BUFF);
                    break;
                case PLAYERSKILL.DEBUFF: //버려진 아들의 원한
<<<<<<< HEAD
                    if (m_Player.getInfo().ILevel >= 30 && m_Cooltime[3] == 0)
=======
                    if (m_Player.Info.ILevel >= 30 && m_Cooltime[3] == 0)
>>>>>>> 4db89e11781eea41b3b39dc0ae779c1a64e1d179
                        DeBuff();
                    else
                        SkillError(PLAYERSKILL.DEBUFF);
                    break;
                case PLAYERSKILL.UCHE: //우최 펀치
<<<<<<< HEAD
                    if (m_Player.getInfo().ILevel >= 50 && m_Cooltime[4] == 0)
=======
                    if (m_Player.Info.ILevel >= 50 && m_Cooltime[4] == 0)
>>>>>>> 4db89e11781eea41b3b39dc0ae779c1a64e1d179
                        Uche();
                    else
                        SkillError(PLAYERSKILL.UCHE);
                    break;
                case PLAYERSKILL.END: //플레이어 스킬 UI정리
                    DuengeonCvs[0] = GameObject.Find("Cvs_Button"); //캔버스 지정
                    for (int i = 11; i <= 16; i++)
                        DuengeonCvs[0].transform.GetChild(i).gameObject.SetActive(false); //플레이어 스킬 UI 띄우기
                    DuengeonCvs[1] = GameObject.Find("Cvs_LevelUpText"); //캔버스 지정
                    DuengeonCvs[1].transform.GetChild(2).gameObject.SetActive(false); //플레이어 스킬 UI 띄우기
                    DuengeonCvs[1].transform.GetChild(3).gameObject.SetActive(false); //플레이어 스킬 UI 띄우기
                    solveBuff();
                    break;
            }
         }
         else
         {
            SkillError(PLAYERSKILL.START);
         }
    }
    private void JamJam()
    {
        m_Cooltime[0] = 1;
        Debug.Log("잼잼펀치 사용");
<<<<<<< HEAD
        m_fDamage = m_Player.getInfo().IAtk * 0.8f; //데미지
=======
        m_fDamage = m_Player.Info.IAtk * 0.8f; //데미지
>>>>>>> 4db89e11781eea41b3b39dc0ae779c1a64e1d179
        PlayerSkillSet(PLAYERSKILL.END);
    }

    private void Maha() //마하펀치
    {
        m_Cooltime[1] = 2;
        Debug.Log("마하펀치 사용");
<<<<<<< HEAD
        m_fDamage = m_Player.getInfo().IAtk * 2.40f; //데미지
=======
        m_fDamage = m_Player.Info.IAtk * 2.40f; //데미지
>>>>>>> 4db89e11781eea41b3b39dc0ae779c1a64e1d179
        PlayerSkillSet(PLAYERSKILL.END);
    }

    private void Buff() //곰돌이 왕가의 축복
    {
        m_Cooltime[2] = 4;
        Debug.Log("버프 사용");
<<<<<<< HEAD
        m_Player.getStat().setPow(ref m_Player.getStat(), m_Player.getStat().IPow + 30); // Pow 30증가
        m_Player.getStat().setInt(ref m_Player.getStat(), m_Player.getStat().IInt + 30); // Int 30증가
        m_Player.getStat().setDex(ref m_Player.getStat(), m_Player.getStat().IDex + 30); // Dex 30증가
=======
        m_Player.getStat().setPow(ref m_Player.getStat(), m_Player.Stat.IPow + 30); // Pow 30증가
        m_Player.getStat().setInt(ref m_Player.getStat(), m_Player.Stat.IInt + 30); // Int 30증가
        m_Player.getStat().setDex(ref m_Player.getStat(), m_Player.Stat.IDex + 30); // Dex 30증가
>>>>>>> 4db89e11781eea41b3b39dc0ae779c1a64e1d179
        PlayerSkillSet(PLAYERSKILL.END);
    }

    private void DeBuff() //버려진 아들의 원한
    {
        m_Cooltime[3] = 6;
        Debug.Log("디버프 사용");
        //
        PlayerSkillSet(PLAYERSKILL.END);
    }

    private void Uche() ///우최펀치
    {
        m_Cooltime[4] = 10;
        Debug.Log("우최펀치 사용");
        //
        PlayerSkillSet(PLAYERSKILL.END);
    }

    private void solveBuff() //버프 해제
    {
        if(m_Cooltime[2] == 2)
        {
<<<<<<< HEAD
            m_Player.getStat().setPow(ref m_Player.getStat(), m_Player.getStat().IPow - 30); // Pow 30빼기
            m_Player.getStat().setInt(ref m_Player.getStat(), m_Player.getStat().IInt - 30); // Int 30빼기
            m_Player.getStat().setDex(ref m_Player.getStat(), m_Player.getStat().IDex - 30); // Dex 30빼기
=======
            m_Player.getStat().setPow(ref m_Player.getStat(), m_Player.Stat.IPow - 30); // Pow 30빼기
            m_Player.getStat().setInt(ref m_Player.getStat(), m_Player.Stat.IInt - 30); // Int 30빼기
            m_Player.getStat().setDex(ref m_Player.getStat(), m_Player.Stat.IDex - 30); // Dex 30빼기
>>>>>>> 4db89e11781eea41b3b39dc0ae779c1a64e1d179
        }
    }

    private void SkillError(PLAYERSKILL ePlayerSkill) //스킬 에러
    {
        DuengeonCvs[1].transform.GetChild(4).gameObject.SetActive(true); //플레이어 스킬Error UI 띄우기
        DuengeonCvs[1].transform.GetChild(5).gameObject.SetActive(true); //플레이어 스킬Error UI 띄우기
        DuengeonCvs[1].transform.GetChild(6).gameObject.SetActive(true); //플레이어 스킬Error UI 띄우기

        switch(ePlayerSkill)
        {
            case PLAYERSKILL.START:
                DuengeonCvs[1].transform.GetChild(5).GetComponent<Text>().text = "첫 번째 턴에는 스킬을 사용할 수 없습니다.";
                break;
            case PLAYERSKILL.JAMJAM:
                if (m_Cooltime[0] != 0)
                    DuengeonCvs[1].transform.GetChild(5).GetComponent<Text>().text = "현재 스킬 쿨타임 중입니다!\n"
                        + "남은 쿨타임 : " + m_Cooltime[0];
                break;
            case PLAYERSKILL.MAHA:
                if (m_Cooltime[1] != 0)
                    DuengeonCvs[1].transform.GetChild(5).GetComponent<Text>().text = "현재 스킬 쿨타임 중입니다!\n"
                        + "남은 쿨타임 : " + m_Cooltime[1];
<<<<<<< HEAD
                else if(m_Player.getInfo().ILevel < 5)
=======
                else if(m_Player.Info.ILevel < 5)
>>>>>>> 4db89e11781eea41b3b39dc0ae779c1a64e1d179
                    DuengeonCvs[1].transform.GetChild(5).GetComponent<Text>().text = "레벨이 부족합니다!\n"
                        + "조건 : 플레이어 레벨 >= 5"; ;
                break;
            case PLAYERSKILL.BUFF:
                if (m_Cooltime[2] != 0)
                    DuengeonCvs[1].transform.GetChild(5).GetComponent<Text>().text = "현재 스킬 쿨타임 중입니다!\n"
                        + "남은 쿨타임 : " + m_Cooltime[2];
<<<<<<< HEAD
                else if (m_Player.getInfo().ILevel < 15)
=======
                else if (m_Player.Info.ILevel < 15)
>>>>>>> 4db89e11781eea41b3b39dc0ae779c1a64e1d179
                    DuengeonCvs[1].transform.GetChild(5).GetComponent<Text>().text = "레벨이 부족합니다!\n"
                        + "조건 : 플레이어 레벨 >= 15";
                break;
            case PLAYERSKILL.DEBUFF:
                if (m_Cooltime[3] != 0)
                    DuengeonCvs[1].transform.GetChild(5).GetComponent<Text>().text = "현재 스킬 쿨타임 중입니다!\n"
                        + "남은 쿨타임 : " + m_Cooltime[3];
<<<<<<< HEAD
                else if (m_Player.getInfo().ILevel < 30)
=======
                else if (m_Player.Info.ILevel < 30)
>>>>>>> 4db89e11781eea41b3b39dc0ae779c1a64e1d179
                    DuengeonCvs[1].transform.GetChild(5).GetComponent<Text>().text = "레벨이 부족합니다!\n"
                        + "조건 : 플레이어 레벨 >= 30";
                break;
            case PLAYERSKILL.UCHE:
                if (m_Cooltime[4] != 0)
                    DuengeonCvs[1].transform.GetChild(5).GetComponent<Text>().text = "현재 스킬 쿨타임 중입니다!\n"
                        + "남은 쿨타임 : " + m_Cooltime[4];
<<<<<<< HEAD
                else if (m_Player.getInfo().ILevel < 50)
=======
                else if (m_Player.Info.ILevel < 50)
>>>>>>> 4db89e11781eea41b3b39dc0ae779c1a64e1d179
                    DuengeonCvs[1].transform.GetChild(5).GetComponent<Text>().text = "레벨이 부족합니다!\n"
                        + "조건 : 플레이어 레벨 >= 50";
                break;
        }
    }
}