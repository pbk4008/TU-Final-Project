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
        StartCoroutine(m_Inventory.PrintInven());
    }
    public void UsedBtn()
    {
        m_Inventory = GameObject.Find("Inventory").GetComponent<Inventory>();
        m_Inventory.EInventoryType = ITEM_TYPE.USE;
        StartCoroutine(m_Inventory.PrintInven());
    }
    public void EtcBtn()
    {
        m_Inventory = GameObject.Find("Inventory").GetComponent<Inventory>();
        m_Inventory.EInventoryType = ITEM_TYPE.ETC;
        StartCoroutine(m_Inventory.PrintInven());
    }
    public void CreateBtn()
    {
        m_Inventory = GameObject.Find("EtcInven").GetComponent<Inventory>();
        itemCreate create= GameObject.Find("Create").GetComponent<itemCreate>();
        int tmpScore = create.INeedCount;
        if (m_Inventory.SelectItemList.Count == 0)
            return;
        
        foreach(GameObject i in m_Inventory.SelectItemList)
        {
            EtcItem tmpItem = i.GetComponent<EtcItem>();
            tmpItem.CodeSolve();
            if (tmpScore <= tmpItem.ICount * tmpItem.ICreateScore)
            {
                if (tmpItem.ICreateScore == 5)
                {
                    Debug.Log(1);
                    m_Inventory.SelectRemoveitem(i, tmpScore / 5 + 1);
                }
                else if (tmpItem.ICreateScore == 1)
                {
                    Debug.Log(2);
                    m_Inventory.SelectRemoveitem(i, tmpScore);
                }
                break;
            }
            else if(tmpScore>tmpItem.ICount*tmpItem.ICreateScore)
            {
                if(tmpItem.ICreateScore==5)
                {
                    m_Inventory.SelectRemoveitem(i, i.GetComponent<Item>().ICount);
                    tmpScore -= tmpItem.ICount*tmpItem.ICreateScore;
                }
                else if(tmpItem.ICreateScore==1)
                {
                    m_Inventory.SelectRemoveitem(i, i.GetComponent<Item>().ICount);
                    tmpScore -= tmpItem.ICount;
                }
                continue;
            }
        }
        m_Inventory.ResetSelect();
        
        m_Inventory.AddItem(create.SelectObject.GetComponent<Item>());
        Debug.Log(functions.CodetoString(m_Inventory.UseInventory[0].Code));
        
    }
    public void ReinforceBtn()
    {
        m_Inventory = GameObject.Find("WeaponInven").GetComponent<Inventory>();
        Equipment m_Equipment = GameObject.Find("Equipment").GetComponent<Equipment>();

        WeaponItem InvenItem = m_Inventory.RemoveObject.GetComponent<WeaponItem>();
        WeaponItem EquipItem = m_Equipment.ReinForceSelectObject.GetComponent<Item>() as WeaponItem;

        
        if(functions.CodetoString(InvenItem.Code)==functions.CodetoString(EquipItem.Code))
        {
            Debug.Log("코드 같음");
            m_Inventory.RemoveItem(InvenItem, 1);
            bool ReinforceRes=m_Equipment.ReinforcePercentage(EquipItem.EGrade, EquipItem.IReinforce);

            if (ReinforceRes)
                EquipItem.IReinforce++;
            //강화 확률에 따라 강화
        }
        else
            Debug.Log("코드 다름");
        InvenItem.gameObject.transform.GetChild(1).gameObject.SetActive(false);
        EquipItem.gameObject.transform.GetChild(1).gameObject.SetActive(false);

        
        Debug.Log(EquipItem.IReinforce);
    }
}
