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
            int buff = int.Parse(data["Arg1"]);//Arg1代表对自己的数值
            if (int.Parse(data["Arg0"]) > 0)
            {
                //套用攻击卡所用的攻击函数，并在攻击完之后让buff归零
            }
            FightManager.Instance.ATKBuff += buff;
            //播放音效
            //刷新数值及文本




        }
        else
        {
            base.OnEndDrag(eventData);
        }
    }
}
