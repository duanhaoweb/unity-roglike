using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

using static UnityEditor.PlayerSettings;
using static UnityEditor.Timeline.Actions.MenuPriority;


//ս��ҳ��
public class FightUI : UIBase
{
    private Text cardCountTxT;//���ƶѿ�������
    private Text usedCardCountTxT;//���ƶѿ�������
    private Text powerTxT;
    private Text hpTxT;
    private Image hpImg;
    private Text defenseTxT;
    private Text attack_upTxT;
    public List<CardItem> CardItemList; //����ʵ�弯��
    private void Awake()
    {
        CardItemList = new List<CardItem>();
        cardCountTxT = transform.Find("hasCard/icon/Text").GetComponent<Text>();
        usedCardCountTxT = transform.Find("noCard/icon/Text").GetComponent<Text>();
        powerTxT = transform.Find("mana/Text").GetComponent<Text>();
        hpTxT = transform.Find("hp/moneyTxt").GetComponent<Text>();
        hpImg = transform.Find("hp/fill").GetComponent<Image>();
        defenseTxT= transform.Find("hp/fangyu/Text").GetComponent<Text>();
        attack_upTxT= transform.Find("attack_up/Text").GetComponent<Text>();
        // ��� transform.Find �Ƿ�ɹ��ҵ���Ӧ����
        if (attack_upTxT == null)
        {
            Debug.LogError("δ���ҵ� attack_up/Text ���� Text �����");
        }
        else
        {
            Debug.Log("�ɹ��ҵ� attack_up/Text������ȡ�� Text �����");
        }
    }

