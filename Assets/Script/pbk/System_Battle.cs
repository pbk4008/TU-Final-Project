using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Delegats;
public class System_Battle : MonoBehaviour
{
    public static event BattleHandler attack;
    public static event BattleHandler hit;

    private Player m_Player;//플레이어 정보
    private Monster m_Monster;//몬스터 정보
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
    private GameObject m_BattleUI;
    private int m_iRound;
    private int m_iPlayerTurn;
    private int m_iMonsterTurn;
    private int m_iDmg;
    private bool m_bBattleDuring;

    public int IDmg { get => m_iDmg; set => m_iDmg = value; }

    // Start is called before the first frame update
    void Start()
    {
        m_Player = GameObject.Find("Player").GetComponent<Player>();
        m_Monster = GameObject.Find("Monster").GetComponent<Monster>();
        m_spSystem = GetComponent<System_Spawn>();
        m_iRound = 0;
        m_iDmg = -1;//전투 시작 시 UI셋팅을 위한 -1
        m_bBattleDuring = false;
        StartCoroutine(BattleProcess());
        attack += AttackToHit;
        attack += UISetting;
    }

    // Update is called once per frame
    private void BattleBefore()//전투 전
    {
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
    }
    private void Battle()//전투중
    {
        m_bBattleDuring = true;
        m_tPlayerSpeed.gameObject.SetActive(false);
        m_tMonSpeed.gameObject.SetActive(false);
        //공격
        if (m_iMonsterTurn < m_iPlayerTurn)//플레이어 공격
        {
            
            m_BattleUI.SetActive(true);
            if (m_Player.BButtonClick)
            {
                attack();
                m_bBattleDuring = false;
            }
            Debug.Log(m_bBattleDuring);
        }
        else if (m_iMonsterTurn > m_iPlayerTurn)//몬스터 공격
        {
            hit();
            m_bBattleDuring = false;
        }
        else
        {
            TurnSelect();
            m_bBattleDuring = false;
        }
        m_iRound++;
    }
    private void BattleAfter()//전투 후
    {
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

        if (m_iDmg == -1)
        {
            m_MonHp.size = m_MonHp.value;
            m_PlayerHp.size = m_PlayerHp.value;
        }
        else
        {
            if (m_iMonsterTurn < m_iPlayerTurn)
                m_MonHp.size -= m_iDmg / m_MonHp.value;
            else
                m_PlayerHp.size -= m_iDmg / m_MonHp.value;
        }
        //라운드 셋팅
        m_RoundCount.text = m_iRound.ToString();

    }
    private void TurnSelect()
    {
        m_iPlayerTurn=Random.Range(m_Player.getInfo().IAtkSpeed,10);
        //m_iMonsterTurn = Random.Range(m_Player.getInfo().IAtkSpeed, 10);
        m_iMonsterTurn = 0;
        m_tPlayerSpeed.gameObject.SetActive(true);
        m_tMonSpeed.gameObject.SetActive(true);
        
    }
    IEnumerator BattleProcess()
    {
        while (true)
        {
            if(!m_bBattleDuring)
                BattleBefore();
            yield return new WaitForSeconds(2.0f);
            Battle();
            
            yield return new WaitWhile(()=>!m_bBattleDuring);
            BattleAfter();
        }
        

    }
    private void AttackToHit()
    {
        if (m_iMonsterTurn < m_iPlayerTurn)
            CalculDmg(m_Player, m_Monster);
        else
            CalculDmg(m_Monster, m_Player);
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
    }
}
