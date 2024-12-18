using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//ս��ҳ��
public class FightUI : UIBase
{





    private List<CardItem> CardItemList;//�������弯��


    private void Awake()
    {
        CardItemList = new List<CardItem>();
    }
















    //������������
    public void CreateCard(int count)
    {
        if (count > FightCardManager.Instance.cardList.Count)
        {
            count=FightCardManager.Instance.cardList.Count;
        }

        for (int i = 0; i < count; i++)
        {
            GameObject obj = Instantiate(Resources.Load("UI/CardItem"),transform)as GameObject;
            obj.GetComponent<RectTransform>().anchoredPosition = new Vector2(-1000, -700);
            
            string cardId = FightCardManager.Instance.DrawCard();
            Dictionary<string,string>data=GameConfigManager.Instance.GetCardById(cardId);
            CardItem item = obj.AddComponent(System.Type.GetType(data["Script"]))as CardItem;
            item.Init(data);
            CardItemList.Add(item);
        }
    }
    public void RemoveCard(CardItem item)
    {

    }
    
}
