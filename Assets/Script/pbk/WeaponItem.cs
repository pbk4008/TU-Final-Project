using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using enums;
using Delegats;

public class WeaponItem : Item
{
    private int m_iReinforce;
    Player m_Player;
    int m_iPlusAtk;
    int m_iPlusMatk;
    int m_iPlusDef;
    int m_iPlusMaxHp;
    int m_iPlusPow;
    int m_iPlusDex;
    int m_iPlusInt;
    public int IReinforce { get => m_iReinforce; set => m_iReinforce = value; }

    // Start is called before the first frame update 
    void Start()
    {
        m_iReinforce = 0;
        m_iPlusAtk=0;
        m_iPlusMatk=0;
        m_iPlusDef=0;
        m_iPlusMaxHp = 0;
        m_iPlusPow=0;
        m_iPlusDex=0;
        m_iPlusInt=0;
    }
    public void EffectSetting()//기타아이템 셋팅(by.박병규 함수 변경)
    {
        m_Player = GameObject.Find("Player").GetComponent<Player>();
        int iWeaponAtk = 0, iWeaponMatk = 0, iWeaponDef = 0;
        switch (m_eEquipType)
        {
            case EQUIP_TYPE.SWORD:
                switch(m_eGrade)
                {
                    case ITEM_GRADE.BASIC:
                        iWeaponAtk = 5+m_iReinforce * 1;
                        iWeaponMatk = 3+m_iReinforce * 1;
                        break;
                    case ITEM_GRADE.NORMAL:
                        iWeaponAtk = 11+ m_iReinforce * 2;
                        iWeaponMatk = 6+ m_iReinforce * 2;
                        break;
                    case ITEM_GRADE.SPECIAL: //검의 Special 등급
                        iWeaponAtk = 25+ m_iReinforce * 3;
                        iWeaponMatk = 20 + m_iReinforce * 3;
                        m_iPlusDex = 6 + m_iReinforce * 1;
                        m_iPlusInt = 6 + m_iReinforce * 1;
                        m_iPlusPow = 6 + m_iReinforce * 1;
                        break;
                    case ITEM_GRADE.UNIQUE:
                        iWeaponAtk = 37+m_iReinforce * 4;
                        iWeaponMatk = 32+m_iReinforce * 4;
                        m_iPlusDex = (int)(m_Player.getStat().IDex * 0.1) + (int)(m_iReinforce * 0.02f);
                        m_iPlusInt = (int)(m_Player.getStat().IInt * 0.1) + (int)(m_iReinforce * 0.02f);
                        m_iPlusPow = (int)(m_Player.getStat().IPow * 0.1) + (int)(m_iReinforce * 0.02f);                       
                        break;
                    case ITEM_GRADE.LEGENDARY:
                        iWeaponAtk = 48+m_iReinforce * 5;
                        iWeaponMatk = 43+m_iReinforce * 5;

                        m_iPlusDex =(int)(m_Player.getStat().IDex * 0.1) + (int)(m_iReinforce * 0.04f);
                        m_iPlusInt =(int)(m_Player.getStat().IInt * 0.1) + (int)(m_iReinforce * 0.04f);
                        m_iPlusPow =(int)(m_Player.getStat().IPow * 0.1) + (int)(m_iReinforce * 0.04f);                    
                        break;
                }
                break;
            case EQUIP_TYPE.KNUKLE:
                switch (m_eGrade)
                {
                    case ITEM_GRADE.NORMAL:
                        iWeaponAtk = 6+m_iReinforce*2;
                        iWeaponMatk = 11+m_iReinforce * 2; ;
                        break;
                    case ITEM_GRADE.SPECIAL:  //너클의 Special 등급
                        iWeaponAtk = 20+m_iReinforce * 3;
                        iWeaponMatk = 25+m_iReinforce * 3;
                        iWeaponDef = 5+ m_iReinforce * 1;

                        m_iPlusDex =(int)(m_Player.getStat().IDex + 3)+m_iReinforce * 1;
                        m_iPlusInt =(int)(m_Player.getStat().IInt + 3)+m_iReinforce *1;
                        m_iPlusPow =(int)(m_Player.getStat().IPow + 3) + m_iReinforce * 1;

                        break;
                    case ITEM_GRADE.UNIQUE:
                        iWeaponAtk = 32 + m_iReinforce * 4;
                        iWeaponMatk = 37 + m_iReinforce * 4;
                        iWeaponDef = 10 + m_iReinforce * 1;

                        m_iPlusDex =(int)(m_Player.getStat().IDex * 0.05) + (int)(m_iReinforce *0.02f);
                        m_iPlusInt =(int)(m_Player.getStat().IInt * 0.05) + (int)(m_iReinforce * 0.02f);
                        m_iPlusPow =(int)(m_Player.getStat().IPow * 0.05) + (int)(m_iReinforce * 0.02f);

                        break;
                    case ITEM_GRADE.LEGENDARY:
                        iWeaponAtk = 43+m_iReinforce * 5;
                        iWeaponMatk = 48+m_iReinforce * 5;
                        iWeaponDef = 10 + m_iReinforce * 1;
                        m_iPlusDex = (int)(m_Player.getStat().IDex * 0.05) + (int)(m_iReinforce * 0.04f);
                        m_iPlusInt = (int)(m_Player.getStat().IInt * 0.05) + (int)(m_iReinforce * 0.04f);
                        m_iPlusPow = (int)(m_Player.getStat().IPow * 0.05) + (int)(m_iReinforce * 0.04f);

                        break;
                }
                break;
            case EQUIP_TYPE.HEAD:
                switch (m_eGrade)
                {
                    case ITEM_GRADE.BASIC:
                        iWeaponDef = 5 + m_iReinforce*1;
                        break;
                    case ITEM_GRADE.NORMAL:
                        iWeaponDef = 8 + m_iReinforce * 2;
                        break;
                    case ITEM_GRADE.SPECIAL: //머리의 Special 등급
                        iWeaponDef = 13 + m_iReinforce * 3;
                        iWeaponDef += 5+m_iReinforce * 1;
                        break;
                    case ITEM_GRADE.UNIQUE:
                        iWeaponDef = 19 + m_iReinforce * 4;
                        iWeaponDef += (int)(m_Player.getInfo().IDef*(0.05f+ (int)(m_iReinforce * 0.02f)));
                        break;
                    case ITEM_GRADE.LEGENDARY:
                        iWeaponDef = 24+m_iReinforce * 5;
                        iWeaponDef += (int)(m_Player.getInfo().IDef * (0.05f+(int)(m_iReinforce * 0.04f)));      
                        break;
                }
                break;
            case EQUIP_TYPE.BODY:
                switch (m_eGrade)
                {
                    case ITEM_GRADE.BASIC:
                        iWeaponDef = 7+ m_iReinforce * 1;
                        break;
                    case ITEM_GRADE.NORMAL:
                        iWeaponDef = 13+m_iReinforce * 2;
                        break;
                    case ITEM_GRADE.SPECIAL: //몸통의 Special 등급
                        iWeaponDef = 18+ m_iReinforce * 3;
                        m_iPlusMaxHp = 30 + m_iReinforce * 5;
          
                        break;
                    case ITEM_GRADE.UNIQUE:
                        iWeaponDef = 23 + m_iReinforce * 4;
                        m_iPlusMaxHp =(int)(m_Player.getInfo().IMaxHp * (0.05f + m_iReinforce * 0.02f));           
                        break;
                    case ITEM_GRADE.LEGENDARY:
                        iWeaponDef = 29 + m_iReinforce * 5;
                        m_iPlusMaxHp =(int)(m_Player.getInfo().IMaxHp * (0.05f + m_iReinforce * 0.04f));
         
                        break;
                }
                break;
            case EQUIP_TYPE.FOOT:
                switch (m_eGrade)
                {
                    case ITEM_GRADE.BASIC:
                        iWeaponDef = 3 + m_iReinforce * 1;
                        break;
                    case ITEM_GRADE.NORMAL:
                        iWeaponDef = 6 + m_iReinforce * 2;
                        break;
                    case ITEM_GRADE.SPECIAL: //머리의 Special 등급
                        iWeaponDef = 10 + m_iReinforce * 3;

                        m_iPlusDex = (int)(m_Player.getStat().IDex + 6) + m_iReinforce * 1;
                        m_iPlusInt = (int)(m_Player.getStat().IInt + 6) + m_iReinforce * 1;
                        m_iPlusPow = (int)(m_Player.getStat().IPow + 6) + m_iReinforce * 1;                    
                        break;
                    case ITEM_GRADE.UNIQUE:
                        iWeaponDef = 16 + m_iReinforce * 4;

                        m_iPlusDex = (int)(m_Player.getStat().IDex * (0.1f + m_iReinforce * 0.02f));
                        m_iPlusInt = (int)(m_Player.getStat().IInt * (0.1f + m_iReinforce * 0.02f));
                        m_iPlusPow = (int)(m_Player.getStat().IPow * (0.1f + m_iReinforce * 0.02f));                    
                        break;
                    case ITEM_GRADE.LEGENDARY:
                        iWeaponDef = 20 + m_iReinforce * 5;
                        m_iPlusDex = (int)(m_Player.getStat().IDex * (0.1f + m_iReinforce * 0.04f));
                        m_iPlusInt = (int)(m_Player.getStat().IInt * (0.1f + m_iReinforce * 0.04f));
                        m_iPlusPow = (int)(m_Player.getStat().IPow * (0.1f + m_iReinforce * 0.04f));                       
                        break;
                }
                break;
        }

        m_iPlusAtk = iWeaponAtk;
        m_iPlusMatk = iWeaponMatk;
        m_iPlusDef = iWeaponDef;
    }
    public void PlusItem()
    {
        m_Player.LevelUp_System.CalculStat();
        m_Player.getStat().setDex(ref m_Player.getStat(), m_Player.getStat().IDex+m_iPlusDex);
        m_Player.getStat().setInt(ref m_Player.getStat(), m_Player.getStat().IInt+m_iPlusInt);
        m_Player.getStat().setPow(ref m_Player.getStat(), m_Player.getStat().IPow+m_iPlusPow);
        m_Player.getInfo().setDef(ref m_Player.getInfo(), m_Player.getInfo().IDef + m_iPlusDef);
        m_Player.getInfo().setAtk(ref m_Player.getInfo(), m_Player.getInfo().IAtk + m_iPlusAtk);
        m_Player.getInfo().setMatk(ref m_Player.getInfo(), m_Player.getInfo().IMatk + m_iPlusMatk);
        m_Player.getInfo().setMaxHp(ref m_Player.getInfo(), m_Player.getInfo().IMaxHp+m_iPlusMaxHp);
    }
    public void ClearItem()
    {
        m_Player = GameObject.Find("Player").GetComponent<Player>();

        Debug.Log("빠지는 방어력 : "+m_iPlusDef);
        m_Player.getInfo().setAtk(ref m_Player.getInfo(), m_Player.getInfo().IAtk - m_iPlusAtk);
        m_Player.getInfo().setMatk(ref m_Player.getInfo(), m_Player.getInfo().IMatk - m_iPlusMatk);
        m_Player.getInfo().setDef(ref m_Player.getInfo(), m_Player.getInfo().IDef - m_iPlusDef);
        m_Player.getStat().setDex(ref m_Player.getStat(), m_Player.getStat().IDex - m_iPlusDex);
        m_Player.getStat().setInt(ref m_Player.getStat(), m_Player.getStat().IInt - m_iPlusInt);
        m_Player.getStat().setPow(ref m_Player.getStat(), m_Player.getStat().IPow - m_iPlusPow);
        m_Player.getInfo().setMaxHp(ref m_Player.getInfo(), m_Player.getInfo().IMaxHp - m_iPlusMaxHp);

        System_LevelUp PL = GameObject.Find("Player").GetComponent<System_LevelUp>();
        PL.CalculStat();
    }
}
