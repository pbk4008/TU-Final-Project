using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using enums;
using UnityEngine.SceneManagement;

public class BattleManager : MonoBehaviour
{
    private GameObject m_Player;
    [SerializeField]
    private GameObject m_Monster;
    private System_Battle m_BattleSystem;
    private System_Spawn m_SpawnSystem;
    [SerializeField]
    private System_BattleBegin m_BattleBegin;
    private BATTLE_CLEAR m_eClear;//던전클리어
    private bool m_bRoundClear;
    private bool m_bClear;
    private Scene m_Scene;

    public bool bClear { get => m_bClear; }
    public bool BRoundClear { get => m_bRoundClear; set => m_bRoundClear = value; }
    public BATTLE_CLEAR EClear { get => m_eClear; set => m_eClear = value; }
    // Start is called before the first frame update
    void Start()
    {
        m_BattleSystem = GetComponent<System_Battle>();
        m_SpawnSystem = GetComponent<System_Spawn>();
        m_Player = GameObject.FindWithTag("Player");
        m_bRoundClear = false;
        m_eClear = BATTLE_CLEAR.END;
        BattleCreate();
    }
    private void BattleCreate()
    {
        m_bClear = m_BattleBegin.bClear;
        if (m_Player.GetComponent<System_LevelUp>().bLevelUp)
            StartCoroutine(BattleMgr());
        else
        {
            m_Player.SetActive(false);
            m_SpawnSystem.enabled = false;
            m_Monster.GetComponent<Monster>().DestroyItem();
            m_BattleBegin.gameObject.SetActive(true);
            m_bRoundClear = false;
            StartCoroutine(BattleMgr());
        }
    }
    IEnumerator BattleMgr()
    {
        m_Scene = SceneManager.GetActiveScene();
        if (m_Player.GetComponent<System_LevelUp>().bLevelUp)
        {
            yield return new WaitForSeconds(2.0f);
            BattleCreate();
        }
        else
        {
            yield return new WaitForSeconds(2.0f);
            m_Player.SetActive(true);
            m_Player.GetComponent<Player>().PlayerActive();
            m_BattleBegin.gameObject.SetActive(false);
            m_SpawnSystem.enabled = true;
            m_SpawnSystem.MonsterSpawn();
            m_BattleSystem.UIInitialize();
            yield return new WaitForSeconds(2.0f);
            m_BattleSystem.BBattle = true;
            yield return new WaitUntil(() => m_bRoundClear);
            yield return new WaitForSeconds(2.0f);
            Scr_DungeonBtn GM = GameObject.FindWithTag("GameMgr").GetComponent<Scr_DungeonBtn>();
            int iFloor = GM.IFloor;
            int iStage = GM.IStage;
            if (m_BattleSystem.BRunStage)
            {
                m_BattleBegin.ICurrentRound--;
                m_BattleSystem.BRunStage = false;
            }
            m_BattleBegin.moveCurrentRoundUI();
            switch (m_eClear)
            {
                case BATTLE_CLEAR.RUN://도망
                    SceneManager.LoadScene(1);
                    break;
                case BATTLE_CLEAR.CLEAR://클리어
                    if (!m_Player.GetComponent<System_LevelUp>().bLevelUp)
                    {
                        m_Player.GetComponent<Player>().IRunCount = 0;
                        AudioSource source = Camera.main.GetComponent<AudioSource>();
                        source.clip = SoundMgr.GetBgm(BGM_TYPE.VICTORY);
                        source.loop = false;
                        source.Play();
                        yield return new WaitForSeconds(source.clip.length);
                        m_BattleSystem.DisableEvent();
                        GM.setbStage(iFloor, iStage + 1, true);
                        SceneManager.LoadScene(1);
                    }
                    break;
                case BATTLE_CLEAR.DIE:
                    yield return new WaitForSeconds(m_Player.GetComponent<AudioSource>().clip.length);
                    m_Player.GetComponent<Player>().ResetPlayer();
                    m_Player.SetActive(false);
                    SceneManager.LoadScene(3);
                    break;
                case BATTLE_CLEAR.END:
                    BattleCreate();
                    break;
            }
        }
    }
}
