using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

// ս��ҳ��
public class FightUI : UIBase
{
    private Text cardCountTxT;       // ���ƶѿ�������
    private Text usedCardCountTxT;  // ���ƶѿ�������
    private Text powerTxT;
    private Text hpTxT;
    private Image hpImg;
    private Text defenseTxT;
    private Text attack_upTxT;
    public List<CardItem> CardItemList; // ����ʵ��

    private void Awake()
    {
        // ��ʼ���б�
        CardItemList = new List<CardItem>();

        // �� UI Ԫ��
        cardCountTxT = FindText("hasCard/icon/Text");
        usedCardCountTxT = FindText("noCard/icon/Text");
        powerTxT = FindText("mana/Text");
        hpTxT = FindText("hp/moneyTxt");
        hpImg = FindImage("hp/fill");
        defenseTxT = FindText("hp/fangyu/Text");
        attack_upTxT = FindText("attack_up/Text");
        transform.Find("turnBtn").GetComponent<Button>().onClick.AddListener(onChangeTurnBtn);
    }
    //��һغϽ������л������˻غ�
    private void onChangeTurnBtn()
    {
        //ֻ����һغϲ����л������˻غ�
        if(FightManager.Instance.fightUnit is Fight_PlayerTurn)
        {
            FightManager.Instance.ChangeType(FightType.Enemy);
        }
    }

    private void Start()
    {
        // ��ʼ�� UI ��ʾ
        UpdateHp();
        UpdatePower();
        UpdateDefense();
        UpdateUsedCardCount();
        UpdateCardCount();
        UpdateATKbuff();
    }

    // ����Ѫ����ʾ
    public void UpdateHp()
    {
        var fightManager = FightManager.Instance;
        hpTxT.text = $"{fightManager.CurrentHP}/{FightManager.MaxHP}";
        hpImg.fillAmount = (float)fightManager.CurrentHP / FightManager.MaxHP;
    }

    // ����������ʾ
    public void UpdatePower()
    {
        var fightManager = FightManager.Instance;
        powerTxT.text = $"{fightManager.CurrentPowerCount}/{fightManager.MaxPowerCount}";
    }

    // ���·�����ʾ
    public void UpdateDefense()
    {
        defenseTxT.text = FightManager.Instance.DefenseCount.ToString();
    }

    // ��������������ʾ
    public void UpdateATKbuff()
    {
        if (attack_upTxT == null || FightManager.Instance == null)
        {
            Debug.LogError("attack_upTxT �� FightManager.Instance δ��ʼ����");
            return;
        }

        attack_upTxT.text = FightManager.Instance.ATKBuff.ToString();
    }

    // ���³鿨������
    public void UpdateCardCount()
    {
        cardCountTxT.text = FightCardManager.Instance.cardList.Count.ToString();
    }

    // �������ƶ�����
    public void UpdateUsedCardCount()
    {
        usedCardCountTxT.text = FightCardManager.Instance.usedcardList.Count.ToString();
    }

    public void CreateCardItem()
    {
        //Ԥ�ȴ����ÿ���ʵ�� ���ھ�ͷ��
        for (int i = FightCardManager.Instance.cardList.Count - 1; i >= 0; i--)
        {
            GameObject cardItem = Instantiate(Resources.Load("UI/CardItem"), transform) as GameObject;
            cardItem.GetComponent<RectTransform>().anchoredPosition = new Vector2(-2000, -2000);//λ�����ݿɵ�


            Dictionary<string, string> data = GameConfigManager.Instance.GetCardById(FightCardManager.Instance.cardList[i]);
            CardItem item = cardItem.AddComponent(System.Type.GetType(data["Script"])) as CardItem;
            item.Init(data);
            FightCardManager.Instance.cardItemList.Add(item);
        }
    }

    // ��ʼ������
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

    // ������������
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

