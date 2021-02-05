using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using enums;

public class WeaponItem : Item
{
    private int m_iReinforce;
    Player m_Player;
    public int IReinforce { get => m_iReinforce; set => m_iReinforce = value; }

    // Start is called before the first frame update 
    void Start()
    {
        m_iReinforce = 0;
        m_Player = GameObject.Find("Player").GetComponent<Player>();
    }
    public void EffectSetting(char argGrade, EQUIP_TYPE e_Type)//기타아이템 셋팅
    {
        System_LevelUp PL = GameObject.Find("Player").GetComponent<System_LevelUp>();
        int iWeaponAtk = 0, iWeaponMatk = 0, iWeaponDef = 0;
        switch (e_Type)
        {
            case EQUIP_TYPE.SWORD:
                switch(argGrade)
                {
                    case '0':
                        iWeaponAtk = 5;
                        iWeaponMatk = 3;
                        break;
                    case '1':
                        iWeaponAtk = 11;
                        iWeaponMatk = 6;
                        break;
                    case '2': //검의 Special 등급
                        iWeaponAtk = 25;
                        iWeaponMatk = 20;
                        m_Player.getStat().setDex(ref m_Player.getStat(), m_Player.getStat().IDex + 6);
                        m_Player.getStat().setInt(ref m_Player.getStat(), m_Player.getStat().IInt + 6);
                        m_Player.getStat().setPow(ref m_Player.getStat(), m_Player.getStat().IPow + 6);
                        break;
                    case '3':
                        iWeaponAtk = 37;
                        iWeaponMatk = 32;
                        m_Player.getStat().setDex(ref m_Player.getStat(), m_Player.getStat().IDex + (int)(m_Player.getStat().IDex*0.1));
                        m_Player.getStat().setInt(ref m_Player.getStat(), m_Player.getStat().IInt + (int)(m_Player.getStat().IInt * 0.1));
                        m_Player.getStat().setPow(ref m_Player.getStat(), m_Player.getStat().IPow + (int)(m_Player.getStat().IPow * 0.1));
                        break;
                    case '4':
                        iWeaponAtk = 48;
                        iWeaponMatk = 43;
                        m_Player.getStat().setDex(ref m_Player.getStat(), m_Player.getStat().IDex + (int)(m_Player.getStat().IDex * 0.1));
                        m_Player.getStat().setInt(ref m_Player.getStat(), m_Player.getStat().IInt + (int)(m_Player.getStat().IInt * 0.1));
                        m_Player.getStat().setPow(ref m_Player.getStat(), m_Player.getStat().IPow + (int)(m_Player.getStat().IPow * 0.1));
                        break;
                }
                break;
            case EQUIP_TYPE.KNUKLE:
                switch (argGrade)
                {
                    case '1':
                        iWeaponAtk = 6;
                        iWeaponMatk = 11;
                        break;
                    case '2':  //너클의 Special 등급
                        iWeaponAtk = 20;
                        iWeaponMatk = 25;
                        iWeaponDef = 5;
                        m_Player.getStat().setDex(ref m_Player.getStat(), m_Player.getStat().IDex + (int)(m_Player.getStat().IDex + 3));
                        m_Player.getStat().setInt(ref m_Player.getStat(), m_Player.getStat().IInt + (int)(m_Player.getStat().IInt + 3));
                        m_Player.getStat().setPow(ref m_Player.getStat(), m_Player.getStat().IPow + (int)(m_Player.getStat().IPow + 3));
                        break;
                    case '3':
                        iWeaponAtk = 32;
                        iWeaponMatk = 37;
                        iWeaponDef = 10;
                        m_Player.getStat().setDex(ref m_Player.getStat(), m_Player.getStat().IDex + (int)(m_Player.getStat().IDex * 0.05));
                        m_Player.getStat().setInt(ref m_Player.getStat(), m_Player.getStat().IInt + (int)(m_Player.getStat().IInt * 0.05));
                        m_Player.getStat().setPow(ref m_Player.getStat(), m_Player.getStat().IPow + (int)(m_Player.getStat().IPow * 0.05));
                        break;
                    case '4':
                        iWeaponAtk = 43;
                        iWeaponMatk = 48;
                        m_Player.getStat().setDex(ref m_Player.getStat(), m_Player.getStat().IDex + (int)(m_Player.getStat().IDex * 0.05));
                        m_Player.getStat().setInt(ref m_Player.getStat(), m_Player.getStat().IInt + (int)(m_Player.getStat().IInt * 0.05));
                        m_Player.getStat().setPow(ref m_Player.getStat(), m_Player.getStat().IPow + (int)(m_Player.getStat().IPow * 0.05));
                        break;
                }
                break;
        }

        PL.CalculStat();
        m_Player.getInfo().setDef(ref m_Player.getInfo(), m_Player.getInfo().IDef + iWeaponDef);
        m_Player.getInfo().setAtk(ref m_Player.getInfo(), m_Player.getInfo().IAtk + iWeaponAtk);
        m_Player.getInfo().setMatk(ref m_Player.getInfo(), m_Player.getInfo().IMatk + iWeaponMatk);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
