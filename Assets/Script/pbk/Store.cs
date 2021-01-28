using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using enums;
using UnityEngine.UI;
using UnityEngine.EventSystems;
public class Store : MonoBehaviour
{
    private List<Item> m_Store;
    GraphicRaycaster raycaster;
    [SerializeField]
    private Inventory m_Inventory;
    [SerializeField]
    private Text m_txtMoney;

    GameObject m_SelectItem;
    // Start is called before the first frame update
    void Start()
    {
        raycaster = gameObject.transform.parent.GetComponent<GraphicRaycaster>();
        m_Store = new List<Item>();
        m_Store.AddRange(gameObject.GetComponentsInChildren<Item>());
        StoreSetting();
        Debug.Log(m_Store.Count);
    }

    // Update is called once per frame
    void StoreSetting()
    {
        for (int i = 0; i < 5; i++)
        {
            if (i == 4)
            {
                m_Store[i].ItemSetting(ITEM_TYPE.USE, EQUIP_TYPE.USE, RandomGrade(1));
                m_Store[i].ICount = 1;
                m_Store[i].ICost = 300;
            }

            else
            {
                m_Store[i].ItemSetting(ITEM_TYPE.EQUIP, (EQUIP_TYPE)Random.RandomRange(1, 5), RandomGrade(0));
                m_Store[i].ICount = 1;
            }

            m_Store[i].CodeSolve();
            m_Store[i].ImageSetting();
            m_Store[i].gameObject.GetComponent<Image>().sprite = m_Store[i].SprImg;
           
        }
    }
    ITEM_GRADE RandomGrade(int argType)//0이면 무기 확률, 1이면 물약확률
    {
        
        int argRes = Random.RandomRange(0, 100);
        if (argType == 0)
        {
            if (argRes <= 20)
                return ITEM_GRADE.BASIC;
            else if (argRes <= 60)
                return ITEM_GRADE.NORMAL;
            else if (argRes <= 80)
                return ITEM_GRADE.SPECIAL;
            else if (argRes <= 95)
                return ITEM_GRADE.UNIQUE;
            else if (argRes <= 100)
                return ITEM_GRADE.LEGENDARY;
        }
        else
        {
            if (argRes <= 60)
                return ITEM_GRADE.NORMAL;
            else if (argRes <= 80)
                return ITEM_GRADE.SPECIAL;
            else if (argRes <= 95)
                return ITEM_GRADE.UNIQUE;
            else if (argRes <= 100)
                return ITEM_GRADE.LEGENDARY;
        }
        return ITEM_GRADE.END;
    }
    private void FixedUpdate()
    {
        int tmpMoney = GameObject.FindWithTag("Player").GetComponent<Player>().IMoney;
        m_txtMoney.text = "돈 : " + tmpMoney.ToString();
        if (Input.GetMouseButton(0))
        {
            PointerEventData pointerData = new PointerEventData(EventSystem.current);
            List<RaycastResult> results = new List<RaycastResult>();

            pointerData.position = Input.mousePosition;
            raycaster.Raycast(pointerData, results);

            m_SelectItem = results[0].gameObject;

         
            if (m_SelectItem.GetComponent<Item>() != null)
                BuyItem(m_SelectItem.GetComponent<Item>());
        }
    }
    private void BuyItem(Item argItem)
    {
        m_Inventory.AddItem(argItem);
        
        string argCode = functions.CodetoString(argItem.Code);
        foreach(Item i in m_Store)
        {
            string tmpCode = functions.CodetoString(i.Code);
            
            if(tmpCode == argCode)
            {
                Player player = GameObject.Find("Player").GetComponent<Player>();
                player.MoneySet(-i.ICost);
                i.ICount--;
                if(i.ICount==0)
                    i.CodeReset();
            }
        }

    }

    void Update()
    {
        
    }
}
