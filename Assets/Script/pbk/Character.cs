﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Structs;

public class Character : MonoBehaviour
{

    // Start is called before the first frame update
    protected tagInfo m_Info = new tagInfo();
    protected SpriteRenderer m_sprRender;//기본 SpriteRenderer
    protected Sprite m_Sprite;//기본 Sprite
    protected Animator m_Animator;//기본 Animator
    protected AudioSource m_Audio;//기본 Audio;
    protected enums.ANIMTRIGGER m_AnimTrigger;
    public ref tagInfo getInfo() { return ref m_Info; }
    protected bool m_bLive;//목숨
    public enums.ANIMTRIGGER AnimTrigger { get => m_AnimTrigger; set => m_AnimTrigger = value; }
    public bool BLive { get => m_bLive; set => m_bLive = value; }
    protected void tagSetting(string argName, int argLevel, int argAtk, int argMatk, int argMaxHp, int argAtkSpeed, int argDef, float argCriDmg)//기본정보 셋팅
    {
        
        m_Info.SName = argName;
        m_Info.ILevel = argLevel;
        m_Info.IAtk = argAtk;
        m_Info.IMatk = argMatk;
        m_Info.IMaxHp = argMaxHp;
        m_Info.ICurrentHp = m_Info.IMaxHp;
        m_Info.IAtkSpeed = m_Info.IAtkSpeed; 
        m_Info.IDef = argDef;
        m_Info.FCri = 0.15f;
        m_Info.FCriDmg = argCriDmg;     
    }
    protected IEnumerator FSM()
    {
        m_Animator = gameObject.GetComponent<Animator>();
        while (true)
        {
            switch (m_AnimTrigger)
            {
                case enums.ANIMTRIGGER.IDLE:
                    m_Animator.SetBool("Anim_Idle", false);
                    m_Animator.SetBool("Anim_Attack", false);
                    m_Animator.SetBool("Anim_Hit", false);
                    m_Animator.SetBool("Anim_Buff", false);
                    m_Animator.SetBool("Anim_Win", false);
                    m_Animator.SetBool("Anim_Fail", false);
                    break;
                case enums.ANIMTRIGGER.HIT:
                    m_Animator.SetBool("Anim_Idle", true);
                    m_Animator.SetBool("Anim_Hit", true);
                    break;
                case enums.ANIMTRIGGER.ATTACK:
                    m_Animator.SetBool("Anim_Idle", true);
                    m_Animator.SetBool("Anim_Attack", true);
                    break;
                case enums.ANIMTRIGGER.SKILL:
                    m_Animator.SetBool("Anim_Idle", true);
                    break;
                case enums.ANIMTRIGGER.BUFF:
                    m_Animator.SetBool("Anim_Buff", true);
                    m_Animator.SetBool("Anim_Idle", true);
                    break;
                case enums.ANIMTRIGGER.DIE:
                    m_Animator.SetBool("Anim_Idle", true);
                    m_Animator.SetBool("Anim_Fail", true);
                    break;
                case enums.ANIMTRIGGER.WIN:
                    m_Animator.SetBool("Anim_Idle", true);
                    m_Animator.SetBool("Anim_Win", true);
                    break;
            }
            yield return new WaitForSeconds(0.1f);
        }
    }
}
