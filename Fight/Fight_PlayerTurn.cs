using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fight_PlayerTurn: FightUnit
{
    public override void Init()
    {
        
            UIManager.Instance.GetUI<FightUI>("FightUI").CreateCard(4);
        
    }
    public override void OnUpdate()
    {
        base.OnUpdate();
    }
}
