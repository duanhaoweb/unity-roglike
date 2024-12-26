using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
public class AddCard : ActionCard
{
    private void Start()
    {
        transform.Find("bg").GetComponent<Image>().sprite = Resources.Load<Sprite>(data["BgImage"]);
        transform.Find("bg/icon").GetComponent<Image>().sprite = Resources.Load<Sprite>(data["Image"]);
        transform.Find("bg/msgTxt").GetComponent<Text>().text = string.Format(data["Des"], data["Arg0"], data["Arg1"]);
        transform.Find("bg/nameTxt").GetComponent<Text>().text = data["Name"];
        transform.Find("bg/useTxt").GetComponent<Text>().text = data["Expend"];
        transform.Find("bg/Text").GetComponent<Text>().text = GameConfigManager.Instance.GetCardTypeById(data["Type"])["Name"];

    }
    public override void OnEndDrag(PointerEventData eventData)
    {

        if (UseCard() == true)
        {
            int num = int.Parse(data["Arg0"]);//arg0为抽卡数 arg1>0加盾，<0扣血
            int val = int.Parse(data["Arg1"]);
            //判定加盾还是扣血
            if (val > 0)
            {
                FightManager.Instance.DefenseCount += val;
                //UIManager.Instance.ShowTip("获得护盾！", Color.blue);
                //播放动画及音效
                AudioManager.Instance.PlayEffect("Defense");
                UIManager.Instance.GetUI<FightUI>("FightUI").UpdateDefense();

            }
            else if (val < 0)
            {
                FightManager.Instance.CurrentHP += val;
                if (FightManager.Instance.CurrentHP < 0) FightManager.Instance.CurrentHP = 0;
                //UIManager.Instance.ShowTip("受到伤害！", Color.red);
                //播放动画及音效
                AudioManager.Instance.PlayEffect("Hurt");
                UIManager.Instance.GetUI<FightUI>("FightUI").UpdateHp();
            }

            //判定卡堆是否有卡
            if (FightCardManager.Instance.HasCard() == true)
            {
                //抽对应的卡，建立对应数量的卡牌对象
                UIManager.Instance.GetUI<FightUI>("FightUI").DrawCardItem(num);
                UIManager.Instance.GetUI<FightUI>("FightUI").UpdateCardItemPos();
                //AudioManager.Instance.PlayEffect("AddCard");

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