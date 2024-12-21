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
        this.transform.DOScale(1.7f, 0.25f);
    }
    public void OnUnSelect()
    {
        this.transform.DOScale(1.3f, 0.25f);
    }
    public void Hit(int val)
    {
        if (Defend >= val)
        {
            Defend-=val;
            //���Ŷ�������Ч
        }
        else
        {
            val = val - Defend;
            Defend = 0;
            CurrentHp -= val;
            if(CurrentHp < 0)
            {
                CurrentHp = 0;
                //��������
                //�����Ƴ�
                EnemyManager.Instance.DeleteEnemy(this);

                Destroy(gameObject, 1);
                Destroy(actionObj);
                Destroy(hpItemObj);
            }
            else
            {
                //����
            }

        }
        //ˢ��Ѫ��
        UpdateDefend();
        UpdateHp();
        
    }
}
