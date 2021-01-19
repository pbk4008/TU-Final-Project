using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using enums;
public class Item : MonoBehaviour
{
    protected List<char> m_Code;
    protected Sprite m_sprImg;
    private ITEM_TYPE m_eType;
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
    // Update is called once per frame
}
