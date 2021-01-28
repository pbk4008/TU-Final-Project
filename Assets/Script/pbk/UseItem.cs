using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using enums;
using UnityEngine.SceneManagement;

public class UseItem : Item
{
    System_Battle m_SystemBattle;
    Player m_Player;
    Scene m_Scene;
    void Start()
    {
        if (m_Scene.name == "Duengeon")
            m_SystemBattle = GameObject.Find("BattleManager").GetComponent<System_Battle>();
        m_Player = GameObject.Find("Player").GetComponent<Player>();
    }

    public void UsingItem(char argGrade)
    {
        int iHillsize = 0;
        switch (argGrade)
        {
            case '1':
                if (m_Player.getInfo().ILevel <= 10)
                    iHillsize =  m_Player.getInfo().ICurrentHp + 30;
                else
                    iHillsize = (int)(m_Player.getInfo().ICurrentHp * 0.05f);
                break;
            case '2':
                if (m_Player.getInfo().ILevel <= 20)
                    iHillsize = m_Player.getInfo().ICurrentHp + 70;
                else
                    iHillsize = (int)(m_Player.getInfo().ICurrentHp * 0.25f);
                break;
            case '3':
                if (m_Player.getInfo().ILevel <= 30)
                    iHillsize = m_Player.getInfo().ICurrentHp + 140;
                else
                    iHillsize = (int)(m_Player.getInfo().ICurrentHp * 0.45f);
                break;
            case '4':
                if (m_Player.getInfo().ILevel <= 40)
                    iHillsize = m_Player.getInfo().ICurrentHp + 200;
                else
                    iHillsize = (int)(m_Player.getInfo().ICurrentHp * 0.60f);
                break;
        }
        Debug.Log("힐량 : " + iHillsize);
        m_Player.getInfo().setCurrentHp(ref m_Player.getInfo(), m_Player.getInfo().ICurrentHp + iHillsize);
        if(m_Player.getInfo().ICurrentHp > m_Player.getInfo().IMaxHp)
            m_Player.getInfo().setCurrentHp(ref m_Player.getInfo(), m_Player.getInfo().IMaxHp);
        if (m_Scene.name == "Duengeon")
            m_SystemBattle.pPlayerHpSize = (float)iHillsize / m_Player.getInfo().IMaxHp;
    }
}
