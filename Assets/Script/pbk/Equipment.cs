using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using enums;

public class Equipment : MonoBehaviour
{
    private List<Item> m_EquipSlot;

    public List<Item> EquipSlot { get => m_EquipSlot; set => m_EquipSlot = value; }

    // Start is called before the first frame update
    void Start()
    {
        m_EquipSlot = new List<Item>();
        m_EquipSlot.AddRange(gameObject.GetComponentsInChildren<Item>());
        m_EquipSlot[0].ItemSetting(ITEM_TYPE.EQUIP,EQUIP_TYPE.HEAD,ITEM_GRADE.BASIC);
        m_EquipSlot[1].ItemSetting(ITEM_TYPE.EQUIP, EQUIP_TYPE.BODY, ITEM_GRADE.BASIC);
        m_EquipSlot[2].ItemSetting(ITEM_TYPE.EQUIP, EQUIP_TYPE.FOOT, ITEM_GRADE.BASIC);
        m_EquipSlot[3].ItemSetting(ITEM_TYPE.EQUIP, EQUIP_TYPE.SWORD, ITEM_GRADE.BASIC);

        Debug.Log(m_EquipSlot.Count);
        foreach (Item i in m_EquipSlot)
        {
            i.CodeSolve();
            i.ImageSetting();
            i.gameObject.GetComponent<Image>().sprite = i.SprImg;
        }
        
    }

    // Update is called once per frame
    void Update()
    {
         
    }
}
