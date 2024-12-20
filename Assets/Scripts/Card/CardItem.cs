using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using DG.Tweening;
public class CardItem : MonoBehaviour,IPointerEnterHandler,IPointerExitHandler
{
    public Dictionary<string, string> data;//������Ϣ
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
            //���ò���

            Debug.Log("���ò���");
            return false;
        }
        else
        {
            //�۷�
            FightManager.Instance.CurrentPowerCount -= cost;
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
    //������
    public void OnPointerEnter(PointerEventData eventData)
    {
        transform.DOScale(1, 0.25f);
        index = transform.GetSiblingIndex();
        transform.SetAsLastSibling();
    }
    //����˳�
    public void OnPointerExit(PointerEventData eventData)
    {
        transform.DOScale(0.602f, 0.25f);
        transform.SetSiblingIndex(index);
    }
}
