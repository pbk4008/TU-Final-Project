using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Scr_DungeonBtn : MonoBehaviour
{
    [SerializeField] private GameObject Cvs_DungeonNPC;
    [SerializeField] private GameObject Cvs_EnterDungeon;
    [SerializeField] private GameObject Cvs_floorDungeon;
    [SerializeField] private GameObject Cvs_StageDungeon;
    [SerializeField] private Text T_Stg1;
    [SerializeField] private Text T_Stg2;
    [SerializeField] private Text T_Stg3;
    [SerializeField] private Text T_Stg4;
    [SerializeField] private Text T_Stg5;

    private void Click_DungeonNPC()
    {
        Cvs_DungeonNPC.SetActive(false);
        Cvs_EnterDungeon.SetActive(true);
    }

    private void Click_EnterDungeon()
    {
        Cvs_floorDungeon.SetActive(true);
        Cvs_EnterDungeon.SetActive(false);
    }

    private void Click_ExitDungeon()
    {
        Cvs_DungeonNPC.SetActive(true);
        Cvs_EnterDungeon.SetActive(false);
    }

    private void Click_ExitFloorDungeon()
    {
        Cvs_floorDungeon.SetActive(false);
        Cvs_EnterDungeon.SetActive(true);
    }

    private void Click_Floor1()
    {
        T_Stg1.GetComponent<Text>().text = "1-1";
        T_Stg2.GetComponent<Text>().text = "1-2";
        T_Stg3.GetComponent<Text>().text = "1-3";
        T_Stg4.GetComponent<Text>().text = "1-4";
        T_Stg5.GetComponent<Text>().text = "1-5";
        //T_Stg6.GetComponent<Text>().text = "1-6";
        Cvs_floorDungeon.SetActive(false);
        Cvs_StageDungeon.SetActive(true);
    }

    private void Click_Floor2()
    {
        T_Stg1.GetComponent<Text>().text = "2-1";
        T_Stg2.GetComponent<Text>().text = "2-2";
        T_Stg3.GetComponent<Text>().text = "2-3";
        T_Stg4.GetComponent<Text>().text = "2-4";
        T_Stg5.GetComponent<Text>().text = "2-5";
        //T_Stg6.GetComponent<Text>().text = "2-6";
        Cvs_floorDungeon.SetActive(false);
        Cvs_StageDungeon.SetActive(true);
    }

    private void Click_Floor3()
    {
        T_Stg1.GetComponent<Text>().text = "3-1";
        T_Stg2.GetComponent<Text>().text = "3-2";
        T_Stg3.GetComponent<Text>().text = "3-3";
        T_Stg4.GetComponent<Text>().text = "3-4";
        T_Stg5.GetComponent<Text>().text = "3-5";
        //T_Stg6.GetComponent<Text>().text = "3-6";
        Cvs_floorDungeon.SetActive(false);
        Cvs_StageDungeon.SetActive(true);
    }

    private void Click_Floor4()
    {
        T_Stg1.GetComponent<Text>().text = "4-1";
        T_Stg2.GetComponent<Text>().text = "4-2";
        T_Stg3.GetComponent<Text>().text = "4-3";
        T_Stg4.GetComponent<Text>().text = "4-4";
        T_Stg5.GetComponent<Text>().text = "4-5";
        //T_Stg6.GetComponent<Text>().text = "4-6";
        Cvs_floorDungeon.SetActive(false);
        Cvs_StageDungeon.SetActive(true);
    }

    private void Click_Floor5()
    {
        T_Stg1.GetComponent<Text>().text = "5-1";
        T_Stg2.GetComponent<Text>().text = "5-2";
        T_Stg3.GetComponent<Text>().text = "5-3";
        T_Stg4.GetComponent<Text>().text = "5-4";
        T_Stg5.GetComponent<Text>().text = "5-5";
        //T_Stg6.GetComponent<Text>().text = "5-6";
        Cvs_floorDungeon.SetActive(false);
        Cvs_StageDungeon.SetActive(true);
    }

    private void Click_Floor6()
    {
        T_Stg1.GetComponent<Text>().text = "6-1";
        T_Stg2.GetComponent<Text>().text = "6-2";
        T_Stg3.GetComponent<Text>().text = "6-3";
        T_Stg4.GetComponent<Text>().text = "6-4";
        T_Stg5.GetComponent<Text>().text = "6-5";
        //T_Stg6.GetComponent<Text>().text = "6-6";
        Cvs_floorDungeon.SetActive(false);
        Cvs_StageDungeon.SetActive(true);
    }


    private void Click_ExitFloor()
    {
        Cvs_floorDungeon.SetActive(true);
        Cvs_StageDungeon.SetActive(false);
    }
}
