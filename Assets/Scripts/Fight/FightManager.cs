using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// ս��ö��
public enum FightType
{
    None,
    Init,
    Player,
    Enemy,
    Win,
    Loss
}

/// <summary>
/// ս��������
/// </summary>
public class FightManager : MonoBehaviour
{
    public static FightManager Instance;
    public FightUnit fightUnit; // ս����Ԫ

    public static int MaxHP=10;//���Ѫ��
    public static int CurrentHP=10;//��ǰѪ��

    public int MaxPowerCount = 0;//����Ʒ��ã�������
    public int CurrentPowerCount = 0;//��ǰ����

    public int DefenseCount = 0;//�ܣ����ף�ֵ
    public int ATKBuff = 0;//����ֵ
    
    public void Init()
    {
        MaxPowerCount= 3;
        CurrentPowerCount = 3;
        DefenseCount = 0;
        ATKBuff = 0;
        
    }
    private void Awake()
    {
        Instance = this;

    }

    // �л�ս������
    public void ChangeType(FightType type)
    {
        Debug.Log($"�л�ս������Ϊ: {type}");

        switch (type)
        {
            case FightType.None:
                Debug.Log("�л��� None ���ͣ�δ�����κβ�����");
                fightUnit = null;
                break;
            case FightType.Init:
                Debug.Log("�л��� Init ���ͣ������µ� FightUnit��");
                fightUnit = new FightInit();
                break;
            case FightType.Player:
                Debug.Log("�л��� Player ���ͣ�������һغϡ�");
                fightUnit = new Fight_PlayerTurn();
                break;
            case FightType.Enemy:
                Debug.Log("�л��� Enemy ���ͣ�������˻غϡ�");
                fightUnit = new Fight_EnemyTurn();
                break;
            case FightType.Win:
                Debug.Log("�л��� Win ���ͣ����ʤ����");
                fightUnit = new Fight_Win();
                break;
            case FightType.Loss:
                Debug.Log("�л��� Loss ���ͣ����ʧ�ܡ�");
                fightUnit = new Fight_Loss();
                break;
        }

        if (fightUnit != null)
        {
            Debug.Log($"��ʼ��ս����Ԫ: {fightUnit.GetType().Name}");
            fightUnit.Init();
        }
        else
        {
            Debug.LogWarning("��ǰս����ԪΪ null��δִ�г�ʼ��������");
        }
    }

    ///���˻غ��������
    public void GetPlayerHit(int hit)
    {
        //�ȿۻ���ֵ
        if(DefenseCount>=hit)
        {
            DefenseCount -= hit;
            //���Ŷ�������Ч
            AudioManager.Instance.PlayEffect("Hurt-Defense");
        }
        else
        {
            hit = hit - DefenseCount;
            DefenseCount = 0;
            CurrentHP -= hit;
            //���Ŷ�������Ч
            AudioManager.Instance.PlayEffect("Hurt");
            if ( CurrentHP<= 0)
            {
                CurrentHP = 0;
                //��Ϸʧ��
                ChangeType(FightType.Loss);

            }

        }
        //������һ��ҳ��
        UIManager.Instance.GetUI<FightUI>("FightUI").UpdateHp();
        UIManager.Instance.GetUI<FightUI>("FightUI").UpdateDefense();

        UIManager.Instance.GetUI<FightUI>("FightUI").UpdatePower();
    }
    private void Update()
    {
        if (fightUnit != null)
        {

            fightUnit.OnUpdate();
        }
        else
        {
            Debug.Log("��ǰս����ԪΪ�գ�δִ�� OnUpdate ����");
        }
    }
}
