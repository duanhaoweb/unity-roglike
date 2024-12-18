using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
public class AddItem : ItemCard
{
    public override void OnEndDrag(PointerEventData eventData)
    {
        if (UseCard() == true)
        {
            int num = int.Parse(data["Arg0"]);//arg0为抽卡数 
            
            

            //判定卡堆是否有卡
            if (FightCardManager.Instance.HasCard() == true)
            {
                //抽对应的卡，建立对应数量的卡牌对象
                UIManager.Instance.GetUI<FightUI>("FightUI").CreateCard(num);
                

            }
            else
            {
                base.OnEndDrag(eventData);
            }

        }
        else
        {
            base.OnEndDrag(eventData);
        }
    }
}
