using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using DG.Tweening;
using static UnityEditor.PlayerSettings;

public class CardItem : MonoBehaviour,IPointerEnterHandler,IPointerExitHandler,IBeginDragHandler,IDragHandler,IEndDragHandler
{
    public Dictionary<string, string> data;//卡牌信息
    private int index;
    public float y;
    public void Init(Dictionary<string,string> data)
    {
        this.data = data;
        y = -520;
        Debug.Log("success!!!!");
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
   
    public virtual bool UseCard()
    {
        int cost = int.Parse(data["Expend"]);
        if (cost > FightManager.Instance.CurrentPowerCount)
        {
            //费用不足
            AudioManager.Instance.PlayEffect("UseCardFail");
            UIManager.Instance.ShowTip("费用不足",Color.red);
            return false;
        }
        else
        {
            //扣费
            FightManager.Instance.CurrentPowerCount -= cost;
            //刷新费用文本
            UIManager.Instance.GetUI<FightUI>("FightUI").UpdatePower();
            UIManager.Instance.GetUI<FightUI>("FightUI").RemoveCard(this);
            
            if (this is ItemCard)
            {
                (this as ItemCard).dur--;
                
                transform.Find("bg/msgTxt").GetComponent<Text>().text = string.Format(data["Des"], data["Arg0"], (this as ItemCard).dur);
                
                if ((this as ItemCard).dur <= 0)
                {
                    UIManager.Instance.GetUI<FightUI>("FightUI").DeleteCardItem(this);
                    //耐久为0直接删除该物品
                }
            }
            

            return true;
        }
    }
    

    //创建卡牌使用特效
    public void CardEffect(Vector3 pos)
    {
        GameObject effect = Instantiate(Resources.Load(data["Effects"]))as GameObject;
        effect.transform.position = pos;
        Destroy(effect, 2);
    }

    //鼠标进入
    public void OnPointerEnter(PointerEventData eventData)
    {
        
        transform.DOScale(0.85f, 0.25f);
        GetComponent<RectTransform>().DOAnchorPosY(y+100,0.25f);
        index = transform.GetSiblingIndex();
        transform.SetAsLastSibling();
    }
    //鼠标退出
    public void OnPointerExit(PointerEventData eventData)
    {
        
        transform.DOScale(0.602f, 0.25f);
        GetComponent<RectTransform>().DOAnchorPosY(y, 0.25f);
        transform.SetSiblingIndex(index);
    }
    Vector2 initPos;//拖拽时记录卡牌位置
    
    public virtual void OnBeginDrag(PointerEventData eventData)
    {
        initPos = transform.GetComponent<RectTransform>().anchoredPosition;
        
            AudioManager.Instance.PlayEffect ("CardClick"); 
        
        
    }
    public virtual  void OnDrag(PointerEventData eventData)
    {
        Vector2 pos;
        if (RectTransformUtility.ScreenPointToLocalPointInRectangle(
            transform.parent.GetComponent<RectTransform>(),
            eventData.position,
            eventData.pressEventCamera,
            out pos
            ))
        {
            transform.GetComponent<RectTransform>().anchoredPosition = pos;
        }
    }
    public virtual void OnEndDrag(PointerEventData eventData)
    {
        transform.GetComponent<RectTransform>().anchoredPosition=initPos;
        transform.SetSiblingIndex(index);
        
    }

    
}
