using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using Structs;
using enums;

public class Player : Character
{
    // Start is called before the first frame update
    [SerializeField]
    private tagStat m_Stat;
    [SerializeField]
    private Transform tr;

    private int m_iMoney;
    private int m_iExp;
    private bool m_bButtonClick;//버튼 클릭했는지 안했는지 판단
    public tagStat Stat { get => m_Stat; set => m_Stat = value; }

    public int IMoney { get => m_iMoney; set => m_iMoney = value; }
    public int IExp { get => m_iExp; set => m_iExp = value; }
    public bool BButtonClick { get => m_bButtonClick; set => m_bButtonClick = value; }

    private void Awake() //싱글톤 DontDestroy시 원래 씬으로 돌아왔을때 오브젝트 중복 피하기
    {
        if (m_Instance != null)
        {
            Destroy(gameObject);
            return;
        }
        m_Instance = this;
        DontDestroyOnLoad(m_Instance);
    }

    void Start()
    {
        m_bButtonClick = false;
        m_bLive = true;
        tagSetting("곰돌이", 1, 3, 3, 50, 1, 5, 1.1f);//플레이어 셋팅
        statSetting();
        m_AnimTrigger = ANIMTRIGGER.IDLE;
        m_iMoney = 10000;
        m_Animator = GetComponent<Animator>();//플레이어 Animator 셋팅
        m_sprRender = GetComponent<SpriteRenderer>();//플레이어 SpriteRenderer셋팅
        m_vfirstZone = gameObject.transform.localPosition;
        tr = m_Instance.GetComponent<Transform>();
        StartCoroutine(this.FSM());
       
    }
    // Update is called once per frame
    void Update()
    {
        //Debug.Log(m_AnimTrigger);
        if (bOnClick)
        {
            switch (sButtonName)
            {
                case "Btn_LevelUp":
                    IExp = 121;
                    break;
                case "Btn_Pow":
                    LevelUp(1);
                    break;
                case "Btn_Int":
                    LevelUp(2);
                    break;
                case "Btn_Dex":
                    LevelUp(3);
                    break;
            }
            bOnClick = false;
        }
        if (IExp >= 100)
            LevelUp(0);


        m_Scene = SceneManager.GetActiveScene();
        switch (m_Scene.name)
        {
            case "Lobby":
                m_sprRend.enabled = false;
                break;
            case "Duengeon":
                m_sprRend.enabled = true;
                break;
        }
    }
    private void statSetting()
    {
        m_Stat.IPow = 5;
        m_Stat.IDex = 5;
        m_Stat.IInt = 5;
        m_Stat.IStat = 3;
    }
    protected override IEnumerator FSM() 
    {
        while (true)
        {
            switch (m_AnimTrigger)
            {
                case ANIMTRIGGER.IDLE:
                    tr.position = m_vfirstZone;
                    break;
                case ANIMTRIGGER.ATTACK:
                    GameObject target = GameObject.FindWithTag("Monster");
                    tr.position = target.transform.position;
                    break;
                case ANIMTRIGGER.SKILL:
                    break;
            }
            StartCoroutine(base.FSM());
            yield return new WaitForSeconds(0.1f);
        }
    }
    //손준호 작업
    //레벨업
    private static Player m_Instance;
    private Scene m_Scene;
    [SerializeField] private SpriteRenderer m_sprRend;
    GameObject[] LevelUpMgr = new GameObject[2];

    private string m_sButtonName;
    private bool m_bOnClick;
    public string sButtonName { get => m_sButtonName; set => m_sButtonName = value; }
    public bool bOnClick { get => m_bOnClick; set => m_bOnClick = value; }
    

    private void LevelUp(int iStat)
    {
        switch (iStat)
        {
            case 0:
                IExp -= 100;
                m_Info.ILevel +=1;
                LevelUpMgr[0] = GameObject.Find("Cvs_Button");
                LevelUpMgr[0].transform.GetChild(6).gameObject.SetActive(true);
                LevelUpMgr[0].transform.GetChild(7).gameObject.SetActive(true);
                LevelUpMgr[0].transform.GetChild(8).gameObject.SetActive(true);
                LevelUpMgr[0].transform.GetChild(9).gameObject.SetActive(true);
                LevelUpMgr[1] = GameObject.Find("Cvs_LevelUpText");
                LevelUpMgr[1].transform.GetChild(0).gameObject.SetActive(true);
                break;
            case 1:
                m_Stat.IStat -= 1;
                m_Stat.IPow++;
                break;
            case 2:
                m_Stat.IStat -= 1;
                m_Stat.IInt++;
                break;
            case 3:
                m_Stat.IStat -= 1;
                m_Stat.IDex++;
                break;
        }

        LevelUpMgr[1].transform.GetChild(0).GetComponent<Text>().text = "원하는 스텟을 선택해 주세요!\n\n"
        + "남은 스텟 포인트 : " + m_Stat.IStat + "\n\n\n"
        + "현재 Pow : " + m_Stat.IPow + "                 현재 Int : " + m_Stat.IInt + "                 현재 Dex : " + m_Stat.IDex;

        if (m_Stat.IStat == 0)
        {
            m_Stat.IStat = 3;
            LevelUpMgr[0].transform.GetChild(6).gameObject.SetActive(false);
            LevelUpMgr[0].transform.GetChild(7).gameObject.SetActive(false);
            LevelUpMgr[0].transform.GetChild(8).gameObject.SetActive(false);
            LevelUpMgr[0].transform.GetChild(9).gameObject.SetActive(false);
            LevelUpMgr[1].transform.GetChild(0).gameObject.SetActive(false);
            SceneManager.LoadScene(1);
        }
    }
}
