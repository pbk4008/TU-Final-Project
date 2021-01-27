using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FadeOut : MonoBehaviour
{
    public float m_fFadeTime = 1.5f; // Fade효과 재생시간

    Text m_tfadetext;

    Color m_cTextColor;

    float m_fstart;

    float m_fend;

    float m_ftime = 0f;
    public float ColorA { get => m_cTextColor.a; set => m_cTextColor.a = value; }
    void Start()
    {
        m_tfadetext = GetComponent<Text>();
        m_cTextColor = m_tfadetext.color;
        m_fstart = 1f;
        m_fend = 0f;
        StartCoroutine("fadeoutplay");    //코루틴 실행
    }
    public void reStartCoroutine()
    {
        m_cTextColor.a = 1f;
        StartCoroutine("fadeoutplay");
    }
    IEnumerator fadeoutplay()
    {
        m_ftime = 0f;

        m_cTextColor.a = Mathf.Lerp(m_fstart, m_fend, m_ftime);


        while (m_cTextColor.a > 0f)
        {

            m_ftime += Time.deltaTime / m_fFadeTime;

            m_cTextColor.a = Mathf.Lerp(m_fstart, m_fend, m_ftime);

            m_tfadetext.color = m_cTextColor;

            yield return null;
        }
    }
}
