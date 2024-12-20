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
            int armor = int.Parse(data["Arg1"]);//Arg1代表对自己的数值
            
           

            
            
                
                FightManager.Instance.DenfenseCount += armor;
                //播放音效
                //刷新护甲及文本
            
            



        }
        else
        {
            base.OnEndDrag(eventData);
        }
    }
}
