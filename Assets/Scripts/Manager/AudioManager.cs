using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

// ����������
public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance;
    private AudioSource bgmSource; // ���� BGM ����Ƶ
    private AudioSource effectSource; // ������Ч

    private void Awake()
    {
        Instance = this;
    }

    // ��ʼ��
    public void Init()
    {
        bgmSource = gameObject.AddComponent<AudioSource>();
        effectSource = gameObject.AddComponent<AudioSource>();
    }

    // ���� BGM
    public void PlayBGM(string name, bool isLoop = true)
    {
        // ���� BGM ����
        AudioClip clip = Resources.Load<AudioClip>("Sounds/BGM/" + name);

        if (clip == null)
        {
            Debug.LogWarning($"BGM clip {name} not found!");
            return;
        }

        bgmSource.clip = clip; // ������Ƶ
        bgmSource.loop = isLoop;
        bgmSource.volume = 0.3f; // ���� BGM ����Ϊ 0.5 ��
        bgmSource.Play();
    }
    // ������Ч���ֶ������Ե���������
    public void PlayEffect(string name, bool isLoop = false)
    {
        // ������Ч
        AudioClip clip = Resources.Load<AudioClip>("Sounds/Effect/" + name);

        if (clip == null)
        {
            Debug.LogWarning($"Effect clip {name} not found!");
            return;
        }

        // ��������Ϊ 2 ��
        effectSource.volume = 8.0f;
        effectSource.clip = clip;
        effectSource.loop = isLoop;
        effectSource.Play();
    }

}
