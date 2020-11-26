using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Scr_House : MonoBehaviour
{
    
    [SerializeField] private GameObject Cvs_HouseHeal;
    [SerializeField] private GameObject Cvs_HouseMenu;
    [SerializeField] private Player m_Player;
    [SerializeField] private Text T_HouseHeal;

    public void Start()
    {
        m_Player.IHp = 25;
    }

    public void Click_House()
    {
        Cvs_HouseMenu.SetActive(true);
    }

    public void Click_HouseMenuExit()
    {
        Cvs_HouseMenu.SetActive(false);
    }

    public void Click_Heal()
    {
        Cvs_HouseHeal.SetActive(true);
        int iBeforeHp = m_Player.Info.ICurrentHp;  //이전 채력 = 플레이어 현재 채력
        m_Player.IHp = m_Player.Info.IMaxHp;       //플레이어 체력을 최대 채력으로 올림
        T_HouseHeal.GetComponent<Text>().text = "회복 전 채력 : " + iBeforeHp.ToString() + "\n\n"
            + "회복 후 채력 : " + m_Player.IHp;
    }

    public void Click_HouseExit()
    {
        Cvs_HouseHeal.SetActive(false);
    }
}
