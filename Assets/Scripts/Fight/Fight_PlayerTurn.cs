using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fight_PlayerTurn: FightUnit
{
    public override void Init()
    {
        base.Init();
        Debug.Log("playeTime");
        UIManager.Instance.ShowTip("玩家回合",Color.green,delegate()
        {
            //回复行动力
            FightManager.Instance.CurrentPowerCount = 3;
            FightManager.Instance.MaxPowerCount = 3;
            UIManager.Instance.GetUI<FightUI>("FightUI").UpdatePower();
            //抽卡堆卡牌数小于等于要求抽卡数时，重新洗牌
            UIManager.Instance.GetUI<FightUI>("FightUI").DrawCardItem(6);//抽6张牌
            UIManager.Instance.GetUI<FightUI>("FightUI").UpdateCardCount();

            UIManager.Instance.GetUI<FightUI>("FightUI").UpdateCardItemPos();
        });
    }
    public override void OnUpdate()
    {
        base.OnUpdate();
    }
}
