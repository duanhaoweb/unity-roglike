using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

// 战斗页面
public class FightUI : UIBase
{
    private Text cardCountTxT;       // 抽牌堆卡牌数量
    private Text usedCardCountTxT;  // 弃牌堆卡牌数量
    private Text powerTxT;
    private Text hpTxT;
    private Image hpImg;
    private Text defenseTxT;
    private Text attack_upTxT;
    public List<CardItem> CardItemList; // 卡牌实体

    private void Awake()
    {
        // 初始化列表
        CardItemList = new List<CardItem>();

        // 绑定 UI 元素
        cardCountTxT = FindText("hasCard/icon/Text");
        usedCardCountTxT = FindText("noCard/icon/Text");
        powerTxT = FindText("mana/Text");
        hpTxT = FindText("hp/moneyTxt");
        hpImg = FindImage("hp/fill");
        defenseTxT = FindText("hp/fangyu/Text");
        attack_upTxT = FindText("attack_up/Text");
        transform.Find("turnBtn").GetComponent<Button>().onClick.AddListener(onChangeTurnBtn);
    }
    //玩家回合结束，切换到敌人回合
    private void onChangeTurnBtn()
    {
        //只有玩家回合才能切换到敌人回合
        if(FightManager.Instance.fightUnit is Fight_PlayerTurn)
        {
            FightManager.Instance.ChangeType(FightType.Enemy);
        }
    }

    private void Start()
    {
        // 初始化 UI 显示
        UpdateHp();
        UpdatePower();
        UpdateDefense();
        UpdateUsedCardCount();
        UpdateCardCount();
        UpdateATKbuff();
    }

    // 更新血量显示
    public void UpdateHp()
    {
        var fightManager = FightManager.Instance;
        hpTxT.text = $"{fightManager.CurrentHP}/{FightManager.MaxHP}";
        hpImg.fillAmount = (float)fightManager.CurrentHP / FightManager.MaxHP;
    }

    // 更新能量显示
    public void UpdatePower()
    {
        var fightManager = FightManager.Instance;
        powerTxT.text = $"{fightManager.CurrentPowerCount}/{fightManager.MaxPowerCount}";
    }

    // 更新防御显示
    public void UpdateDefense()
    {
        defenseTxT.text = FightManager.Instance.DefenseCount.ToString();
    }

    // 更新力量提升显示
    public void UpdateATKbuff()
    {
        if (attack_upTxT == null || FightManager.Instance == null)
        {
            Debug.LogError("attack_upTxT 或 FightManager.Instance 未初始化！");
            return;
        }

        attack_upTxT.text = FightManager.Instance.ATKBuff.ToString();
    }

    // 更新抽卡堆数量
    public void UpdateCardCount()
    {
        cardCountTxT.text = FightCardManager.Instance.cardList.Count.ToString();
    }

    // 更新弃牌堆数量
    public void UpdateUsedCardCount()
    {
        usedCardCountTxT.text = FightCardManager.Instance.usedcardList.Count.ToString();
    }

    public void CreateCardItem()
    {
        //预先创建好卡牌实体 放在镜头外
        for (int i = FightCardManager.Instance.cardList.Count - 1; i >= 0; i--)
        {
            GameObject cardItem = Instantiate(Resources.Load("UI/CardItem"), transform) as GameObject;
            cardItem.GetComponent<RectTransform>().anchoredPosition = new Vector2(-2000, -2000);//位置数据可调


            Dictionary<string, string> data = GameConfigManager.Instance.GetCardById(FightCardManager.Instance.cardList[i]);
            CardItem item = cardItem.AddComponent(System.Type.GetType(data["Script"])) as CardItem;
            item.Init(data);
            FightCardManager.Instance.cardItemList.Add(item);
        }
    }

    // 初始化背包
    public void UnitBag()
    {
        for (int i = 1021; i >= 1011; i--)
        {
            GameObject cardItem = Instantiate(Resources.Load<GameObject>("UI/CardItem"), transform);
            cardItem.GetComponent<RectTransform>().anchoredPosition = new Vector2(-2000, -2000);

            Dictionary<string, string> data = GameConfigManager.Instance.GetCardById(i.ToString());
            ItemCard item = AddCardScript<ItemCard>(cardItem, data);
            item.Init(data);
            item.dur = int.Parse(data["Arg1"]);

            RoleManager.Instance.BagList.Add(item);
        }
    }

    // 创建背包卡牌
    public void CreateItemCard()
    {
        for (int i = RoleManager.Instance.BagList.Count - 1; i >= 0; i--)
        {
            var bagCard = RoleManager.Instance.BagList[i];
            bagCard.GetComponent<RectTransform>().anchoredPosition = new Vector2(-3000, -3000);

            FightCardManager.Instance.cardItemList.Add(bagCard);
            FightCardManager.Instance.cardList.Add(bagCard.data["Id"]);
            RoleManager.Instance.BagList.RemoveAt(i);
        }
    }

