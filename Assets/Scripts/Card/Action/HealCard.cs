using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
public class HealCard : ActionCard
{

    public override void OnEndDrag(PointerEventData eventData)
    {
        if (UseCard() == true)
        {
            int heal = int.Parse(data["Arg1"]);//Arg1代表对自己的数值

            FightManager.Instance.CurrentHP += heal;
            if (FightManager.Instance.CurrentHP >= FightManager.Instance.MaxHP)
            {
                FightManager.Instance.CurrentHP = FightManager.Instance.MaxHP;
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