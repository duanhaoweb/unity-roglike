using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoleManager 
{
    public static RoleManager Instance =new RoleManager();

    public List<string> cardList=new List<string>();

    public void Init()
    {
        GameObject obj = Object.Instantiate(Resources.Load("Model/Player")) as GameObject;//����Դ·�����ض�Ӧ�ĵ���ģ��

        Player player = obj.AddComponent<Player>();//��ӵ��˽ű�

        obj.transform.position = new Vector3(-5, 1, -6);
        //��ӳ�ʼ�ж�����
        
        int AttackCardCount = 5;
        int DenfenseCardCount = 5;
        
        int ActionCardCount = 10;

        for (int i = 0; i < AttackCardCount; i++)
        {
            cardList.Add("1000");
        }
        for (int i = 0; i <DenfenseCardCount; i++)
        {
            cardList.Add("1001");
        }
        for (int i = 0; i < ActionCardCount; i++)
        {
            int id=Random.Range(1003, 1011);
            cardList.Add(id.ToString());
        }
        

    }
}
