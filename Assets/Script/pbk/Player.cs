using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.IO; // 파일 입출력
using System.Runtime.Serialization.Formatters.Binary; //바이너리 파일 포멧
using DataInfo;
using Structs;
using enums;

public class Player : Character
{
    // Start is called before the first frame update
    private tagStat m_Stat;
    [SerializeField]
    private Transform tr;

    private int m_iMoney;
    private int m_iExp;
    public ref tagStat getStat() { return ref m_Stat; }
    private bool m_bButtonClick;//버튼 클릭했는지 안했는지 판단
    private bool m_bDeeffect;
   
    public int IMoney { get => m_iMoney; set => m_iMoney = value; }
    public int IExp { get => m_iExp; set => m_iExp = value; }
    public bool BButtonClick { get => m_bButtonClick; set => m_bButtonClick = value; }
    public bool bDeeffect { get => m_bDeeffect; set => m_bDeeffect = value; }

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
        BLive = true;
        m_Animator = GetComponent<Animator>();//플레이어 Animator 셋팅
        m_sprRender = GetComponent<SpriteRenderer>();//플레이어 SpriteRenderer셋팅
        m_vfirstZone = gameObject.transform.localPosition;
        tr = m_Instance.GetComponent<Transform>();
        StartCoroutine(this.FSM());
       
    }
    // Update is called once per frame
    void Update()
    {
        m_Scene = SceneManager.GetActiveScene();
        switch (m_Scene.name)
        {
            case "Lobby":
                m_sprRender.enabled = false;
                break;
            case "Duengeon":
                m_sprRender.enabled = true;
                break;
        }
    }
    private void statSetting()
    {
        m_Stat.IPow = 1500;
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
    private static Player m_Instance;
    private Scene m_Scene;
}
