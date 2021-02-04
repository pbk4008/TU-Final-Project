using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class Scr_ButtonName : MonoBehaviour
{
    [SerializeField] private Scr_DungeonBtn obj_Dungeon; //던전 스크립트
    private System_LevelUp m_PlayerLevelUp; //플레이어 레벨업 스크립트
    [SerializeField] private System_PlayerSkill m_PlayerSkill; //플레이어 스킬 스크립트
    private Scene m_Scene; //신

    public void Set_ButtonName() //버튼 이름 저하기
    {
        m_Scene = SceneManager.GetActiveScene(); //씬의 정보를 가져옴
        switch (m_Scene.name) //씬의 이름에 따라 행동함
        {
            case "Lobby":
                obj_Dungeon.sButtonName = this.gameObject.name;  //던전 스크립트에 버튼 이름지정
                obj_Dungeon.sOnClick = true; //버튼을 클릭했다는 정보를 넘겨줌
                break;
            case "Duengeon":
                m_PlayerLevelUp = GameObject.Find("Player").GetComponent<System_LevelUp>();
                m_PlayerLevelUp.sButtonName = this.gameObject.name; //레벨업 스크립트에 버튼 이름지정
                m_PlayerLevelUp.bOnClick = true; //버튼을 클릭했다는 정보를 넘겨줌
                m_PlayerSkill.sButtonName = this.gameObject.name; //스킬 스크립트에 버튼 이름지정
                m_PlayerSkill.bOnClick = true; //버튼을 클릭했다는 정보를 넘겨줌
                break;
        }
    }
}