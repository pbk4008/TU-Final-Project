using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO; // 파일 입출력
using System.Runtime.Serialization.Formatters.Binary; //바이너리 파일 포멧
using DataInfo;
using UnityEngine.SceneManagement;

public class System_DataMgr : MonoBehaviour
{
    [SerializeField]
    private Player m_Player; //플레이어
    [SerializeField]
    private System_LevelUp m_StatCal; //스텟 계산 함수를 사용하기 위해
    private string dataPath; //파일이 저장될 물리적인 경로 및 파일명을 저장할 변수
    void Start()
    {
        m_Player = GameObject.FindWithTag("Player").GetComponent<Player>(); //플레이어 스크립트 가져오기
        m_StatCal = GameObject.FindWithTag("Player").GetComponent<System_LevelUp>();//플레이어 클래스에서 객체를 가지고 있음(by. pbk)
    }
    public void Update()
    {
        if (m_Player.BLive == false) //플레이어가 죽으면
        {
            //persistentDataPath 속성은 파일을 읽고 쓸 수 있는 폴더의 경로를 반환
            dataPath = Application.persistentDataPath + "/gameData.dat"; //파일 경로 지정

            BinaryFormatter bf = new BinaryFormatter(); //바이너리 파일 포맷을 위한 BinaryFormatter 생성
            FileStream file = File.Create(dataPath); //데이터 저장을 위한 파일 생성

            //파일에 저장할 클래스에 데이터 할당
            GameData data = new GameData(); 
            data.IPow = m_Player.getStat().IPow;
            data.IInt = m_Player.getStat().IInt;
            data.IDex = m_Player.getStat().IDex;
            data.IMoney = m_Player.IMoney;
            data.ILevel = m_Player.getInfo().ILevel;

            //BinaryFormatter를 사용해 파일에 데이터 기록
            bf.Serialize(file, data);
            file.Close();

            //죽었으니 저장 후에  플레이어 스텟 초기화
            /*m_Player.getInfo().setLevel(ref m_Player.getInfo(), 1);
            m_Player.getStat().setPow(ref m_Player.getStat(), 5);
            m_Player.getStat().setInt(ref m_Player.getStat(), 5);
            m_Player.getStat().setDex(ref m_Player.getStat(), 5);
            m_Player.IMoney = 10000;*/
            //m_Player.BLive = true;
        }
    }

    public GameData Load() //불러오기
    {
        dataPath = Application.persistentDataPath + "/gameData.dat";
        
        SceneManager.LoadScene(1);
        GameData data = new GameData();
        return data;
        /*
        if (File.Exists(dataPath))
        {
            //파일이 존재할 경우 데이터 불러오기
            BinaryFormatter bf = new BinaryFormatter();
            FileStream file = File.Open(dataPath, FileMode.Open);
            //GameData 클래스에 파일로부터 읽은 데이터를 기록
            GameData data = (GameData)bf.Deserialize(file);
            m_Player.getStat().setPow(ref m_Player.getStat(), data.IPow);
            m_Player.getStat().setInt(ref m_Player.getStat(), data.IInt);
            m_Player.getStat().setDex(ref m_Player.getStat(), data.IDex);
            m_Player.getInfo().setLevel(ref m_Player.getInfo(), data.ILevel);
            m_Player.IMoney = data.IMoney;
            file.Close();
            m_StatCal.CalculStat(); //스텟계산

            return data;
        }
        else
        {
            //파일이 없을 경우 기본값을 반환
            GameData data = new GameData();

            return data;
        }*/
    }
}
