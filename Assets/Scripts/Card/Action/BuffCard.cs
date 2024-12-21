using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
public class BuffCard : ActionCard
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
            int buff = int.Parse(data["Arg1"]);//Arg1�������Լ�����ֵ
            if (int.Parse(data["Arg0"]) > 0)
            {
                //���ù��������õĹ������������ڹ�����֮����buff����
            }
            FightManager.Instance.ATKBuff += buff;
            //������Ч
            //ˢ����ֵ���ı�




        }
        else
        {
            base.OnEndDrag(eventData);
        }
    }
}