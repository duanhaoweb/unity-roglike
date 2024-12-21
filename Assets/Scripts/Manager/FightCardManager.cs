using System.Collections;
using DG.Tweening;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.Timeline.Actions.MenuPriority;
using Unity.VisualScripting;
using UnityEngine.UI;

/// <summary>
/// 战斗卡牌管理器
/// </summary>
public class FightCardManager:UIBase
{
    public static FightCardManager Instance = new FightCardManager();

    public List<string> cardList;//抽牌堆
    public List<CardItem> cardItemList;//卡牌实例化

    public List<string> usedcardList;//弃牌堆
    public List<CardItem> usedcarditem;

    //初始化
    public void Init()
    {
        cardList = new List<string>();
        cardItemList = new List<CardItem>();
        usedcardList = new List<string>();
        usedcarditem = new List<CardItem>();

        // 获取卡组并进行边界检查
        if (RoleManager.Instance == null || RoleManager.Instance.cardList == null || RoleManager.Instance.cardList.Count == 0)
        {
            Debug.LogError("RoleManager cardList is null or empty.");
            return;
        }

        // 复制卡组
        cardList.AddRange(RoleManager.Instance.cardList);

        // Fisher-Yates 洗牌算法，将卡组中的牌打乱顺序后加入到抽牌堆
        for (int i = cardList.Count - 1; i >= 0; i--)
        {
            int randomIndex = Random.Range(0, i + 1);
            // 交换当前元素和随机索引的元素
            string temp = cardList[i];
            cardList[i] = cardList[randomIndex];
            cardList[randomIndex] = temp;
        }


        Debug.Log("Card count: " + cardList.Count);
    }
    //玩家是否有卡
    public bool HasCard()
    {
        return cardList.Count > 0;
    }
    //抽卡
    public string DrawCard()
    {
        string id = this.cardList[cardList.Count - 1];

        this.cardList.RemoveAt(cardList.Count - 1);

        return id;
    }
}
