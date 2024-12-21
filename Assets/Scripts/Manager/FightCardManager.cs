using System.Collections;
using DG.Tweening;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.Timeline.Actions.MenuPriority;
using Unity.VisualScripting;
using UnityEngine.UI;

/// <summary>
/// ս�����ƹ�����
/// </summary>
public class FightCardManager:UIBase
{
    public static FightCardManager Instance = new FightCardManager();

    public List<string> cardList;//���ƶ�
    public List<CardItem> cardItemList;//����ʵ����

    public List<string> usedcardList;//���ƶ�
    public List<CardItem> usedcarditem;

    //��ʼ��
    public void Init()
    {
        cardList = new List<string>();
        cardItemList = new List<CardItem>();
        usedcardList = new List<string>();
        usedcarditem = new List<CardItem>();

        // ��ȡ���鲢���б߽���
        if (RoleManager.Instance == null || RoleManager.Instance.cardList == null || RoleManager.Instance.cardList.Count == 0)
        {
            Debug.LogError("RoleManager cardList is null or empty.");
            return;
        }

        // ���ƿ���
        cardList.AddRange(RoleManager.Instance.cardList);

        // Fisher-Yates ϴ���㷨���������е��ƴ���˳�����뵽���ƶ�
        for (int i = cardList.Count - 1; i >= 0; i--)
        {
            int randomIndex = Random.Range(0, i + 1);
            // ������ǰԪ�غ����������Ԫ��
            string temp = cardList[i];
            cardList[i] = cardList[randomIndex];
            cardList[randomIndex] = temp;
        }


        Debug.Log("Card count: " + cardList.Count);
    }
    //����Ƿ��п�
    public bool HasCard()
    {
        return cardList.Count > 0;
    }
    //�鿨
    public string DrawCard()
    {
        string id = this.cardList[cardList.Count - 1];

        this.cardList.RemoveAt(cardList.Count - 1);

        return id;
    }
}