    private void Start()
    {
        UpdateHp();
        UpdatePower();
        UpdateDefense();
        UpdateUsedCardCount();
        UpdateCardCount();
        UpdateATKbuff();
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
    //��������������ʾ
    public void UpdateATKbuff()
    {
        // ��� attack_upTxT �Ƿ��Ѿ���ʼ��
        if (attack_upTxT == null)
        {
            Debug.LogError("attack_upTxT ��δ��ʼ������������Ƿ���ȷ�󶨣�");
            return;
        }

        // ��� FightManager.Instance �Ƿ�Ϊ null
        if (FightManager.Instance == null)
        {
            Debug.LogError("FightManager.Instance δ��ʼ����");
            return;
        }

        // ��� ATKBuff ֵ
        Debug.Log($"��ǰ ATKBuff ֵΪ: {FightManager.Instance.ATKBuff}");

        // ���� UI �ı�
        attack_upTxT.text = FightManager.Instance.ATKBuff.ToString();
        Debug.Log("attack_upTxT �ѳɹ�����Ϊ: " + attack_upTxT.text);
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
    public void CreateCardItem()
    {
        //Ԥ�ȴ����ÿ���ʵ�� ���ھ�ͷ��
        for (int i = FightCardManager.Instance.cardList.Count - 1; i >= 0; i--)
        {
            GameObject cardItem = Instantiate(Resources.Load("UI/CardItem"), transform) as GameObject;
            cardItem.GetComponent<RectTransform>().anchoredPosition = new Vector2(-2000, -2000);//λ�����ݿɵ�
            

            Dictionary<string, string> data = GameConfigManager.Instance.GetCardById(FightCardManager.Instance.cardList[i]);
            CardItem item = cardItem.AddComponent(System.Type.GetType(data["Script"]))as CardItem;
            item.Init(data);
            FightCardManager.Instance.cardItemList.Add(item);
        }
    }
    public void UnitBag()
    {
        //��ʼ������
        for (int i = 1021; i>=1011; i--)
        {


            GameObject cardItem = Instantiate(Resources.Load("UI/CardItem"), transform) as GameObject;
            cardItem.GetComponent<RectTransform>().anchoredPosition = new Vector2(-2000, -2000);//λ�����ݿɵ�
            Dictionary<string, string> data = GameConfigManager.Instance.GetCardById(i.ToString());

            ItemCard item = cardItem.AddComponent(System.Type.GetType(data["Script"])) as ItemCard;

            
            item.Init(data);
            item.dur = int.Parse(data["Arg1"]);

            RoleManager.Instance.BagList.Add(item);

        }
        
    }
    public void CreateItemCard()
    {
        //Ԥ�ȴ����ÿ���ʵ�� ���ھ�ͷ��
        for (int i = RoleManager.Instance.BagList.Count - 1; i >= 0; i--)
        {

            RoleManager.Instance.BagList[i].GetComponent<RectTransform>().anchoredPosition = new Vector2(-3000, -3000);//λ�����ݿɵ�


            
            FightCardManager.Instance.cardItemList.Add(RoleManager.Instance.BagList[i]);
            FightCardManager.Instance.cardList.Add(RoleManager.Instance.BagList[i].data["Id"]);
            RoleManager.Instance.BagList.RemoveAt(i);
        }
    }

    public void DrawCardItem(int count)
    {
        if (count > FightCardManager.Instance.cardList.Count)
        {
            count = FightCardManager.Instance.cardList.Count;
        }
        Vector2 pos = new Vector2(-807,-520);
        // Fisher-Yates ϴ���㷨���������е��ƴ���˳�����뵽���ƶ�
        for (int i = FightCardManager.Instance.cardItemList.Count - 1; i >= 0; i--)
        {
            int randomIndex = UnityEngine.Random.Range(0, i + 1);
            // ������ǰԪ�غ����������Ԫ��
            CardItem temp = FightCardManager.Instance.cardItemList[i];
            FightCardManager.Instance.cardItemList[i] = FightCardManager.Instance.cardItemList[randomIndex];
            FightCardManager.Instance.cardItemList[randomIndex] = temp;
        }
        for (int i = count-1; i >=0; i--)
        {
            
            AudioManager.Instance.PlayEffect("DrawCard");
            
            FightCardManager.Instance.cardItemList[i].transform.SetSiblingIndex(FightCardManager.Instance.cardItemList.Count);
            FightCardManager.Instance.cardItemList[i].GetComponent<RectTransform>().DOAnchorPos(pos,0.5f);
            Debug.Log(FightCardManager.Instance.cardItemList.Count);
            CardItemList.Add(FightCardManager.Instance.cardItemList[i]);
            FightCardManager.Instance.cardList.RemoveAt(i);
            FightCardManager.Instance.cardItemList.RemoveAt(i);
            UpdateCardCount();
            //GameObject cardItem = Instantiate(Resources.Load("UI/CardItem"), transform) as GameObject;
            //cardItem.GetComponent<RectTransform>().anchoredPosition = new Vector2(-800, -300);//λ�����ݿɵ�
            //var item = cardItem.AddComponent<CardItem>();
            //string id = FightCardManager.Instance.DrawCard();
            //Dictionary<string, string> data = GameConfigManager.Instance.GetCardById(id);

            //item.Init(data);
            //CardItemList.Add(item);
        }
    }

    public void UpdateCardItemPos()
    {
        float offset = 1000.0f/CardItemList.Count;
        Vector2 pos = new Vector2(-CardItemList.Count / 2.0f * offset + offset * 0.5f, -520);
        for(int i = 0;i < CardItemList.Count;i++)
        {
            CardItemList[i].GetComponent<RectTransform>().DOAnchorPos(pos,0.5f);
            pos.x = pos.x+offset;
        }
    }
    public void RemoveCard(CardItem cardItem)
    {
        AudioManager.Instance.PlayEffect("Use");

        //cardItem.enabled = false;

        FightCardManager.Instance.usedcardList.Add(cardItem.data["Id"]);
        FightCardManager.Instance.usedcarditem.Add(cardItem);
        Vector2 pos = new Vector2(-2000, -2000);
        
        CardItemList.Remove(cardItem);
        usedCardCountTxT.text = FightCardManager.Instance.usedcardList.Count.ToString();
        UpdateCardCount();
        //ˢ�¿���λ��
        UpdateCardItemPos();

        //�����Ƶ����ƶ�Ч��
        cardItem.GetComponent<RectTransform>().DOAnchorPos(new Vector2(-143,126), 0.5f);
        cardItem.transform.DOScale(0, 0.5f);
        cardItem.GetComponent<RectTransform>().DOAnchorPos(new Vector2(-2000, -2000), 0.5f);
        cardItem.transform.DOScale(0.602f, 0.5f);

    }
    public void DeleteCardItem(CardItem cardItem)
    {
        Destroy(cardItem.gameObject,1);
    }
}
