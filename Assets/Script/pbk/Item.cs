using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using enums;
using UnityEngine.UI;
public class Item : MonoBehaviour
{
    protected List<char> m_Code=null;
    protected Sprite m_sprImg;
    protected int iCount;
    protected int iCost;
    protected ITEM_TYPE m_eType;
    protected ITEM_GRADE m_eGrade;
    protected EQUIP_TYPE m_eEquipType;
    protected int m_iNum;
    public List<char> Code { get => m_Code; set => m_Code = value; }
    public int INum { get => m_iNum; set => m_iNum = value; }
    public Sprite SprImg { get => m_sprImg; set => m_sprImg = value; }
    public int ICount { get => iCount; set => iCount = value; }
    public ITEM_TYPE EType { get => m_eType; set => m_eType = value; }
    public ITEM_GRADE EGrade { get => m_eGrade; set => m_eGrade = value; }
    public EQUIP_TYPE EEquipType { get => m_eEquipType; set => m_eEquipType = value; }
    public int ICost { get => iCost; set => iCost = value; }

    // Start is called before the first frame update

    //아이템 코드 : 0x00
    //첫번째 : 아이템 종류 0기타 1장비 2소비
    //세번째 : 아이템 분류/기타템일 경우
    //네번째 : 아이템 번호/장비 시 등급
    public void ItemSetting(ITEM_TYPE argType, EQUIP_TYPE argEquip, ITEM_GRADE argGrade)
    {
        m_Code = new List<char>();
        m_Code.Add(char.Parse(((int)argType).ToString()));
        m_Code.Add('x');
        m_Code.Add(char.Parse(((int)argEquip).ToString()));
        m_Code.Add(char.Parse(((int)argGrade).ToString()));
    }
    virtual public void CodeSolve()
    {
        m_eType = (ITEM_TYPE)(int.Parse(m_Code[0].ToString()));
        if (m_eType == ITEM_TYPE.ETC)
        {
            m_iNum = int.Parse(m_Code[2].ToString()) * 10 + int.Parse(m_Code[3].ToString());
        }
        else
        {
            m_eEquipType = (EQUIP_TYPE)(int.Parse(m_Code[2].ToString()));
            m_eGrade = (ITEM_GRADE)(int.Parse(m_Code[3].ToString()));
        }
        MoneySetting();
    }
    public void ImageSetting()
    {
        switch (m_eType)
        {
            case ITEM_TYPE.ETC:
                m_sprImg = Resources.Load<Sprite>("Item/Etc/etc " + m_iNum.ToString());
                break;
            case ITEM_TYPE.EQUIP:
                m_sprImg = Resources.Load<Sprite>("Item/Weapon/weapon " + ((int)m_eEquipType).ToString() + ((int)m_eGrade).ToString());
                break;
            case ITEM_TYPE.USE:
                m_sprImg = Resources.Load<Sprite>("Item/Use/Use " + ((int)m_eEquipType).ToString() + ((int)m_eGrade).ToString());
                break;
        }
    }
    public void CodeReset()
    {
        m_Code = null;
        m_sprImg = Resources.Load<Sprite>("Item/blank");
        if (gameObject.GetComponent<Image>() != null)
            gameObject.GetComponent<Image>().sprite = m_sprImg;
    }

    private void MoneySetting()
    {
        switch (m_eType)
        {
            case ITEM_TYPE.ETC:
                switch(m_iNum/5)
                {
                    case 0:
                        iCost = 2;
                        break;
                    case 1:
                        iCost = 3;
                        break;
                    case 2:
                        iCost = 4;
                        break;
                    case 3:
                        iCost = 5;
                        break;
                    case 4:
                        iCost = 6;
                        break;
                    case 5:
                        iCost = 7;
                        break;
                }
                break;
            case ITEM_TYPE.EQUIP:
                switch (m_eGrade)
                {
                    case ITEM_GRADE.BASIC:
                        iCost = 50;
                        break;
                    case ITEM_GRADE.NORMAL:
                        iCost = 100;
                        break;
                    case ITEM_GRADE.SPECIAL:
                        iCost = 300;
                        break;
                    case ITEM_GRADE.UNIQUE:
                        iCost = 500;
                        break;
                    case ITEM_GRADE.LEGENDARY:
                        iCost = 1000;
                        break;
                }
                break;
            case ITEM_TYPE.USE:
                switch (m_eGrade)
                {
                    case ITEM_GRADE.NORMAL:
                        iCost = 15;
                        break;
                    case ITEM_GRADE.SPECIAL:
                        iCost = 35;
                        break;
                    case ITEM_GRADE.UNIQUE:
                        iCost = 70;
                        break;
                    case ITEM_GRADE.LEGENDARY:
                        iCost = 140;
                        break;
                }
                break;
        }
    }
}
