using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

/// <summary>
/// 地图房间
/// </summary>
public class MapRoom : MonoBehaviour
{
    //public int roomID;
    public List<MapRoom> connectRooms;//需要连线的房间
    public Sprite[] sprites;
    public float posX;
    public float posY;
    public int type;

    private void Awake()
    {
        type = Random.Range(0, sprites.Length);
        GetComponent<SpriteRenderer>().sprite = sprites[type];
    }

    private void OnMouseEnter()
    {
        transform.DOBlendableScaleBy(Vector3.one * 0.5f, 0.2f).SetEase(Ease.OutSine);
    }

    private void OnMouseExit()
    {
        transform.DOBlendableScaleBy(Vector3.one * -0.5f, 0.2f).SetEase(Ease.OutSine);
    }

    private void OnMouseDown()
    {
        SceneManager.LoadScene("Temple");
    }
}
