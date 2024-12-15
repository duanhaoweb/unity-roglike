using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoleManager 
{
    public static RoleManager Instance =new RoleManager();

    public List<string> cardList=new List<string>();

    public void Init()
    {
        
        //ÃÌº”≥ı ºø®≈∆
        int AttackCardCount = 4;
        int DenfenseCardCount = 4;
        int DealCardCount = 2;
        int BuffCardCount = 2;
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
        for (int i = 0; i < BuffCardCount; i++)
        {
            cardList.Add("1003");
        }
    }
}
