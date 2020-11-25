using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour
{
    public enum ANIMTRIGGER { IDLE, HIT, ATTACK, SKILL, DIE, WIN }
    // Start is called before the first frame update
    protected structs.tagInfo m_Info;//기본 정보 구조체
    protected SpriteRenderer m_sprRender;//기본 SpriteRenderer
    protected Sprite m_Sprite;//기본 Sprite
    protected Animator m_Animator;//기본 Animator
    protected AudioSource m_Audio;//기본 Audio;
    protected ANIMTRIGGER m_AnimTrigger;

    public ANIMTRIGGER AnimTrigger { get => m_AnimTrigger; set => m_AnimTrigger = value; }

    protected void tagSetting(string argName, int argLevel, int argAtk, int argMatk, int argMaxHp, int argAtkSpeed, int argDef, float argCriDmg)//기본정보 셋팅
    {
        m_Info.SName = argName;
        m_Info.IAtk = argAtk;
        m_Info.IMatk = argMatk;
        m_Info.IMaxHp = argMaxHp;
        m_Info.ICurrentHp = m_Info.IMaxHp;
        m_Info.IAtkSpeed = m_Info.IAtkSpeed; 
        m_Info.IDef = argDef;
        m_Info.FCri = 0.15f;
        m_Info.FCriDmg = argCriDmg;     
    }
    protected void FSM(ANIMTRIGGER argAnimTrigger)
    {
        m_Animator = gameObject.GetComponent<Animator>();
        switch(argAnimTrigger)
        {
            case ANIMTRIGGER.IDLE:
                m_Animator.SetBool("Anim_Idle", false);
                m_Animator.SetBool("Anim_Attack", false);
                break;
            case ANIMTRIGGER.HIT:
                m_Animator.SetBool("Anim_Idle", true);
                break;
            case ANIMTRIGGER.ATTACK:
                m_Animator.SetBool("Anim_Idle", true);
                m_Animator.SetBool("Anim_Attack", true);
                break;
            case ANIMTRIGGER.SKILL:
                m_Animator.SetBool("Anim_Idle", true);
                break;
            case ANIMTRIGGER.DIE:
                m_Animator.SetBool("Anim_Idle", true);
                break;
            case ANIMTRIGGER.WIN:
                m_Animator.SetBool("Anim_Idle", true);
                break;
        }
    }
}
