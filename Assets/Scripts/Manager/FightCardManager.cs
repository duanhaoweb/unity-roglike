  using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// ս�����ƹ�����
/// </summary>
public class FightCardManager
{
    public static FightCardManager Instance = new FightCardManager();

    public List<string> cardList;//���ƶ�

    public List<string> usedcardList;//���ƶ�

    //��ʼ��
    public void Init()
    {
        cardList = new List<string>();
        usedcardList = new List<string>();

        // ��ȡ���鲢���б߽���
        if (RoleManager.Instance == null || RoleManager.Instance.cardList == null || RoleManager.Instance.cardList.Count == 0)
        {
            Debug.LogError("RoleManager cardList is null or empty.");
            return;
        }

        // ���ƿ���
        cardList.AddRange(RoleManager.Instance.cardList);

        // Fisher-Yates ϴ���㷨���������е��ƴ���˳�����뵽���ƶ�
        for (int i = cardList.Count - 1; i > 0; i--)
        {
            int randomIndex = Random.Range(0, i + 1);
            // ������ǰԪ�غ����������Ԫ��
            string temp = cardList[i];
            cardList[i] = cardList[randomIndex];
            cardList[randomIndex] = temp;
        }

        Debug.Log("Card count: " + cardList.Count);
    }

}
