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
    private bool m_blive;


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
<<<<<<< HEAD
        System_Battle.Swordeffect += WeaponEffect;
        BtnManager.Knuckleeffect += kunckleEffect;
        System_Battle.Headeffect += HeadEffect;
        System_Battle.Bodyeffect += BodyEffect;
        Boss.Footeffect += FootEffect;
        m_EquipSlot = new List<Item>();
        m_EquipSlot.AddRange(gameObject.GetComponentsInChildren<Item>());
        m_EquipSlot[0].ItemSetting(ITEM_TYPE.EQUIP, EQUIP_TYPE.HEAD, ITEM_GRADE.BASIC);
        m_EquipSlot[1].ItemSetting(ITEM_TYPE.EQUIP, EQUIP_TYPE.BODY, ITEM_GRADE.BASIC);
        m_EquipSlot[2].ItemSetting(ITEM_TYPE.EQUIP, EQUIP_TYPE.FOOT, ITEM_GRADE.BASIC);
        m_EquipSlot[3].ItemSetting(ITEM_TYPE.EQUIP, EQUIP_TYPE.SWORD, ITEM_GRADE.BASIC);

=======
        if (m_EquipSlot == null)
        {
            m_EquipSlot = new List<Item>();
            m_EquipSlot.AddRange(gameObject.GetComponentsInChildren<Item>());
            m_EquipSlot[0].ItemSetting(ITEM_TYPE.EQUIP, EQUIP_TYPE.HEAD, ITEM_GRADE.BASIC);
            m_EquipSlot[1].ItemSetting(ITEM_TYPE.EQUIP, EQUIP_TYPE.BODY, ITEM_GRADE.BASIC);
            m_EquipSlot[2].ItemSetting(ITEM_TYPE.EQUIP, EQUIP_TYPE.FOOT, ITEM_GRADE.BASIC);
            m_EquipSlot[3].ItemSetting(ITEM_TYPE.EQUIP, EQUIP_TYPE.SWORD, ITEM_GRADE.BASIC);
        }
>>>>>>> 2198666e61b7ebc74be3e278e8d9e46b7f5a772a
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

    private void WeaponEffect()
    {
        System_Battle SB = GameObject.Find("BattleManager").GetComponent<System_Battle>();
        Player PL = GameObject.Find("Player").GetComponent<Player>();
        switch (m_EquipSlot[3].EEquipType)
            {
                case EQUIP_TYPE.SWORD:
                    switch (m_EquipSlot[3].EGrade)
                    {
                        case ITEM_GRADE.LEGENDARY:
                            Debug.Log("무기(검)효과 발동");
                            int iDmg = (int)(SB.ItotalDmg * 0.2f);
                            Debug.Log("흡수 한 데미지 : " + iDmg);
                            SB.pPlayerHpSize = SB.pPlayerHpSize = (float)iDmg / PL.getInfo().IMaxHp;
                            if(SB.pPlayerHpSize > 1)
                            {
                                iDmg = PL.getInfo().IMaxHp - (int)SB.pPlayerHpSize* PL.getInfo().IMaxHp;
                                SB.pPlayerHpSize = SB.pPlayerHpSize = -(float)iDmg / PL.getInfo().IMaxHp;
                             }
                            break;
                    }
                    break;
        }
    }

    private void kunckleEffect()
    {
        System_PlayerSkill PS = GameObject.Find("System").GetComponent<System_PlayerSkill>();
        switch (m_EquipSlot[3].EEquipType)
        {
            case EQUIP_TYPE.KNUKLE:
                switch (m_EquipSlot[3].EGrade)
                {
                    case ITEM_GRADE.LEGENDARY:
                        Debug.Log("무기(너클)효과 발동");
                        if (functions.Percentage(20))
                            PS.MinusPlayerSkill();
                        break;
                }
                break;
        }
    }

    private void HeadEffect()
    {
        if (!m_blive)
        {
            System_Battle SB = GameObject.Find("BattleManager").GetComponent<System_Battle>();
            switch (m_EquipSlot[0].EEquipType)
            {
                case EQUIP_TYPE.HEAD:
                    switch (m_EquipSlot[0].EGrade)
                    {
                        case ITEM_GRADE.LEGENDARY:
                            Debug.Log("모자 효과 발동");
                            SB.pPlayerHpSize = SB.pPlayerHpSize = 1;
                            Debug.Log("- 부 활 -");
                            break;
                    }
                    break;
            }
            m_blive = true;
        }
    }

    private void BodyEffect()
    {
        System_Battle SB = GameObject.Find("BattleManager").GetComponent<System_Battle>();

        switch (m_EquipSlot[1].EEquipType)
        {
            case EQUIP_TYPE.BODY:
                switch (m_EquipSlot[1].EGrade)
                {
                    case ITEM_GRADE.LEGENDARY:
                        Debug.Log("옷 효과 발동");
                        int iDmg = (int)(SB.ItotalDmg * 0.5f);
                        Debug.Log("가갑 데미지 : " + iDmg);
                        if (!GameObject.Find("Boss").gameObject.activeSelf)
                        {
                            Monster Mon = GameObject.Find("Monster").GetComponent<Monster>();
                            SB.pMonHpSize = SB.pMonHpSize = -(float)iDmg / Mon.getInfo().IMaxHp;
                        }
                        break;
                }
                break;
        }
    }

    private void FootEffect()
    {
        Boss Bos = GameObject.Find("Boss").GetComponent<Boss>();
        switch (m_EquipSlot[2].EEquipType)
        {
            case EQUIP_TYPE.FOOT:
                switch (m_EquipSlot[2].EGrade)
                {
                    case ITEM_GRADE.LEGENDARY:
                        if (functions.Percentage(30))
                        {
                            Debug.Log("신발 효과 발동");
                            Bos.bLockSkill = true;
                            Debug.Log("스킬 봉인");
                        }
                        break;
                }
                break;
        }
    }
}
