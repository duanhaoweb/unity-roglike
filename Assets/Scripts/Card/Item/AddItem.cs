using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
public class AddItem : ItemCard
{
    private AddItem()
    {
        dur = int.Parse(data["Arg0"]);
    }
    private void Start()
    {
        transform.Find("bg").GetComponent<Image>().sprite = Resources.Load<Sprite>(data["BgImage"]);
        transform.Find("bg/icon").GetComponent<Image>().sprite = Resources.Load<Sprite>(data["Image"]);
        transform.Find("bg/msgTxt").GetComponent<Text>().text = string.Format(data["Des"], data["Arg0"],dur);
        transform.Find("bg/nameTxt").GetComponent<Text>().text = data["Name"];
        transform.Find("bg/useTxt").GetComponent<Text>().text = data["Expend"];
        transform.Find("bg/Text").GetComponent<Text>().text = GameConfigManager.Instance.GetCardTypeById(data["Type"])["Name"];
        dur = int.Parse(data["Arg1"]);
    }
    public override void OnEndDrag(PointerEventData eventData)
    {

        if (UseCard() == true)
        {
            int num = int.Parse(data["Arg0"]);


            //判定卡堆是否有卡
            if (FightCardManager.Instance.HasCard() == true)
            {
                //抽对应的卡，建立对应数量的卡牌对象
                UIManager.Instance.GetUI<FightUI>("FightUI").DrawCardItem(num);
                UIManager.Instance.GetUI<FightUI>("FightUI").UpdateCardItemPos();
                AudioManager.Instance.PlayEffect("AddCard");

                //播放音效
                //刷新护甲及文本

                UIManager.Instance.ShowTip("获得手牌！", Color.yellow);
                

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
