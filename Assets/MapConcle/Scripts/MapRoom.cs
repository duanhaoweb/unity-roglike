using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public enum RoomType
{
    //既定
    Start,//开始房间0
    Boss,// 出口房间1
    //限定
    Camp,//营地房间2
    Treasure,// 宝藏房间3
    Tool,// 工匠房间4
    Church,// 祭坛房间5
    Back,//回城房间6
    //随机
    Normal,// 普通房间7
    Shop,//商店房间8
    Thing//事件房间9

}

/// <summary>
/// 地图房间
/// </summary>
public class MapRoom : MonoBehaviour
{
    //动画缩放
    public float scaleDuration = 1f; // 一次放大或缩小过程持续的时间，单位：秒，可以在Unity编辑器中调整该值
    public float scaleFactor = 1.5f; // 缩放倍数，例如1.5表示放大到原来的1.5倍，可根据需要调整
    public float cycleDuration = 2f; // 一个完整的放大缩小周期时长，单位：秒，同样可在编辑器中修改
    private DG.Tweening.Sequence scaleSequence; // 用于存储动画序列，方便后续控制

    public List<MapRoom> connectRooms;//需要连线的房间

    public Sprite[] sprites;
    public float posX;
    public float posY;
    public int type;
    public bool layerchoose = false;
    public bool roomchoose = false;
    public int roomi;
    public int roomj;
    private void Awake()
    {
        if (MapManager.roomtype == 1)
        {
            type = 0;
            GetComponent<SpriteRenderer>().sprite = sprites[type];
        }
        else if (MapManager.roomtype == 2)
        {
            type = 1;
            GetComponent<SpriteRenderer>().sprite = sprites[type];
        }
        else if (MapManager.roomtype == 3) 
        {
            type = 2;
            GetComponent<SpriteRenderer>().sprite = sprites[type];
        }
        else if (MapManager.roomtype == 4)
        {
            type = JudgeType();
            GetComponent<SpriteRenderer>().sprite = sprites[type];
        }
    }

    private void Start()
    {
        
        if (layerchoose&&roomchoose)
        {
            scaleSequence = DOTween.Sequence();

            // 先执行放大动画
            scaleSequence.Append(transform.DOScale(transform.localScale * scaleFactor, scaleDuration));

            // 接着执行缩小动画，回到原始大小
            scaleSequence.Append(transform.DOScale(transform.localScale, scaleDuration));

            // 设置整个序列循环播放，循环方式采用Restart模式，即每次循环都重新开始整个序列的动画
            scaleSequence.SetLoops(-1, LoopType.Restart);

            // 设置整个动画序列的周期时长，通过调整时间缩放来实现
            scaleSequence.timeScale = cycleDuration / (scaleDuration * 2);
        }
    }
    private void OnMouseEnter()
    {
        if (layerchoose && roomchoose)
        {
            scaleSequence.Pause();
            transform.DOBlendableScaleBy(Vector3.one * 0.5f, 0.2f).SetEase(Ease.OutSine);
        }
    }

    private void OnMouseExit()
    {
        if (layerchoose && roomchoose)
        {
            scaleSequence.Play();
        }
    }

    private void OnMouseDown()
    {
        if (layerchoose && roomchoose)
        {
            MapManager.Chooselayer++;
            MapManager.Chooseroom = roomj;
            if (type == 7) 
            {
                SceneManager.LoadScene(0);
            }
            else
            {
                SceneManager.LoadScene("Temple");
            }
        }
    }

    public int JudgeType ()
    {
        int num = Random.Range(1, 101);
        if (num <=  70)
        {
            return 7;
        }
        else if (num <= 80 && num > 70)
        {
            return 8;
        }
        else if (num <= 90 && num > 80)
        {
            return 9;
        }
        else if (num > 90)
        {
            int num_special = Random.Range(1, 101);
            if (num_special <= 60 && num_special > 50)
            {
                return 2;
            }
            else if (num_special <= 50)
            {
                return 3;
            }
            else if (num_special <= 80 && num_special > 60)
            {
                return 4;
            }
            else if (num_special >= 85)
            {
                return 5;
            }
            else if (num_special <= 85 && num_special > 80)
            {
                return 6;
            }
        }
       return -1;
    }
}
