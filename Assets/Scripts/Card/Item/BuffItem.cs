using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
public class BuffItem : ItemCard
{
    public override void OnEndDrag(PointerEventData eventData)
    {
        if (UseCard() == true)
        {
            int buff = int.Parse(data["Arg0"]);//Arg1�����;�
            
            FightManager.Instance.ATKBuff += buff;
            //������Ч
            //ˢ����ֵ���ı�

            


        }
        else
        {
            base.OnEndDrag(eventData);
        }
    }
}
