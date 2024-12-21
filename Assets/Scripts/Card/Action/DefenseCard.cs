using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
public class DefenseCard : ActionCard
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
            int armor = int.Parse(data["Arg0"]);
            AudioManager.Instance.PlayEffect("defense");





            FightManager.Instance.DenfenseCount += armor;
                //播放音效
                //刷新护甲及文本
            UIManager.Instance.GetUI<FightUI>("FightUI").UpdateDefense();

            //Vector3 pos = Camera.main.transform.position;
            //pos.y = 0;
            UIManager.Instance.ShowTip("防御提升！", Color.blue);




        }
        else
        {
            base.OnEndDrag(eventData);
        }
    }
}
