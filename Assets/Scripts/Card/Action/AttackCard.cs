using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
public class AttackCard : ActionCard//, IPointerDownHandler
{
    //IEnumerator OnMouseDownRight(PointerEventData data)
    //{
    //    while(true)
    //    {
    //        //若再次按下右键跳出循环
    //        if(Input.GetMouseButtonDown(1))
    //        {
    //            break;
    //        }
    //        Vector2 pos;
    //        if (RectTransformUtility.ScreenPointToLocalPointInRectangle(
    //            transform.parent.GetComponent<RectTransform>(),
    //            data.position,
    //            data.pressEventCamera,
    //            out pos
    //            ))
    //        {
    //            //进行射线检测是否碰到怪物
    //            CheckRayToEnemy();
    //        }

    //        yield return null;
    //    }
    //    //跳出循环后显示鼠标
    //    Cursor.visible = true;
    //}
    Enemy hitEnemy;//被检测到的敌人
    private bool CheckRayToEnemy()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        bool isUse = false;
        RaycastHit hit;
        Debug.DrawLine(ray.origin, ray.direction, Color.blue, 100);
        if (Physics.Raycast(ray, out hit, 10000, LayerMask.GetMask("Enemy")))
        {
            Debug.Log("探测到敌人");
            hitEnemy = hit.transform.GetComponent<Enemy>();
            hitEnemy.OnSelect();
            
                

                if (UseCard() == true)
                {
                    UIManager.Instance.ShowTip("进行攻击！", Color.red);
                    AudioManager.Instance.PlayEffect("AttackCard");
                    //敌人受伤
                    int val = int.Parse(data["Arg0"]);
                    int hurt = int.Parse(data["Arg1"]);
                    hitEnemy.Hit(val,hurt);
                UIManager.Instance.GetUI<FightUI>("FightUI").UpdateATKbuff();

                hitEnemy.OnUnSelect();
                hitEnemy = null;
                return true;
                }
                //敌人未选中
                hitEnemy.OnUnSelect();
                hitEnemy = null;
            
            
        }
        
        return false;
    }
    //public void OnPointerDown(PointerEventData eventData)
    //{
    //    AudioManager.Instance.PlayEffect("Use");
    //    //Cursor.visible = false;//隐藏鼠标
    //    //StopAllCoroutines();//关闭协同程序
    //    StartCoroutine(OnMouseDownRight(eventData));//鼠标操作协同程序
    //}
    public override void OnBeginDrag(PointerEventData eventData)
    {
        
        base.OnBeginDrag(eventData);

    }
    public override void OnDrag(PointerEventData eventData)
    {
       base.OnDrag(eventData);
    }
    public override void OnEndDrag(PointerEventData eventData)
    {
        Vector2 pos;
        if (RectTransformUtility.ScreenPointToLocalPointInRectangle(
            transform.parent.GetComponent<RectTransform>(),
            eventData.position,
            eventData.pressEventCamera,
            out pos
            ))
        {
            //进行射线检测是否碰到怪物
            if(!CheckRayToEnemy()) base.OnEndDrag(eventData);
        }
    }
    private void Start()
    {
        transform.Find("bg").GetComponent<Image>().sprite = Resources.Load<Sprite>(data["BgImage"]);
        transform.Find("bg/icon").GetComponent<Image>().sprite = Resources.Load<Sprite>(data["Image"]);
        transform.Find("bg/msgTxt").GetComponent<Text>().text = string.Format(data["Des"], data["Arg0"], data["Arg1"]);
        transform.Find("bg/nameTxt").GetComponent<Text>().text = data["Name"];
        transform.Find("bg/useTxt").GetComponent<Text>().text = data["Expend"];
        transform.Find("bg/Text").GetComponent<Text>().text = GameConfigManager.Instance.GetCardTypeById(data["Type"])["Name"];

    }
    



}