    // �����߼������Զ����俨�ƣ�
    public void DrawCardItem(int count)
    {
        // �������Ҫ����������ڳ��ƶѿ�����������ʣ���߼�
        while (count > 0)
        {
            int currentDeckCount = FightCardManager.Instance.cardList.Count;

            if (currentDeckCount > 0)
            {
                // �ȴӵ�ǰ���ƶ��г�ȡ�����ܶ����
                int drawCount = Mathf.Min(count, currentDeckCount);
                DrawFromDeck(drawCount);
                count -= drawCount;
            }
            else
            {
                // ������ƶ�Ϊ�գ�����Ƿ������ƶ�
                if (FightCardManager.Instance.usedcardList.Count > 0)
                {
                    Debug.Log("���ƶ�Ϊ�գ������ƶ�����ϴ�Ʋ���");
                    RefillDeckFromUsedCards();
                }
                else
                {
                    // ������ƶ�ҲΪ�գ��޷���������
                    Debug.LogWarning("���ƶѺ����ƶѾ�Ϊ�գ��޷��������ƣ�");
                    return;
                }
            }
        }
    }

    // �ӵ�ǰ���ƶ��г�ȡָ����������
    private void DrawFromDeck(int count)
    {
        ShuffleCards(FightCardManager.Instance.cardItemList); // ��ѡ������ǰϴ��

        Vector2 pos = new Vector2(-807, -520); // ���ƶ���λ��
        for (int i = count - 1; i >= 0; i--)
        {
            AudioManager.Instance.PlayEffect("DrawCard");

            // �ӳ��ƶ��л�ȡ����
            var card = FightCardManager.Instance.cardItemList[i];
            card.transform.SetSiblingIndex(FightCardManager.Instance.cardItemList.Count);
            card.GetComponent<RectTransform>().DOAnchorPos(pos, 0.5f);

            // �ƶ����Ƶ��������
            CardItemList.Add(card);

            // �ӳ��ƶ����Ƴ�
            FightCardManager.Instance.cardList.RemoveAt(i);
            FightCardManager.Instance.cardItemList.RemoveAt(i);

            // ������ʾ
            UpdateCardCount();
        }
    }

    // �����ƶ�����ϴ�Ʋ����䵽���ƶ�
    private void RefillDeckFromUsedCards()
    {
        // �����ƶѵĿ��� ID �ƻ��ƿ�
        FightCardManager.Instance.cardList.AddRange(FightCardManager.Instance.usedcardList);
        FightCardManager.Instance.usedcardList.Clear();

        // �����ƶѵĿ���ʵ���ƻ��ƿ�
        FightCardManager.Instance.cardItemList.AddRange(FightCardManager.Instance.usedcarditem);
        FightCardManager.Instance.usedcarditem.Clear();

        // ϴ��
        ShuffleCards(FightCardManager.Instance.cardItemList);

        // ���� UI
        UpdateCardCount();
        UpdateUsedCardCount();

        Debug.Log("���ƶ���ϴ�س��ƶѣ������¸����ƿ�����");
    }


    // ���¿���λ��
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

    // �Ƴ�����
    public void RemoveCard(CardItem cardItem)
    {
        AudioManager.Instance.PlayEffect("Use");

        FightCardManager.Instance.usedcardList.Add(cardItem.data["Id"]);
        FightCardManager.Instance.usedcarditem.Add(cardItem);

        CardItemList.Remove(cardItem);
        UpdateUsedCardCount();
        UpdateCardCount();

        // �����Ƶ����ƶ�Ч��
        Vector2 discardPos = new Vector2(-143, 126);
        cardItem.GetComponent<RectTransform>().DOAnchorPos(discardPos, 0.5f).OnComplete(() =>
        {
            cardItem.GetComponent<RectTransform>().anchoredPosition = new Vector2(-2000, -2000);
        });
        cardItem.transform.DOScale(0.602f, 0.5f);
    }

    // ɾ������
    public void DeleteCardItem(CardItem cardItem)
    {
        Destroy(cardItem.gameObject, 1);
    }

    // ���߷������� Text
    private Text FindText(string path)
    {
        return transform.Find(path)?.GetComponent<Text>();
    }

    // ���߷������� Image
    private Image FindImage(string path)
    {
        return transform.Find(path)?.GetComponent<Image>();
    }

    // ���߷�������ӽű�
    private T AddCardScript<T>(GameObject obj, Dictionary<string, string> data) where T : CardItem
    {
        return obj.AddComponent(Type.GetType(data["Script"])) as T;
    }

    // ���߷�����ϴ��
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

    //ɾ�����п���
    public void RemoveAllCards()
    {
        for(int i = CardItemList.Count-1;i>=0;i--)
        {
            RemoveCard(CardItemList[i]);
        }
    }

}
