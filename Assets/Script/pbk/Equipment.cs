using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using enums;

public class Equipment : MonoBehaviour
{
    private List<Item> m_EquipSlot;
    private List<GameObject> m_ReinForceSelect;
    private bool m_bReinforceCheck;
    private GameObject m_ReinForceSelectObject;


    public List<Item> EquipSlot { get => m_EquipSlot; set => m_EquipSlot = value; }
    public bool BReinforceCheck { get => m_bReinforceCheck; set => m_bReinforceCheck = value; }
    public GameObject ReinForceSelectObject { get => m_ReinForceSelectObject; set => m_ReinForceSelectObject = value; }

    private GraphicRaycaster rayCaster;
    // Start is called before the first frame update
    void Start()
    {
        rayCaster = transform.parent.GetComponent<GraphicRaycaster>();
        Debug.Log(rayCaster);
        EquipSetting();
        EquipPlusWeaponStat();
    }
    public void EquipSetting()
    {
        if (m_EquipSlot == null)
        {
            m_EquipSlot = new List<Item>();
            m_EquipSlot.AddRange(gameObject.GetComponentsInChildren<Item>());
            m_EquipSlot[0].ItemSetting(ITEM_TYPE.EQUIP, EQUIP_TYPE.HEAD, ITEM_GRADE.BASIC);
            m_EquipSlot[1].ItemSetting(ITEM_TYPE.EQUIP, EQUIP_TYPE.BODY, ITEM_GRADE.BASIC);
            m_EquipSlot[2].ItemSetting(ITEM_TYPE.EQUIP, EQUIP_TYPE.FOOT, ITEM_GRADE.BASIC);
            m_EquipSlot[3].ItemSetting(ITEM_TYPE.EQUIP, EQUIP_TYPE.SWORD, ITEM_GRADE.BASIC);
        }
        Debug.Log(m_EquipSlot.Count);
        foreach (Item i in m_EquipSlot)
        {
            i.CodeSolve();
            i.ImageSetting();
            i.gameObject.GetComponent<Image>().sprite = i.SprImg;
        }
    }
    public void EquipPlusWeaponStat ()
    {
        
        for(int i=0; i<m_EquipSlot.Count; i++)
        {
            WeaponItem tmpEquip = m_EquipSlot[i] as WeaponItem;
            Debug.Log("확인");
            tmpEquip.CodeSolve();
            tmpEquip.EffectSetting();
            tmpEquip.PlusItem();
        }
    }
    // Update is called once per frame
    void FixedUpdate()
    {
        if (Input.GetMouseButtonDown(0))
        {
            
            Debug.Log(m_bReinforceCheck);
            PointerEventData pointerData = new PointerEventData(EventSystem.current);
            List<RaycastResult> results = new List<RaycastResult>();

            pointerData.position = Input.mousePosition;
            rayCaster.Raycast(pointerData, results);

            if (results.Count == 0)
                return;
            if (results[0].gameObject.tag != "ItemUI")
                return;

            m_ReinForceSelectObject = results[0].gameObject;

            if (m_bReinforceCheck)
            {
                Debug.Log("확인");
                //Rainforce;
                SelectEquipment(m_ReinForceSelectObject);
            }
        }
        
    }
    private void SelectEquipment(GameObject argObject)
    {
        Debug.Log(argObject.name);
        if(argObject.name == "Image")
            argObject.SetActive(false);
        else if (!argObject.transform.GetChild(1).gameObject.active)
            argObject.transform.GetChild(1).gameObject.SetActive(true);
    }
    public bool ReinforcePercentage(ITEM_GRADE argGrade,int argReinforce)
    {
        bool tmpRes=false;
        switch(argGrade)
        {
            case ITEM_GRADE.BASIC:
                tmpRes = functions.Percentage(80-5 * argReinforce);
                break;
            case ITEM_GRADE.NORMAL:
                tmpRes = functions.Percentage(70-5*argReinforce);
                break;
            case ITEM_GRADE.SPECIAL:
                tmpRes = functions.Percentage(60 - 5 * argReinforce);
                break;
            case ITEM_GRADE.UNIQUE:
                tmpRes = functions.Percentage(50 - 5 * argReinforce);
                break;
            case ITEM_GRADE.LEGENDARY:
                tmpRes = functions.Percentage(40 - 5 * argReinforce);
                break;
        }
        return tmpRes;
    }
}
