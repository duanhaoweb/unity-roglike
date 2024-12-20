using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;


//ս��ҳ��
public class FightUI : UIBase
{
    private Text cardCountTxT;//���ƶѿ�������
    private Text usedCardCountTxT;//���ƶѿ�������
    private Text powerTxT;
    private Text hpTxT;
    private Image hpImg;
    private Text defenseTxT;
    private List<CardItem> CardItemList; //����ʵ�弯��
    private void Awake()
    {
        CardItemList = new List<CardItem>();
        cardCountTxT = transform.Find("hasCard/icon/Text").GetComponent<Text>();
        usedCardCountTxT = transform.Find("noCard/icon/Text").GetComponent<Text>();
        powerTxT = transform.Find("mana/Text").GetComponent<Text>();
        hpTxT = transform.Find("hp/moneyTxt").GetComponent<Text>();
        hpImg = transform.Find("hp/fill").GetComponent<Image>();
        defenseTxT= transform.Find("hp/fangyu/Text").GetComponent<Text>();
    }

    private void Start()
    {
        UpdateHp();
        UpdatePower();
        UpdateDefense();
        UpdateUsedCardCount();
        UpdateCardCount();
    }
    //����Ѫ����ʾ
    public void UpdateHp()
    {
        hpTxT.text = FightManager.Instance.CurrentHP + "/" + FightManager.Instance.MaxHP;
        hpImg.fillAmount=(float)FightManager.Instance.CurrentHP/(float)FightManager.Instance.MaxHP;//�ٷֱ���ʾͼƬ
    }
    //����������ʾ
    public void UpdatePower()
    {
        powerTxT.text = FightManager.Instance.CurrentPowerCount + "/" + FightManager.Instance.MaxPowerCount;

    }
    //���·�����ʾ
    public void UpdateDefense()
    {
        defenseTxT.text=FightManager.Instance.DenfenseCount.ToString();
    }
    //���³鿨������
    public void UpdateCardCount()
    {
        cardCountTxT.text=FightCardManager.Instance.cardList.Count.ToString();
    }
    //�������ƶ�����
    public void UpdateUsedCardCount()
    {
        usedCardCountTxT.text=FightCardManager.Instance.usedcardList.Count.ToString();
    }

    public void CreateCardItem(int count)
    {
        if(count>FightCardManager.Instance.cardList.Count)
        {
            count = FightCardManager.Instance.cardList.Count;
        }
        for(int i = 0; i < count; i++)
        {
            GameObject cardItem = Instantiate(Resources.Load("UI/CardItem"),transform)as GameObject;
            cardItem.GetComponent<RectTransform>().anchoredPosition = new Vector2(-807, -358);//λ�����ݿɵ�
            var item = cardItem.AddComponent<CardItem>();
            string cardId = FightCardManager.Instance.DrawCard();
            Dictionary<string,string> data = GameConfigManager.Instance.GetCardById(cardId);
            item.Init(data);
            CardItemList.Add(item);
        }
    }

    public void UpdateCardItemPos()
    {
        float offset = 1300.0f/CardItemList.Count;
        Vector2 pos = new Vector2(-CardItemList.Count / 2.0f * offset + offset * 0.5f, -358);
        for(int i = 0;i < CardItemList.Count;i++)
        {
            CardItemList[i].GetComponent<RectTransform>().DOAnchorPos(pos,0.5f);
            pos.x = pos.x+offset;
        }
    }
    public void RemoveCard(CardItem cardItem)
    {
        
    }
}
