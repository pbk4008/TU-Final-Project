using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using enums;
using UnityEngine.UI;

public class Debug_Item : MonoBehaviour
{
    private EtcItem m_EtcItem;
    private Inventory m_argInven;
    private UseItem m_UseItem;
    // Start is called before the first frame update
    void Start()
    {
        //m_EtcItem = GetComponent<EtcItem>();
        //m_EtcItem.ItemSetting(ITEM_TYPE.ETC, 0);
        //m_EtcItem.ICount = 1;
        //Image argImage = GetComponent<Image>();
        //argImage.sprite = GetComponent<SpriteRenderer>().sprite;
        m_argInven = GameObject.Find("Inventory").GetComponent<Inventory>();
    }
    public void getItem()
    {
        m_EtcItem = GetComponent<EtcItem>();
        m_EtcItem.ItemSetting(ITEM_TYPE.ETC, 0,false);
        m_EtcItem.ICount = 1;
        m_argInven.AddItem(m_EtcItem);
        Image argImage = GetComponent<Image>();
        argImage.sprite = GetComponent<SpriteRenderer>().sprite;
    }

    public void Useitemadd()
    {
        m_UseItem = GetComponent<UseItem>();
        m_UseItem.ItemSetting(ITEM_TYPE.USE, EQUIP_TYPE.USE, ITEM_GRADE.NORMAL);
        m_UseItem.ICount = 1;
        m_argInven.AddItem(m_UseItem);
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
