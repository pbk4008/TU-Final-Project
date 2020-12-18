using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using enums;
using Delegats;

public class BtnManager : MonoBehaviour
{
    public static event BattleHandler attack;
    public static event BattleHandler hit;

    // Start is called before the first frame update
    public void GameStart()//게임시작 버튼 클릭
    {
        Application.LoadLevel(1);
    }
    public void GameEnd()//게임종료 버튼 클릭
    {
        Application.Quit();
    }
    public void AttackBtn()//공격버튼
    {
        Player m_Player = GameObject.FindWithTag("Player").GetComponent<Player>();
        m_Player.AnimTrigger = ANIMTRIGGER.ATTACK;
        m_Player.BButtonClick = true;
        attack();
        GameObject tmpCanvas = gameObject.transform.parent.gameObject;
        tmpCanvas.SetActive(false);
    }
    public void HitBtn()
    {
        Player m_Player = GameObject.FindWithTag("Player").GetComponent<Player>();
        m_Player.AnimTrigger = ANIMTRIGGER.HIT;
        
    }
    public void BuffBtn()
    {
        Player m_Player = GameObject.FindWithTag("Player").GetComponent<Player>();
        m_Player.AnimTrigger = ANIMTRIGGER.BUFF;
    }
    public void WinBtn()
    {
        Player m_Player = GameObject.FindWithTag("Player").GetComponent<Player>();
        m_Player.AnimTrigger = ANIMTRIGGER.WIN;
    }
    public void FailBtn()
    {
        Player m_Player = GameObject.FindWithTag("Player").GetComponent<Player>();
        m_Player.AnimTrigger = ANIMTRIGGER.DIE;
    }
}
