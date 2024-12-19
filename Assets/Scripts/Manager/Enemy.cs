using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//敌人的行动枚举
public enum ActionType
{
    None,
    Defend,//加防御
    Attack,//加攻击

}

public class Enemy : MonoBehaviour
{
    protected Dictionary<string, string> data;//敌人数据表信息

    public ActionType type;
    public GameObject hpItemObj;
    public GameObject actionObj;

    //ui相关

    public Transform attackTf;
    public Transform defendTf;
    public Text defendTxt;
    public Text hpTxt;
    public Image hpImg;

    //数值相关
    public int Defend;
    public int Attack;
    public int MaxHp;
    public int CurrentHp;


    public void Init(Dictionary<string,string>data)
    {
        this.data = data;
    }
    void Start()
    {
        type = ActionType.None;
        hpItemObj = UIManager.Instance.CreatHpItem();

        actionObj = UIManager.Instance.CreatActionIcon();

        attackTf = actionObj.transform.Find("attack");
        defendTf = actionObj.transform.Find("defend");

        defendTxt=hpItemObj.transform.Find("fangyu/Text").GetComponent<Text>();
        hpTxt = hpItemObj.transform.Find("hpTxt").GetComponent<Text>();
        hpImg = hpItemObj.transform.Find("fill").GetComponent<Image>();

        hpItemObj.transform.position=Camera.main.WorldToScreenPoint(transform.position + Vector3.down * 1.5f);
        actionObj.transform.position = Camera.main.WorldToScreenPoint(transform.Find("head").position - Vector3.down * 0.4f);
        SetAction();
        //Hp	Attack	Defend
        //初始化数值
        Attack=int.Parse(data["Attack"]);
        CurrentHp = int.Parse(data["Hp"]);
        MaxHp=CurrentHp;
        Defend = int.Parse(data["Defend"]);

        UpdateDefend();
        UpdateHp();
    }
    //随机一个行动
    public void SetAction()
    {
        int ran = Random.Range(1, 3);
        type = (ActionType)ran;
        switch (type)
        {
            case ActionType.Attack:
                attackTf.gameObject.SetActive(true);
                defendTf.gameObject.SetActive(false);
                break;
            case ActionType.Defend:
                attackTf.gameObject.SetActive(false);
                defendTf.gameObject.SetActive(true);
                break;
            case ActionType.None:
                break;
            default:
                break;
        }
    }
    //更新盾值和血量
    public void UpdateHp()
    {
        hpTxt.text = CurrentHp + "/" + MaxHp;
        hpImg.fillAmount = (float)CurrentHp / (float)MaxHp;
    }

    public void UpdateDefend()
    {
        defendTxt.text=Defend.ToString();
    }
}
