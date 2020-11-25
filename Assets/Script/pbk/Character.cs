using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour
{
    // Start is called before the first frame update
    protected tag.tagInfo m_Info;//기본 정보 구조체
    protected SpriteRenderer m_sprRender;//기본 SpriteRenderer
    protected Sprite m_Sprite;//기본 Sprite
    protected Animator m_Animator;//기본 Animator
    protected AudioSource m_Audio;//기본 Audio;
    
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
}
