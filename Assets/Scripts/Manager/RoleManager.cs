using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoleManager 
{
    public static RoleManager Instance =new RoleManager();

    public List<string> cardList=new List<string>();

    public List<CardItem> BagList = new List<CardItem>();
    

    public void Init()
    {

        CreatRole();
        //添加初始行动卡牌

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
    public void CreatRole()
    {
        GameObject obj = Object.Instantiate(Resources.Load("Model/Player")) as GameObject;//从资源路径加载对应的人物模型

        Player player = obj.AddComponent<Player>();//添加人物脚本

        obj.transform.position = new Vector3(-5, 1, -6);//人物的位置
    }

}
