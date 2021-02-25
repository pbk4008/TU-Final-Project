using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.IO; // 파일 입출력
using System.Runtime.Serialization.Formatters.Binary; //바이너리 파일 포멧
using DataInfo;
using Structs;
using enums;

public class Player : Character
{
    // Start is called before the first frame update
    private tagStat m_Stat;
    [SerializeField]
    private Transform tr;

    private int m_iMoney;
    private float m_fExp;
    private int m_iRunCount;
    private bool m_bSoundCheck;//사운드 들렸는지 안들렸는지 판단
    public ref tagStat getStat() { return ref m_Stat; }
    private bool m_bButtonClick;//버튼 클릭했는지 안했는지 판단
    private bool[] m_bDeeffect = new bool[3]; //지속딜 받기 - 손준호
    private bool m_bStun; //스턴 - 손준호
    private GameObject m_Player;
      
    public int IMoney { get => m_iMoney; set => m_iMoney = value; }
    public float FExp { get => m_fExp; set => m_fExp = value; }
    public bool BButtonClick { get => m_bButtonClick; set => m_bButtonClick = value; }
    public bool bStun { get => m_bStun; set => m_bStun = value; }
    public System_LevelUp LevelUp_System { get => m_LevelUp_System; set => m_LevelUp_System = value; }
    public int IRunCount { get => m_iRunCount; set => m_iRunCount = value; }
    public Equipment Equipment { get => m_Equipment; set => m_Equipment = value; }
    public bool BSoundCheck { get => m_bSoundCheck; set => m_bSoundCheck = value; }

    private System_LevelUp m_LevelUp_System;
    [SerializeField]
    private Equipment m_Equipment;

    private void Awake() //싱글톤 DontDestroy시 원래 씬으로 돌아왔을때 오브젝트 중복 피하기
      {
        if (m_Instance != null)
        {
            Destroy(gameObject);
            return;
        }
        m_Instance = this;
        DontDestroyOnLoad(m_Instance);
    }

    void Start()
    {
        m_bButtonClick = false;
        m_bSoundCheck = false;
        m_bLive = true;
        m_iRunCount = 0;
        tagSetting("곰돌이", 100, 3, 3, 50, 1, 5, 1.1f);//플레이어 셋팅
        statSetting();
        m_AnimTrigger = ANIMTRIGGER.IDLE;
        m_iMoney = 10000;
        m_Animator = GetComponent<Animator>();//플레이어 Animator 셋팅
        m_sprRender = GetComponent<SpriteRenderer>();//플레이어 SpriteRenderer셋팅
        m_Audio = GetComponent<AudioSource>();//플레이어 AudioSource셋팅
        m_vfirstZone = gameObject.transform.localPosition;
        tr = m_Instance.GetComponent<Transform>();
        m_LevelUp_System = GetComponent<System_LevelUp>();
        m_Equipment.EquipSetting();
        m_Equipment.EquipPlusWeaponStat();
    }
    public void PlayerActive()
    {
        StartCoroutine(this.FSM());
    }
    // Update is called once per frame
    void Update()
    {
        m_Scene = SceneManager.GetActiveScene();
        switch (m_Scene.name)
        {
            case "Lobby":
                m_sprRender.enabled = false;
                break;
            case "Duengeon":
                m_sprRender.enabled = true;
                break;
        }
    }
    private void statSetting()
    {
        m_Stat.IPow = 5;
        m_Stat.IDex = 5;
        m_Stat.IInt = 5;
        m_Stat.IStat = 3;
    }
    protected override IEnumerator FSM() 
    {
        Sound sound = new Sound();
        while (true)
        {
            switch (m_AnimTrigger)
            {
                case ANIMTRIGGER.IDLE:
                    tr.position = m_vfirstZone;
                    sound.SoundSetting(gameObject, null);

                    break;
                case ANIMTRIGGER.ATTACK:
                    GameObject target = GameObject.FindWithTag("Monster");
                    if (m_bSoundCheck)
                    {
                        sound.SoundSetting(gameObject, SoundMgr.GetAudio(SOUND_TYPE.HIT));
                        m_bSoundCheck = false;
                    }
                    tr.position = target.transform.position;
                    break;    
                case ANIMTRIGGER.SKILL:
                    break;
                case ANIMTRIGGER.DIE:
                    if (m_bSoundCheck)
                    {
                        sound.SoundSetting(gameObject, SoundMgr.GetAudio(SOUND_TYPE.DIE));
                        m_bSoundCheck = false;
                    }
                        
                    break;
                case ANIMTRIGGER.HIT:
                    break;
                case ANIMTRIGGER.WIN:
                    if (m_bSoundCheck)
                    {
                        sound.SoundSetting(gameObject, SoundMgr.GetAudio(SOUND_TYPE.WIN));                        m_bSoundCheck = false;
                    }
                    break;
                case ANIMTRIGGER.VICTORY:
                    break;

            }
            StartCoroutine(base.FSM());
            yield return new WaitForSeconds(0.1f);
        }
    }
    public void MoneySet(int argMoney)
    { 
        if (m_iMoney + argMoney < 0)
        {
            Debug.Log("돈이 없어 살 수 가 없습니다.");
        }
        else
            m_iMoney += argMoney;
    }
    //손준호 작업
    private static Player m_Instance;
    private Scene m_Scene;

    public bool GetDeEffect(int argint)
    {
        return m_bDeeffect[argint];
    }
    public void SetDeEffect(int argint, bool argbool)
    {
        m_bDeeffect[argint] = argbool;
    }
}