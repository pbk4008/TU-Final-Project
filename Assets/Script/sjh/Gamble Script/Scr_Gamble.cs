using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Scr_Gamble : MonoBehaviour
{
    [SerializeField] private GameObject Cvs_GambleNPC;
    [SerializeField] private GameObject Cvs_GambleMenu;
    [SerializeField] private GameObject Cvs_GambleReady;
    [SerializeField] private GameObject Cvs_GambleResult;
    [SerializeField] private Player m_Player;
    [SerializeField] private Text T_GamblePlayerMoney;
    [SerializeField] private Text T_GambleReady;
    [SerializeField] private Text T_GambleResult;
    [SerializeField] private InputField Input_Money;
    private string m_sPlayerSelect;

    private int m_iPlayerSelect;
    private int m_iPay;

    private void Click_GambleNPC()
    {
        Cvs_GambleMenu.SetActive(true);
        T_GamblePlayerMoney.GetComponent<Text>().text = "소지금 : " + m_Player.IMoney.ToString();
        m_iPay = 0;
    }

    private void Click_ExitGamble()
    {
        Cvs_GambleMenu.SetActive(false);
    }

    private void Click_GambleRight()
    {
        Cvs_GambleReady.SetActive(true);
        m_iPlayerSelect = 1;
        GambleReady();
    }

    private void Click_GambleLeft()
    {
        Cvs_GambleReady.SetActive(true);
        m_iPlayerSelect = 2;
        GambleReady();
    }

    private void Click_GambleReadyExit()
    {
        Cvs_GambleReady.SetActive(false);
    }

    private void Click_GambleSelect()
    {
        Cvs_GambleResult.SetActive(true);
        GambleResult();
    }

    private void Click_GambleResult()
    {
        Cvs_GambleResult.SetActive(false);
        Cvs_GambleReady.SetActive(false);
        Cvs_GambleMenu.SetActive(false);
    }

    private void SettingMoney()
    {
        string sMoney = Input_Money.text;
        m_iPay = int.Parse(sMoney);
    }

    private void GambleReady()
    {
        switch (m_iPlayerSelect)
        {
            case 1:
                m_sPlayerSelect = "오른쪽";
                break;
            case 2:
                m_sPlayerSelect = "왼쪽";
                break;
        }
       

        T_GambleReady.GetComponent<Text>().text = "소지금 : " + m_Player.IMoney.ToString() + "\n"
            + "거신 돈 : " + m_iPay + "\n"
            + "선택한 곳 : " + m_sPlayerSelect + "\n\n"
            + "오른쪽과 왼쪽 중 하나를 선택해 고르면 건 돈의 2배!!\n"
            + "단 틀릴 경우 거신돈 전부 잃습니다.\n"
            + "진행하시겠습니까?";
    }

    
    private void GambleResult()
    {
        if(m_iPay <0)
        {
            T_GambleResult.GetComponent<Text>().text = "음수를 넣을 수는 없습니다!!\n"
               + "건 돈을 음수가 되지않게 걸어주세요!!\n\n"
               + "조건\n"
               + "건 돈은 양수가 되어야합니다.";
        }
        else if (m_iPay > m_Player.IMoney)
            T_GambleResult.GetComponent<Text>().text = "소지금이 건 돈보다 적습니다!!\n"
                + "건 돈을 소지금 보다 적게 적어주세요!!\n\n" 
                + "조건\n"
                + "소지금 > 건 돈";
        else
        {
            int RandomNum = Random.Range(1, 3);
            if(RandomNum == m_iPlayerSelect)
            {
                m_Player.IMoney += m_iPay;
                T_GambleResult.GetComponent<Text>().text = "축하합니다!!\n"
                    + m_iPay + "원을 걸어" + m_iPay * 2 + "원을 얻었습니다.\n\n"
                    + "도박 후 소지금 : " + m_Player.IMoney;
            }
            else
            {
                m_Player.IMoney -= m_iPay;
                T_GambleResult.GetComponent<Text>().text = "실패!!\n"
                    + m_iPay + "원 전부 잃었습니다.\n\n"
                    + "도박 후 소지금 : " + m_Player.IMoney;
            }
        }
    }
}