using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LobbyUI : MonoBehaviour
{
    // Start is called before the first frame update
    private static LobbyUI m_Instance;
    private Scene m_Scene; //신

    private void Awake() //싱글톤 DontDestroy시 원래 씬으로 돌아왔을때 오브젝트 중복 피하기
    {
        if (m_Instance != null)
        {
            Destroy(gameObject);
            return;
        }
        m_Instance = this;
        DontDestroyOnLoad(gameObject);
    }

    private void Start()
    {
        StartCoroutine(SceneCheck());
    }

    IEnumerator SceneCheck()//스킬 대미지 계산 - 손준호
    {
        while (true)
        {
            m_Scene = SceneManager.GetActiveScene();
            if (m_Scene.name == "Lobby")
                gameObject.SetActive(true);
            else
                gameObject.SetActive(false);

            yield return new WaitForSeconds(0.1f); //0.1초마다 실행
        }
    }
}
