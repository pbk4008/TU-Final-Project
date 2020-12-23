using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.IO; // 파일 입출력
using System.Runtime.Serialization.Formatters.Binary; //바이너리 파일 포멧
using DataInfo;
using Structs;

public class Player : Character
{
    
    // Start is called before the first frame update
    [SerializeField]
    private tagStat m_Stat = new tagStat();
    [SerializeField]

    private int m_iMoney;
    private int m_iExp;
    public ref tagStat getStat() { return ref m_Stat; }
    public tagStat Stat { get => m_Stat;}
    public tagInfo Info { get => m_Info;}

    public int IMoney { get => m_iMoney; set => m_iMoney = value; }
    public int IExp { get => m_iExp; set => m_iExp = value; }

    private void Awake() //싱글톤 DontDestroy시 원래 씬으로 돌아왔을때 오브젝트 중복 피하기
    {
        if(m_Instance != null)
        {
            Destroy(gameObject);
            return;
        }
        m_Instance = this;
        DontDestroyOnLoad(gameObject);
    }

    void Start()
    {
        tagSetting("곰돌이", 1, 3, 3, 50, 1, 5, 1.1f);//플레이어 셋팅
        statSetting();
        m_AnimTrigger = enums.ANIMTRIGGER.IDLE;
        m_iMoney = 10000;
        BLive = true;
        m_Animator = GetComponent<Animator>();//플레이어 Animator 셋팅
        m_sprRender = GetComponent<SpriteRenderer>();//플레이어 SpriteRenderer셋팅
        StartCoroutine(FSM());
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
        m_Stat.IPow = 5;
        m_Stat.IDex = 5;
        m_Stat.IInt = 5;
        m_Stat.IStat = 3;
    }

    //손준호 작업
    private static Player m_Instance;
    private Scene m_Scene;

}