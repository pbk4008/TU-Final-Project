using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class Scr_ButtonName : MonoBehaviour
{
    [SerializeField] private Scr_DungeonBtn obj_Dungeon;
    [SerializeField] private Player m_Player;
    private Scene m_Scene;
    GameObject PlayerMgr;

    public void Set_ButtonName()
    {
        m_Scene = SceneManager.GetActiveScene();
        if (m_Scene.name == "Duengeon")
        {
            PlayerMgr = GameObject.Find("Player");
            m_Player = PlayerMgr.GetComponent<Player>();
        }

        switch (m_Scene.name)
        {
            case "Lobby":
                obj_Dungeon.sButtonName = this.gameObject.name;
                obj_Dungeon.sOnClick = true;
                break;
            case "Duengeon":
                m_Player.sButtonName = this.gameObject.name;
                m_Player.bOnClick = true;
                break;
        }
    }
}