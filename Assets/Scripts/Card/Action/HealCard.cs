using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
public class HealCard : ActionCard
{
    private void Start()
    {
        transform.Find("bg").GetComponent<Image>().sprite = Resources.Load<Sprite>(data["BgImage"]);
        transform.Find("bg/icon").GetComponent<Image>().sprite = Resources.Load<Sprite>(data["Image"]);
        transform.Find("bg/msgTxt").GetComponent<Text>().text = string.Format(data["Des"], data["Arg1"]);
        transform.Find("bg/nameTxt").GetComponent<Text>().text = data["Name"];
        transform.Find("bg/useTxt").GetComponent<Text>().text = data["Expend"];
        transform.Find("bg/Text").GetComponent<Text>().text = GameConfigManager.Instance.GetCardTypeById(data["Type"])["Name"];

    }

    public override void OnEndDrag(PointerEventData eventData)
    {
        if (UseCard() == true)
        {
            int heal = int.Parse(data["Arg1"]);//Arg1代表对自己的数值

            FightManager.Instance.CurrentHP += heal;
            if (FightManager.Instance.CurrentHP >= FightManager.MaxHP)
            {
                FightManager.Instance.CurrentHP = FightManager.MaxHP;
            }
            AudioManager.Instance.PlayEffect("Heal");
            UIManager.Instance.ShowTip("生命恢复！", Color.green);
            //播放音效
            //刷新数值及文本
            UIManager.Instance.GetUI<FightUI>("FightUI").UpdateHp();



        }
        else
        {
            base.OnEndDrag(eventData);
        }
    }
}