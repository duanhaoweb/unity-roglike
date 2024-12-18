using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{

    protected Dictionary<string, string> data;//�������ݱ���Ϣ

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

















        //��ʼ����������
        ATK = int.Parse(data["Attack"]);
        DEF = int.Parse(data["Defend"]);
        MaxHP = int.Parse(data["Hp"]);
        CurHP = MaxHP;

    }
    //����ж����ҽ���Ѫ��Խ�͹�������Խǿ��
    public void SetRandomAction() { }
    //���¹�������
    public void UpdateHp() {
    //���Լ����ı��������ݰ�
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
                CurHP = 0;//��������
            }
        }
    }
}
