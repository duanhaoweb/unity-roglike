using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
public class AddItem : ItemCard
{
    private AddItem()
    {
        dur = int.Parse(data["Arg0"]);
    }
    private void Start()
    {
        transform.Find("bg").GetComponent<Image>().sprite = Resources.Load<Sprite>(data["BgImage"]);
        transform.Find("bg/icon").GetComponent<Image>().sprite = Resources.Load<Sprite>(data["Image"]);
        transform.Find("bg/msgTxt").GetComponent<Text>().text = string.Format(data["Des"], data["Arg0"],dur);
        transform.Find("bg/nameTxt").GetComponent<Text>().text = data["Name"];
        transform.Find("bg/useTxt").GetComponent<Text>().text = data["Expend"];
        transform.Find("bg/Text").GetComponent<Text>().text = GameConfigManager.Instance.GetCardTypeById(data["Type"])["Name"];
        dur = int.Parse(data["Arg1"]);
    }
    public override void OnEndDrag(PointerEventData eventData)
    {

        if (UseCard() == true)
        {
            int num = int.Parse(data["Arg0"]);


            //�ж������Ƿ��п�
            if (FightCardManager.Instance.HasCard() == true)
            {
                //���Ӧ�Ŀ���������Ӧ�����Ŀ��ƶ���
                UIManager.Instance.GetUI<FightUI>("FightUI").DrawCardItem(num);
                UIManager.Instance.GetUI<FightUI>("FightUI").UpdateCardItemPos();
                AudioManager.Instance.PlayEffect("AddCard");

                //������Ч
                //ˢ�»��׼��ı�

                UIManager.Instance.ShowTip("������ƣ�", Color.yellow);
                

            }
            else
            {
                base.OnEndDrag(eventData);
            }


        }
        else
        {
            base.OnEndDrag(eventData);
        }
    }
}