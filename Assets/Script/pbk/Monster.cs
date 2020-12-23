using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using enums;

public class Monster : Character
{
    private GRADE_MON m_eType;
    private Transform tr;
    // Start is called before the first frame update
    void Start()
    {
        m_bLive = false;
        tr = GetComponent<Transform>();
        m_vfirstZone = gameObject.transform.position;
        StartCoroutine(this.FSM());
    }

    // Update is called once per frame
    void Update()
    {

    }
    public void SetInfo(int argIndex)
    {
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
                    break;
                case ANIMTRIGGER.ATTACK:
                    GameObject target = GameObject.FindWithTag("Player");
                    tr.position = target.transform.position;
                    break;
                case ANIMTRIGGER.SKILL:
                    break;
            }
            //StartCoroutine(base.FSM());
            yield return new WaitForSeconds(0.1f);
        }
    }
}
