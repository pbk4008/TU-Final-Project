using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using enums;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class Inventory : MonoBehaviour
{
    private ITEM_TYPE m_eInventoryType;
    [SerializeField]
    private GameObject m_objargItem;
    public ITEM_TYPE EInventoryType { get => m_eInventoryType; set => m_eInventoryType = value; }
    public List<EtcItem> ListEtcInventory { get => m_EtcInventory; set => m_EtcInventory = value; }
    private Canvas m_InvenUI;
    public GameObject ObjargItem { get => m_objargItem; set => m_objargItem = value; }
    public List<UseItem> UseInventory { get => m_UseInventory; set => m_UseInventory = value; }
    public List<WeaponItem> WeaponInventory { get => m_WeaponInventory; set => m_WeaponInventory = value; }

    private List<EtcItem> m_EtcInventory;
    private List<UseItem> m_UseInventory;
    private List<WeaponItem> m_WeaponInventory;
    private UseItem m_UseItem;
    private Scene m_Scene;

    private GameObject m_removeItem;
    GraphicRaycaster raycaster;


    // Start is called before the first frame update
    void Start()
    {
        if(m_Scene.name == "Duengeon")
            m_InvenUI = GameObject.Find("InventoryUI").GetComponent<Canvas>();
        raycaster = gameObject.transform.parent.GetComponent<GraphicRaycaster>();
        
        m_eInventoryType = ITEM_TYPE.ETC;

        m_EtcInventory = new List<EtcItem>();
        m_UseInventory = new List<UseItem>();
        m_WeaponInventory = new List<WeaponItem>();
        m_objargItem.GetComponent<RectTransform>().localScale = new Vector3(1.3f, 1.3f, 1);
        for (int i = 0; i < 14; i++)
        {
            GameObject tmpItem1 = Instantiate(m_objargItem);
            GameObject tmpItem2 = Instantiate(m_objargItem);
            GameObject tmpItem3 = Instantiate(m_objargItem);

            m_EtcInventory.Add(tmpItem1.GetComponent<EtcItem>());
            m_UseInventory.Add(tmpItem2.GetComponent<UseItem>());
            m_WeaponInventory.Add(tmpItem3.GetComponent<WeaponItem>());

            InventoryCreate(i);
        }
        StartCoroutine(PrintInven());
    }
    private void InventoryCreate(int argIndex)
    {
        
        int xIndex = argIndex % 7;
        int yIndex = argIndex / 7;
        float PannelH, PannelV, ItemH, ItemV;
        

        PannelH = functions.CalculSpriteHorizontalSize(gameObject)*20;
        PannelV = functions.CalculSpriteVerticalSize(gameObject)*5;


        ItemH = functions.CalculSpriteHorizontalSize(m_objargItem);
        ItemV = functions.CalculSpriteVerticalSize(m_objargItem);
       
        float H=PannelH + ItemH;
        float xPos = H / 8-ItemH/2;
        float V = PannelV + ItemV;
        float yPos = V / 3 - ItemV / 2;
        Vector3 pos = gameObject.GetComponent<RectTransform>().position;

        pos.x += xPos * (xIndex + 1)-500;
        pos.y -= yPos * (yIndex + 1)-130;
        pos.z += 1;

        m_EtcInventory[argIndex].GetComponent<RectTransform>(). anchoredPosition= pos;
        m_EtcInventory[argIndex].transform.parent = gameObject.transform;
       

        m_UseInventory[argIndex].GetComponent<RectTransform>().anchoredPosition = pos;
        m_UseInventory[argIndex].transform.parent = gameObject.transform;

        m_WeaponInventory[argIndex].GetComponent<RectTransform>().anchoredPosition = pos;
        m_WeaponInventory[argIndex].transform.parent = gameObject.transform;
    }
    public void AddItem(Item argItem)
    {
        string tmpCode = functions.CodetoString(argItem.Code);
        int index = 0;
        switch (argItem.Code[0])
        {
            case '0'://기타
                for(;index<m_EtcInventory.Count;)
                {
                    EtcItem tmpEtcItem = m_EtcInventory[index].GetComponent<EtcItem>();
                    string argCode = functions.CodetoString(tmpEtcItem.Code);

                    if (argCode == null)//아이템 없을 때
                    {
                        tmpEtcItem.Code = argItem.Code;
                        tmpEtcItem.ICount = argItem.ICount;
                        tmpEtcItem.CodeSolve();
                        tmpEtcItem.ImageSetting();
                        break;
                    }
                    else if (tmpCode == argCode)//같은 아이템 있을때
                    {
                        if (tmpEtcItem.ICount >= 99)
                        {
                            index++;
                            continue;
                        }
                        else
                        {
                            if (tmpEtcItem.ICount + argItem.ICount > 99)
                            {
                                int argCount = argItem.ICount;
                                argCount = tmpEtcItem.ICount + argItem.ICount - 99;
                                tmpEtcItem.ICount = 99;
                                m_EtcInventory[index + 1].GetComponent<EtcItem>().Code = argItem.Code;
                                m_EtcInventory[index + 1].GetComponent<EtcItem>().ICount = argCount;
                                break;
                            }
                            else
                            {
                                tmpEtcItem.ICount += argItem.ICount;
                                break;
                            }
                        }
                    }
                    else//다른 아이템 일 때
                    {
                        index++;
                        continue;
                    }
                }
                break;
            case '2':
                for (;index<m_UseInventory.Count;)
                {
                    UseItem tmpUseItem = m_UseInventory[index].GetComponent<UseItem>();
                    string argCode = functions.CodetoString(tmpUseItem.Code);
                    if (argCode == null)//아이템 없을 때
                    {

                        tmpUseItem.Code = argItem.Code;
                        tmpUseItem.ICount = argItem.ICount;
                        tmpUseItem.CodeSolve();
                        tmpUseItem.ImageSetting();
                        break;
                    }
                    else if (tmpCode == argCode)//같은 아이템 있을때
                    {
                        if (tmpUseItem.ICount >= 99)
                        {
                            index++;
                            continue;
                        }
                        else
                        {
                            if (tmpUseItem.ICount + argItem.ICount > 99)
                            {
                                int argCount = argItem.ICount;
                                argCount = tmpUseItem.ICount + argItem.ICount - 99;
                                tmpUseItem.ICount = 99;
                                m_UseInventory[index + 1].GetComponent<UseItem>().Code = argItem.Code;
                                m_UseInventory[index + 1].GetComponent<UseItem>().ICount = argCount;
                                break;
                            }
                            else
                            {
                                tmpUseItem.ICount += argItem.ICount;
                                break;
                            }
                        }
                    }
                    else//다른 아이템 일 때
                    {
                        index++;
                        continue;
                    }
                }
                break;
            case '1':
                for (; index<m_WeaponInventory.Count;)
                {
                    WeaponItem tmpWeaponItem = m_WeaponInventory[index].GetComponent<WeaponItem>();
                    string argCode = functions.CodetoString(tmpWeaponItem.Code);

                    if (argCode == null)//아이템 없을 때
                    {
                        tmpWeaponItem.Code = argItem.Code;
                        tmpWeaponItem.CodeSolve();
                        tmpWeaponItem.ImageSetting();
                        break;
                    }
                    else//같은 아이템 있을때
                    {
                        index++;
                        continue;
                    }
                }
                break;
        }
    }
    public void RemoveItem(Item argItem)
    {
        string tmpCode = functions.CodetoString(argItem.Code);
        Debug.Log(tmpCode);
        int index = 13;
        switch(argItem.Code[0])
        {
            case '0'://기타
               for(; index>=0; )
                {
                    EtcItem tmpEtcItem = m_EtcInventory[index].GetComponent<EtcItem>();
                    string invenItemCode = functions.CodetoString(tmpEtcItem.Code);
                    if (invenItemCode == null)
                    {
                        index--;
                        continue;
                    }
                    else if (tmpCode == invenItemCode)
                    { 
                        if (tmpEtcItem.ICount - 1 == 0)
                        {
                            tmpEtcItem.CodeReset();
                            break;
                        }
                        else
                        {
                            tmpEtcItem.ICount--;
                            break;
                        }
                    }
                }
                break;
            case '1'://장비
                for (; index >= 0;)
                {
                    WeaponItem tmpWeaponItem = m_WeaponInventory[index].GetComponent<WeaponItem>();
                    string invenItemCode = functions.CodetoString(tmpWeaponItem.Code);
                    if (invenItemCode == null)
                    {
                        index--;
                        continue;
                    }
                    else if (tmpWeaponItem.ICount - 1 == 0)
                    {
                        tmpWeaponItem.CodeReset();
                        break;
                    }
                    else
                    {
                        tmpWeaponItem.ICount--;
                        break;
                    }
                }
                break;
            case '2'://소비
                for(; index>=0;)
                {
                    UseItem tmpUseItem = m_UseInventory[index].GetComponent<UseItem>();
                    string invenItemCode = functions.CodetoString(tmpUseItem.Code);
                    if (invenItemCode == null)
                    {
                        index--;
                        continue;
                    }
                    else if (invenItemCode == tmpCode)
                    { 
                        if (tmpUseItem.ICount - 1 == 0)
                        {
                            tmpUseItem.CodeReset();
                            break;
                        }
                        else
                        {
                            tmpUseItem.ICount--;
                            break;
                        }
                    }
                }
                break;
        }
        Player tmpPlayer = GameObject.FindWithTag("Player").GetComponent<Player>();
        tmpPlayer.MoneySet(argItem.ICost);
        Debug.Log(argItem.ICost);
    }
    private IEnumerator PrintInven()
    {
        while (true)
        {
            for(int i=0; i<14; i++)
            {
                switch(m_eInventoryType)
                {
                    case ITEM_TYPE.ETC:
                     m_EtcInventory[i].gameObject.SetActive(true);
                     m_UseInventory[i].gameObject.SetActive(false);
                     m_WeaponInventory[i].gameObject.SetActive(false);
                     m_EtcInventory[i].GetComponent<Image>().sprite = m_EtcInventory[i].GetComponent<EtcItem>().SprImg;
                     m_EtcInventory[i].GetComponentInChildren<Text>().text = m_EtcInventory[i].GetComponent<EtcItem>().ICount.ToString();
                     break;
                    case ITEM_TYPE.USE:
                       m_EtcInventory[i].gameObject.SetActive(false);
                       m_UseInventory[i].gameObject.SetActive(true);
                       m_WeaponInventory[i].gameObject.SetActive(false);
                       m_UseInventory[i].GetComponent<Image>().sprite = m_UseInventory[i].GetComponent<UseItem>().SprImg;
                       m_UseInventory[i].GetComponentInChildren<Text>().text = m_UseInventory[i].GetComponent<UseItem>().ICount.ToString();
                        break;
                    case ITEM_TYPE.EQUIP:
                       m_EtcInventory[i].gameObject.SetActive(false);
                       m_UseInventory[i].gameObject.SetActive(false);
                       m_WeaponInventory[i].gameObject.SetActive(true);
                       m_WeaponInventory[i].GetComponent<Image>().sprite = m_WeaponInventory[i].GetComponent<WeaponItem>().SprImg;
                       m_WeaponInventory[i].GetComponentInChildren<Text>().text = m_WeaponInventory[i].GetComponent<WeaponItem>().ICount.ToString();
                       break;
                } 
            }
            yield return new WaitForSeconds(0.1f);
        } 
    }

    void Update()
    {
        
    }
    private void FixedUpdate()//캔버스 위에 있는 UI인식
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (m_Scene.name == "Duengeon")
                if (!m_InvenUI.enabled)
                    return;
            PointerEventData pointerData = new PointerEventData(EventSystem.current);
            List<RaycastResult> results = new List<RaycastResult>();

            pointerData.position = Input.mousePosition;
            raycaster.Raycast(pointerData, results);
            m_removeItem = results[0].gameObject;

            switch(m_eInventoryType)
            {
                case ITEM_TYPE.ETC:
                    foreach(EtcItem i in m_EtcInventory)
                    {
                        if(i.gameObject==m_removeItem)
                        {
                            m_removeItem.GetComponent<Item>().Code = i.Code;
                            m_removeItem.GetComponent<Item>().ICost = i.ICost;
                            break;
                        }
                    }
                    break;
                case ITEM_TYPE.USE:
                    foreach (UseItem i in m_UseInventory)
                    {
                        if (i.gameObject == m_removeItem)
                        {
                            m_removeItem.GetComponent<Item>().Code = i.Code;
                            m_removeItem.GetComponent<Item>().ICost = i.ICost;
                            break;
                        }
                    }
                    break;
                case ITEM_TYPE.EQUIP:
                    foreach (WeaponItem i in m_WeaponInventory)
                    {
                        if (i.gameObject == m_removeItem)
                        {
                            m_removeItem.GetComponent<Item>().Code = i.Code;
                            m_removeItem.GetComponent<Item>().ICost = i.ICost;
                            break;
                        }
                    }
                    break;
            }
            Debug.Log(functions.CodetoString(m_removeItem.GetComponent<Item>().Code));

            if (m_removeItem.GetComponent<Item>() != null)
            {
                RemoveItem(m_removeItem.GetComponent<Item>());
            }
        }
    }
    // Update is called once per frame
}
