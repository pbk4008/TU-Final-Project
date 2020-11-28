using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Scr_PlayerUI : MonoBehaviour
{
    [SerializeField] private GameObject Cvs_PlayerMenu;
    [SerializeField] private Text T_PlayerUI;
    [SerializeField] private Player m_Player;

    public void Click_PlayerUI()
    {
        Cvs_PlayerMenu.SetActive(true);
        PrintPlayerInfo();
    }

    public void Click_PlayerUIExit()
    {
        Cvs_PlayerMenu.SetActive(false);
    }

    private void PrintPlayerInfo()
    {
        T_PlayerUI.GetComponent<Text>().text = "이름 : " + m_Player.Info.SName + "\n"
            + "레벨 : " + m_Player.Info.ILevel.ToString() + "\n"
            + "물리 공격력 : " + m_Player.Info.IAtk.ToString() + "\n"
            + "마법 공격력 : " + m_Player.Info.IMatk.ToString() + "\n"
            + "체력 / 최대체력 : " + m_Player.Info.ICurrentHp.ToString() + " / " + m_Player.Info.IMaxHp.ToString() + "\n"
            + "공격 속도 : " + m_Player.Info.IAtkSpeed.ToString() + "\n"
            + "방어력 : " + m_Player.Info.IDef.ToString() + "\n"
            + "크리티컬 데미지 : " + m_Player.Info.FCriDmg.ToString() + "\n"
            + "--------------플레이어 스텟--------------\n"
            + "힘 : " + m_Player.Stat.IPow.ToString() + "\n"
            + "민첩 : " + m_Player.Stat.IDex.ToString() + "\n"
            + "지능 : " + m_Player.Stat.IInt.ToString();
    }
}
