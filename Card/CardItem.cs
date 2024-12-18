using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
public class CardItem : MonoBehaviour
{
    public Dictionary<string, string> data;//卡牌信息

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
            //费用不足

            Debug.Log("费用不足");
            return false;
        }
        else
        {
            //扣费
            FightManager.Instance.CurAP -= cost;
            //刷新费用文本
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

                //删除用过的卡牌
                UIManager.Instance.GetUI<FightUI>("FightUI").RemoveCard(this);
            }

            return true;
        }
    }
    

    //创建卡牌使用特效
    public void CardEffect(Vector3 pos)
    {
        GameObject effect = Instantiate(Resources.Load(data["Effects"]))as GameObject;
        Destroy(effect, 2);
    }
}
