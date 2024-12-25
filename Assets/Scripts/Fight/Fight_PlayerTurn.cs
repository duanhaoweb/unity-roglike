using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fight_PlayerTurn: FightUnit
{
    public override void Init()
    {
        base.Init();
        Debug.Log("playeTime");
        UIManager.Instance.ShowTip("��һغ�",Color.green,delegate()
        {
            //�ظ��ж���
            FightManager.Instance.CurrentPowerCount = 3;
            FightManager.Instance.MaxPowerCount = 3;
            UIManager.Instance.GetUI<FightUI>("FightUI").UpdatePower();
            //�鿨�ѿ�����С�ڵ���Ҫ��鿨��ʱ������ϴ��
            UIManager.Instance.GetUI<FightUI>("FightUI").DrawCardItem(6);//��6����
            UIManager.Instance.GetUI<FightUI>("FightUI").UpdateCardCount();

            UIManager.Instance.GetUI<FightUI>("FightUI").UpdateCardItemPos();
        });
    }
    public override void OnUpdate()
    {
        base.OnUpdate();
    }
}
