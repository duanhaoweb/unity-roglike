using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
public class FoodCard : ItemCard
{
    public override void OnEndDrag(PointerEventData eventData)
    {
        if (UseCard() == true)
        {
            int heal = int.Parse(data["Arg0"]);//Arg1�����;�
            
            FightManager.Instance.CurrentHP += heal;
            if(FightManager.Instance.CurrentHP >=FightManager.Instance.MaxHP) {
            FightManager.Instance.CurrentHP=FightManager.Instance.MaxHP;
            }
            //������Ч
            //ˢ����ֵ���ı�




        }
        else
        {
            base.OnEndDrag(eventData);
        }
    }
}
