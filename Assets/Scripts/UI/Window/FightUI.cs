using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//战斗页面
public class FightUI : UIBase
{
    private Text cardCountTxT;//抽牌堆卡牌数量
    private Text usedCardCountTxT;//弃牌堆卡牌数量
    private Text powerTxT;
    private Text hpTxT;
    private Image hpImg;
    private Text defenseTxT;
    private void Awake()
    {
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
    //更新血量显示
    public void UpdateHp()
    {
        hpTxT.text = FightManager.Instance.CurrentHP + "/" + FightManager.Instance.MaxHP;
        hpImg.fillAmount=(float)FightManager.Instance.CurrentHP/(float)FightManager.Instance.MaxHP;//百分比显示图片
    }
    //更新能量显示
    public void UpdatePower()
    {
        powerTxT.text = FightManager.Instance.CurrentPowerCount + "/" + FightManager.Instance.MaxPowerCount;

    }
    //更新防御显示
    public void UpdateDefense()
    {
        defenseTxT.text=FightManager.Instance.DenfenseCount.ToString();
    }
    //更新抽卡堆数量
    public void UpdateCardCount()
    {
        cardCountTxT.text=FightCardManager.Instance.cardList.Count.ToString();
    }
    //更新弃牌堆数量
    public void UpdateUsedCardCount()
    {
        usedCardCountTxT.text=FightCardManager.Instance.usedcardList.Count.ToString();
    }
}
