using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
public class DefenseCard : ActionCard
{
    
    public override void OnEndDrag(PointerEventData eventData)
    {
        if (UseCard() == true)
        {
            int armor = int.Parse(data["Arg1"]);//Arg1������Լ�����ֵ
            
           

            
            
                
                FightManager.Instance.DenfenseCount += armor;
                //������Ч
                //ˢ�»��׼��ı�
            
            



        }
        else
        {
            base.OnEndDrag(eventData);
        }
    }
}
