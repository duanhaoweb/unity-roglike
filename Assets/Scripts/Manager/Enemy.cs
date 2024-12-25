using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

//���˵��ж�ö��
public enum ActionType
{
    None,
    Defend,//�ӷ���
    Attack,//�ӹ���

}

public class Enemy : MonoBehaviour
{
    protected Dictionary<string, string> data;//�������ݱ���Ϣ

    public ActionType type;
    public GameObject hpItemObj;
    public GameObject actionObj;

    //ui���

    public Transform attackTf;
    public Transform defendTf;
    public Text defendTxt;
    public Text hpTxt;
    public Image hpImg;

    //��ֵ���
    public int Defend;
    public int Attack;
    public int MaxHp;
    public int CurrentHp;
    private int index;

    //������
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
        //��ʼ����ֵ
        Attack=int.Parse(data["Attack"]);
        CurrentHp = int.Parse(data["Hp"]);
        MaxHp=CurrentHp;
        Defend = int.Parse(data["Defend"]);

        UpdateDefend();
        UpdateHp();
    }
    //���һ���ж�
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
    //���¶�ֵ��Ѫ��
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
            //���Ŷ�������Ч
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
                //��������
                AudioManager.Instance.PlayEffect("Die");
                //�����Ƴ�
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
                //����
                AudioManager.Instance.PlayEffect("Hurt");
            }

        }
        FightManager.Instance.ATKBuff = 0;
        //�ж���buff���ǿ�Ѫ
        if (hurt > 0)
        {
            FightManager.Instance.ATKBuff += hurt;
            UIManager.Instance.ShowTip("�����˺���ߣ�", Color.cyan);
            //UIManager.Instance.GetUI<FightUI>("FightUI").UpdateDefense();

        }
        else if (hurt < 0)
        {
            FightManager.Instance.CurrentHP += hurt;
            if (FightManager.Instance.CurrentHP < 0) FightManager.Instance.CurrentHP = 0;
            UIManager.Instance.ShowTip("�ܵ��˺���", Color.red);
            UIManager.Instance.GetUI<FightUI>("FightUI").UpdateHp();

        }
        //ˢ��Ѫ��
        UpdateDefend();
        UpdateHp();
        
    }
    public void Hit(int val)
    {
        val += FightManager.Instance.ATKBuff;

        if (Defend >= val)
        {
            Defend -= val;
            //���Ŷ�������Ч
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
                //��������
                AudioManager.Instance.PlayEffect("Die");
                //�����Ƴ�
                EnemyManager.Instance.DeleteEnemy(this);

                Destroy(gameObject, 1);
                Destroy(actionObj);
                Destroy(hpItemObj);
            }
            else
            {
                //����
                AudioManager.Instance.PlayEffect("Hurt");
            }

        }
        
        //ˢ��Ѫ��
        UpdateDefend();
        UpdateHp();

    }
    //���ع���ͷ�ϵ��ж���־
    public void HideAction()
    {
        attackTf.gameObject.SetActive(false);
        defendTf.gameObject.SetActive(false);
    }
    //�����ж�
    public IEnumerator DoAction()
    {
        HideAction();
        //���Ź��﹥������
        //�ȴ�ĳһʱ���ִ�ж�Ӧ����Ϊ����Ҫ���õ�excel��
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
                //��ҿ�Ѫ
                FightManager.Instance.GetPlayerHit(Attack);
                //�����΢΢����
                Camera.main.DOShakePosition(0.1f,0.2f,5,45);
                break;
        }
        //�ȴ������������
        yield return new WaitForSeconds(1);
        //���Ŵ�������

    }
}
