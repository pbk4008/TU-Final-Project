using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using enums;
using Structs;
using Delegats;

public class Boss : Monster
{
    public static event LegEffect Footeffect;
    private Player m_Player; //플레이어 정보
    private int m_iFloor; //던전 층
    private int m_iStage; //던전 스테이지
    private int[] m_iCurrentDurationtime = new int[7]; //현재 스킬 지속시간
    private int[] m_iCurrentCooltime = new int[7]; //현재 스킬 쿨타임
    private int m_iDurationtime; //스킬 지속시간
    private int m_iOverlapping; //스킬 중첩
    private int m_iCooltime; //스킬 쿨타임
    private int m_ilives;
    private int[] m_idamage = new int[3]; //지속 딜 대미지
    private int[] m_iSkillDmg = new int[3];
    private string[] m_sBossSkillName = new string[3]; //보스 스킬 이름
    private bool m_bSkillOn;//스킬 사용
    private bool m_bMinusTime; //쿨타임 지속시간 감소
    private bool m_bLockSkill;
    [SerializeField]
    private System_Battle m_SB; //배틀 정보
    [SerializeField]
    private Text m_tBossSKill; //보스 스킬 Text UI

    public bool bSkillOn { get => m_bSkillOn; set => m_bSkillOn = value; }
    public bool bMinusTime { get => m_bMinusTime; set => m_bMinusTime = value; }
    public int ilives { get => m_ilives; set => m_ilives = value; }
    public int iOverlapping { get => m_iOverlapping; set => m_iOverlapping = value; }
    public bool bLockSkill { get => m_bLockSkill; set => m_bLockSkill = value; }

    // Start is called before the first frame update
    public void BossSpawn() //보스 생성
    {
        GameObject GM = GameObject.FindWithTag("GameMgr");
        m_Player = GameObject.Find("Player").GetComponent<Player>();
        m_iFloor = GM.GetComponent<Scr_DungeonBtn>().IFloor;
        m_iStage = GM.GetComponent<Scr_DungeonBtn>().IStage;
        SetInfo();
        StartCoroutine(BossSkill()); //코르틴 시작
    }

    // Update is called once per frame
    private void SetInfo() //Info 설정
    {
        switch(m_iFloor)//층수에 따라
        {
            case 0:
                if (m_iStage == 4) //스테이지에 따라
                {
                    m_Info.SName = "망*뇽 인형"; //보스 이름
                    setStatus(30, 330, 120, 3); //보스 공격력
                    m_Info.ILevel = 10; //보스 레벨
                }
                break;
            case 1:
                if (m_iStage == 4)
                {
                    m_Info.SName = "말하는 카우보이 인형";
                    setStatus(60, 90, 130, 4);
                    m_Info.ILevel = 20;
                }
                break;
            case 2:
                if (m_iStage == 6)
                {
                    m_Info.SName = "군인모형";
                    setStatus(100, 150, 140, 5);
                    m_Info.ILevel = 30;
                }
                break;
            case 3:
                if (m_iStage == 6)
                {
                    m_Info.SName = "커다란 달팽이 인형";
                    setStatus(30, 30, 120, 3);
                    m_Info.ILevel = 40;
                }
                break;
            case 4:
                if (m_iStage == 9)
                {
                    m_Info.SName = "건담";
                    setStatus(150, 210, 150, 6);
                    m_Info.ILevel = 50;
                    m_ilives = 1; //목숨 2개
                }
                break;
            case 5:
                if (m_iStage == 9)
                {
                    m_Info.SName = "흰 곰돌이";
                    setStatus(210, 270, 160, 7);
                    m_Info.ILevel = 70;
                    m_ilives = 2; //목숨 3개
                }
                break;
        }
    }

