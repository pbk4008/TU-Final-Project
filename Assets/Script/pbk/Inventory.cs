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
    public List<GameObject> SelectItemList { get => m_SelectItemList; set => m_SelectItemList = value; }
    public itemCreate Create { get => m_Create; set => m_Create = value; }
    public Equipment Equipment { get => m_Equipment; set => m_Equipment = value; }
    public bool BReinforceCheck { get => m_bReinforceCheck; set => m_bReinforceCheck = value; }
    public GameObject RemoveObject { get => m_RemoveObject; set => m_RemoveObject = value; }

    private bool m_bReinforceCheck;
    private itemCreate m_Create;
    private Equipment m_Equipment;

    private List<EtcItem> m_EtcInventory;
    private List<UseItem> m_UseInventory;
    private List<WeaponItem> m_WeaponInventory;
    private List<GameObject> m_SelectItemList;
    private UseItem m_UseItem;
    private Scene m_Scene;

    private GameObject m_RemoveObject;
    GraphicRaycaster raycaster;

    [SerializeField]
    private Text m_tCountTxt;
    [SerializeField]
    private Text m_tSelectTxt;

    private WINVEN_TYPE m_WInvenType; //장비 인벤 타입 (손준호
    public WINVEN_TYPE WinvenType { get => m_WInvenType; set => m_WInvenType = value; }
    // Start is called before the first frame update
    void Start()
    {
        raycaster = gameObject.transform.parent.GetComponent<GraphicRaycaster>();
        if(gameObject.name == "EtcInven")
            m_Create = GameObject.Find("Create").GetComponent<itemCreate>();
        if (gameObject.name == "WeaponInven")
            m_Equipment = GameObject.Find("Equipment").GetComponent<Equipment>();
        
        m_eInventoryType = ITEM_TYPE.ETC;
        m_WInvenType = WINVEN_TYPE.NONE;
        m_EtcInventory = new List<EtcItem>();
        m_UseInventory = new List<UseItem>();
        m_WeaponInventory = new List<WeaponItem>();
        m_SelectItemList = new List<GameObject>();
        m_objargItem.GetComponent<RectTransform>().localScale = new Vector3(1.3f, 1.3f, 1);
        for (int i = 0; i < 14; i++)
        {
            GameObject tmpItem3 = Instantiate(m_objargItem);
            GameObject tmpItem1 = Instantiate(m_objargItem);
            GameObject tmpItem2 = Instantiate(m_objargItem);

            m_WeaponInventory.Add(tmpItem3.GetComponent<WeaponItem>());

            m_EtcInventory.Add(tmpItem1.GetComponent<EtcItem>());
            m_UseInventory.Add(tmpItem2.GetComponent<UseItem>());

            m_WeaponInventory[i].CodeReset();
            m_EtcInventory[i].CodeReset();
            m_UseInventory[i].CodeReset();
        }
        for (int i = 0; i < 14; i++)
        {
            if (gameObject.name == "EtcInven"||gameObject.name=="WeaponInven")
                EtcCreateInvenCreate(i);
            else
                InventoryCreate(i);
        }
        DebugAddItem();
        DebugAddItem();
        StartCoroutine(PrintInven());
    }
    public void EtcCreateInvenCreate(int argIndex)
    {
        int xIndex = argIndex % 4;
        int yIndex = argIndex / 4;
        float PannelH, PannelV, ItemH, ItemV;

        PannelH = functions.CalculSpriteHorizontalSize(gameObject);
        PannelV = functions.CalculSpriteVerticalSize(gameObject);
        ItemH = functions.CalculSpriteHorizontalSize(m_objargItem);
        ItemV = functions.CalculSpriteVerticalSize(m_objargItem);

        float H = PannelH + ItemH;
        float xPos = H / 8 - ItemH / 2;
        float V = PannelV + ItemV;
        float yPos = V / 5 - ItemV / 2;
        Vector3 pos = gameObject.GetComponent<RectTransform>().position;

        pos.x += xPos * (xIndex + 1)-380;
        pos.y -= yPos * (yIndex + 1)-320;
        pos.z += 1;

        if (gameObject.name == "EtcInven")
        {
            m_EtcInventory[argIndex].GetComponent<RectTransform>().anchoredPosition = pos;
            m_EtcInventory[argIndex].transform.parent = gameObject.transform;
        }
        else if(gameObject.name == "WeaponInven" || m_WInvenType == WINVEN_TYPE.TRADE || m_WInvenType == WINVEN_TYPE.REINFORCE)
        {
            pos.x -= xPos * (xIndex * 8) + 1340;
            pos.y += yPos * (yIndex * 8) - 200;
            pos.z += 1;
            m_WeaponInventory[argIndex].GetComponent<RectTransform>().anchoredPosition = pos;
            m_WeaponInventory[argIndex].transform.parent = gameObject.transform;
        }
    }
    public void InventoryCreate(int argIndex)
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
        if (m_WInvenType == WINVEN_TYPE.NONE)
        {
            m_EtcInventory[argIndex].GetComponent<RectTransform>().anchoredPosition = pos;
            m_EtcInventory[argIndex].transform.parent = gameObject.transform;

            m_UseInventory[argIndex].GetComponent<RectTransform>().anchoredPosition = pos;
            m_UseInventory[argIndex].transform.parent = gameObject.transform;

            m_WeaponInventory[argIndex].GetComponent<RectTransform>().anchoredPosition = pos;
            m_WeaponInventory[argIndex].transform.parent = gameObject.transform;
        }
        else
        {
            pos.x -= 990;
            pos.y -= 270;
           m_WeaponInventory[argIndex].GetComponent<RectTransform>().anchoredPosition = pos;
            m_WeaponInventory[argIndex].transform.parent = gameObject.transform;
        }
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
                                m_EtcInventory[index + 1].GetComponent<EtcItem>().CodeSolve();
                                m_EtcInventory[index + 1].GetComponent<EtcItem>().ImageSetting();
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
                    Debug.Log("확인");
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
                                m_EtcInventory[index + 1].GetComponent<UseItem>().CodeSolve();
                                m_EtcInventory[index + 1].GetComponent<UseItem>().ImageSetting();
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
    public void SellItem(Item argItem)
    {
        if (functions.CodetoString(argItem.Code) != null)
        {
            Sound sound = new Sound();
            sound.SoundSetting(gameObject, SoundMgr.GetAudio(SOUND_TYPE.SELL));
        }
        RemoveItem(argItem,1);
        Store tmpStore = GameObject.Find("Store").GetComponent<Store>();
        Player tmpPlayer = GameObject.FindWithTag("Player").GetComponent<Player>();
        tmpPlayer.MoneySet(argItem.ICost);
        tmpStore.StoreSetting();
        tmpStore.TxtMoney.text = "돈 : " + tmpPlayer.IMoney;
    }
    public void RemoveItem(Item argItem,int argCount)
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
                        if (tmpEtcItem.ICount - argCount <= 0)
                        {
                            argCount=-tmpEtcItem.ICount;
                            tmpEtcItem.CodeReset();
                            continue;
                        }
                        else
                        {
                            tmpEtcItem.ICount-=argCount;
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
                    if (invenItemCode == null||tmpCode!= invenItemCode)
                    {
                        index--;
                        continue;
                    }
   
                    else if(tmpCode==invenItemCode)
                    {
                        tmpWeaponItem.CodeReset();
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
    }
    public IEnumerator PrintInven()
    {
        GameObject CreateBtn=null;
        while (true)
        {
            if (gameObject.name == "EtcInven")
            {
                if (CreateBtn == null)
                    CreateBtn = GameObject.Find("CreateBtn");
                if (m_Create.BSelect == true)
                {
                    for (int i = 0; i < 14; i++)
                    {
                        m_EtcInventory[i].gameObject.SetActive(true);
                        m_EtcInventory[i].GetComponent<Image>().sprite = m_EtcInventory[i].GetComponent<EtcItem>().SprImg;
                        m_EtcInventory[i].GetComponentInChildren<Text>().text = m_EtcInventory[i].GetComponent<EtcItem>().ICount.ToString();
                        m_UseInventory[i].gameObject.SetActive(false);
                        m_WeaponInventory[i].gameObject.SetActive(false);
                    }
                    m_tSelectTxt.gameObject.SetActive(false);
                    m_tCountTxt.gameObject.SetActive(true);
                    m_tCountTxt.text = "필요 점수 : " + m_Create.INeedCount.ToString() + 
                        "\n일반 : 개당 1점     보스 : 개당 5점";
                    CreateBtn.SetActive(true);
                }
                else
                {
                    for (int i = 0; i < 14; i++)
                    {
                        m_EtcInventory[i].gameObject.SetActive(false);
                    }
                    m_tSelectTxt.gameObject.SetActive(true);
                    m_tCountTxt.gameObject.SetActive(false);
                    CreateBtn.SetActive(false);
                }
            }
            else if(gameObject.name == "WeaponInven")
            {
                m_eInventoryType = ITEM_TYPE.EQUIP;
                for (int i = 0; i < 14; i++)
                {
                    m_WeaponInventory[i].gameObject.SetActive(true);
                    m_WeaponInventory[i].GetComponent<Image>().sprite = m_WeaponInventory[i].GetComponent<WeaponItem>().SprImg;
                    m_WeaponInventory[i].GetComponentInChildren<Text>().text = m_WeaponInventory[i].GetComponent<WeaponItem>().ICount.ToString();
                    m_UseInventory[i].gameObject.SetActive(false);
                    m_EtcInventory[i].gameObject.SetActive(false);
                }
            }
            else
            {
                for (int i = 0; i < 14; i++)
                {
                    switch (m_eInventoryType)
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
            }
            
            yield return new WaitForSeconds(0.1f);
        } 
    }
    private void UseItem(GameObject argObject)
    {
        Debug.Log("물약사용");
        Sound sound = new Sound();
        Debug.Log(SoundMgr.GetAudio(SOUND_TYPE.POTION));
        sound.SoundSetting(gameObject, SoundMgr.GetAudio(SOUND_TYPE.POTION));
        UseItem argItem = argObject.GetComponent<UseItem>();
        argItem.UsingItem(argItem.Code[3]);
        RemoveItem(argItem, 1);
        gameObject.transform.parent.GetComponent<Canvas>().enabled=false;
        GameObject.Find("BattleManager").GetComponent<System_Battle>().EBattleProcess = BATTLE_PROCESS.BEFORE;
    }
    public void SelectRemoveitem(GameObject argItem, int argCount)
    {
        int tmpCount = 0;
        foreach(EtcItem i in m_EtcInventory)
        {
            if(i.BSelect)
            {
                if(i.ICount>argCount)
                {
                    i.ICount -= argCount;
                    break;
                }
                else
                {
                    tmpCount = argCount - i.ICount;
                    i.CodeReset();
                    continue;
                }
                    
            }
        }
    }
    private void FixedUpdate()//캔버스 위에 있는 UI인식
    {
        if (Input.GetMouseButtonDown(0))
        {
            PointerEventData pointerData = new PointerEventData(EventSystem.current);
            List<RaycastResult> results = new List<RaycastResult>();

            pointerData.position = Input.mousePosition;
            raycaster.Raycast(pointerData, results);
            
           if (results.Count == 0)
                return;  
            if (results[0].gameObject.tag != "ItemUI")
                return;

            m_RemoveObject = results[0].gameObject;
            switch(m_eInventoryType)
            {
                case ITEM_TYPE.ETC:
                    foreach(EtcItem i in m_EtcInventory)
                    {
                        if(i.gameObject== m_RemoveObject)
                        {
                            m_RemoveObject.GetComponent<Item>().Code = i.Code;
                            m_RemoveObject.GetComponent<Item>().ICost = i.ICost;
                            break;
                        }
                    }
                    break;
                case ITEM_TYPE.USE:
                    foreach (UseItem i in m_UseInventory)
                    {
                        if (i.gameObject == m_RemoveObject)
                        {
                            m_RemoveObject.GetComponent<Item>().Code = i.Code;
                            m_RemoveObject.GetComponent<Item>().ICost = i.ICost;
                            break;
                        }
                    }
                    break;
                case ITEM_TYPE.EQUIP:
                    foreach (WeaponItem i in m_WeaponInventory)
                    {
                        if (i.gameObject == m_RemoveObject)
                        {
                            m_RemoveObject.GetComponent<Item>().Code = i.Code;
                            m_RemoveObject.GetComponent<Item>().ICost = i.ICost;
                            break;
                        }
                    }
                    break;
            }
            if (gameObject.name == "EtcInven")
            {
                if (m_Create.BSelect)
                {
                    SelectItem(m_RemoveObject);
                }
            }
            else if(m_WInvenType == WINVEN_TYPE.TRADE || m_WInvenType == WINVEN_TYPE.REINFORCE)
            {
                if (m_bReinforceCheck)
                {
                    ReinforceSelect(m_RemoveObject);
                }
                else
                    TradeItem(m_RemoveObject);
            }
            else
            {
               
                if (m_RemoveObject.GetComponent<Item>() != null)
                {
                    Scene tmpScene = SceneManager.GetActiveScene();
                    if (tmpScene.name != "Duengeon")
                        SellItem(m_RemoveObject.GetComponent<Item>());
                    else
                    {
                        if (m_RemoveObject.GetComponent<Item>().Code[0] =='2')
                        {
                            UseItem(m_RemoveObject);
                        }
                    }
                }
            }
        }
    }
    public void DebugAddItem()
    {
        WeaponItem tmpItem = m_objargItem.GetComponent<WeaponItem>();
        tmpItem.ItemSetting(ITEM_TYPE.EQUIP,EQUIP_TYPE.HEAD,ITEM_GRADE.NORMAL);
        
        tmpItem.CodeSolve();
        
        tmpItem.ImageSetting();
        
        tmpItem.ICount = 1;
        AddItem(tmpItem);
    }
    private void SelectItem(GameObject argItem)
    {
        if (argItem.name == "Image")
        {
            argItem.SetActive(false);
            m_SelectItemList.Remove(argItem.transform.parent.gameObject);
            return;
        }
        if (!argItem.transform.GetChild(1).gameObject.active)
        {
            argItem.GetComponent<EtcItem>().BSelect = true;
            argItem.gameObject.transform.GetChild(1).gameObject.SetActive(true);
            m_SelectItemList.Add(argItem); 
        }
    }
    public void ResetSelect()
    {
        foreach(GameObject i in m_SelectItemList)
        {
            i.transform.GetChild(1).gameObject.SetActive(false);
        }
        m_SelectItemList.Clear();
    }
    private void TradeItem(GameObject argItem)
    {
        if (gameObject.name == "WeaponInven"||m_WInvenType == WINVEN_TYPE.TRADE)
            m_Equipment = GameObject.Find("Equipment").GetComponent<Equipment>();
        WeaponItem argWeapon = argItem.GetComponent<WeaponItem>();//바꾸는 아이템
        WeaponItem tmpItem = new WeaponItem(); //장비창 아이템
        switch(argWeapon.EEquipType)
        {
            case EQUIP_TYPE.HEAD:           
                tmpItem.Code = m_Equipment.EquipSlot[0].Code;
                tmpItem.CodeSolve();
                tmpItem.EffectSetting();
                tmpItem.ClearItem();
                m_Equipment.EquipSlot[0].Code = argWeapon.Code;
                break;
            case EQUIP_TYPE.BODY:
                tmpItem.Code = m_Equipment.EquipSlot[1].Code;
                tmpItem.CodeSolve();
                tmpItem.EffectSetting();
                tmpItem.ClearItem();
                m_Equipment.EquipSlot[1].Code = argWeapon.Code;
                break;
            case EQUIP_TYPE.FOOT:
                tmpItem.Code = m_Equipment.EquipSlot[2].Code;
                tmpItem.CodeSolve();
                tmpItem.EffectSetting();
                tmpItem.ClearItem();
                m_Equipment.EquipSlot[2].Code = argWeapon.Code;
                break;
            case EQUIP_TYPE.KNUKLE:
                tmpItem.Code = m_Equipment.EquipSlot[3].Code;
                tmpItem.CodeSolve();
                tmpItem.EffectSetting();
                tmpItem.ClearItem();
                m_Equipment.EquipSlot[3].Code = argWeapon.Code;
                break;
            case EQUIP_TYPE.SWORD:
                tmpItem.Code = m_Equipment.EquipSlot[3].Code;
                tmpItem.CodeSolve();
                tmpItem.EffectSetting();
                tmpItem.ClearItem();
                m_Equipment.EquipSlot[3].Code = argWeapon.Code;
                break;
        }
        foreach(Item i in m_Equipment.EquipSlot)
        {
            i.CodeSolve();
            i.ImageSetting();
            i.gameObject.GetComponent<Image>().sprite = i.SprImg;
        }
        Player argPlayer = GameObject.Find("Player").GetComponent<Player>();
        Debug.Log(argPlayer.getInfo().IDef);
        argWeapon.EffectSetting();
        argWeapon.PlusItem();
        Debug.Log(argPlayer.getInfo().IDef);

        RemoveItem(argItem.GetComponent<WeaponItem>(), 1);
        AddItem(tmpItem);
        
    }
    private void ReinforceSelect(GameObject argObject)
    {
        if (argObject.name == "Image")
            argObject.SetActive(false);
        else if (!argObject.transform.GetChild(1).gameObject.active)
            argObject.transform.GetChild(1).gameObject.SetActive(true);
    }

    // Update is called once per frame
}
