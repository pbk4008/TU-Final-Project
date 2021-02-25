using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using enums;
public class SoundMgr : MonoBehaviour
{
    private static List<AudioClip> m_EffectSoundList;
    private static List<AudioClip> m_BGMList;
    // Start is called before the first frame update
    void Start()
    {
        m_EffectSoundList = new List<AudioClip>();
        m_BGMList = new List<AudioClip>();
        AudioClip audio;
        Debug.Log((int)SOUND_TYPE.END);
        for (int i = 0; i < (int)SOUND_TYPE.END; i++)
        {
            audio = Resources.Load<AudioClip>("AudioClip/audio " + i);
            m_EffectSoundList.Add(audio);
            Debug.Log(m_EffectSoundList[i]);
        }
        for(int j=0; j<(int)BGM_TYPE.END; j++)
        {
            audio = Resources.Load<AudioClip>("AudioClip/BGM " + j);
            m_BGMList.Add(audio);
        }

    }
    public static AudioClip GetAudio(SOUND_TYPE audioType)
    {
        return m_EffectSoundList[(int)audioType];
    }
    public static AudioClip GetBgm(BGM_TYPE audioType)
    {
        return m_BGMList[(int)audioType];
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
