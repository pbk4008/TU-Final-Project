﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using Structs;
using enums;

public class Monster : Character
{
    private GRADE_MON m_eType;
    private Transform tr;
    private Sprite m_HitSprite;
    private int m_iMonNum;
    [SerializeField]
    private Boss m_Boss;
    public GRADE_MON EType { get => m_eType; set => m_eType = value; }
    public Sprite SprMain { get => m_SprMain; set => m_SprMain = value; }
    public int IMonNum { get => m_iMonNum; set => m_iMonNum = value; }

    // Start is called before the first frame update
    public void MonsterActive()
    {
        tr = GetComponent<Transform>();
        m_vfirstZone = gameObject.transform.position;
        StartCoroutine(this.FSM());
    }
    // Update is called once per frame
    public void SetInfo(int argIndex)
    {
        m_iMonNum = argIndex;
        FileStream fs = new FileStream("Monster.txt", FileMode.Open, FileAccess.Read);
        StreamReader sr = new StreamReader(fs);
        string tmpString = sr.ReadLine();
        while(tmpString!=argIndex.ToString())
        {
            tmpString = sr.ReadLine();
        }
        m_Info.SName = sr.ReadLine();
        SetTypeStatus(sr.ReadLine());
        //아이템 이름
        Debug.Log(m_SprMain);
        m_SprMain = Resources.Load<Sprite>("Monster/monster "+ argIndex);
        Debug.Log(m_SprMain);
        m_sprRender = GetComponent<SpriteRenderer>();
        m_sprRender.sprite = m_SprMain;
        m_HitSprite = Resources.Load<Sprite>("Monster/monster " + argIndex + " Hit");
    }
    private void SetTypeStatus(string argType)
    {
        switch(argType)
        {
            case "기본형\n":
                m_eType = enums.GRADE_MON.BASE;
                m_Info.IAtk += (int)(m_Info.IAtk * 0.1f);
                break;
            case "마법형\n":
                m_eType = enums.GRADE_MON.MASIC;
                m_Info.IAtk += (int)(m_Info.IMatk * 0.1f);
                break;
            case "방어형\n":
                m_eType = enums.GRADE_MON.DEF;
                m_Info.IDef += (int)(m_Info.IDef * 0.1f);
                m_Info.IMaxHp += (int)(m_Info.IMaxHp * 0.1f);
                m_Info.ICurrentHp = m_Info.IMaxHp;
                break;
            case "정예형\n":
                m_eType = enums.GRADE_MON.RARE;
                m_Info.IAtk += (int)(m_Info.IAtk * 0.1f);
                m_Info.IAtk += (int)(m_Info.IMatk * 0.1f);
                m_Info.IDef += (int)(m_Info.IDef * 0.1f);
                break;
        }
    }
    public void setStatus(int argAtk, int argHp,float argCriDmg, int argAtkSpeed)
    {
        //몬스터 기본 능력치 셋팅
        m_Info.IAtk = m_Info.IMatk = argAtk;
        m_Info.IMaxHp = argHp;
        m_Info.ICurrentHp = m_Info.IMaxHp;
        m_Info.FCriDmg = argCriDmg;
        m_Info.IAtkSpeed = argAtkSpeed;

        //레벨당 능력치 추가 셋팅
       
        m_Info.IAtk += m_Info.ILevel * 2;
        m_Info.IMatk += m_Info.ILevel * 2;

        m_Info.IDef = m_Info.IAtk / 2;
    }
    protected override IEnumerator FSM()
    {
        while (true)
        {
            switch (m_AnimTrigger)
            {
                case ANIMTRIGGER.IDLE:
                    tr.position = m_vfirstZone;
                    m_sprRender.sprite = m_SprMain;
                    break;
                case ANIMTRIGGER.ATTACK:
                    GameObject target = GameObject.FindWithTag("Player");
                    tr.position = target.transform.position;
                    break;
                case ANIMTRIGGER.HIT:
                    m_sprRender.sprite = m_HitSprite;
                    break;
                case ANIMTRIGGER.SKILL:
                    break;
            }
            yield return new WaitForSeconds(0.09f);
        }
    }
}
