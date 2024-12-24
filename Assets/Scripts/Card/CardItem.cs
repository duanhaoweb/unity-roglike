using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using DG.Tweening;
using static UnityEditor.PlayerSettings;

public class CardItem : MonoBehaviour,IPointerEnterHandler,IPointerExitHandler,IBeginDragHandler,IDragHandler,IEndDragHandler
{
    public Dictionary<string, string> data;//������Ϣ
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
            //���ò���
            AudioManager.Instance.PlayEffect("UseCardFail");
            UIManager.Instance.ShowTip("���ò���",Color.red);
            return false;
        }
        else
        {
            //�۷�
            FightManager.Instance.CurrentPowerCount -= cost;
            //ˢ�·����ı�
            UIManager.Instance.GetUI<FightUI>("FightUI").UpdatePower();
            UIManager.Instance.GetUI<FightUI>("FightUI").RemoveCard(this);
            
            if (this is ItemCard)
            {
                (this as ItemCard).dur--;
                
                transform.Find("bg/msgTxt").GetComponent<Text>().text = string.Format(data["Des"], data["Arg0"], (this as ItemCard).dur);
                
                if ((this as ItemCard).dur <= 0)
                {
                    UIManager.Instance.GetUI<FightUI>("FightUI").DeleteCardItem(this);
                    //�;�Ϊ0ֱ��ɾ������Ʒ
                }
            }
            

            return true;
        }
    }
    

    //��������ʹ����Ч
    public void CardEffect(Vector3 pos)
    {
        GameObject effect = Instantiate(Resources.Load(data["Effects"]))as GameObject;
        effect.transform.position = pos;
        Destroy(effect, 2);
    }

    //������
    public void OnPointerEnter(PointerEventData eventData)
    {
        
        transform.DOScale(0.85f, 0.25f);
        GetComponent<RectTransform>().DOAnchorPosY(y+100,0.25f);
        index = transform.GetSiblingIndex();
        transform.SetAsLastSibling();
    }
    //����˳�
    public void OnPointerExit(PointerEventData eventData)
    {
        
        transform.DOScale(0.602f, 0.25f);
        GetComponent<RectTransform>().DOAnchorPosY(y, 0.25f);
        transform.SetSiblingIndex(index);
    }
    Vector2 initPos;//��קʱ��¼����λ��
    
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
