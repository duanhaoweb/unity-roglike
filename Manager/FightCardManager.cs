  using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//ս����Ƭ������
public class FightCardManager
{
    public static FightCardManager Instance = new FightCardManager();

    public List<string> cardList;//���Ѽ���

    public List<string> usedCardList;//���ƶ�

    //��ʼ��
    public void Init()
    {
        cardList = new List<string>();
        usedCardList = new List<string>();

        //��ʱ����
        List<string> tempList = new List<string>();
        //����ҿ��ƴ�����ʱ����

        tempList.AddRange(RoleManager.Instance.cardList);

        while (tempList.Count > 0)
        {
            //����±�
            int tempIndex = Random.Range(0, tempList.Count);

            //��ӵ�����
            cardList.Add(tempList[tempIndex]);

            //ɾ����ʱ�������ѽ����ƶѵĿ���
            tempList.RemoveAt(tempIndex);
        }

        Debug.Log(cardList.Count);
    }

    //�Ƿ��п����п����ؿ���
    public bool HasCard()
    {
        return cardList.Count > 0;
    }

    //�鿨
    public string DrawCard()
    {
        //�鿨�����һ�ſ�
        string id = cardList[cardList.Count-1];

        cardList.RemoveAt(cardList.Count-1);

        return id;
    }
}
