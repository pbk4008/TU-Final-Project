using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using enums;
public class EtcItem : Item
{
    // Start is called before the first frame update
    private ETCITEM_TYPE m_eItemType;
    private int m_iCreateScore;
    private bool m_bSelect;

    public ETCITEM_TYPE EItemType { get => m_eItemType; set => m_eItemType = value; }
    public int ICreateScore { get => m_iCreateScore; set => m_iCreateScore = value; }
    public bool BSelect { get => m_bSelect; set => m_bSelect = value; }

    public void ItemSetting(ITEM_TYPE argType, int argNum, bool argBoss)//기타아이템 셋팅
    {
        m_bSelect = false;
        m_Code = new List<char>();
        m_Code.Add(char.Parse(((int)argType).ToString()));
        if (argBoss)
        {
            m_Code.Add('y');
            m_iCreateScore = 5;
            m_eItemType = ETCITEM_TYPE.BOSS;
        }
        else
        {
            m_eItemType = ETCITEM_TYPE.NOR;
            m_iCreateScore = 1;
            m_Code.Add('x');
        }
        EtcSetting(argNum);
    }
    private void EtcSetting(int argNum)
    {
        m_iNum = argNum;
        int tmpTen = argNum / 10;
        int tmpOne = argNum % 10;
        m_Code.Add(char.Parse(tmpTen.ToString()));
        m_Code.Add(char.Parse(tmpOne.ToString()));
    }
    override public void CodeSolve()
    {
        base.CodeSolve();
        if (Code[1] == 'y')
            m_iCreateScore = 5;
        else if (Code[1] == 'x')
            m_iCreateScore = 1;
    }
    
    // Update is called once per frame
    
}
