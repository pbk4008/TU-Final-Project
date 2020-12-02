using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class System_LevelUp : MonoBehaviour//LevelUp 및 스텟 관련 클래스
{
    private Player m_Player;
    void Start()
    {
        m_Player = GameObject.FindWithTag("Player").GetComponent<Player>();
    }
    public void CalculStat()//스텟 계산
    {
<<<<<<< HEAD
       
        m_Player.Info.setMaxHp(m_Player.Stat.IPow * 3 + 50);
        m_Player.Info.setCurrentHp(m_Player.Info.IMaxHp);
        m_Player.Info.setAtkSpeed(m_Player.Stat.IDex / 15 + 1);
        if (m_Player.Info.IAtkSpeed >= 10)
            m_Player.Info.setAtkSpeed(10);
        m_Player.Info.setAtk(m_Player.Stat.IPow/2+3);
        m_Player.Info.setMatk(m_Player.Stat.IInt+3);
        m_Player.Info.setCriDmg(m_Player.Stat.IDex / 3 * 0.02f + 1.1f);
=======
        m_Player.getInfo().setMaxHp(ref m_Player.getInfo(),m_Player.Stat.IPow * 3 + 50);
        m_Player.getInfo().setCurrentHp(ref m_Player.getInfo(), m_Player.getInfo().IMaxHp);
        m_Player.getInfo().setAtkSpeed(ref m_Player.getInfo(), m_Player.Stat.IDex / 15 + 1);
        if (m_Player.getInfo().IAtkSpeed >= 10)
            m_Player.getInfo().setAtkSpeed(ref m_Player.getInfo(),10);
        m_Player.getInfo().setAtk(ref m_Player.getInfo(),m_Player.Stat.IPow/2+3);
        m_Player.getInfo().setMatk(ref m_Player.getInfo(),m_Player.Stat.IInt+3);
        m_Player.getInfo().setCriDmg(ref m_Player.getInfo(),m_Player.Stat.IDex / 3 * 0.02f + 1.1f);
>>>>>>> feature/monster
        //argInfo.IDef = 방어구
    }
    void Update()
    {
        CalculStat();
    }
}
