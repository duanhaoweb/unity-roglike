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
            int num = int.Parse(data["Arg0"]);//arg0Ϊ�鿨�� 
            
            

            //�ж������Ƿ��п�
            if (FightCardManager.Instance.HasCard() == true)
            {
                //���Ӧ�Ŀ���������Ӧ�����Ŀ��ƶ���
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
