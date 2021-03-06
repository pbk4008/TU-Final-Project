﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using enums;

public class System_PlayerSkill : MonoBehaviour
{
    [SerializeField] Canvas Cvs_BattleCanvas; 
    Player m_Player; //플레이어 정보를 가져올 변수 
    Monster m_Monster; //몬스터 정보를 가져올 변수
    Boss m_Boss;
    GameObject DuengeonCvs;  //던전 씬에 있는 캔버스 1개
    System_Battle m_SB;
    private string m_sButtonName; //버튼 이름
    private bool m_bOnClick; //버튼을 클릭했는가
    private int m_iturn; //현재 턴
    private int[] m_Cooltime = new int[5]; //쿨타임
    private int[] m_DuringTime = new int[2];//버프지속,디버프지속 시간(by.pbk)
    private int m_iDamage; //데미지
    private bool m_bBuffOn;
    private bool m_bDeBuffOn;
    private int m_iFloor;
    private int m_iStage;
    public string sButtonName { get => m_sButtonName; set => m_sButtonName = value; }
    public bool bOnClick { get => m_bOnClick; set => m_bOnClick = value; }
    public int iDamage { get => m_iDamage; set => m_iDamage = value; }

    private void Start()
    {
        Scr_DungeonBtn GM = GameObject.FindWithTag("GameMgr").GetComponent<Scr_DungeonBtn>();
        m_iFloor = GM.IFloor;
        m_iStage = GM.IStage;
        DuengeonCvs = GameObject.Find("TextCanvas"); //캔버스 지정
        m_iturn = 1; //전투가 일어나면 현재턴을 1로 함
        m_Player = GameObject.Find("Player").GetComponent<Player>(); //플레이어 스크립트 가져오기
    }
    private void Update()
    {
        if (bOnClick) //버튼을 클릭했으면
        {
            m_SB = GameObject.Find("BattleManager").GetComponent<System_Battle>();
            if (m_SB.eBattleProcess == BATTLE_PROCESS.DURING)
            {
                if ((m_iFloor == 0 || m_iFloor == 1) && m_iStage != 4) //보스와 몬스터 구분하기 -  손준호
                    m_Monster = GameObject.Find("Monster").GetComponent<Monster>();
                else if ((m_iFloor == 2 || m_iFloor == 3) && m_iStage != 6)
                    m_Monster = GameObject.Find("Monster").GetComponent<Monster>();
                else if ((m_iFloor == 4 || m_iFloor == 5) && m_iStage != 9)
                    m_Monster = GameObject.Find("Monster").GetComponent<Monster>();
                else
                {
                    m_Boss = GameObject.Find("Boss").GetComponent<Boss>();
                }
            }
            m_Player = GameObject.Find("Player").GetComponent<Player>(); //플레이어 스크립트 가져오기
            Canvas Cvs_inven = GameObject.Find("InvenCanvas").GetComponent<Canvas>();
            switch (sButtonName) //버튼 이름에 따라 실행문 실행
            {
                case "Bag":
                    Cvs_inven.enabled = true;
                    DuengeonCvs.transform.GetChild(11).gameObject.SetActive(true); //플레이어 스킬 UI 띄우기
                    break;
                case "Skill": //플레이어 스킬 UI 띄우기
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
                    for (int i = 0; i <= 10; i++)
                        DuengeonCvs.transform.GetChild(i).gameObject.SetActive(false); //플레이어 스킬 UI 띄우기
                    break;
                case "Btn_SkillErrorExit":
                    DuengeonCvs.transform.GetChild(8).gameObject.SetActive(false); //플레이어 스킬 UI 띄우기
                    DuengeonCvs.transform.GetChild(9).gameObject.SetActive(false); //플레이어 스킬 UI 띄우기
                    DuengeonCvs.transform.GetChild(10).gameObject.SetActive(false); //플레이어 스킬 UI 띄우기
                    m_SB.BattleUI.SetActive(true);
                    break;
                case "Btn_LevelPlus": //플레이어 레벨 증가
                    m_Player.getInfo().setLevel(ref m_Player.getInfo(), m_Player.getInfo().ILevel + 10);
                    break;
                case "Btn_InvenExit":
                    Cvs_inven.enabled = false;
                    DuengeonCvs.transform.GetChild(11).gameObject.SetActive(false);
                    Cvs_BattleCanvas.enabled = true;
                    break;
            }
            bOnClick = false; //버튼 클릭 끝
        }
    }