    // 抽牌逻辑（含自动补充卡牌）
    public void DrawCardItem(int count)
    {
        // 如果总需要抽的牌数大于抽牌堆卡牌数，处理剩余逻辑
        while (count > 0)
        {
            int currentDeckCount = FightCardManager.Instance.cardList.Count;

            if (currentDeckCount > 0)
            {
                // 先从当前抽牌堆中抽取尽可能多的牌
                int drawCount = Mathf.Min(count, currentDeckCount);
                DrawFromDeck(drawCount);
                count -= drawCount;
            }
            else
            {
                // 如果抽牌堆为空，检查是否有弃牌堆
                if (FightCardManager.Instance.usedcardList.Count > 0)
                {
                    Debug.Log("抽牌堆为空，从弃牌堆重新洗牌补充");
                    RefillDeckFromUsedCards();
                }
                else
                {
                    // 如果弃牌堆也为空，无法继续抽牌
                    Debug.LogWarning("抽牌堆和弃牌堆均为空，无法继续抽牌！");
                    return;
                }
            }
        }
    }

    // 从当前抽牌堆中抽取指定数量的牌
    private void DrawFromDeck(int count)
    {
        ShuffleCards(FightCardManager.Instance.cardItemList); // 可选：抽牌前洗牌

        Vector2 pos = new Vector2(-807, -520); // 抽牌动画位置
        for (int i = count - 1; i >= 0; i--)
        {
            AudioManager.Instance.PlayEffect("DrawCard");

            // 从抽牌堆中获取卡牌
            var card = FightCardManager.Instance.cardItemList[i];
            card.transform.SetSiblingIndex(FightCardManager.Instance.cardItemList.Count);
            card.GetComponent<RectTransform>().DOAnchorPos(pos, 0.5f);

            // 移动卡牌到玩家手牌
            CardItemList.Add(card);

            // 从抽牌堆中移除
            FightCardManager.Instance.cardList.RemoveAt(i);
            FightCardManager.Instance.cardItemList.RemoveAt(i);

            // 更新显示
            UpdateCardCount();
        }
    }

    // 从弃牌堆重新洗牌并补充到抽牌堆
    private void RefillDeckFromUsedCards()
    {
        // 将弃牌堆的卡牌 ID 移回牌库
        FightCardManager.Instance.cardList.AddRange(FightCardManager.Instance.usedcardList);
        FightCardManager.Instance.usedcardList.Clear();

        // 将弃牌堆的卡牌实体移回牌库
        FightCardManager.Instance.cardItemList.AddRange(FightCardManager.Instance.usedcarditem);
        FightCardManager.Instance.usedcarditem.Clear();

        // 洗牌
        ShuffleCards(FightCardManager.Instance.cardItemList);

        // 更新 UI
        UpdateCardCount();
        UpdateUsedCardCount();

        Debug.Log("弃牌堆已洗回抽牌堆，并重新更新牌库数据");
    }


    // 更新卡牌位置
    public void UpdateCardItemPos()
    {
        float offset = 1000.0f / CardItemList.Count;
        Vector2 pos = new Vector2(-CardItemList.Count / 2.0f * offset + offset * 0.5f, -520);

        for (int i = 0; i < CardItemList.Count; i++)
        {
            CardItemList[i].GetComponent<RectTransform>().DOAnchorPos(pos, 0.5f);
            pos.x += offset;
        }
    }

    // 移除卡牌
    public void RemoveCard(CardItem cardItem)
    {
        AudioManager.Instance.PlayEffect("Use");

        FightCardManager.Instance.usedcardList.Add(cardItem.data["Id"]);
        FightCardManager.Instance.usedcarditem.Add(cardItem);

        CardItemList.Remove(cardItem);
        UpdateUsedCardCount();
        UpdateCardCount();

        // 卡牌移到弃牌堆效果
        Vector2 discardPos = new Vector2(-143, 126);
        cardItem.GetComponent<RectTransform>().DOAnchorPos(discardPos, 0.5f).OnComplete(() =>
        {
            cardItem.GetComponent<RectTransform>().anchoredPosition = new Vector2(-2000, -2000);
        });
        cardItem.transform.DOScale(0.602f, 0.5f);
    }

    // 删除卡牌
    public void DeleteCardItem(CardItem cardItem)
    {
        Destroy(cardItem.gameObject, 1);
    }

    // 工具方法：绑定 Text
    private Text FindText(string path)
    {
        return transform.Find(path)?.GetComponent<Text>();
    }

    // 工具方法：绑定 Image
    private Image FindImage(string path)
    {
        return transform.Find(path)?.GetComponent<Image>();
    }

    // 工具方法：添加脚本
    private T AddCardScript<T>(GameObject obj, Dictionary<string, string> data) where T : CardItem
    {
        return obj.AddComponent(Type.GetType(data["Script"])) as T;
    }

    // 工具方法：洗牌
    private void ShuffleCards<T>(List<T> cards)
    {
        for (int i = cards.Count - 1; i > 0; i--)
        {
            int randomIndex = UnityEngine.Random.Range(0, i + 1);
            T temp = cards[i];
            cards[i] = cards[randomIndex];
            cards[randomIndex] = temp;
        }
    }

    //删除所有卡牌
    public void RemoveAllCards()
    {
        for(int i = CardItemList.Count-1;i>=0;i--)
        {
            RemoveCard(CardItemList[i]);
        }
    }

}
