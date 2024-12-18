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
            int armor = int.Parse(data["Arg0"]);//Arg1代表耐久
            



            
               
                FightManager.Instance.Armor += armor;
                //播放音效
                //刷新护甲及文本
            
            




        }
        else
        {
            base.OnEndDrag(eventData);
        }
    }
}
