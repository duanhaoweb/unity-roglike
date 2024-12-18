using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{

    protected Dictionary<string, string> data;//敌人数据表信息

    public int ATK;
    public int DEF;
    public int MaxHP;
    public int CurHP;
    public void Init(Dictionary<string,string>data)
    {
        this.data = data;
    }
    void Start()
    {

















        //初始化怪物数据
        ATK = int.Parse(data["Attack"]);
        DEF = int.Parse(data["Defend"]);
        MaxHP = int.Parse(data["Hp"]);
        CurHP = MaxHP;

    }
    //随机行动（我建议血量越低攻击欲望越强）
    public void SetRandomAction() { }
    //更新怪物数据
    public void UpdateHp() {
    //你自己往文本里填数据吧
    }
    public void UpdateDEF() { 
    
    }




    public void Hit(int val)
    {
       if(DEF>=val)
        {
            DEF -= val;
        }
        else
        {
            val=val-DEF;
            DEF = 0;
            CurHP -= val;
            if (CurHP <= 0)
            {
                CurHP = 0;//怪物死了
            }
        }
    }
}
