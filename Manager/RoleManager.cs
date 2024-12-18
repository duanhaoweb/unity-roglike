using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class RoleManager 
{
    public static RoleManager Instance =new RoleManager();

    public List<string> cardList;

    public void Init()
    {
        cardList = new List<string>();
        //添加初始卡牌，现在是随机生成，之后可改为自定义卡组
        int ActionCardCount = 5;
        int ItemCardCount = 5;
        
        for (int i = 0; i < ActionCardCount; i++)
        {
            int cardTypeid = Random.Range(1001, 1011);
            cardList.Add(cardTypeid.ToString());
        }
        for (int i = 0; i <ItemCardCount; i++)
        {
            int cardTypeid = Random.Range(1012, 1022);
            cardList.Add(cardTypeid.ToString());
        }
        
    }
}
