using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
public class AddCard : ActionCard
{
    public override void OnEndDrag(PointerEventData eventData)
    {
        if (UseCard() == true)
        {
            int num = int.Parse(data["Arg0"]);//arg0为抽卡数 arg1>0加盾，<0扣血
            int val = int.Parse(data["Arg1"]);
            int apcost = int.Parse(data["Expend"]);
            
                //判定卡堆是否有卡
                if (FightCardManager.Instance.HasCard()==true)
                {
                //抽对应的卡，建立对应数量的卡牌对象
                UIManager.Instance.GetUI<FightUI>("FightUI").CreateCardItem(num);
                //判定加盾还是扣血
                if (val>0)
                    {
                        FightManager.Instance.DenfenseCount += val;
                    }else if(val<0)
                    {
                        FightManager.Instance.CurrentHP -= val;
                        if(FightManager.Instance.CurrentHP < 0) FightManager.Instance.CurrentHP = 0;
                    }
                   
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
