  using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//战斗卡片管理器
public class FightCardManager
{
    public static FightCardManager Instance = new FightCardManager();

    public List<string> cardList;//卡堆集合

    public List<string> usedCardList;//弃牌堆

    //初始化
    public void Init()
    {
        cardList = new List<string>();
        usedCardList = new List<string>();

        //临时集合
        List<string> tempList = new List<string>();
        //将玩家卡牌存入临时集合

        tempList.AddRange(RoleManager.Instance.cardList);

        while (tempList.Count > 0)
        {
            //随机下标
            int tempIndex = Random.Range(0, tempList.Count);

            //添加到卡堆
            cardList.Add(tempList[tempIndex]);

            //删除临时集合中已进入牌堆的卡牌
            tempList.RemoveAt(tempIndex);
        }

        Debug.Log(cardList.Count);
    }

    //是否有卡，有卡返回卡数
    public bool HasCard()
    {
        return cardList.Count > 0;
    }

    //抽卡
    public string DrawCard()
    {
        //抽卡堆最后一张卡
        string id = cardList[cardList.Count-1];

        cardList.RemoveAt(cardList.Count-1);

        return id;
    }
}
