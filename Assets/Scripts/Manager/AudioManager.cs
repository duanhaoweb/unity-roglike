using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

//����������
public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance;
    private AudioSource bgmSource;//����bgm����Ƶ
    private void Awake()
    {
        Instance = this;
    }
    //��ʼ��
    public void Init()
    {
        bgmSource=gameObject.AddComponent<AudioSource>();
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
    public void PlayEffect(string name)
    {
        AudioClip clip = Resources.Load<AudioClip>("Sounds/Cards/" + name);

        AudioSource.PlayClipAtPoint(clip,transform.position);//����
    }
}
