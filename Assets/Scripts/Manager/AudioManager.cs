using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Unity.VisualScripting;
using UnityEngine;

//声音管理器
public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance;
    private AudioSource bgmSource;//播放bgm的音频
    private AudioSource effectSource;//播放音效
    private void Awake()
    {
        Instance = this;
    }
    //初始化
    public void Init()
    {
        bgmSource=gameObject.AddComponent<AudioSource>();
        effectSource = gameObject.AddComponent<AudioSource>();
    }
    public void PlayBGM(string name,bool isLoop =true)
    {
        //加载bgm声音
        AudioClip clip= Resources.Load<AudioClip>("Sounds/BGM/"+name);   

        bgmSource.clip = clip;//设置音频
        bgmSource.loop = isLoop;
        bgmSource.Play();

    }
    //播放音效
    public void PlayEffect(string name, bool isLoop = false)
    {
        AudioClip clip = Resources.Load<AudioClip>("Sounds/BGM/" + name);
        effectSource.clip = clip;//设置音频
        effectSource.loop = isLoop;
        

        AudioSource.PlayClipAtPoint(effectSource.clip,transform.position);//播放
    }
}
