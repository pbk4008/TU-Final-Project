using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class Sound : MonoBehaviour
{
    private AudioSource sound;

    // Start is called before the first frame update
    
    public void SoundSetting(GameObject target, AudioClip clip)
    {
        if (target.GetComponent<AudioSource>() == null)
            target.AddComponent<AudioSource>();
        sound = target.GetComponent<AudioSource>();
        if (clip == null)
        {
            sound.clip = null;
            return;
        }
        //사운드 가져오기
        sound.clip = clip;
        sound.PlayOneShot(sound.clip);
        Debug.Log("소리출력!!");
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
