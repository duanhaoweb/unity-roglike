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
            int num = int.Parse(data["Arg0"]);//arg0Ϊ�鿨�� arg1>0�Ӷܣ�<0��Ѫ
            int val = int.Parse(data["Arg1"]);
            int apcost = int.Parse(data["Expend"]);
            
                //�ж������Ƿ��п�
                if (FightCardManager.Instance.HasCard()==true)
                {
                //���Ӧ�Ŀ���������Ӧ�����Ŀ��ƶ���
                UIManager.Instance.GetUI<FightUI>("FightUI").CreateCardItem(num);
                //�ж��Ӷܻ��ǿ�Ѫ
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
