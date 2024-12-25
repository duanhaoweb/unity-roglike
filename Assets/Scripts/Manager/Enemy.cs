using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
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
    private int index;

    //组件相关
    SkinnedMeshRenderer _meshRenderer;

    public void Init(Dictionary<string,string>data)
    {
        this.data = data;
    }
    void Start()
    {
        //_meshRenderer=transform.GetComponentInChildren<SkinnedMeshRenderer>();
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
    public void OnSelect()
    {
        index=this.transform.GetSiblingIndex();
        this.transform.DOScale(2.0f, 0.25f);
        transform.SetAsLastSibling();
    }
    public void OnUnSelect()
    {
        this.transform.DOScale(1.3f, 0.25f);
        transform.SetSiblingIndex(index);
    }
    public void Hit(int val,int hurt)
    {
        val += FightManager.Instance.ATKBuff;
   
<<<<<<< HEAD
        if (Defend > val)
=======
        if (Defend >= val)
>>>>>>> 3c6361cd8efdc39327dc4171c22830d48c2fb0d2
        {
            Defend-=val;
            //播放动画及音效
            AudioManager.Instance.PlayEffect("Hurt");
        }
        else
        {
            val = val - Defend;
            Defend = 0;
            CurrentHp -= val;
            if(CurrentHp <= 0)
            {
                CurrentHp = 0;
                //播放死亡
                AudioManager.Instance.PlayEffect("Die");
                //敌人移除
                EnemyManager.Instance.DeleteEnemy(this);

                Destroy(gameObject, 1);
                Destroy(actionObj);
                Destroy(hpItemObj);
                if(EnemyManager.Instance.enemyList.Count==0)
                {
                    FightManager.Instance.ChangeType(FightType.Win);
                }
            }
            else
            {
                //受伤
                AudioManager.Instance.PlayEffect("Hurt");
            }

        }
        FightManager.Instance.ATKBuff = 0;
        //判定加buff还是扣血
        if (hurt > 0)
        {
            FightManager.Instance.ATKBuff += hurt;
            UIManager.Instance.ShowTip("攻击伤害提高！", Color.cyan);
            //UIManager.Instance.GetUI<FightUI>("FightUI").UpdateDefense();

        }
        else if (hurt < 0)
        {
            FightManager.Instance.CurrentHP += hurt;
            if (FightManager.Instance.CurrentHP < 0) FightManager.Instance.CurrentHP = 0;
            UIManager.Instance.ShowTip("受到伤害！", Color.red);
            UIManager.Instance.GetUI<FightUI>("FightUI").UpdateHp();

        }
        //刷新血量
        UpdateDefend();
        UpdateHp();
        
    }
    public void Hit(int val)
    {
        val += FightManager.Instance.ATKBuff;

        if (Defend >= val)
        {
            Defend -= val;
            //播放动画及音效
            AudioManager.Instance.PlayEffect("Hurt");
        }
        else
        {
            val = val - Defend;
            Defend = 0;
            CurrentHp -= val;
            if (CurrentHp < 0)
            {
                CurrentHp = 0;
                //播放死亡
                AudioManager.Instance.PlayEffect("Die");
                //敌人移除
                EnemyManager.Instance.DeleteEnemy(this);

                Destroy(gameObject, 1);
                Destroy(actionObj);
                Destroy(hpItemObj);
            }
            else
            {
                //受伤
                AudioManager.Instance.PlayEffect("Hurt");
            }

        }
        
        //刷新血量
        UpdateDefend();
        UpdateHp();

    }
    //隐藏怪物头上的行动标志
    public void HideAction()
    {
        attackTf.gameObject.SetActive(false);
        defendTf.gameObject.SetActive(false);
    }
    //敌人行动
    public IEnumerator DoAction()
    {
        HideAction();
        //播放怪物攻击动画
        //等待某一时间后执行对应的行为（都要配置到excel表）
        yield return new WaitForSeconds(0.5f);
        switch (type) 
        {
            case ActionType.None:
                break;
            case ActionType.Defend:
                Defend += 5;
                UpdateDefend();
                break;
            case ActionType.Attack:
                //玩家扣血
                FightManager.Instance.GetPlayerHit(Attack);
                //摄像机微微颤抖
                Camera.main.DOShakePosition(0.1f,0.2f,5,45);
                break;
        }
        //等待动画播放完毕
        yield return new WaitForSeconds(1);
        //播放待机动画

    }
}
