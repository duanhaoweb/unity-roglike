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
            int heal = int.Parse(data["Arg0"]);//Arg1代表耐久
            
            FightManager.Instance.CurHp += heal;
            if(FightManager.Instance.CurHp >=FightManager.Instance.MaxHp) {
            FightManager.Instance.CurHp=FightManager.Instance.MaxHp;
            }
            //播放音效
            //刷新数值及文本




        }
        else
        {
            base.OnEndDrag(eventData);
        }
    }
}
