using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using enums;
using Delegats;
using UnityEngine.UI;

public class BtnManager : MonoBehaviour
{
    public static event LegEffect Knuckleeffect;
    public static event BattleHandler attack;
    public static event BattleHandler run;
    private Inventory m_Inventory;
    // Start is called before the first frame update
    public void AttackBtn()//공격버튼
    {
        Player m_Player = GameObject.FindWithTag("Player").GetComponent<Player>();
        System_Battle SB = GameObject.Find("BattleManager").GetComponent<System_Battle>();
        
        if (!m_Player.bStun)
        {
            Knuckleeffect();
            m_Player.AnimTrigger = ANIMTRIGGER.ATTACK;
            m_Player.BSoundCheck = true;
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
        
        System_PlayerSkill PlayerSkill = GameObject.Find("System").GetComponent<System_PlayerSkill>();
        PlayerSkill.bOnClick = true;
        PlayerSkill.sButtonName = gameObject.name;
        gameObject.transform.parent.gameObject.SetActive(false);
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
        m_Inventory = GameObject.Find("Inventory").GetComponent<Inventory>();
        itemCreate create= GameObject.Find("Create").GetComponent<itemCreate>();
        int tmpScore = create.INeedCount;
        if (m_Inventory.SelectItemList.Count == 0)
            return;
        
        foreach(GameObject i in m_Inventory.SelectItemList)
        {
            EtcItem tmpItem = i.GetComponent<EtcItem>();
            tmpItem.CodeSolve();
            if (tmpScore <= tmpItem.ICount * tmpItem.ICreateScore)//기타아이템의 토탈 점수가 높을때
            {
                if (tmpItem.ICreateScore == 5)//보스 아이템
                {
                    Debug.Log(1);
                    m_Inventory.SelectRemoveitem(i, tmpScore / 5 + 1);
                }
                else if (tmpItem.ICreateScore == 1)//노말 아이템
                {
                    Debug.Log(2);
                    m_Inventory.SelectRemoveitem(i, tmpScore);
                }
                break;
            }
            else if(tmpScore>tmpItem.ICount*tmpItem.ICreateScore)//기타아이템의 토탈 점수가 낮을때
            {
                if(tmpItem.ICreateScore==5)//보스 아이템
                {
                    m_Inventory.SelectRemoveitem(i, tmpItem.ICount);
                    tmpScore -= tmpItem.ICount*tmpItem.ICreateScore;
                }
                else if(tmpItem.ICreateScore==1)//노말 아이템
                {
                    Debug.Log(3);
                    m_Inventory.SelectRemoveitem(i, tmpItem.ICount);
                    tmpScore -= tmpItem.ICount;
                }
                continue;
            }
        }
        m_Inventory.ResetSelect();
        Item resItem = create.SelectObject.GetComponent<Item>();
        resItem.ICount = 1;
        m_Inventory.AddItem(resItem);
    }
    public void ItemUseBtn()
    {
        Player PL = GameObject.Find("Player").GetComponent<Player>();
        GameObject Inventory = GameObject.Find("InvenCanvas");
        Inventory.GetComponentInChildren<Inventory>().EInventoryType = ITEM_TYPE.USE;
        Inventory.transform.GetChild(6).GetComponent<Text>().text = PL.IMoney.ToString();
        Inventory.transform.GetChild(2).gameObject.SetActive(false);
        Inventory.GetComponent<Canvas>().enabled = true;
        GameObject.Find("TextCanvas").transform.GetChild(11).gameObject.SetActive(true);
    }
    public void ReinforceBtn()
    {
        Sound tmpSound=new Sound();
        m_Inventory = GameObject.Find("Inventory").GetComponent<Inventory>();
        Equipment m_Equipment = GameObject.Find("Equipment").GetComponent<Equipment>();

       WeaponItem InvenItem = m_Inventory.RemoveObject.GetComponent<WeaponItem>();
       WeaponItem EquipItem = m_Equipment.ReinForceSelectObject.GetComponent<Item>() as WeaponItem;
       //
       //
       if(functions.CodetoString(InvenItem.Code)==functions.CodetoString(EquipItem.Code))
       {
            Debug.Log("코드 같음");
            m_Inventory.RemoveItem(InvenItem, 1);
            bool ReinforceRes=m_Equipment.ReinforcePercentage(EquipItem.EGrade, EquipItem.IReinforce);
            //
            if (ReinforceRes)
            {
                tmpSound.SoundSetting(gameObject,SoundMgr.GetAudio(SOUND_TYPE.REIN_SUC));
                EquipItem.IReinforce++;
            }
            else
               tmpSound.SoundSetting(gameObject,SoundMgr.GetAudio(SOUND_TYPE.REIN_FAIL));
           
           ////강화 확률에 따라 강화
       }
       else
           Debug.Log("코드 다름");
       InvenItem.gameObject.transform.GetChild(1).gameObject.SetActive(false);
       EquipItem.gameObject.transform.GetChild(1).gameObject.SetActive(false);

       Debug.Log(EquipItem.IReinforce);
    }
}
