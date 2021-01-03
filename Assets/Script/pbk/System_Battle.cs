using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Delegats;
using enums;
public class System_Battle : MonoBehaviour
{

    private Player m_Player;//플레이어 정보
    [SerializeField]
    private Monster m_Monster;//몬스터 정보
    [SerializeField]
    private Boss m_Boss;
    private System_Spawn m_spSystem;
    [SerializeField]
    private Scrollbar m_MonHp;//몬스터 체력바
    [SerializeField]
    private Scrollbar m_PlayerHp;//플레이어 체력바
    [SerializeField]
    private Image m_MonImg;//몬스터 이미지
    [SerializeField]
    private Text m_RoundCount;//라운드 카운트 Text
    [SerializeField]
    private Text m_tPlayerSpeed;
    [SerializeField]
    private Text m_tMonSpeed;
    [SerializeField]
    private Text m_tBossSKill;
    [SerializeField]
    private GameObject m_BattleUI;
    private int m_iRound;
    private int m_iPlayerTurn;
    private int m_iMonsterTurn;
    private int m_iDmg;
    private int m_iSkillDmg;
    private int m_iFloor;
    private int m_iStage;
    private BATTLE_PROCESS m_eBattleProcess;//배틀 처리 변수
    private bool m_bBattle;//배틀 중 판단
    private bool m_bBossSkillOn;

    public int IDmg { get => m_iDmg; set => m_iDmg = value; }
    public bool bBossSkillOn { get => m_bBossSkillOn; set => m_bBossSkillOn = value; }

    // Start is called before the first frame update
    void Start()
    {
        GameObject GM = GameObject.FindWithTag("GameMgr");
        m_iFloor = GM.GetComponent<Scr_DungeonBtn>().IFloor;
        m_iStage = GM.GetComponent<Scr_DungeonBtn>().IStage;
        m_spSystem = GetComponent<System_Spawn>();
        m_Player = GameObject.Find("Player").GetComponent<Player>();
        if (m_iStage != 4)
        {
            m_Monster.gameObject.SetActive(true);
        }
        else
        {
            m_Monster = m_Boss;
            Debug.Log("몬스터 이름 : " + m_Monster.getInfo().SName);
        }
        m_iRound = 1;
        m_iDmg = -1;//전투 시작 시 UI셋팅을 위한 -1
        m_eBattleProcess = BATTLE_PROCESS.BEFORE;
        StartCoroutine(BattleProcess());
        StartCoroutine(CalculSkillDmg(m_Player));
        BtnManager.attack += AttackToHit;
        m_bBattle = false;
        m_bBossSkillOn = false;
    }

