using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

// 声音管理器
public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance;
    private AudioSource bgmSource; // 播放 BGM 的音频
    private AudioSource effectSource; // 播放音效

    private void Awake()
    {
        Instance = this;
    }

    // 初始化
    public void Init()
    {
        bgmSource = gameObject.AddComponent<AudioSource>();
        effectSource = gameObject.AddComponent<AudioSource>();
    }

    // 播放 BGM
    public void PlayBGM(string name, bool isLoop = true)
    {
        // 加载 BGM 声音
        AudioClip clip = Resources.Load<AudioClip>("Sounds/BGM/" + name);

        if (clip == null)
        {
            Debug.LogWarning($"BGM clip {name} not found!");
            return;
        }

        bgmSource.clip = clip; // 设置音频
        bgmSource.loop = isLoop;
        bgmSource.volume = 0.3f; // 设置 BGM 音量为 0.5 倍
        bgmSource.Play();
    }
    // 播放音效（手动播放以调整音量）
    public void PlayEffect(string name, bool isLoop = false)
    {
        // 加载音效
        AudioClip clip = Resources.Load<AudioClip>("Sounds/Effect/" + name);

        if (clip == null)
        {
            Debug.LogWarning($"Effect clip {name} not found!");
            return;
        }

        // 设置音量为 2 倍
        effectSource.volume = 8.0f;
        effectSource.clip = clip;
        effectSource.loop = isLoop;
        effectSource.Play();
    }

}
