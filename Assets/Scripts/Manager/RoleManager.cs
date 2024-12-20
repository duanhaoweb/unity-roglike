using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoleManager 
{
    public static RoleManager Instance =new RoleManager();

    public List<string> cardList=new List<string>();

    public void Init()
    {
        GameObject obj = Object.Instantiate(Resources.Load("Model/Player")) as GameObject;//从资源路径加载对应的敌人模型

        Player player = obj.AddComponent<Player>();//添加敌人脚本

        obj.transform.position = new Vector3(-5, 1, -6);
        //添加初始卡牌
        int AttackCardCount = 4;
        int DenfenseCardCount = 4;
        int DealCardCount = 2;

        for (int i = 0; i < AttackCardCount; i++)
        {
            cardList.Add("1000");
        }
        for (int i = 0; i <DenfenseCardCount; i++)
        {
            cardList.Add("1001");
        }
        for (int i = 0; i < DealCardCount; i++)
        {
            cardList.Add("1002");
        }

    }
}
