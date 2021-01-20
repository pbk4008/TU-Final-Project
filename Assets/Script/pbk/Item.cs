﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using enums;
public class Item : MonoBehaviour
{
    protected List<char> m_Code=null;
    protected Sprite m_sprImg;
    private ITEM_TYPE m_eType;
    private ITEM_GRADE m_eGrade;
    private EQUIP_TYPE m_eEquipType;
    protected int m_iNum;
    public List<char> Code { get => m_Code; set => m_Code = value; }
    public int INum { get => m_iNum; set => m_iNum = value; }
    public Sprite SprImg { get => m_sprImg; set => m_sprImg = value; }

    // Start is called before the first frame update

    //아이템 코드 : 0x00
    //첫번째 : 아이템 종류
    //세번째 : 아이템 분류/기타템일 경우 아이템번호
    //네번째 : 아이템 번호
    public void ItemSetting(ITEM_TYPE argType, EQUIP_TYPE argEquip, ITEM_GRADE argGrade)
    {
        m_Code = new List<char>();
        m_Code.Add(char.Parse(((int)argType).ToString()));
        m_Code.Add('x');
        m_Code.Add(char.Parse(((int)argEquip).ToString()));
        m_Code.Add(char.Parse(((int)argGrade).ToString()));
    }
    public void CodeSolve()
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
    }
    public void ImageSetting()
    {
        
        switch(m_eType)
        {
            case ITEM_TYPE.ETC:
                m_sprImg = Resources.Load<Sprite>("Item/Etc/etc " + m_iNum.ToString());
                break;
            case ITEM_TYPE.EQUIP:
                m_sprImg = Resources.Load<Sprite>("Item/Weapon/weapon " + ((int)m_eEquipType).ToString() + ((int)m_eGrade).ToString());
                break;
            case ITEM_TYPE.USE:
                m_sprImg = Resources.Load<Sprite>("Item/Use/use " + ((int)m_eEquipType).ToString() + ((int)m_eGrade).ToString());
                break;
        }
    }
}
