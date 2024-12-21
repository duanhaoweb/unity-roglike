using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fight_PlayerTurn: FightUnit
{
    public override void Init()
    {
        base.Init();
        Debug.Log("playeTime");
        UIManager.Instance.ShowTip("ÕÊº“ªÿ∫œ",Color.green,delegate()
        {

            

            //≥È≈∆
            Debug.Log("≥È≈∆");
            UIManager.Instance.GetUI<FightUI>("FightUI").DrawCardItem(6);//≥È6’≈≈∆
            UIManager.Instance.GetUI<FightUI>("FightUI").UpdateCardItemPos();
        });
    }
    public override void OnUpdate()
    {
        base.OnUpdate();
    }
}
