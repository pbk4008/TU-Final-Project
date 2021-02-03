using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class InvenUI : MonoBehaviour
{
    // Start is called before the first frame update
    private static InvenUI m_Instance;
    Canvas Cvs_inven;

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
        Canvas Cvs_inven = GameObject.Find("InvenCanvas").GetComponent<Canvas>();
        Cvs_inven.enabled = false;
    }
}
