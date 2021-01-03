using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class System_Spawn : MonoBehaviour
{
    private int m_iFloor;
    private int m_iStage;
    [SerializeField]
    private Monster m_Monster;
    [SerializeField]
    private Boss m_Boss;
    private bool m_bMonSpawnCheck;//몬스터 스폰됐는지 안됐는지 체크변수
    private int m_MonNum;//몬스터 번호

    public bool BMonSpawnCheck { get => m_bMonSpawnCheck; set => m_bMonSpawnCheck = value; }

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
                switch (m_iFloor)
                {
                    case 0://층에 따라 몬스터 Num설정
                        if (m_iStage != 4)
                        {
                            m_MonNum = Random.RandomRange(0, 4);
                            //레벨
                            m_Monster.getInfo().setLevel(ref m_Monster.getInfo(), Random.RandomRange(1, 10));
                            m_Monster.setStatus(5, 10, 1.1f, 1);
                        }
                        else
                        {
                            m_Boss.gameObject.SetActive(true);
                            m_Boss.BossSpawn();
                        }
                        break;
                    case 1:
                        m_MonNum = Random.RandomRange(5, 9);
                        m_Monster.getInfo().setLevel(ref m_Monster.getInfo(), Random.RandomRange(11, 20));
                        m_Monster.setStatus(15, 30, 1.2f, 3);
                        break;
                    case 2:
                        m_MonNum = Random.RandomRange(10, 14);
                        m_Monster.getInfo().setLevel(ref m_Monster.getInfo(), Random.RandomRange(21, 30));
                        m_Monster.setStatus(5, 10, 1.1f, 1);
                        break;
                    case 3:
                        m_MonNum = Random.RandomRange(15, 19);
                        m_Monster.getInfo().setLevel(ref m_Monster.getInfo(), Random.RandomRange(31, 40));
                        m_Monster.setStatus(45, 90, 1.4f, 5);
                        break;
                    case 4:
                        m_MonNum = Random.RandomRange(20, 24);
                        m_Monster.getInfo().setLevel(ref m_Monster.getInfo(), Random.RandomRange(41, 50));
                        m_Monster.setStatus(60, 120, 1.5f, 5);
                        break;
                    case 5:
                        m_MonNum = Random.RandomRange(25, 29);
                        m_Monster.getInfo().setLevel(ref m_Monster.getInfo(), Random.RandomRange(51, 70));
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
