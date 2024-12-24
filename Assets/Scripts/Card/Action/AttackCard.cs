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
    //        //���ٴΰ����Ҽ�����ѭ��
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
    //            //�������߼���Ƿ���������
    //            CheckRayToEnemy();
    //        }

    //        yield return null;
    //    }
    //    //����ѭ������ʾ���
    //    Cursor.visible = true;
    //}
    Enemy hitEnemy;//����⵽�ĵ���
    private bool CheckRayToEnemy()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        bool isUse = false;
        RaycastHit hit;
        Debug.DrawLine(ray.origin, ray.direction, Color.blue, 100);
        if (Physics.Raycast(ray, out hit, 10000, LayerMask.GetMask("Enemy")))
        {
            Debug.Log("̽�⵽����");
            hitEnemy = hit.transform.GetComponent<Enemy>();
            hitEnemy.OnSelect();
            
                

                if (UseCard() == true)
                {
                    UIManager.Instance.ShowTip("���й�����", Color.red);
                    AudioManager.Instance.PlayEffect("AttackCard");
                    //��������
                    int val = int.Parse(data["Arg0"]);
                    int hurt = int.Parse(data["Arg1"]);
                    hitEnemy.Hit(val,hurt);
                UIManager.Instance.GetUI<FightUI>("FightUI").UpdateATKbuff();

                hitEnemy.OnUnSelect();
                hitEnemy = null;
                return true;
                }
                //����δѡ��
                hitEnemy.OnUnSelect();
                hitEnemy = null;
            
            
        }
        
        return false;
    }
    //public void OnPointerDown(PointerEventData eventData)
    //{
    //    AudioManager.Instance.PlayEffect("Use");
    //    //Cursor.visible = false;//�������
    //    //StopAllCoroutines();//�ر�Эͬ����
    //    StartCoroutine(OnMouseDownRight(eventData));//������Эͬ����
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
            //�������߼���Ƿ���������
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
