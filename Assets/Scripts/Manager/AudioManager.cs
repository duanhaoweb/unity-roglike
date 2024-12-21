using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Unity.VisualScripting;
using UnityEngine;

//����������
public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance;
    private AudioSource bgmSource;//����bgm����Ƶ
    private AudioSource effectSource;//������Ч
    private void Awake()
    {
        Instance = this;
    }
    //��ʼ��
    public void Init()
    {
        bgmSource=gameObject.AddComponent<AudioSource>();
        effectSource = gameObject.AddComponent<AudioSource>();
    }
    public void PlayBGM(string name,bool isLoop =true)
    {
        //����bgm����
        AudioClip clip= Resources.Load<AudioClip>("Sounds/BGM/"+name);   

        bgmSource.clip = clip;//������Ƶ
        bgmSource.loop = isLoop;
        bgmSource.Play();

    }
    //������Ч
    public void PlayEffect(string name, bool isLoop = false)
    {
        AudioClip clip = Resources.Load<AudioClip>("Sounds/BGM/" + name);
        effectSource.clip = clip;//������Ƶ
        effectSource.loop = isLoop;
        

        AudioSource.PlayClipAtPoint(effectSource.clip,transform.position);//����
    }
}
