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
    private System_Spawn m_spSystem;
    private BattleManager m_BattleMgr;
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
    private int m_iFloor;
    private int m_iStage;
    private BATTLE_PROCESS m_eBattleProcess;//배틀 처리 변수
    private bool m_bBattle;//배틀 시작 판단
    private bool m_bRunStage;

    System_PlayerSkill m_SPB;
    [SerializeField]
    private Boss m_Boss;// 보스 정보 가져오기 - 손준호
    [SerializeField]
    private Text m_tBossSKill; // 보스 스킬 UI 정보 가져오기 - 손준호
    [SerializeField]
    private Text m_tMonDamage; //몬스터가 받는 대미지
    [SerializeField]
    private Text m_tPlayerDamage; //플레이어가 받는 대미지

    private int m_iSkillDmg; // 스킬 대미지 - 손준호
    private bool m_bBossSkillOn; // 보스 스킬 사용하기 - 손준호
    private bool m_bPlayerSkillOn; // 보스 스킬 사용하기 - 손준호
    private int m_iCDamage; //지속딜

    public int IDmg { get => m_iDmg; set => m_iDmg = value; }
    public bool bBossSkillOn { get => m_bBossSkillOn; set => m_bBossSkillOn = value; }
    public bool bPlayerSkillOn { get => m_bPlayerSkillOn; set => m_bPlayerSkillOn = value; }
    public BATTLE_PROCESS EBattleProcess { get => m_eBattleProcess; set => m_eBattleProcess = value; }
    public bool BBattle { get => m_bBattle; set => m_bBattle = value; }
    public BATTLE_PROCESS eBattleProcess { get => m_eBattleProcess; set => m_eBattleProcess = value; }
    public int iRound { get => m_iRound; set => m_iRound = value; }

    private GameObject m_MonFill, m_PlayerFill, m_ExpFill;//체력바 및 Exp바 채우기변수

    // Start is called before the first frame update
    public void Start()
    {
        m_SPB = GameObject.Find("System").GetComponent<System_PlayerSkill>();
        GameObject GM = GameObject.FindWithTag("GameMgr");
        m_iFloor = GM.GetComponent<Scr_DungeonBtn>().IFloor;
        m_iStage = GM.GetComponent<Scr_DungeonBtn>().IStage;
        m_BattleMgr = gameObject.GetComponent<BattleManager>();
        m_spSystem = GetComponent<System_Spawn>();
        m_Player = GameObject.Find("Player").GetComponent<Player>();
        m_iRound = 2; 
        m_iDmg = -1;//전투 시작 시 UI셋팅을 위한 -1
        m_eBattleProcess = BATTLE_PROCESS.BEFORE;
        m_MonFill = GameObject.Find("MonFill");
        m_PlayerFill = GameObject.Find("PlayerFill");
        m_ExpFill = GameObject.Find("ExpFill");
        BtnManager.attack += AttackToHit;
        BtnManager.run += Run;
        m_bBossSkillOn = false; //보스 스킬 사용하기 - 손준호
        m_bBattle = false;
        StartCoroutine(BattleProcess());
        StartCoroutine(CalculSkillDmg(m_Player)); //스킬 대미지 계산하는 코루틴 시작 
    }
    // Update is called once per frame
    private void BattleSetting()//전투 전
    {
        if ((m_iFloor == 0 || m_iFloor == 1) && m_iStage != 4) //보스와 몬스터 구분하기 -  손준호
            m_Monster.gameObject.SetActive(true);
        else if ((m_iFloor == 2 || m_iFloor == 3) && m_iStage != 6)
            m_Monster.gameObject.SetActive(true);
        else if ((m_iFloor == 4 || m_iFloor == 5) && m_iStage != 9)
            m_Monster.gameObject.SetActive(true);
        else
        {
            m_Monster = m_Boss; //몬스터를 보스로 치환 - 손준호
            Debug.Log("몬스터 이름 : " + m_Monster.getInfo().SName);
            m_Monster.EType = enums.GRADE_MON.BOSS; //몬스터 타임을 보스로 변경 - 손준호
        }
        m_tMonDamage.gameObject.SetActive(false);
        m_tPlayerDamage.gameObject.SetActive(false);
        m_Boss.bMinusTime = true;
        m_Boss.bSkillOn = false;
        m_Monster.AnimTrigger = ANIMTRIGGER.IDLE;
        m_Player.AnimTrigger = ANIMTRIGGER.IDLE;
        System_PlayerSkill SPB = GameObject.Find("System").GetComponent<System_PlayerSkill>();
        SPB.MinusPlayerSkill();
        //UISetting
        UISetting();
        if(!m_Monster.BLive||!m_Player.BLive)
            return;
        
        //공격 차례 정하기
        TurnSelect();
        //공격속도 UI표시
        m_tPlayerSpeed.text = m_iPlayerTurn.ToString();
        m_tMonSpeed.text = m_iMonsterTurn.ToString();
        Vector3 PlayerSpeedpos = m_Player.VfirstZone;
        Vector3 MonSpeedpos = m_Monster.VfirstZone;
        m_tMonDamage.text = m_tMonDamage.ToString();      //몬스터가 받는 대미지
        m_tPlayerDamage.text = m_tPlayerDamage.ToString(); //플레이어가 받는 대미지

    //공격속도 플레이어 및 몬스터 머리위에 띄우기(몬스터의 크기가 다르기 때문)
        PlayerSpeedpos.y += functions.CalculSpriteVerticalSize(m_Player.gameObject)/2 + 0.5f;
        MonSpeedpos.y += functions.CalculSpriteVerticalSize(m_Monster.gameObject)/2+0.5f;
        m_tPlayerSpeed.transform.position = PlayerSpeedpos;
        m_tMonSpeed.transform.position = MonSpeedpos;
        m_tPlayerDamage.transform.position = PlayerSpeedpos;
        m_tMonDamage.transform.position = MonSpeedpos;
        m_eBattleProcess = BATTLE_PROCESS.DURING;
    }
    private void Battle()//전투중
    {
        for (int i = 0; i < 3; i++)
            m_Boss.SetDmg(i, 0);
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
            //보스 스킬 사용 - 손준호
            if (m_Monster.EType == enums.GRADE_MON.BOSS)
            {
                //스킬 사용
                m_Boss.bSkillOn = true;
            }
            AttackToHit();
        }
        else//공격속도 같을 시
            m_eBattleProcess = BATTLE_PROCESS.BEFORE;
    }
    private void BattleAfter()//전투 후
    {
        //플레이어가 죽었는지 혹은 몬스터가 죽었는지 판단
        if (!m_Player.BLive)//플레이어 죽음
        {
            m_Player.AnimTrigger = ANIMTRIGGER.DIE;
        }
        if (!m_Monster.BLive)//몬스터 죽음
        {
            m_Monster.AnimTrigger = ANIMTRIGGER.DIE;
            m_Player.AnimTrigger = ANIMTRIGGER.WIN;
            //경험치 분배
            plusExp();
            //아이템 생성
            m_Monster.CreateItem();
            m_Monster.gameObject.SetActive(false);
        }
        m_bBattle = false;
        m_BattleMgr.BRoundClear = true;
        BattleReset();
        //플레이어 버프 효과 해제
        m_SPB.SkillReset();
        Debug.Log(m_eBattleProcess);
        //퀘스트 완료 판단
        Scr_DungeonBtn GM = GameObject.FindWithTag("GameMgr").GetComponent<Scr_DungeonBtn>();
        if (m_Monster.EType != GRADE_MON.BOSS)
            GM.PlusQuestMonster();
        else
            GM.PlusQuestBoss();
        //아이템 얻게 되면 주석 풀기
        //GM.PlusQuestEtcItem();
        GM.RewardQuest();
        GM.SettingQuest();
        //맵으로 돌아기
    }
    private void BattleReset()
    {
        m_iRound = 0;
        m_RoundCount.text = m_iRound.ToString();
        m_eBattleProcess = BATTLE_PROCESS.BEFORE;
        m_iDmg = -1;
    }
    public void UIInitialize()
    {
        m_MonImg.sprite = Resources.Load<Sprite>("Monster/head/monster "+ m_Monster.IMonNum+" head");
        m_MonImg.color = new Color(255, 255, 255, 255);
        m_MonHp.value = m_Monster.getInfo().ICurrentHp / m_Monster.getInfo().IMaxHp;
        m_PlayerHp.value = m_Player.getInfo().ICurrentHp / m_Player.getInfo().IMaxHp;
        m_PlayerExp.value = 1;

        m_MonFill.SetActive(true);
        m_PlayerFill.SetActive(true);

        if (m_Player.FExp != 0)
        {
            m_ExpFill.SetActive(true);
            m_PlayerExp.size = m_Player.FExp / 100.0f;
        }
        else
        {
            m_ExpFill.SetActive(false);
            m_PlayerExp.size = 0;
        }
        m_MonHp.size = m_MonHp.value;
        m_PlayerHp.size = m_PlayerHp.value;
    }
    private void UISetting()
    {
        //체력바 셋팅
        if (m_iDmg == -1)
            UIInitialize();
        else
        {
            if (m_iMonsterTurn < m_iPlayerTurn)//플레이어가 공격시
            {
                if (!m_Player.bStun)
                    m_MonHp.size -= (float)m_iDmg / m_Monster.getInfo().IMaxHp;
                if (m_MonHp.size <= 0)
                {
                    if (m_Monster.EType != GRADE_MON.BOSS)
                    {
                        m_MonFill.SetActive(false);
                        m_Monster.BLive = false;
                        m_eBattleProcess = BATTLE_PROCESS.END;
                    }
                    else
                    {
                        if (m_Boss.ilives <= 0)
                        {
                            m_MonFill.SetActive(false);
                            m_Monster.BLive = false;
                            m_eBattleProcess = BATTLE_PROCESS.END;
                        }
                        else
                        {
                            Debug.Log("부활");
                            m_Monster.getInfo().setCurrentHp(ref m_Monster.getInfo(), m_Monster.getInfo().IMaxHp);
                            m_eBattleProcess = BATTLE_PROCESS.BEFORE;
                            m_MonHp.size = m_Monster.getInfo().ICurrentHp;
                            m_Boss.ilives--;
                        }
                    }
                }
            }
            else if (m_iMonsterTurn > m_iPlayerTurn)// 몬스터가 공격시
            {
                m_PlayerHp.size -= (float)m_iDmg / m_Player.getInfo().IMaxHp;

                if (m_PlayerHp.size <= 0)
                {
                    m_Player.BLive = false;
                    m_PlayerFill.SetActive(false);
                    m_eBattleProcess = BATTLE_PROCESS.END;
                }
            }
        }
        m_iRound++;
    }
    private void TurnSelect()//턴 속도 랜덤 표시
    {
        if (m_Boss.BossSkillUI(0) == "슬로우")
            m_iPlayerTurn =Random.Range(m_Player.getInfo().IAtkSpeed - 2,10);
        else if (m_Boss.BossSkillUI(0) == "빙결")
            m_iPlayerTurn = Random.Range(m_Player.getInfo().IAtkSpeed - 3, 10);
        else
            m_iPlayerTurn = Random.Range(m_Player.getInfo().IAtkSpeed, 10);
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
            yield return new WaitUntil(() => m_bBattle);
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
        {
            CalculDmg(m_Player, m_Monster);
            m_Monster.AnimTrigger = ANIMTRIGGER.HIT;
        }
        else
            CalculDmg(m_Monster, m_Player);

        if (m_Player.GetDeEffect(0) || m_Player.GetDeEffect(1) || m_Player.GetDeEffect(2))
        {
            m_iCDamage = m_Boss.GetSkillDmg(0) + m_Boss.GetSkillDmg(1) + m_Boss.GetSkillDmg(2);
            m_Player.getInfo().ICurrentHp -= m_iCDamage;
            m_tPlayerDamage.text = m_iCDamage.ToString();
            Debug.Log("[지속딜] " + m_Boss.BossSkillUI(0) + "대미지 : " + m_Boss.GetSkillDmg(0) + m_Boss.BossSkillUI(1) + "대미지 : " + m_Boss.GetSkillDmg(1) + m_Boss.BossSkillUI(2) + "대미지 : " + m_Boss.GetSkillDmg(2));
        }

        m_eBattleProcess = BATTLE_PROCESS.BEFORE;
    }
    private void CalculDmg(Character Attacker, Character Hitter)
    {
        int AtkDmg = Attacker.getInfo().IAtk + Attacker.getInfo().IMatk;
        if (functions.Percentage((int)(Attacker.getInfo().FCri * 100f)))
        {
            AtkDmg += (int)(AtkDmg * Attacker.getInfo().FCriDmg);
        }
        int DefNum = Hitter.getInfo().IDef;
        m_iDmg = AtkDmg - DefNum;

        if (m_Monster.EType == GRADE_MON.BOSS && m_iFloor == 5 && iRound % 10 == 0)
            m_iDmg = Hitter.getInfo().IMaxHp / 2;
        if (m_Monster.EType == GRADE_MON.BOSS && m_iFloor == 5 && iRound % 20 == 0)
            m_iDmg = Hitter.getInfo().IMaxHp;
        Hitter.getInfo().ICurrentHp -= m_iDmg;
        TotalDmg();
        Debug.Log(Attacker + "일반 공격 대미지 : " + m_iDmg);
    }

    IEnumerator CalculSkillDmg(Character Hitter)//스킬 대미지 계산 - 손준호
    {
        while (true)
        {
            if (m_iPlayerTurn > m_iMonsterTurn)
            {
                if (m_bPlayerSkillOn)
                {
                    m_iSkillDmg = 0;
                    m_iSkillDmg = m_SPB.iDamage; //플레이어 스킬 대미지
                    Debug.Log("플레이어 스킬 대미지 : " + m_iSkillDmg);
                    m_Monster.getInfo().ICurrentHp -= m_iSkillDmg;
                    m_MonHp.size -= (float)m_iSkillDmg / m_Monster.getInfo().IMaxHp; //체력바 계산
                    m_bPlayerSkillOn = false;
                    TotalDmg();
                }
            }
            else
            {
                m_iSkillDmg = 0;
                m_iSkillDmg = m_Boss.GetDmg(0) + m_Boss.GetDmg(1) + m_Boss.GetDmg(2); //스킬 대미지 = Boss에서 계산한 스킬 대미지
                m_tBossSKill.text = m_Boss.BossSkillUI(0) + m_Boss.BossSkillUI(1) + m_Boss.BossSkillUI(2); //보스 스킬 텍스트 UI 이름 = 보스 스킬

                if (m_bBossSkillOn) //보스 스킬이 사용되었다면
                {
                    Hitter.getInfo().ICurrentHp -= m_iSkillDmg; //플레이어 체력을 깎음
                    Debug.Log("스킬 대미지 : " + m_iSkillDmg);
                    m_PlayerHp.size -= (float)m_iSkillDmg / m_Player.getInfo().IMaxHp; //체력바 계산

                    m_tBossSKill.gameObject.SetActive(true); //보스 스킬 UI 띄우기
                    m_bBossSkillOn = false; // 보스 스킬 사용할 수 있게 하기.
                    TotalDmg();
                }
            }
            yield return new WaitForSeconds(0.1f); //0.1초마다 실행
        }
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
    }
    private void Run()
    {
        bool Runres=functions.Percentage(100);
        if(Runres)
        {
            m_Player.IRunCount++;
            if (m_Player.IRunCount == 3)
            {
                m_Player.IRunCount = 0;
                m_BattleMgr.EClear = BATTLE_CLEAR.RUN;
                return;
            }
            m_BattleMgr.BRoundClear = true;
            m_bBattle = false;
            m_Monster.gameObject.SetActive(false);
            BattleReset();
        }
    }

    private void TotalDmg()
    {
        if(m_iMonsterTurn>m_iPlayerTurn)
        {
            //플레이어가 맞는 대미지 계산
            int totalDmg = m_iDmg + m_iSkillDmg + m_iCDamage;
            m_tPlayerDamage.gameObject.SetActive(true);
            m_tPlayerDamage.text = totalDmg.ToString();
        }
        else
        {
            int totalDmg = m_iDmg + m_iSkillDmg;
            m_tMonDamage.gameObject.SetActive(true);
            m_tMonDamage.text = totalDmg.ToString();
        }
    }
}
