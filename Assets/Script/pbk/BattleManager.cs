using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using enums;
using UnityEngine.SceneManagement;

public class BattleManager : MonoBehaviour
{
    private GameObject m_Player;
    private System_Battle m_BattleSystem;
    private System_Spawn m_SpawnSystem;
    [SerializeField]
    private System_BattleBegin m_BattleBegin;
    private bool m_bClear;//던전클리어
    private bool m_bRoundClear;

    public bool BRoundClear { get => m_bRoundClear; set => m_bRoundClear = value; }
    public bool BClear { get => m_bClear; set => m_bClear = value; }

    // Start is called before the first frame update
    void Start()
    {
        m_BattleSystem = GetComponent<System_Battle>();
        m_SpawnSystem = GetComponent<System_Spawn>();
        m_Player = GameObject.FindWithTag("Player");
        m_bRoundClear = false;
        BattleCreate();
    }
    private void BattleCreate()
    {
        m_Player.SetActive(false);
        m_SpawnSystem.enabled = false;
        m_BattleBegin.gameObject.SetActive(true);
        m_bRoundClear = false;
        StartCoroutine(BattleMgr());
    }
    IEnumerator BattleMgr()
    {
        yield return new WaitForSeconds(2.0f);
        m_Player.SetActive(true);
        m_Player.GetComponent<Player>().PlayerActive();
        m_BattleBegin.gameObject.SetActive(false);
        m_SpawnSystem.enabled = true;
        m_SpawnSystem.MonsterSpawn();
        yield return new WaitForSeconds(2.0f);
        m_BattleSystem.BBattle = true;
        yield return new WaitUntil(() => m_bRoundClear);
        yield return new WaitForSeconds(2.0f);
        m_BattleBegin.moveCurrentRoundUI();
        if (m_bClear)
            SceneManager.LoadScene(1);
        BattleCreate();
    }
}
