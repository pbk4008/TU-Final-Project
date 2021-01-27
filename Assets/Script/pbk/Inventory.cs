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

        PannelH = functions.CalculSpriteHorizontalSize(gameObject);
        PannelV = functions.CalculSpriteVerticalSize(gameObject);

        ItemH = functions.CalculSpriteHorizontalSize(m_objargItem);
        ItemV = functions.CalculSpriteVerticalSize(m_objargItem);

        float H=PannelH + ItemH;
        float xPos = H / 8-ItemH/2;
        float V = PannelV + ItemV;
        float yPos = V / 3 - ItemV / 2;
        Vector3 pos;
        pos.x = xPos * (xIndex + 1);
        pos.y = yPos * (yIndex + 1);
        pos.z = 1;

        m_EtcInventory[argIndex].GetComponent<Transform>().position = pos;
        m_EtcInventory[argIndex].transform.parent = gameObject.transform;

        m_UseInventory[argIndex].GetComponent<Transform>().position = pos;
        m_UseInventory[argIndex].transform.parent = gameObject.transform;

        m_WeaponInventory[argIndex].GetComponent<Transform>().position = pos;
        m_WeaponInventory[argIndex].transform.parent = gameObject.transform;
    }
    public void AddItem(Item argItem)
    {
        string tmpCode = functions.CodetoString(argItem.Code);
        int index = 0; 
        switch (argItem.Code[0])
        {
            case '0'://기타
                foreach (EtcItem i in m_EtcInventory)
                {
                    string argCode = functions.CodetoString(i.Code);

                    if (argCode == null)//아이템 없을 때
                    {
                        m_EtcInventory[index].Code = argItem.Code;
                        m_EtcInventory[index].ICount = argItem.ICount;
                        break;
                    }
                    else if (tmpCode == argCode)//같은 아이템 있을때
                    {
                        if (m_EtcInventory[index].ICount >= 99)
                        {
                            index++;
                            continue;
                        }
                        else
                        {
                            if (m_EtcInventory[index].ICount + argItem.ICount > 99)
                            {
                                int argCount = argItem.ICount;
                                argCount = m_EtcInventory[index].ICount + argItem.ICount - 99;
                                m_EtcInventory[index].ICount = 99;
                                m_EtcInventory[index + 1].Code = argItem.Code;
                                m_EtcInventory[index + 1].ICount = argCount;
                                break;
                            }
                            else
                            {
                                m_EtcInventory[index].ICount += argItem.ICount;
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
                foreach (UseItem i in m_UseInventory)
                {
                    string argCode = functions.CodetoString(i.Code);
                    if (argCode == null)//아이템 없을 때
                    {
                        m_UseInventory[index].Code = argItem.Code;
                        m_UseInventory[index].ICount = argItem.ICount;
                        break;
                    }
                    else if (tmpCode == argCode)//같은 아이템 있을때
                    {
                        if (m_UseInventory[index].ICount >= 99)
                        {
                            index++;
                            continue;
                        }
                        else
                        {
                            if (m_UseInventory[index].ICount + argItem.ICount > 99)
                            {
                                int argCount = argItem.ICount;
                                argCount = m_EtcInventory[index].ICount + argItem.ICount - 99;
                                m_UseInventory[index].ICount = 99;
                                m_UseInventory[index + 1].Code = argItem.Code;
                                m_UseInventory[index + 1].ICount = argCount;
                                break;
                            }
                            else
                            {
                                m_UseInventory[index].ICount += argItem.ICount;
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
                foreach (WeaponItem i in m_WeaponInventory)
                {
                    string argCode = functions.CodetoString(i.Code);

                    if (argCode == null)//아이템 없을 때
                    {
                        m_WeaponInventory[index].Code = argItem.Code;
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
    public void RemoveItem(GameObject argItem)
    {
        Item tmpItem = argItem.GetComponent<Item>();
        if (tmpItem == null)
            Debug.Log("아이템 클릭 실패");
        string tmpCode = functions.CodetoString(tmpItem.Code);
        int index = 13;
        switch(tmpItem.Code[0])
        {
            case '0'://기타
               for(int i=m_EtcInventory.Count; i>=0;i--)
                {
                    string invenItemCode = functions.CodetoString(m_EtcInventory[index].Code);
                    if(invenItemCode == null)
                    {
                        index--;
                        continue;
                    }
                    else if(m_EtcInventory[index].ICount-1==0)
                    {
                        m_EtcInventory[index].CodeReset();
                        break;
                    }
                    else
                    {
                        m_EtcInventory[index].ICount--;
                        break;
                    }
                }
                break;
            case '1'://장비
                for (int i = m_WeaponInventory.Count; i >= 0; i--)
                {
                    string invenItemCode = functions.CodetoString(m_WeaponInventory[index].Code);
                    if (invenItemCode == null)
                    {
                        index--;
                        continue;
                    }
                    else if (m_WeaponInventory[index].ICount - 1 == 0)
                    {
                        m_WeaponInventory[index].CodeReset();
                        break;
                    }
                    else
                    {
                        m_WeaponInventory[index].ICount--;
                        break;
                    }
                }
                break;
            case '2'://소비
                for(int i=m_UseInventory.Count; i>=0; i--)
                {
                    string invenItemCode = functions.CodetoString(m_UseInventory[index].Code);
                    if (invenItemCode == null)
                    {
                        index--;
                        continue;
                    }
                    else if (tmpCode == invenItemCode)
                    {
                        UseItem uItem = tmpItem.GetComponent<UseItem>();
                        uItem.UsingItem(tmpCode[3]);
                        if (m_UseInventory[index].ICount - 1 == 0)
                        {
                            m_UseInventory[index].CodeReset();
                            break;
                        }
                        else
                        {
                            m_UseInventory[index].ICount--;
                            break;
                        }
                    }
                }
                break;
        }
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
                        break;
                    case ITEM_TYPE.USE:
                       m_EtcInventory[i].gameObject.SetActive(false);
                        m_UseInventory[i].gameObject.SetActive(true);
                        m_WeaponInventory[i].gameObject.SetActive(false);
                        break;
                    case ITEM_TYPE.EQUIP:
                       m_EtcInventory[i].gameObject.SetActive(false);
                        m_UseInventory[i].gameObject.SetActive(false);
                        m_WeaponInventory[i].gameObject.SetActive(true);
                        break;
                }
            }
            foreach (EtcItem i in m_EtcInventory)
            {
                if (i.Code == null)
                    break;
                i.CodeSolve();
                i.ImageSetting();
                i.gameObject.GetComponent<Image>().sprite = i.SprImg;
                i.gameObject.GetComponentInChildren<Text>().text = i.ICount.ToString();
            }
            foreach (UseItem i in m_UseInventory)
            {
                if (i.Code == null)
                    break;
                i.CodeSolve();
                i.ImageSetting();
                i.gameObject.GetComponent<Image>().sprite = i.SprImg;
                i.gameObject.GetComponentInChildren<Text>().text = i.ICount.ToString();
            }
            yield return new WaitForSeconds(0.1f);
        } 
    }

    void Update()
    {
        
    }

    void FixedUpdate()//캔버스 위에 있는 UI인식
    {
        //if (Input.GetMouseButtonDown(0))
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
            if (m_removeItem.GetComponent<Item>() != null)
            {
                Debug.Log(results[0].gameObject.GetComponent<Item>().Code);
                RemoveItem(m_removeItem);
            }
        }
    }
    // Update is called once per frame
}
