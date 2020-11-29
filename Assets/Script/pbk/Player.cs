using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Character
{
    // Start is called before the first frame update
    [SerializeField]
    private structs.tagStat m_Stat;
    [SerializeField]
    private int m_iMoney;
    private int m_iExp;

    public structs.tagStat Stat { get => m_Stat; set => m_Stat = value; }
    public structs.tagInfo Info { get => m_Info; set => m_Info = value; }

    public int IMoney { get => m_iMoney; set => m_iMoney = value; }
   //Hp 수정하기 - 손준호
    public int IHp { get => m_Info.ICurrentHp; set => m_Info.ICurrentHp = value; }
    public int IExp { get => m_iExp; set => m_iExp = value; }

    void Start()
    {
        
        tagSetting("곰돌이", 1, 3, 3, 50, 1, 5, 1.1f);//플레이어 셋팅
        statSetting();
        m_AnimTrigger = enums.ANIMTRIGGER.IDLE;
        m_iMoney = 10000;
        m_Animator = GetComponent<Animator>();//플레이어 Animator 셋팅
        m_sprRender = GetComponent<SpriteRenderer>();//플레이어 SpriteRenderer셋팅
        StartCoroutine(FSM());
    }
    // Update is called once per frame
    void Update()
    {
        
    }
    private void statSetting()
    {
        m_Stat.IPow = 5;
        m_Stat.IDex = 5;
        m_Stat.IInt = 5;
        m_Stat.IStat = 0;
    }
}