    public void ActiveBossSkill(BOSSSKILL eBossSkill) // 보스 스킬
    {
        Footeffect();
        if (!bLockSkill)
        {
            switch (eBossSkill) //스킬 이름에 따라
            {
                case enums.BOSSSKILL.FIRE: //화염 데미지 10%
                    m_sBossSkillName[0] = "화염 ";
                    m_Player.SetDeEffect(0, true); //플레이어에게 지속 대미지를 넣음
                    m_iCooltime = 3;
                    m_iCurrentCooltime[0] = m_iCooltime; //현재 쿨타임 = 쿨타임
                    m_iDurationtime = 3; //지속시간 = 2턴 2 + 1을 해야함
                    m_iCurrentDurationtime[0] = m_iDurationtime; //현재 지속시간 = 지속시간
                    m_idamage[0] = (int)(m_Info.IAtk * 0.2f); //화염 대미지
                    m_iSkillDmg[0] = (int)(m_Info.IAtk * 0.2f);
                    break;
                case enums.BOSSSKILL.STUN:
                    m_sBossSkillName[0] = "스턴 ";
                    m_Player.SetDeEffect(0, true);
                    m_Player.bStun = true;
                    m_iCooltime = 3;
                    m_iCurrentCooltime[1] = m_iCooltime;
                    m_iDurationtime = 2; //지속시간 = 1턴
                    m_iCurrentDurationtime[1] = m_iDurationtime;
                    break;
                case enums.BOSSSKILL.BLEEDING:
                    m_sBossSkillName[0] = "흡혈 " + m_iOverlapping + " ";
                    m_Player.SetDeEffect(0, true); //플레이어에게 지속 대미지를 넣음
                    m_iCooltime = 2;
                    m_iCurrentCooltime[2] = m_iCooltime;
                    m_iDurationtime = 4; //지속시간 = 3턴
                    m_iCurrentDurationtime[2] = m_iDurationtime;
                    m_idamage[0] = (int)(m_Info.IAtk * 0.2f); //지속시간
                    m_iSkillDmg[0] = (int)(m_Info.IAtk * 0.2f);
                    m_iOverlapping++; //중첩
                    if (m_iOverlapping > 1) //중첩이 1이 넘으면
                    {
                        m_iOverlapping = 2; //중첩 2로 고정
                        m_idamage[0] = (int)(m_Info.IAtk * 0.2f) * m_iOverlapping; //대미지
                    }
                    break;
                case enums.BOSSSKILL.SLOW:
                    m_sBossSkillName[0] = "슬로우 ";
                    m_Player.SetDeEffect(0, true);
                    m_iCooltime = 3;
                    m_iCurrentCooltime[3] = m_iCooltime;
                    m_iDurationtime = 3; //지속시간 = 2턴
                    m_iCurrentDurationtime[3] = m_iDurationtime;
                    break;
                case enums.BOSSSKILL.POISON:
                    m_iOverlapping++; //중첩
                    m_sBossSkillName[0] = "독" + m_iOverlapping + " ";
                    m_Player.SetDeEffect(0, true);
                    m_iCooltime = 2;
                    m_iCurrentCooltime[4] = m_iCooltime;
                    m_iDurationtime = 4; //지속시간 = 3턴
                    m_iCurrentDurationtime[4] = m_iDurationtime;
                    m_idamage[0] = (int)(m_Info.IAtk * 1);
                    m_iSkillDmg[0] = (int)(m_Info.IAtk * 1);
                    if (m_iOverlapping > 1) //중첩이 1이 넘으면
                    {
                        if (m_iOverlapping > 2)
                            m_iOverlapping = 3; //중첩 2로 고정
                        m_idamage[0] = (int)(m_Info.IAtk * 1f) * m_iOverlapping; //대미지
                    }
                    break;
                case enums.BOSSSKILL.STONING:
                    m_sBossSkillName[1] = "석화 ";
                    m_Player.SetDeEffect(1, true);
                    m_Player.bStun = true;
                    m_iCooltime = 3;
                    m_iCurrentCooltime[5] = m_iCooltime;
                    m_iDurationtime = 3; //지속시간 = 2턴
                    m_iCurrentDurationtime[5] = m_iDurationtime;
                    m_idamage[1] = (int)(m_Info.IAtk * 1);
                    m_iSkillDmg[1] = (int)(m_Info.IAtk * 1);
                    break;
                case enums.BOSSSKILL.FREEZE:
                    m_sBossSkillName[2] = "빙결 ";
                    m_Player.SetDeEffect(2, true);
                    m_iCooltime = 5;
                    m_iCurrentCooltime[6] = m_iCooltime;
                    m_iDurationtime = 5; //지속시간 = 16턴
                    m_iCurrentDurationtime[6] = m_iDurationtime;
                    m_idamage[2] = (int)(m_Info.IAtk * 0.6f);
                    m_iSkillDmg[2] = (int)(m_Info.IAtk * 0.6f);
                    break;
            }

            m_SB.bBossSkillOn = true;
        }
        bLockSkill = false;
    }

