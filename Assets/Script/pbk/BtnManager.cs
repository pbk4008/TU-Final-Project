using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using enums;
using Delegats;

public class BtnManager : MonoBehaviour
{
    public static event BattleHandler attack;
    public static event BattleHandler run;
    private Inventory m_Inventory;
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
        System_Battle SB = GameObject.Find("BattleManager").GetComponent<System_Battle>();
        if (!m_Player.bStun)
        {
            m_Player.AnimTrigger = ANIMTRIGGER.ATTACK;
            m_Player.BButtonClick = true;
            attack();
            GameObject tmpCanvas = gameObject.transform.parent.gameObject;
            tmpCanvas.SetActive(false);
        }
        else
        {
            Debug.Log("스턴 중에는 공격할 수 없습니다");
            SB.eBattleProcess = BATTLE_PROCESS.BEFORE;
            m_Player.BButtonClick = true;
            GameObject tmpCanvas = gameObject.transform.parent.gameObject;
            tmpCanvas.SetActive(false);
        }
    }
    public void SkillBtn()
    {
        Canvas Cvs = GameObject.Find("BattleCanvas").GetComponent<Canvas>();
        Player m_Player = GameObject.FindWithTag("Player").GetComponent<Player>();
        m_Player.AnimTrigger = ANIMTRIGGER.ATTACK;
        Cvs.gameObject.SetActive(false);
        m_Player.BButtonClick = true;
        attack();
    }
    public void RunBtn()
    {
        run();
        GameObject tmpCanvas = gameObject.transform.parent.gameObject;
        tmpCanvas.SetActive(false);
    }
    public void WeaponBtn()
    {
        m_Inventory = GameObject.Find("Inventory").GetComponent<Inventory>();
        m_Inventory.EInventoryType = ITEM_TYPE.EQUIP;
    }
    public void UsedBtn()
    {
        m_Inventory = GameObject.Find("Inventory").GetComponent<Inventory>();
        m_Inventory.EInventoryType = ITEM_TYPE.USE;
    }
    public void EtcBtn()
    {
        m_Inventory = GameObject.Find("Inventory").GetComponent<Inventory>();
        m_Inventory.EInventoryType = ITEM_TYPE.ETC;
    }
}
