using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using DG.Tweening;
public class CardItem : MonoBehaviour,IPointerEnterHandler,IPointerExitHandler
{
    public Dictionary<string, string> data;//卡牌信息
    private int index;
    public void Init(Dictionary<string,string> data)
    {
        this.data = data;
    }

    private void Start()
    {
        transform.Find("bg").GetComponent<Image>().sprite = Resources.Load<Sprite>(data["BgImage"]);
        transform.Find("bg/icon").GetComponent<Image>().sprite = Resources.Load<Sprite>(data["Image"]);
        transform.Find("bg/msgTxt").GetComponent<Text>().text = string.Format(data["Des"], data["Arg0"]);
        transform.Find("bg/nameTxt").GetComponent<Text>().text = data["Name"];
        transform.Find("bg/useTxt").GetComponent<Text>().text = data["Expend"];
        transform.Find("bg/Text").GetComponent<Text>().text = GameConfigManager.Instance.GetCardTypeById(data["Type"])["Name"];
    }
    public virtual void OnEndDrag(PointerEventData eventData)
    {

    }








    public virtual bool UseCard()
    {
        int cost = int.Parse(data["Expend"]);
        if (cost > FightManager.Instance.CurrentPowerCount)
        {
            //费用不足

            Debug.Log("费用不足");
            return false;
        }
        else
        {
            //扣费
            FightManager.Instance.CurrentPowerCount -= cost;
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
    //鼠标进入
    public void OnPointerEnter(PointerEventData eventData)
    {
        transform.DOScale(1, 0.25f);
        index = transform.GetSiblingIndex();
        transform.SetAsLastSibling();
    }
    //鼠标退出
    public void OnPointerExit(PointerEventData eventData)
    {
        transform.DOScale(0.602f, 0.25f);
        transform.SetSiblingIndex(index);
    }
}
