using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
public class DefenseWeapon : ItemCard
{
    public override void OnEndDrag(PointerEventData eventData)
    {

        if (UseCard() == true)
        {
            int armor = int.Parse(data["Arg0"]);//Arg1�����;�
            



            
               
                FightManager.Instance.Armor += armor;
                //������Ч
                //ˢ�»��׼��ı�
            
            




        }
        else
        {
            base.OnEndDrag(eventData);
        }
    }
}
