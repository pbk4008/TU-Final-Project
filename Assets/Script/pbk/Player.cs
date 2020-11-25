using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Character
{
    // Start is called before the first frame update

    void Start()
    {
        tagSetting("곰돌이",1,3,3,50,1,5,1.1f);//플레이어 셋팅
        m_Animator = GetComponent<Animator>();//플레이어 Animator 셋팅
        m_sprRender = GetComponent<SpriteRenderer>();//플레이어 SpriteRenderer셋팅
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
