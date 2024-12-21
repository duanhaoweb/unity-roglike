using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
public class AddItem : ItemCard
{
    private void Start()
    {
        transform.Find("bg").GetComponent<Image>().sprite = Resources.Load<Sprite>(data["BgImage"]);
        transform.Find("bg/icon").GetComponent<Image>().sprite = Resources.Load<Sprite>(data["Image"]);
        transform.Find("bg/msgTxt").GetComponent<Text>().text = string.Format(data["Des"], data["Arg0"]);
        transform.Find("bg/nameTxt").GetComponent<Text>().text = data["Name"];
        transform.Find("bg/useTxt").GetComponent<Text>().text = data["Expend"];
        transform.Find("bg/Text").GetComponent<Text>().text = GameConfigManager.Instance.GetCardTypeById(data["Type"])["Name"];

    }
    public override void OnEndDrag(PointerEventData eventData)
    {
        if (UseCard() == true)
        {
            int num = int.Parse(data["Arg0"]);//arg0为抽卡数 
            
            

            //判定卡堆是否有卡
            if (FightCardManager.Instance.HasCard() == true)
            {
                //抽对应的卡，建立对应数量的卡牌对象
                UIManager.Instance.GetUI<FightUI>("FightUI").DrawCardItem(num);
                

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
