﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using enums;

public class System_BattleBegin : MonoBehaviour
{
    private List<GameObject> m_RoundUI;
    private int m_iRound;
    private bool m_bClear;
    private int m_iCurrentRound;
    [SerializeField]
    private GameObject m_ORoundUI;
    [SerializeField]
    private GameObject m_OCurrentRoundUI;
    private BattleManager m_BattleMgr;

    public int IRound { get => m_iRound; set => m_iRound = value; }
    public bool bClear { get => m_bClear; }
    public int ICurrentRound { get => m_iCurrentRound; set => m_iCurrentRound = value; }

    // Start is called before the first frame update
    void Start()
    {
        m_bClear = false;
        GameObject GM = GameObject.FindWithTag("GameMgr");
        m_iRound = GM.GetComponent<Scr_DungeonBtn>().IRound;
        m_BattleMgr = GameObject.Find("BattleManager").GetComponent<BattleManager>();
        m_iCurrentRound = 1;
        m_RoundUI = new List<GameObject>();
        InstantiateRoundUI();
        m_OCurrentRoundUI.transform.position = m_RoundUI[0].transform.position;
    }

    // Update is called once per frame
    public void moveCurrentRoundUI()
    {
        m_iCurrentRound++;
        if (m_iCurrentRound >= m_iRound+1)
        {
            m_bClear = true;
            m_BattleMgr.EClear = BATTLE_CLEAR.CLEAR;
            return;
        }
        m_OCurrentRoundUI.transform.position = m_RoundUI[m_iCurrentRound-1].transform.position;
    }
    private void InstantiateRoundUI()
    {
        int tmpindex=0;
        for (int i = 0; i < m_iRound; i++)
        {
            GameObject tmpObj=Instantiate(m_ORoundUI,new Vector3(0,0,0),Quaternion.identity);
            tmpObj.transform.parent = gameObject.transform;
            m_RoundUI.Add(tmpObj);
        }
        foreach(GameObject obj in m_RoundUI)
        {
            UiPositionSetting(obj, tmpindex);
            tmpindex++;
        }
        
    }
    private void UiPositionSetting(GameObject argObj,int argindex)
    {
        Transform argTr = argObj.GetComponent<Transform>();
        Vector3 argPosition = argTr.position;
        argPosition.x -= functions.CalculSpriteHorizontalSize(gameObject)/2;
        argPosition.y -= functions.CalculSpriteVerticalSize(gameObject)/2;
        float hbor= functions.CalculSpriteHorizontalSize(argObj) + functions.CalculSpriteHorizontalSize(gameObject);
        if (m_iRound < 6)
        {
            hbor /= m_iRound+1;
            argPosition.x += (hbor * (argindex + 1)) - functions.CalculSpriteHorizontalSize(argObj)/2;
            argPosition.y += functions.CalculSpriteVerticalSize(gameObject)/2;
        }
        else
        {
            float vbor = functions.CalculSpriteVerticalSize(argObj) + functions.CalculSpriteVerticalSize(gameObject);
            vbor /= 3;
            if (m_iRound==7)//x위치
            {
                if (argindex<4)
                {
                    hbor /= 5; 
                    argPosition.x += (hbor * (argindex + 1)) - functions.CalculSpriteHorizontalSize(argObj)/2;
                    argPosition.y += vbor - functions.CalculSpriteVerticalSize(argObj) / 2;
                }
                else
                {
                    hbor /= 4;
                    argPosition.x += (hbor * (argindex-3)) - functions.CalculSpriteHorizontalSize(argObj) / 2;
                    argPosition.y += vbor * 2 - functions.CalculSpriteVerticalSize(argObj) / 2;
                }

            }
            else if(m_iRound==10)
            {
                hbor /= 6;
                if (argindex < 5)
                {
                    argPosition.x += (hbor * (argindex + 1)) - functions.CalculSpriteHorizontalSize(argObj) / 2;
                    argPosition.y += vbor - functions.CalculSpriteVerticalSize(argObj) / 2;
                }
                else
                {
                    argPosition.x += (hbor * (argindex - 5 + 1)) - functions.CalculSpriteHorizontalSize(argObj) / 2;
                    argPosition.y += vbor * 2 - functions.CalculSpriteVerticalSize(argObj) / 2;
                }
            }
        }
        argTr.position = argPosition;
    }
}
