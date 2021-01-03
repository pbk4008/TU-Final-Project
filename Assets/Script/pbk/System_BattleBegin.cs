using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class System_BattleBegin : MonoBehaviour
{
    private List<GameObject> m_RoundUI;
    private int m_iRound;
    private int m_iCurrentRound;
    [SerializeField]
    private GameObject m_ORoundUI;
    [SerializeField]
    private GameObject m_OCurrentRoundUI;
    // Start is called before the first frame update
    void Start()
    {
        GameObject GM = GameObject.FindWithTag("GameMgr");
        m_iRound = GM.GetComponent<Scr_DungeonBtn>().IRound;
        m_RoundUI = new List<GameObject>();
        InstantiateRoundUI();
        m_OCurrentRoundUI.transform.position = m_RoundUI[0].transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        
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
        argPosition.x -= CalculSpriteHorizontalSize(gameObject)/2;
        argPosition.y -= CalculSpriteVerticalSize(gameObject)/2;
        float hbor= CalculSpriteHorizontalSize(argObj) + CalculSpriteHorizontalSize(gameObject);
        if (m_iRound < 6)
        {
            hbor /= m_iRound+1;
            argPosition.x += (hbor * (argindex + 1)) - CalculSpriteHorizontalSize(argObj)/2;
            argPosition.y += CalculSpriteVerticalSize(gameObject)/2;
        }
        else
        {
            float vbor = CalculSpriteVerticalSize(argObj) + CalculSpriteVerticalSize(gameObject);
            vbor /= 3;
            if (m_iRound==7)//x위치
            {
                if (argindex<4)
                {
                    hbor /= 5; 
                    argPosition.x += (hbor * (argindex + 1)) - CalculSpriteHorizontalSize(argObj)/2;
                    argPosition.y += vbor - CalculSpriteVerticalSize(argObj) / 2;
                }
                else
                {
                    hbor /= 4;
                    argPosition.x += (hbor * (argindex-3)) - CalculSpriteHorizontalSize(argObj) / 2;
                    argPosition.y += vbor * 2 - CalculSpriteVerticalSize(argObj) / 2;
                }

            }
            else if(m_iRound==10)
            {
                hbor /= 6;
                if (argindex < 5)
                {
                    argPosition.x += (hbor * (argindex + 1)) - CalculSpriteHorizontalSize(argObj) / 2;
                    argPosition.y += vbor - CalculSpriteVerticalSize(argObj) / 2;
                }
                else
                {
                    argPosition.x += (hbor * (argindex - 5 + 1)) - CalculSpriteHorizontalSize(argObj) / 2;
                    argPosition.y += vbor * 2 - CalculSpriteVerticalSize(argObj) / 2;
                }
            }
        }
        argTr.position = argPosition;
    }
    private float CalculSpriteHorizontalSize(GameObject argObj)
    {
        return argObj.GetComponent<SpriteRenderer>().sprite.bounds.size.x * argObj.GetComponent<Transform>().localScale.x;
    }
    private float CalculSpriteVerticalSize(GameObject argObj)
    {
        return argObj.GetComponent<SpriteRenderer>().sprite.bounds.size.y * argObj.GetComponent<Transform>().localScale.y;
    }
}
