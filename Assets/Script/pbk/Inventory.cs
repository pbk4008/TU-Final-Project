using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using enums;
using UnityEngine.UI;

public class Inventory : MonoBehaviour
{
    private ITEM_TYPE m_eInventoryType;
    [SerializeField]
    private GameObject m_objargItem;
    public ITEM_TYPE EInventoryType { get => m_eInventoryType; set => m_eInventoryType = value; }
    public List<EtcItem> ListEtcInventory { get => m_EtcInventory; set => m_EtcInventory = value; }

    public GameObject ObjargItem { get => m_objargItem; set => m_objargItem = value; }
    private List<EtcItem> m_EtcInventory;
    private List<UseItem> m_UseInventory;
    private List<WeaponItem> m_WeaponInventory;


    // Start is called before the first frame update
    void Start()
    {
        m_eInventoryType = ITEM_TYPE.ETC;
        m_EtcInventory = new List<EtcItem>();
        m_UseInventory = new List<UseItem>();
        m_WeaponInventory = new List<WeaponItem>();

        for (int i = 0; i < 14; i++)
        {
            GameObject tmpItem = Instantiate(m_objargItem);
            m_EtcInventory.Add(tmpItem.GetComponent<EtcItem>());
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
        Vector3 pos = m_EtcInventory[argIndex].GetComponent<Transform>().position;
        pos.x += xPos * (xIndex + 1);
        pos.y -= yPos * (yIndex + 1);
        pos.z = 1;

        m_EtcInventory[argIndex].GetComponent<Transform>().position = pos;
        m_EtcInventory[argIndex].transform.parent = gameObject.transform;
    }
    public void AddItem(EtcItem argItem)
    {
        string tmpCode = functions.CodetoString(argItem.Code);
        switch (argItem.Code[0])
        {
            case '0'://기타
                int index = 0;
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
                                Debug.Log(argCount);
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
            case '1':

                break;
            case '2':

                break;
        }
        
    }
    private IEnumerator PrintInven()
    {
        while (true)
        {
            foreach (EtcItem i in m_EtcInventory)
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
    // Update is called once per frame
}
