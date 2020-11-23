using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Scr_DungeonBtn : MonoBehaviour
{
    [SerializeField] private GameObject Cvs_DungeonNPC;   //던전 NPC의 캔버스
    [SerializeField] private GameObject Cvs_EnterDungeon; //던전 메뉴 캔버스        ex) 던전 참가, 던전 나가기
    [SerializeField] private GameObject Cvs_floorDungeon;  //던전 종류 캔버스        ex) 숲, 마을, 성문
    [SerializeField] private GameObject Cvs_StageDungeon; //던전 스테이지 캔버스  ex) 6-1, 1-5 
    [SerializeField] private Text T_Stg1; //N-1 텍스트          ex) 6-1
    [SerializeField] private Text T_Stg2; //N-2                   ex) 4-2
    [SerializeField] private Text T_Stg3; //N-3
    [SerializeField] private Text T_Stg4; //N-4
    [SerializeField] private Text T_Stg5; //N-5

    private void Click_DungeonNPC() //던전 NPC 버튼을 클릭 시
    {
        Cvs_DungeonNPC.SetActive(false);  //던전 NPC 캔버스 비활성화
        Cvs_EnterDungeon.SetActive(true); //던전 메뉴 캔버스 활성화
    }

    private void Click_EnterDungeon() //던전 참가 버튼을 클릭 시
    {
        Cvs_EnterDungeon.SetActive(false); //던전 메뉴 캔버스 비활성화
        Cvs_floorDungeon.SetActive(true);  //던전 종류 캔버스 활성화
    }

    private void Click_ExitDungeon() //던전 나가기 버튼을 클릭 시
    {
        Cvs_EnterDungeon.SetActive(false); //던전 참메뉴 캔버스 비활성화
        Cvs_DungeonNPC.SetActive(true);   //던전 NPC 캔버스 활성화
    }

    private void Click_ExitFloorDungeon() //던전 종류 캔버스에서 이전으로 버튼을 클릭 시
    {
        Cvs_floorDungeon.SetActive(false); //던전 종류 캔버스 비활성화
        Cvs_EnterDungeon.SetActive(true); //던전 메뉴 캔버스 활성화
    }

    private void Click_Floor1() //숲 버튼을 눌렀을 경우
    {
        T_Stg1.GetComponent<Text>().text = "1-1"; //1-1, 1-2 같은 텍스트를 출력함          
        T_Stg2.GetComponent<Text>().text = "1-2";
        T_Stg3.GetComponent<Text>().text = "1-3";            
        T_Stg4.GetComponent<Text>().text = "1-4";
        T_Stg5.GetComponent<Text>().text = "1-5";             
        //T_Stg6.GetComponent<Text>().text = "1-6";

        Cvs_floorDungeon.SetActive(false); //던전 종류 캔버스 비활성화 
        Cvs_StageDungeon.SetActive(true); //던전 스테이지 캔버스 활성화
    }

    private void Click_Floor2() //마을 버튼을 눌렀을 경우
    {
        T_Stg1.GetComponent<Text>().text = "2-1";
        T_Stg2.GetComponent<Text>().text = "2-2";
        T_Stg3.GetComponent<Text>().text = "2-3";
        T_Stg4.GetComponent<Text>().text = "2-4";
        T_Stg5.GetComponent<Text>().text = "2-5";
        //T_Stg6.GetComponent<Text>().text = "2-6";

        Cvs_floorDungeon.SetActive(false); //던전 종류 캔버스 비활성화 
        Cvs_StageDungeon.SetActive(true); //던전 스테이지 캔버스 활성화
    }

    private void Click_Floor3() //성문 버튼을 눌렀을 경우
    {
        T_Stg1.GetComponent<Text>().text = "3-1";
        T_Stg2.GetComponent<Text>().text = "3-2";
        T_Stg3.GetComponent<Text>().text = "3-3";
        T_Stg4.GetComponent<Text>().text = "3-4";
        T_Stg5.GetComponent<Text>().text = "3-5";
        //T_Stg6.GetComponent<Text>().text = "3-6";

        Cvs_floorDungeon.SetActive(false); //던전 종류 캔버스 비활성화 
        Cvs_StageDungeon.SetActive(true); //던전 스테이지 캔버스 활성화
    }

    private void Click_Floor4() //정원 버튼을 눌렀을 경우
    {
        T_Stg1.GetComponent<Text>().text = "4-1";
        T_Stg2.GetComponent<Text>().text = "4-2";
        T_Stg3.GetComponent<Text>().text = "4-3";
        T_Stg4.GetComponent<Text>().text = "4-4";
        T_Stg5.GetComponent<Text>().text = "4-5";
        //T_Stg6.GetComponent<Text>().text = "4-6";

        Cvs_floorDungeon.SetActive(false); //던전 종류 캔버스 비활성화 
        Cvs_StageDungeon.SetActive(true); //던전 스테이지 캔버스 활성화
    }

    private void Click_Floor5() //성 내부 버튼을 눌렀을 경우
    {
        T_Stg1.GetComponent<Text>().text = "5-1";
        T_Stg2.GetComponent<Text>().text = "5-2";
        T_Stg3.GetComponent<Text>().text = "5-3";
        T_Stg4.GetComponent<Text>().text = "5-4";
        T_Stg5.GetComponent<Text>().text = "5-5";
        //T_Stg6.GetComponent<Text>().text = "5-6";

        Cvs_floorDungeon.SetActive(false); //던전 종류 캔버스 비활성화 
        Cvs_StageDungeon.SetActive(true); //던전 스테이지 캔버스 활성화
    }

    private void Click_Floor6() //알현실 버튼을 눌렀을 경우
    {
        T_Stg1.GetComponent<Text>().text = "6-1";
        T_Stg2.GetComponent<Text>().text = "6-2";
        T_Stg3.GetComponent<Text>().text = "6-3";
        T_Stg4.GetComponent<Text>().text = "6-4";
        T_Stg5.GetComponent<Text>().text = "6-5";
        //T_Stg6.GetComponent<Text>().text = "6-6";

        Cvs_floorDungeon.SetActive(false); //던전 종류 캔버스 비활성화 
        Cvs_StageDungeon.SetActive(true); //던전 스테이지 캔버스 활성화
    }


    private void Click_ExitFloor() //던전 스테이지 캔버스에서 이전으로를 선택했을 경우
    {
        Cvs_StageDungeon.SetActive(false); //던전 스테이지 캔버스 비활성화 
        Cvs_floorDungeon.SetActive(true);   //던전 종류 캔버스 활성화
    }
}
