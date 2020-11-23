using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scr_QuestScript : MonoBehaviour
{
    private int m_iRandomNum;
    private int m_iQuest;
    private int m_iGrade;
    private int m_iCurrentBossCount;
    private int m_iCurrentMonsterCount;
    private int m_iCurrentEtcitemCount;

    private void Click_QuestSign()
    {
        SettingGrade();
    }

    private void SettingGrade()
    {
        m_iRandomNum = Random.Range(0, 100);

        if (m_iRandomNum > 0 && m_iRandomNum <= 60)
            m_iGrade = 1;
        else if (m_iRandomNum > 60 && m_iRandomNum <= 90)
            m_iGrade = 2;
        else if (m_iRandomNum <= 100)
            m_iGrade = 3;
    }


}
