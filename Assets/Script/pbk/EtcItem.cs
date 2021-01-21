using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using enums;
public class EtcItem : Item
{
    // Start is called before the first frame update
    
    public void ItemSetting(ITEM_TYPE argType, int argNum)//기타아이템 셋팅
    {
        m_Code = new List<char>();
        m_Code.Add(char.Parse(((int)argType).ToString()));
        m_Code.Add('x');
        EtcSetting(argNum);

        m_sprImg = Resources.Load<Sprite>("Item/Etc/etc " + m_iNum.ToString());
        gameObject.GetComponent<SpriteRenderer>().sprite = m_sprImg;
    }
    private void EtcSetting(int argNum)
    {
        m_iNum = argNum;
        int tmpTen = argNum / 10;
        int tmpOne = argNum % 10;
        m_Code.Add(char.Parse(tmpTen.ToString()));
        m_Code.Add(char.Parse(tmpOne.ToString()));
    }
    
    
    // Update is called once per frame
    
}
