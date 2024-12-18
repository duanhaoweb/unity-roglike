using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
public class CardItem : MonoBehaviour
{
    public Dictionary<string, string> data;//������Ϣ

    public void Init(Dictionary<string,string> data)
    {
        this.data = data;
    }
    public virtual void OnEndDrag(PointerEventData eventData)
    {

    }








    public virtual bool UseCard()
    {
        int cost = int.Parse(data["Expend"]);
        if (cost > FightManager.Instance.CurAP)
        {
            //���ò���

            Debug.Log("���ò���");
            return false;
        }
        else
        {
            //�۷�
            FightManager.Instance.CurAP -= cost;
            //ˢ�·����ı�
            if (this is ItemCard)
            {
                (this as ItemCard).dur--;
                if ((this as ItemCard).dur <= 0)
                {
                    UIManager.Instance.GetUI<FightUI>("FightUI").RemoveCard(this);
                }
            }
            else
            {

                //ɾ���ù��Ŀ���
                UIManager.Instance.GetUI<FightUI>("FightUI").RemoveCard(this);
            }

            return true;
        }
    }
    

    //��������ʹ����Ч
    public void CardEffect(Vector3 pos)
    {
        GameObject effect = Instantiate(Resources.Load(data["Effects"]))as GameObject;
        Destroy(effect, 2);
    }
}
