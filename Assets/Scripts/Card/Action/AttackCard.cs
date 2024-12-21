using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
public class AttackCard : ActionCard, IPointerDownHandler
{
    private IEnumerator OnMouseDownRight(PointerEventData data)
    {
        while(true)
        {
            //若再次按下右键跳出循环
            if(Input.GetMouseButtonDown(1))
            {
                break;
            }
            Vector2 pos;
            if (RectTransformUtility.ScreenPointToLocalPointInRectangle(
                transform.parent.GetComponent<RectTransform>(),
                data.position,
                data.pressEventCamera,
                out pos
                ))
            {
                //进行射线检测是否碰到怪物
                CheckRayToEnemy();
            }

            yield return null;
        }
        //跳出循环后显示鼠标
        Cursor.visible = true;
    }
    Enemy hitEnemy;//被检测到的敌人
    private void CheckRayToEnemy()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        RaycastHit hit;
        if(Physics.Raycast(ray, out hit,10000,LayerMask.GetMask("Enemy")))
        {
            hitEnemy = hit.transform.GetComponent<Enemy>();
            hitEnemy.OnSelect();
            //按左键攻击
            if (Input.GetMouseButtonDown(0))
            {
                //关闭所有协同程序
                StopAllCoroutines();
                //显示鼠标
                Cursor.visible = true;

                if (UseCard() == true)
                {
                    UIManager.Instance.ShowTip("进行攻击！", Color.red);
                    AudioManager.Instance.PlayEffect("AttackCard");
                    //敌人受伤
                    int val = int.Parse(data["Arg0"]);
                    hitEnemy.Hit(val);
                }
                //敌人未选中
                hitEnemy.OnUnSelect();
                hitEnemy=null;
            }
        }
        else
        {
            //未探测到敌人
            if(hitEnemy == null)
            {
                hitEnemy.OnUnSelect();
                hitEnemy = null;
            }
        }
    }
    public void OnPointerDown(PointerEventData eventData)
    {
        AudioManager.Instance.PlayEffect("Use");
        Cursor.visible = false;//隐藏鼠标
        StopAllCoroutines();//关闭协同程序
        StartCoroutine(OnMouseDownRight(eventData));//鼠标操作协同程序
    }
    public override void OnBeginDrag(PointerEventData eventData)
    {
        


    }
    public override void OnDrag(PointerEventData eventData)
    {
       
    }
    public override void OnEndDrag(PointerEventData eventData)
    {
    }
    private void Start()
    {
        transform.Find("bg").GetComponent<Image>().sprite = Resources.Load<Sprite>(data["BgImage"]);
        transform.Find("bg/icon").GetComponent<Image>().sprite = Resources.Load<Sprite>(data["Image"]);
        transform.Find("bg/msgTxt").GetComponent<Text>().text = string.Format(data["Des"], data["Arg0"]);
        transform.Find("bg/nameTxt").GetComponent<Text>().text = data["Name"];
        transform.Find("bg/useTxt").GetComponent<Text>().text = data["Expend"];
        transform.Find("bg/Text").GetComponent<Text>().text = GameConfigManager.Instance.GetCardTypeById(data["Type"])["Name"];

    }
    



}