    void PlayerSkillSet(PLAYERSKILL ePlayerSkill) //스킬 셋팅
    {
        System_Battle SB = GameObject.Find("BattleManager").GetComponent<System_Battle>();
        if (SB.iRound != 1) //현재 턴이 첫번째 턴이라면
        {
            switch (ePlayerSkill)
            {
                case PLAYERSKILL.START:
                    for(int i=0;i<8;i++)
                        DuengeonCvs.transform.GetChild(i).gameObject.SetActive(true); //플레이어 스킬 UI 띄우기
                    break;
                case PLAYERSKILL.JAMJAM: //잼잼펀치
                    if (m_Player.getInfo().ILevel >= 1 && m_Cooltime[0] == 0)
                        JamJam();
                    else
                        SkillError(PLAYERSKILL.JAMJAM);
                    break;
                case PLAYERSKILL.MAHA: //마하펀치
                    if (m_Player.getInfo().ILevel >= 5 && m_Cooltime[1] == 0)
                        Maha();
                    else
                        SkillError(PLAYERSKILL.MAHA);
                    break;
                case PLAYERSKILL.BUFF: //곰돌이 왕가의 축복
                    if (m_Player.getInfo().ILevel >= 15 && m_Cooltime[2] == 0)
                        Buff();
                    else
                        SkillError(PLAYERSKILL.BUFF);
                    break;
                case PLAYERSKILL.DEBUFF: //버려진 아들의 원한
                    if (m_Player.getInfo().ILevel >= 30 && m_Cooltime[3] == 0)
                        DeBuff();
                    else
                        SkillError(PLAYERSKILL.DEBUFF);
                    break;
                case PLAYERSKILL.UCHE: //우최 펀치
                    if (m_Player.getInfo().ILevel >= 50 && m_Cooltime[4] == 0)
                        Uche();
                    else
                        SkillError(PLAYERSKILL.UCHE);
                    break;
                case PLAYERSKILL.END: //플레이어 스킬 UI정리
                    for (int i = 0; i <= 10; i++)
                        DuengeonCvs.transform.GetChild(i).gameObject.SetActive(false); //플레이어 스킬 UI 띄우기
                    m_SB.eBattleProcess = BATTLE_PROCESS.BEFORE;
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
        UnActiveUI();
        m_SB.bPlayerSkillOn = true;
        m_Cooltime[0] = 2; //1턴 쿨타임;
        m_iDamage = (int)(m_Player.getInfo().IAtk * 0.8f); //데미지
        PlayerSkillSet(PLAYERSKILL.END);
    }

    private void Maha() //마하펀치
    {
        UnActiveUI();
        m_SB.bPlayerSkillOn = true;
        m_Cooltime[1] = 3; //2턴 쿨타임
        m_iDamage = (int)(m_Player.getInfo().IAtk * 2.40f); //데미지
        PlayerSkillSet(PLAYERSKILL.END);
    }

    private void Buff() //곰돌이 왕가의 축복
    {
        UnActiveUI();
        m_bBuffOn = true;
        m_SB.bPlayerSkillOn = true;
        m_Cooltime[2] = 5; //4턴 쿨타임
        m_DuringTime[0] = 3;//2턴 지속시간
        m_Player.transform.GetChild(0).gameObject.SetActive(true);
        m_Player.getStat().setPow(ref m_Player.getStat(), m_Player.getStat().IPow + 30); // Pow 30증가
        m_Player.getStat().setInt(ref m_Player.getStat(), m_Player.getStat().IInt + 30); // Int 30증가
        m_Player.getStat().setDex(ref m_Player.getStat(), m_Player.getStat().IDex + 30); // Dex 30증가
        PlayerSkillSet(PLAYERSKILL.END);
    }

    private void DeBuff() //버려진 아들의 원한
    {
        UnActiveUI();
        m_bDeBuffOn = true;
        m_SB.bPlayerSkillOn = true;
        m_Cooltime[3] = 7;//6턴 쿨타임
        m_DuringTime[1] = 5;//4턴 지속시간
        m_Monster.getInfo().setAtk(ref m_Monster.getInfo(), (int)(m_Monster.getInfo().IAtk * 0.6f));
        m_Monster.transform.GetChild(0).gameObject.SetActive(true);
        m_Monster.getInfo().setMatk(ref m_Monster.getInfo(), (int)(m_Monster.getInfo().IMatk * 0.6f));
        m_Monster.getInfo().setDef(ref m_Monster.getInfo(), (int)(m_Monster.getInfo().IDef * 0.6f));
        PlayerSkillSet(PLAYERSKILL.END);
    }

    private void Uche() ///우최펀치
    {
        UnActiveUI();
        m_SB.bPlayerSkillOn = true;
        m_Cooltime[4] = 11; //10턴 쿨타임
        m_iDamage = (int)(m_Monster.getInfo().IMatk * 0.4f); //데미지
        PlayerSkillSet(PLAYERSKILL.END);
    }
    private void SkillError(PLAYERSKILL ePlayerSkill) //스킬 에러
    {
        DuengeonCvs.transform.GetChild(8).gameObject.SetActive(true); //플레이어 스킬Error UI 띄우기
        DuengeonCvs.transform.GetChild(9).gameObject.SetActive(true); //플레이어 스킬Error UI 띄우기
        DuengeonCvs.transform.GetChild(10).gameObject.SetActive(true); //플레이어 스킬Error UI 띄우기

        switch(ePlayerSkill)
        {
            case PLAYERSKILL.START:
                DuengeonCvs.transform.GetChild(9).GetComponent<Text>().text = "첫 번째 턴에는 스킬을 사용할 수 없습니다.";
                break;
            case PLAYERSKILL.JAMJAM:
                if (m_Cooltime[0] != 0)
                    DuengeonCvs.transform.GetChild(9).GetComponent<Text>().text = "현재 스킬 쿨타임 중입니다!\n"
                        + "남은 쿨타임 : " + m_Cooltime[0];
                break;
            case PLAYERSKILL.MAHA:
                if (m_Cooltime[1] != 0)
                    DuengeonCvs.transform.GetChild(9).GetComponent<Text>().text = "현재 스킬 쿨타임 중입니다!\n"
                        + "남은 쿨타임 : " + m_Cooltime[1];
                else if(m_Player.getInfo().ILevel < 5)
                    DuengeonCvs.transform.GetChild(9).GetComponent<Text>().text = "레벨이 부족합니다!\n"
                        + "조건 : 플레이어 레벨 >= 5"; ;
                break;
            case PLAYERSKILL.BUFF:
                if (m_Cooltime[2] != 0)
                    DuengeonCvs.transform.GetChild(9).GetComponent<Text>().text = "현재 스킬 쿨타임 중입니다!\n"
                        + "남은 쿨타임 : " + m_Cooltime[2];
                else if (m_Player.getInfo().ILevel < 15)
                    DuengeonCvs.transform.GetChild(9).GetComponent<Text>().text = "레벨이 부족합니다!\n"
                        + "조건 : 플레이어 레벨 >= 15";
                break;
            case PLAYERSKILL.DEBUFF:
                if (m_Cooltime[3] != 0)
                    DuengeonCvs.transform.GetChild(9).GetComponent<Text>().text = "현재 스킬 쿨타임 중입니다!\n"
                        + "남은 쿨타임 : " + m_Cooltime[3];
                else if (m_Player.getInfo().ILevel < 30)
                    DuengeonCvs.transform.GetChild(9).GetComponent<Text>().text = "레벨이 부족합니다!\n"
                        + "조건 : 플레이어 레벨 >= 30";
                break;
            case PLAYERSKILL.UCHE:
                if (m_Cooltime[4] != 0)
                    DuengeonCvs.transform.GetChild(9).GetComponent<Text>().text = "현재 스킬 쿨타임 중입니다!\n"
                        + "남은 쿨타임 : " + m_Cooltime[4];
                else if (m_Player.getInfo().ILevel < 50)
                    DuengeonCvs.transform.GetChild(9).GetComponent<Text>().text = "레벨이 부족합니다!\n"
                        + "조건 : 플레이어 레벨 >= 50";
                break;
        }
    }

    public void MinusPlayerSkill()
    {
        for (int i = 0; i < m_Cooltime.Length; i++)
        {
            m_Cooltime[i]--; //쿨타임 감소
            if (m_Cooltime[i] < 0)
                m_Cooltime[i] = 0;
        }
        for (int i=0; i < m_DuringTime.Length; i++)
        {
            if(m_DuringTime[i]!=0)
            {
                m_DuringTime[i]--;
                if (m_DuringTime[i] == 0)
                {
                    if (i == 0)
                    {
                        m_bBuffOn = false;
                        m_Player.getStat().setPow(ref m_Player.getStat(), m_Player.getStat().IPow - 30); // Pow 30빼기
                        m_Player.getStat().setInt(ref m_Player.getStat(), m_Player.getStat().IInt - 30); // Int 30빼기
                        m_Player.getStat().setDex(ref m_Player.getStat(), m_Player.getStat().IDex - 30); // Dex 30빼기
                        m_Player.transform.GetChild(0).gameObject.SetActive(false);
                    }
                    else if (i == 1)
                    {
                        m_bDeBuffOn = false;
                        m_Monster.getInfo().setAtk(ref m_Monster.getInfo(), (int)(m_Monster.getInfo().IAtk / 0.6f));
                        m_Monster.getInfo().setMatk(ref m_Monster.getInfo(), (int)(m_Monster.getInfo().IMatk / 0.6f));
                        m_Monster.getInfo().setDef(ref m_Monster.getInfo(), (int)(m_Monster.getInfo().IDef / 0.6f));
                        m_Monster.transform.GetChild(0).gameObject.SetActive(false);
                    }
                }
            }
        }
    }

    private void UnActiveUI()
    {
        for (int i = 0; i < 8; i++)
            DuengeonCvs.transform.GetChild(i).gameObject.SetActive(false); //플레이어 스킬 UI 띄우기
    }
    public void SkillReset()
    {
        for (int i = 0; i < m_Cooltime.Length; i++)
        {
            m_Cooltime[i] = 0;
        }
        for(int j = 0; j<m_DuringTime.Length; j++)
        {
            m_DuringTime[j] = 0;
        }
        if(m_bBuffOn)
        {
            m_bBuffOn = false;
            m_Player.getStat().setPow(ref m_Player.getStat(), m_Player.getStat().IPow - 30); // Pow 30빼기
            m_Player.getStat().setInt(ref m_Player.getStat(), m_Player.getStat().IInt - 30); // Int 30빼기
            m_Player.getStat().setDex(ref m_Player.getStat(), m_Player.getStat().IDex - 30); // Dex 30빼기
        }

        if(m_bDeBuffOn)
        {
            m_bDeBuffOn = false;
            m_Monster.getInfo().setAtk(ref m_Monster.getInfo(), (int)(m_Monster.getInfo().IAtk / 0.6f));
            m_Monster.getInfo().setMatk(ref m_Monster.getInfo(), (int)(m_Monster.getInfo().IMatk / 0.6f));
            m_Monster.getInfo().setDef(ref m_Monster.getInfo(), (int)(m_Monster.getInfo().IDef / 0.6f));
        }
    }
}