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
    [SerializeField] private GameObject Cvs_StageDungeon6_7; //던전 스테이지 캔버스  ex) 6-6, 4-7
    [SerializeField] private GameObject Cvs_StageDungeon8_10; //던전 스테이지 캔버스  ex) 6-10, 5-8
    [SerializeField] private Text[] T_Stg = new Text[10];      //N-1 텍스트          ex) 6-1

    private int m_iFloor;

    public void Click_DungeonNPC() //던전 NPC 버튼을 클릭 시
    {
        Cvs_DungeonNPC.SetActive(false);  //던전 NPC 캔버스 비활성화
        Cvs_EnterDungeon.SetActive(true); //던전 메뉴 캔버스 활성화
    }

    public void Click_EnterDungeon() //던전 참가 버튼을 클릭 시
    {
        Cvs_EnterDungeon.SetActive(false); //던전 메뉴 캔버스 비활성화
        Cvs_floorDungeon.SetActive(true);  //던전 종류 캔버스 활성화
    }

    public void Click_ExitDungeon() //던전 나가기 버튼을 클릭 시
    {
        Cvs_EnterDungeon.SetActive(false); //던전 참메뉴 캔버스 비활성화
        Cvs_DungeonNPC.SetActive(true);   //던전 NPC 캔버스 활성화
    }

    public void Click_ExitFloorDungeon() //던전 종류 캔버스에서 이전으로 버튼을 클릭 시
    {
        Cvs_floorDungeon.SetActive(false); //던전 종류 캔버스 비활성화
        Cvs_EnterDungeon.SetActive(true); //던전 메뉴 캔버스 활성화
    }

    public void Click_Floor1() //숲 버튼을 눌렀을 경우
    {
        Cvs_floorDungeon.SetActive(false); //던전 종류 캔버스 비활성화 
        Cvs_StageDungeon.SetActive(true); //던전 스테이지 캔버스 활성화
        m_iFloor = 1;
        StageText();
    }

    public void Click_Floor2() //마을 버튼을 눌렀을 경우
    {
        Cvs_floorDungeon.SetActive(false); //던전 종류 캔버스 비활성화 
        Cvs_StageDungeon.SetActive(true); //던전 스테이지 캔버스 활성화
        m_iFloor = 2;
        StageText();
    }

    public void Click_Floor3() //성문 버튼을 눌렀을 경우
    {
        Cvs_floorDungeon.SetActive(false); //던전 종류 캔버스 비활성화 
        Cvs_StageDungeon.SetActive(true); //던전 스테이지 캔버스 활성화
        Cvs_StageDungeon6_7.SetActive(true);
        m_iFloor = 3;
        StageText();
    }

    public void Click_Floor4() //정원 버튼을 눌렀을 경우
    {
        Cvs_floorDungeon.SetActive(false); //던전 종류 캔버스 비활성화 
        Cvs_StageDungeon.SetActive(true); //던전 스테이지 캔버스 활성화
        Cvs_StageDungeon6_7.SetActive(true);
        m_iFloor = 4;
        StageText();
    }

    public void Click_Floor5() //성 내부 버튼을 눌렀을 경우
    {
        Cvs_floorDungeon.SetActive(false); //던전 종류 캔버스 비활성화 
        Cvs_StageDungeon.SetActive(true); //던전 스테이지 캔버스 활성화
        Cvs_StageDungeon6_7.SetActive(true);
        Cvs_StageDungeon8_10.SetActive(true);
        m_iFloor = 5;
        StageText();
    }

    public void Click_Floor6() //알현실 버튼을 눌렀을 경우
    {
        Cvs_floorDungeon.SetActive(false); //던전 종류 캔버스 비활성화 
        Cvs_StageDungeon.SetActive(true); //던전 스테이지 캔버스 활성화
        Cvs_StageDungeon6_7.SetActive(true);
        Cvs_StageDungeon8_10.SetActive(true);
        m_iFloor = 6;
        StageText();
    }

    private void StageText()
    {
        for(int i=0;i<10;i++)
        {
            int iStage = 1 + i;
            T_Stg[i].GetComponent<Text>().text = m_iFloor + " - " + iStage;
        }
    }


    public void Click_ExitStage() //던전 스테이지 캔버스에서 이전으로를 선택했을 경우
    {
        Cvs_StageDungeon.SetActive(false); //던전 스테이지 캔버스 비활성화 
        Cvs_StageDungeon8_10.SetActive(false);
        Cvs_StageDungeon6_7.SetActive(false);
        Cvs_floorDungeon.SetActive(true);   //던전 종류 캔버스 활성화
    }
}