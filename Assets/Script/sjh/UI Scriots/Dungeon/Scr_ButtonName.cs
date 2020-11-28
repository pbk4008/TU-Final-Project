using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scr_ButtonName : MonoBehaviour
{
    [SerializeField] private Scr_DungeonBtn obj_Dungeon;
    public void Set_ButtonName()
    {
        obj_Dungeon.sButtonName = this.gameObject.name;
        obj_Dungeon.sOnClick = true;
    }
}