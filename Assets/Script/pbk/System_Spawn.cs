using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class System_Spawn : MonoBehaviour
{
    private int m_iFloor;
    private int m_iStage;
    [SerializeField]
    private Monster m_Monster;
    private bool m_bMonSpawnCheck;//몬스터 스폰됐는지 안됐는지 체크변수
    private int m_MonNum;//몬스터 번호
    // Start is called before the first frame update
    void Start()
    {
        GameObject GM = GameObject.FindWithTag("GameMgr");
        m_iFloor = GM.GetComponent<Scr_DungeonBtn>().IFloor;
        m_iStage = GM.GetComponent<Scr_DungeonBtn>().IStage;
        m_bMonSpawnCheck = true;
        StartCoroutine(MonsterSpawn());
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private IEnumerator MonsterSpawn()
    {
        while (true)
        {
            if (m_bMonSpawnCheck&&!m_Monster.BLive)
            {
                m_Monster.BLive = true;
                int argLevel=0;
                switch (m_iFloor)
                {
                    case 0://층에 따라 몬스터 Num설정
                        m_MonNum = Random.Range(0, 4);
                        argLevel=Random.Range(1, 10);
                        m_Monster.getInfo().setLevel(ref m_Monster.getInfo(), argLevel);
                        m_Monster.setStatus(5, 10, 1.1f, 1);
                        break;
                    case 1:
                        m_MonNum = Random.Range(5, 9);
                        argLevel = Random.Range(11,20);
                        m_Monster.getInfo().setLevel(ref m_Monster.getInfo(), argLevel);
                        m_Monster.setStatus(15, 30, 1.2f, 3);
                        break;
                    case 2:
                        m_MonNum = Random.Range(10, 14);
                        argLevel = Random.Range(21, 30);
                        m_Monster.getInfo().setLevel(ref m_Monster.getInfo(), argLevel);
                        m_Monster.setStatus(30, 60, 1.3f, 3);
                   
                        break;
                    case 3:
                        m_MonNum = Random.Range(15, 19);
                        argLevel = Random.Range(31, 40);
                        m_Monster.getInfo().setLevel(ref m_Monster.getInfo(), argLevel);
                        m_Monster.setStatus(45, 90, 1.4f, 5);

                        break;
                    case 4:
                        m_MonNum = Random.Range(20, 24);
                        argLevel = Random.Range(41, 50);
                        m_Monster.getInfo().setLevel(ref m_Monster.getInfo(), argLevel);
                        m_Monster.setStatus(60, 120, 1.5f, 5);

                        break;
                    case 5:
                        m_MonNum = Random.Range(25, 29);
                        argLevel = Random.Range(51, 60);
                        m_Monster.getInfo().setLevel(ref m_Monster.getInfo(), argLevel);
                        m_Monster.setStatus(75, 150, 1.6f, 7);
                        break;
                }

                m_Monster.SetInfo(m_MonNum);
                //m_bMonSpawnCheck = false;
            }
            yield return new WaitForSeconds(0.1f);
        }
    }
}
