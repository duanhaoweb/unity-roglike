using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
public class AddCard : ActionCard
{
    private void Start()
    {
        transform.Find("bg").GetComponent<Image>().sprite = Resources.Load<Sprite>(data["BgImage"]);
        transform.Find("bg/icon").GetComponent<Image>().sprite = Resources.Load<Sprite>(data["Image"]);
        transform.Find("bg/msgTxt").GetComponent<Text>().text = string.Format(data["Des"], data["Arg0"]);
        transform.Find("bg/nameTxt").GetComponent<Text>().text = data["Name"];
        transform.Find("bg/useTxt").GetComponent<Text>().text = data["Expend"];
        transform.Find("bg/Text").GetComponent<Text>().text = GameConfigManager.Instance.GetCardTypeById(data["Type"])["Name"];

    }
    public override void OnEndDrag(PointerEventData eventData)
    {
        if (UseCard() == true)
        {
            int num = int.Parse(data["Arg0"]);//arg0Ϊ�鿨�� arg1>0�Ӷܣ�<0��Ѫ
            //int val = int.Parse(data["Arg1"]);
            
            
                //�ж������Ƿ��п�
                if (FightCardManager.Instance.HasCard()==true)
                {
                //���Ӧ�Ŀ���������Ӧ�����Ŀ��ƶ���
                UIManager.Instance.GetUI<FightUI>("FightUI").DrawCardItem(num);
                UIManager.Instance.GetUI<FightUI>("FightUI").UpdateCardItemPos();

                //�ж��Ӷܻ��ǿ�Ѫ
                //if (val>0)
                //    {
                //        FightManager.Instance.DenfenseCount += val;
                //    }else if(val<0)
                //    {
                //        FightManager.Instance.CurrentHP -= val;
                //        if(FightManager.Instance.CurrentHP < 0) FightManager.Instance.CurrentHP = 0;
                //    }

                AudioManager.Instance.PlayEffect("AddCard");

                //������Ч
                //ˢ�»��׼��ı�
               

                //Vector3 pos = Camera.main.transform.position;
                //pos.y = 0;
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