    private IEnumerator BossSkill() //보스 스킬
    {
        System_Battle SB = GameObject.Find("BattleManager").GetComponent<System_Battle>();
        while (true)
        {
            switch(m_iFloor)
            {
                case 0:
                    if (m_iCurrentCooltime[0] == 0 && m_bSkillOn && SB.iRound % 3 == 0)
                        ActiveBossSkill(enums.BOSSSKILL.FIRE);
                    else if (m_iCurrentDurationtime[0] == 0 && m_Player.GetDeEffect(0))
                    {
                        m_tBossSKill.gameObject.SetActive(false);
                        m_sBossSkillName[0] = "";
                        m_idamage[0] = 0;
                        m_Player.SetDeEffect(0, false);
                    }
                    break;
                case 1:
                    if (m_iCurrentCooltime[1] == 0 && m_bSkillOn && SB.iRound % 3 == 0)
                        ActiveBossSkill(enums.BOSSSKILL.STUN);
                    else if (m_iCurrentDurationtime[1] == 0 && m_Player.GetDeEffect(0))
                    {
                        m_tBossSKill.gameObject.SetActive(false);
                        m_sBossSkillName[0] = "";
                        m_idamage[0] = 0;
                        m_Player.bStun = false;
                    }
                    break;
                case 2:
                    if (m_iCurrentCooltime[2] == 0 && m_bSkillOn && SB.iRound % 2 == 0)
                        ActiveBossSkill(enums.BOSSSKILL.BLEEDING);
                    else if (m_iCurrentDurationtime[2] == 0 && m_Player.GetDeEffect(0))
                    {
                        m_tBossSKill.gameObject.SetActive(false);
                        m_iOverlapping = 0;
                        m_idamage[0] = 0;
                        m_sBossSkillName[0] = "";
                        m_Player.SetDeEffect(0, false);
                    }
                    break;
                case 3:
                    if (m_iCurrentCooltime[3] == 0 && m_bSkillOn && SB.iRound % 3 == 0)
                        ActiveBossSkill(enums.BOSSSKILL.SLOW);
                    else if (m_iCurrentDurationtime[3] == 0 && m_Player.GetDeEffect(0))
                    {
                        m_tBossSKill.gameObject.SetActive(false);
                        m_Player.SetDeEffect(0, false);
                        m_idamage[0] = 0;
                        m_sBossSkillName[0] = "";
                        m_Player.getInfo().setAtkSpeed(ref m_Player.getInfo(), m_Player.getInfo().IAtkSpeed + 2); //이동속도 느리게 함
                    }
                    break;
                case 4:
                    break;
                case 5:
                    if (m_iCurrentCooltime[4] == 0 && m_bSkillOn && SB.iRound % 2 == 0)
                        ActiveBossSkill(enums.BOSSSKILL.POISON);
                    else if (m_iCurrentDurationtime[4] == 0 && m_Player.GetDeEffect(0))
                    {
                        m_tBossSKill.gameObject.SetActive(false);
                        m_Player.SetDeEffect(0, false);
                        m_idamage[0] = 0;
                        m_iOverlapping = 0;
                        m_sBossSkillName[0] = "";
                    }
                    if (m_iCurrentCooltime[5] == 0 && m_bSkillOn && SB.iRound % 3 == 0)
                        ActiveBossSkill(enums.BOSSSKILL.STONING);
                    else if (m_iCurrentDurationtime[5] == 0 && m_Player.GetDeEffect(1))
                    {
                        m_tBossSKill.gameObject.SetActive(false);
                        m_Player.SetDeEffect(1, false);
                        m_idamage[1] = 0;
                        m_Player.bStun = false;
                        m_sBossSkillName[1] = "";
                    }
                    if (m_iCurrentCooltime[6] == 0 && m_bSkillOn && SB.iRound % 5 == 0)
                        ActiveBossSkill(enums.BOSSSKILL.FREEZE);
                    else if (m_iCurrentDurationtime[6] == 0 && m_Player.GetDeEffect(2))
                    {
                        m_tBossSKill.gameObject.SetActive(false);
                        m_Player.SetDeEffect(2, false);
                        m_idamage[2] = 0;
                        m_sBossSkillName[2] = "";
                        m_Player.getInfo().setAtkSpeed(ref m_Player.getInfo(), m_Player.getInfo().IAtkSpeed + 3); //이동속도 느리게 함
                    }
                    break;
            }

            if (m_bMinusTime) //스킬이 사용 되었다면
            {
                for (int i = 0; i < m_iCurrentDurationtime.Length; i++) 
                {
                    m_iCurrentCooltime[i]--; //각 현재 스킬 쿨타임을 1씩 줄인다.
                    m_iCurrentDurationtime[i]--; //각 현재 스킬 지속시간을 1씩 줄인다.

                    if (m_iCurrentCooltime[i] < 0) //각 스킬의 쿨타임이 0보다 작으면
                        m_iCurrentCooltime[i] = 0; //0으로 고정한다.
                    if (m_iCurrentDurationtime[i] < 0) 
                        m_iCurrentDurationtime[i] = 0; //현재 스킬 지속시간을 0으로 설정
                }
                m_bMinusTime = false; // 스킬 사용 할 수 있도록 함
            }

            yield return new WaitForSeconds(0.1f);
        }
    }

    public string BossSkillUI(int argint)
    {
        return m_sBossSkillName[argint];
    }
    public int GetSkillDmg(int argint)
    {
        return m_idamage[argint];
    }
    public int GetDmg(int argint)
    {
        return m_iSkillDmg[argint];
    }
    public void SetDmg(int argint, int argint2)
    {
        m_iSkillDmg[argint] = argint2;
    }
}