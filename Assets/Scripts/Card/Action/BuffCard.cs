using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
public class BuffCard : ActionCard
{
    public override void OnEndDrag(PointerEventData eventData)
    {
        if (UseCard() == true)
        {
            int buff = int.Parse(data["Arg1"]);//Arg1������Լ�����ֵ
            if (int.Parse(data["Arg0"]) > 0)
            {
                //���ù��������õĹ������������ڹ�����֮����buff����
            }
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
