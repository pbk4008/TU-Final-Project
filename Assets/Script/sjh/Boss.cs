using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using enums;
using Structs;

public class Boss : Monster
{
    private Player m_Player; //플레이어 정보
    private int m_iFloor; //던전 층
    private int m_iStage; //던전 스테이지
    private int[] m_iCurrentDurationtime = new int[7]; //현재 스킬 지속시간
    private int[] m_iCurrentCooltime = new int[7]; //현재 스킬 쿨타임
    private int m_iDurationtime; //스킬 지속시간
    private int m_iOverlapping; //스킬 중첩
    private int m_iCooltime; //스킬 쿨타임
    private int m_idamage; //스킬 대미지
    private string m_sBossSkillName; //보스 스킬 이름
    private bool m_bSkillOn;//스킬 사용
    [SerializeField]
    private System_Battle m_SB; //배틀 정보
    [SerializeField]
    private Text m_tBossSKill; //보스 스킬 Text UI

    public bool bSkillOn { get => m_bSkillOn; set => m_bSkillOn = value; }
    public int iSkillDmg { get => m_idamage; set => m_idamage = value; }
    public string sBossSkill { get => m_sBossSkillName; set => m_sBossSkillName = value; }

    // Start is called before the first frame update
    public void BossSpawn() //보스 생성
    {
        Debug.Log("보스 생성");
        GameObject GM = GameObject.FindWithTag("GameMgr");
        m_Player = GameObject.Find("Player").GetComponent<Player>();
        m_iFloor = GM.GetComponent<Scr_DungeonBtn>().IFloor;
        m_iStage = GM.GetComponent<Scr_DungeonBtn>().IStage;
        m_bLive = true;
        SetInfo();

        m_iCurrentCooltime[0] = 3; //쿹라임 셋팅
        m_iCurrentCooltime[1] = 3;
        m_iCurrentCooltime[2] = 2;
        m_iCurrentCooltime[3] = 3;
        m_iCurrentCooltime[4] = 2;
        m_iCurrentCooltime[5] = 3;
        m_iCurrentCooltime[6] = 5;
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
                    setStatus(30, 30, 120, 3); //보스 공격력
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
                }
                break;
            case 5:
                if (m_iStage == 9)
                {
                    m_Info.SName = "흰 곰돌이";
                    setStatus(210, 270, 160, 7);
                    m_Info.ILevel = 70;
                }
                break;
        }
    }

    public void ActiveBossSkill(BOSSSKILL eBossSkill) // 보스 스킬
    {
        switch (eBossSkill) //스킬 이름에 따라
        {
            case enums.BOSSSKILL.FIRE: //화염 데미지 10%
                Debug.Log("화염 사용");
                m_sBossSkillName = "화염";
                m_Player.bDeeffect = true; //플레이어에게 지속 대미지를 넣음
                m_iCooltime = 3;
                m_iCurrentCooltime[0] = m_iCooltime; //현재 쿨타임 = 쿨타임
                m_iDurationtime = 2; //지속시간
                m_iCurrentDurationtime[0] += m_iDurationtime; //현재 지속시간 = 지속시간
                m_idamage = (int)(m_Info.IAtk * 0.2f); //화염 대미지
                break;
            case enums.BOSSSKILL.STUN:
                Debug.Log("스턴 사용");
                m_sBossSkillName = "스턴";
                m_Player.bDeeffect = true;
                m_iCooltime = 3;
                m_iCurrentCooltime[1] = m_iCooltime;
                m_iDurationtime = 1; //지속시간
                m_iCurrentDurationtime[1] += m_iDurationtime;

                //스턴을 어떻게 할 것인가
                //스턴 내용
                break;
            case enums.BOSSSKILL.BLEEDING:
                Debug.Log("흡혈 사용");
                m_sBossSkillName = "흡혈";
                m_Player.bDeeffect = true; //플레이어에게 지속 대미지를 넣음
                m_iCooltime = 2;
                m_iCurrentCooltime[2] = m_iCooltime;
                m_iDurationtime = 3;
                m_iCurrentDurationtime[2] += m_iDurationtime;
                m_idamage = (int)(m_Info.IAtk * 0.2f); //지속시간
                m_iOverlapping++; //중첩
                if (m_iOverlapping > 1) //중첩이 1이 넘으면
                {
                    m_iOverlapping = 2; //중첩 2로 고정
                    m_idamage = (int)(m_Info.IAtk * 0.2f) * m_iOverlapping; //대미지
                }
                break;
            case enums.BOSSSKILL.SLOW:
                Debug.Log("슬로우 사용");
                m_sBossSkillName = "슬로우";
                m_Player.bDeeffect = true;
                m_iCooltime = 3;
                m_iCurrentCooltime[3] = m_iCooltime;
                m_iDurationtime = 2;
                m_iCurrentDurationtime[3] += m_iDurationtime;
                m_Player.getInfo().setAtkSpeed(ref m_Player.getInfo(), m_Player.getInfo().IAtkSpeed - 2); //이동속도 느리게 함
                break;
            case enums.BOSSSKILL.POISON:
                Debug.Log("독 사용");
                m_sBossSkillName = "독";
                m_Player.bDeeffect = true;
                m_iCooltime = 2;
                m_iCurrentCooltime[4] = m_iCooltime;
                m_iDurationtime = 3;
                m_iCurrentDurationtime[4] += m_iDurationtime;
                m_idamage = (int)(m_Info.IAtk * 1);
                break;
            case enums.BOSSSKILL.STONING:
                Debug.Log("석화 사용");
                m_sBossSkillName = "석화";
                m_Player.bDeeffect = true;
                m_iCooltime = 3;
                m_iCurrentCooltime[5] = m_iCooltime;
                m_iDurationtime = 2;
                m_iCurrentDurationtime[5] += m_iDurationtime;
                m_idamage = (int)(m_Info.IAtk * 1);
                break;
            case enums.BOSSSKILL.FREEZE:
                Debug.Log("빙결 사용");
                m_sBossSkillName = "빙결";
                m_iCooltime = 5;
                m_iCurrentCooltime[6] = m_iCooltime;
                m_iDurationtime += 6;
                m_iCurrentDurationtime[6] += m_iDurationtime;
                m_idamage = (int)(m_Info.IAtk * 0.6f);
                m_Player.getInfo().setAtkSpeed(ref m_Player.getInfo(), m_Player.getInfo().IAtkSpeed - 3);
                break;
        }
        m_SB.bBossSkillOn = true;
    }

    private IEnumerator BossSkill() //보스 스킬
    {
        while (true)
        {
            switch(m_iFloor)
            {
                case 0:
                    if (m_iCurrentCooltime[0] == 0)
                        ActiveBossSkill(enums.BOSSSKILL.FIRE);
                    else if (m_iCurrentDurationtime[0] == 0)
                    {
                        m_tBossSKill.gameObject.SetActive(false);
                        m_Player.bDeeffect = false;
                    }
                    break;
                case 1:
                    if (m_iCurrentCooltime[1] == 0)
                        ActiveBossSkill(enums.BOSSSKILL.STUN);
                    else if (m_iCurrentDurationtime[1] == 0)
                        m_tBossSKill.gameObject.SetActive(false);
                    break;
                case 2:
                    if (m_iCurrentCooltime[2] == 0)
                        ActiveBossSkill(enums.BOSSSKILL.BLEEDING);
                    else if (m_iCurrentDurationtime[2] == 0)
                    {
                        m_tBossSKill.gameObject.SetActive(false);
                        m_Player.bDeeffect = false;
                    }
                    break;
                case 3:
                    if (m_iCurrentCooltime[3] == 0)
                        ActiveBossSkill(enums.BOSSSKILL.SLOW);
                    else if (m_iCurrentDurationtime[3] == 0)
                    {
                        m_tBossSKill.gameObject.SetActive(false);
                        m_Player.bDeeffect = false;
                        m_Player.getInfo().setAtkSpeed(ref m_Player.getInfo(), m_Player.getInfo().IAtkSpeed + 2); //이동속도 느리게 함
                    }
                    break;
                case 4:
                    //못숨2개
                    break;
                case 5:
                    if (m_iCurrentCooltime[4] == 0)
                        ActiveBossSkill(enums.BOSSSKILL.POISON);
                    else if (m_iCurrentDurationtime[4] == 0)
                    {
                        m_tBossSKill.gameObject.SetActive(false);
                        m_Player.bDeeffect = false;
                    }
                    if (m_iCurrentCooltime[5] == 0)
                        ActiveBossSkill(enums.BOSSSKILL.STONING);
                    else if (m_iCurrentDurationtime[5] == 0)
                    {
                        m_tBossSKill.gameObject.SetActive(false);
                        m_Player.bDeeffect = false;
                    }
                    if (m_iCurrentCooltime[6] == 0)
                        ActiveBossSkill(enums.BOSSSKILL.FREEZE);
                    else if (m_iCurrentDurationtime[6] == 0)
                    {
                        m_tBossSKill.gameObject.SetActive(false);
                        m_Player.bDeeffect = false;
                        m_Player.getInfo().setAtkSpeed(ref m_Player.getInfo(), m_Player.getInfo().IAtkSpeed + 3); //이동속도 느리게 함
                    }
                    break;
            }

            if (m_bSkillOn) //스킬이 사용 되었다면
            {
                for (int i = 0; i < m_iCurrentCooltime.Length; i++) 
                {
                    m_iCurrentCooltime[i]--; //각 현재 스킬 쿨타임을 1씩 줄인다.
                    m_iCurrentDurationtime[i]--; //각 현재 스킬 지속시간을 1씩 줄인다.

                    if (m_iCurrentCooltime[i] < 0) //각 스킬의 쿨타임이 0보다 작으면
                        m_iCurrentCooltime[i] = 0; //0으로 고정한다.
                    else if (m_iCurrentDurationtime[i] < 0) 
                        m_iCurrentDurationtime[i] = 0; //현재 스킬 지속시간을 0으로 설정
                }
                m_bSkillOn = false; // 스킬 사용 할 수 있도록 함
            }

            yield return new WaitForSeconds(0.1f);
        }
    }
}