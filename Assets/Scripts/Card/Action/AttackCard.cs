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
            //���ٴΰ����Ҽ�����ѭ��
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
                //�������߼���Ƿ���������
                CheckRayToEnemy();
            }

            yield return null;
        }
        //����ѭ������ʾ���
        Cursor.visible = true;
    }
    Enemy hitEnemy;//����⵽�ĵ���
    private void CheckRayToEnemy()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        RaycastHit hit;
        if(Physics.Raycast(ray, out hit,10000,LayerMask.GetMask("Enemy")))
        {
            hitEnemy = hit.transform.GetComponent<Enemy>();
            hitEnemy.OnSelect();
            //���������
            if (Input.GetMouseButtonDown(0))
            {
                //�ر�����Эͬ����
                StopAllCoroutines();
                //��ʾ���
                Cursor.visible = true;

                if (UseCard() == true)
                {
                    UIManager.Instance.ShowTip("���й�����", Color.red);
                    AudioManager.Instance.PlayEffect("AttackCard");
                    //��������
                    int val = int.Parse(data["Arg0"]);
                    hitEnemy.Hit(val);
                }
                //����δѡ��
                hitEnemy.OnUnSelect();
                hitEnemy=null;
            }
        }
        else
        {
            //δ̽�⵽����
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
        Cursor.visible = false;//�������
        StopAllCoroutines();//�ر�Эͬ����
        StartCoroutine(OnMouseDownRight(eventData));//������Эͬ����
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
