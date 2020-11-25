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
        structs.tagInfo argInfo = new structs.tagInfo();
        argInfo.IMaxHp = m_Player.Stat.IPow * 3 + 50;
        argInfo.ICurrentHp = argInfo.IMaxHp;
        argInfo.IAtkSpeed = m_Player.Stat.IDex/15+1;
        if (argInfo.IAtkSpeed >= 10)
            argInfo.IAtkSpeed = 10;
        argInfo.IAtk = m_Player.Stat.IPow/2+3;
        argInfo.IMatk = m_Player.Stat.IInt+3;
        argInfo.FCriDmg = m_Player.Stat.IDex / 3 * 0.02f + 1.1f;

        m_Player.Info = argInfo;

        //argInfo.IDef = 방어구
    }
    void Update()
    {
        CalculStat();
    }
}