    // Update is called once per frame
    private void BattleSetting()//전투 전
    {
        if (m_iStage == 4)
            m_Monster.eType = enums.GRADE_MON.BOSS;
        m_Monster.AnimTrigger = ANIMTRIGGER.IDLE;
        //UISetting
        UISetting();
        //공격 차례 정하기
        TurnSelect();
        //공격속도 UI표시
        m_tPlayerSpeed.text = m_iPlayerTurn.ToString();
        m_tMonSpeed.text = m_iMonsterTurn.ToString();
        Vector3 PlayerSpeedpos = m_Player.transform.position;
        Vector3 MonSpeedpos = m_Monster.transform.position;

        //공격속도 플레이어 및 몬스터 머리위에 띄우기(몬스터의 크기가 다르기 때문)
        PlayerSpeedpos.y += m_Player.gameObject.GetComponent<SpriteRenderer>().sprite.bounds.size.y/2+0.5f;
        MonSpeedpos.y += m_Monster.gameObject.GetComponent<SpriteRenderer>().sprite.bounds.size.y/2+0.5f;
        m_tPlayerSpeed.transform.position = PlayerSpeedpos;
        m_tMonSpeed.transform.position = MonSpeedpos;
        m_eBattleProcess = BATTLE_PROCESS.DURING;
        m_bBattle = false;


    }
    private void Battle()//전투중
    {
        if (!m_bBattle)
        {
            m_bBattle = true;
            m_RoundCount.text = m_iRound.ToString();
            m_tPlayerSpeed.gameObject.SetActive(false);
            m_tMonSpeed.gameObject.SetActive(false);
            //공격
            if (m_iMonsterTurn < m_iPlayerTurn)//플레이어 공격
                m_BattleUI.SetActive(true);
            else if (m_iMonsterTurn > m_iPlayerTurn)//몬스터 공격
            {
                m_Player.AnimTrigger = ANIMTRIGGER.HIT;
                m_Monster.AnimTrigger = ANIMTRIGGER.ATTACK;
                m_eBattleProcess = BATTLE_PROCESS.BEFORE;
                //보스 스킬 사용 손준호
                if(m_Monster.eType == enums.GRADE_MON.BOSS)
                {
                    //스킬
                    m_Boss.bSkillOn = true;
                }

                AttackToHit();
            }
            else//공격속도 같을 시
                m_eBattleProcess = BATTLE_PROCESS.BEFORE;
        }
    }
    private void BattleAfter()//전투 후
    {
        //플레이어가 죽었는지 혹은 몬스터가 죽었는지 판단
        if (!m_Player.BLive)
        {
            m_Player.AnimTrigger = ANIMTRIGGER.DIE;
            m_iRound = 0;
        }
        if (!m_Monster.BLive)
        {
            m_Monster.AnimTrigger = ANIMTRIGGER.DIE;
            m_Monster.gameObject.SetActive(false);
            m_iRound = 0;
        }
        //경험치 분배
        //레벨업
        //퀘스트 완료 판단
        //맵으로 돌아기
    }
    private void UISetting()
    {
        //체력바 셋팅
        m_MonHp.value = m_Monster.getInfo().IMaxHp / m_Monster.getInfo().IMaxHp;
        m_PlayerHp.value = m_Player.getInfo().IMaxHp / m_Player.getInfo().IMaxHp;
        GameObject tmpMonFill, tmpPlayerFill;
        tmpMonFill = GameObject.Find("MonFill");
        tmpPlayerFill = GameObject.Find("PlayerFill");

        if (m_iDmg == -1)//초기화
        {
            tmpMonFill.SetActive(true);
            tmpPlayerFill.SetActive(true);
            m_MonHp.size = m_MonHp.value;
            m_PlayerHp.size = m_PlayerHp.value;
        }
        else
        {
            if (m_iMonsterTurn < m_iPlayerTurn)//플레이어가 공격시
            {
                m_MonHp.size -= (float)m_iDmg / m_Monster.getInfo().IMaxHp;
                if (m_MonHp.size <= 0)
                {
                    tmpMonFill.SetActive(false);
                    m_Monster.BLive = false;
                    m_eBattleProcess = BATTLE_PROCESS.END;
                }
            }
            else if(m_iMonsterTurn> m_iPlayerTurn)// 몬스터가 공격시
            {
                
                m_PlayerHp.size -= (float)m_iDmg / m_Player.getInfo().IMaxHp;

                if (m_PlayerHp.size <= 0)
                {
                    m_Player.BLive = false;
                    tmpPlayerFill.SetActive(false);
                    m_eBattleProcess = BATTLE_PROCESS.END;
                }
            }
        }
        m_iRound++;
    }
    private void TurnSelect()//턴 속도 랜덤 표시
    {
        //m_iPlayerTurn=Random.Range(m_Player.getInfo().IAtkSpeed,10);
        m_iMonsterTurn = Random.Range(m_Player.getInfo().IAtkSpeed, 10);
        m_iPlayerTurn = 0;
        //m_iMonsterTurn = 0;
        m_tPlayerSpeed.gameObject.SetActive(true);
        m_tMonSpeed.gameObject.SetActive(true);
    }
    IEnumerator BattleProcess()//배틀 과정 before->battle->before  //공격 시 체력 판단후 after로 이동 
    {
        while (true)
        {
            switch(m_eBattleProcess)
            {
                case BATTLE_PROCESS.BEFORE:
                    BattleSetting();
                    break;
                case BATTLE_PROCESS.DURING:
                    Battle();
                    break;
                case BATTLE_PROCESS.END:
                    BattleAfter();
                    break;
            }
            yield return new WaitForSeconds(2.0f);
        }
    }
    private void AttackToHit()
    {
        if (m_iMonsterTurn < m_iPlayerTurn)
            CalculDmg(m_Player, m_Monster);
        else
            CalculDmg(m_Monster, m_Player);
        m_eBattleProcess = BATTLE_PROCESS.BEFORE;
    }
    private void CalculDmg(Character Attacker, Character Hitter)
    {
        int AtkDmg = Attacker.getInfo().IAtk + Attacker.getInfo().IMatk;
        if (functions.Percentage((int)(Attacker.getInfo().FCri * 100f)))
        {
            AtkDmg += (int)(AtkDmg * Attacker.getInfo().FCriDmg);
        }
        else if(m_Player.bDeeffect)
        {
            AtkDmg += m_Boss.iSkillDmg;
        }
        int DefNum = Hitter.getInfo().IDef;
        m_iDmg = AtkDmg - DefNum;

        Hitter.getInfo().ICurrentHp -= m_iDmg;
        Debug.Log("입은 대미지 : " + m_iDmg);
    }

    IEnumerator CalculSkillDmg(Character Hitter)//스킬 대미지 계산
    {
        while (true)
        {
            m_iSkillDmg = m_Boss.iSkillDmg;

            if (m_bBossSkillOn)
            {
                Hitter.getInfo().ICurrentHp -= m_iSkillDmg;
                Debug.Log("스킬 대미지 : " + m_iSkillDmg);
                m_PlayerHp.size -= (float)m_iSkillDmg / m_Player.getInfo().IMaxHp;

                m_tBossSKill.gameObject.SetActive(true);
                m_tBossSKill.text = m_Boss.sBossSkill;
                m_bBossSkillOn = false;
            }

            yield return new WaitForSeconds(0.1f);
        }
    }
}