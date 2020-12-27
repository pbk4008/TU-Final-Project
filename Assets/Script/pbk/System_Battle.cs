using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Delegats;
using enums;
public class System_Battle : MonoBehaviour
{

    private Player m_Player;//플레이어 정보
    private Monster m_Monster;//몬스터 정보
    private System_Spawn m_spSystem;
    [SerializeField]
    private Scrollbar m_MonHp;//몬스터 체력바
    [SerializeField]
    private Scrollbar m_PlayerHp;//플레이어 체력바
    [SerializeField]
    private Scrollbar m_PlayerExp;//플레이어 경험치바
    [SerializeField]
    private Image m_MonImg;//몬스터 이미지
    [SerializeField]
    private Text m_RoundCount;//라운드 카운트 Text
    [SerializeField]
    private Text m_tPlayerSpeed;
    [SerializeField]
    private Text m_tMonSpeed;
    [SerializeField]
    private GameObject m_BattleUI;
    private int m_iRound;
    private int m_iPlayerTurn;
    private int m_iMonsterTurn;
    private int m_iDmg;
    private BATTLE_PROCESS m_eBattleProcess;//배틀 처리 변수
    private bool m_bBattle;//배틀 중 판단

    public int IDmg { get => m_iDmg; set => m_iDmg = value; }
    public bool BBattle { get => m_bBattle; set => m_bBattle = value; }

    // Start is called before the first frame update
    void Start()
    {
        m_Player = GameObject.Find("Player").GetComponent<Player>();
        m_Monster = GameObject.Find("Monster").GetComponent<Monster>();
        m_spSystem = GetComponent<System_Spawn>();
        m_iRound = 1;
        m_iDmg = -1;//전투 시작 시 UI셋팅을 위한 -1
        m_eBattleProcess = BATTLE_PROCESS.BEFORE;
        StartCoroutine(BattleProcess());
        BtnManager.attack += AttackToHit;
        m_bBattle = false;
    }

    // Update is called once per frame
    private void BattleSetting()//전투 전
    {
        m_Monster.AnimTrigger = ANIMTRIGGER.IDLE;
        //UISetting
        UISetting();
        if(!m_Monster.BLive||!m_Player.BLive)
            return;
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
                AttackToHit();
            }
            else//공격속도 같을 시
                m_eBattleProcess = BATTLE_PROCESS.BEFORE;
        }
    }
    private void BattleAfter()//전투 후
    {
        Debug.Log(m_Monster.BLive);
        Debug.Log(m_Player.BLive);
        //StopCoroutine(BattleProcess());
        //플레이어가 죽었는지 혹은 몬스터가 죽었는지 판단
        if (!m_Player.BLive)//플레이어 죽음
        {
            Debug.Log("확인");
            m_Player.AnimTrigger = ANIMTRIGGER.DIE;
            m_iRound = 0;
        }
        if (!m_Monster.BLive)//몬스터 죽음
        {
            m_Monster.AnimTrigger = ANIMTRIGGER.DIE;
            m_Player.AnimTrigger = ANIMTRIGGER.WIN;
            //경험치 분배
            plusExp();
            //레벨업 판단
            m_Monster.gameObject.SetActive(false);
            m_iRound = 0;
        }
        //퀘스트 완료 판단
        //맵으로 돌아기
    }
    private void UISetting()
    {
        //체력바 셋팅
        m_MonHp.value = m_Monster.getInfo().IMaxHp / m_Monster.getInfo().IMaxHp;
        m_PlayerHp.value = m_Player.getInfo().IMaxHp / m_Player.getInfo().IMaxHp;
        m_PlayerExp.value = 1;
        GameObject tmpMonFill, tmpPlayerFill, tmpExpFill;
        tmpMonFill = GameObject.Find("MonFill");
        tmpPlayerFill = GameObject.Find("PlayerFill");
        if (m_iDmg == -1)//초기화
        {
            tmpExpFill = GameObject.Find("ExpFill");
            tmpMonFill.SetActive(true);
            tmpPlayerFill.SetActive(true);
            tmpExpFill.SetActive(true);
            if (m_Player.FExp != 0)
            {
                m_PlayerExp.size = m_Player.FExp / 100.0f;
            }
            else
            {
                tmpExpFill.SetActive(false);
                m_PlayerExp.size = 0;
            }
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
        m_iPlayerTurn=Random.Range(m_Player.getInfo().IAtkSpeed,10);
        m_iMonsterTurn = Random.Range(m_Player.getInfo().IAtkSpeed, 10);
        //m_iPlayerTurn = 0;
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
        if(functions.Percentage((int)(Attacker.getInfo().FCri*100f)))
        {
            AtkDmg += (int)(AtkDmg * Attacker.getInfo().FCriDmg);
        }
        int DefNum = Hitter.getInfo().IDef;
        m_iDmg = AtkDmg - DefNum;
        Hitter.getInfo().ICurrentHp -= m_iDmg;
    }
    private void plusExp()
    {
        float tmpExp = 0;
        if (m_Monster.EType == GRADE_MON.RARE)
            tmpExp = 8;
        else
            tmpExp = 5;
        if (Mathf.Abs(m_Monster.getInfo().ILevel - m_Player.getInfo().ILevel) > 10)
            tmpExp *= 0.1f;
        else if (Mathf.Abs(m_Monster.getInfo().ILevel - m_Player.getInfo().ILevel) > 5)
            tmpExp *= 0.5f;
        m_Player.FExp += tmpExp;
        m_PlayerExp.size = m_Player.FExp / 100.0f;
        Debug.Log(tmpExp);
        Debug.Log(m_PlayerExp.size);
    }
}
