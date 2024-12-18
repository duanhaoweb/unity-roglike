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

    public int MaxHp;//���Ѫ��
    public int CurHp;//��ǰѪ��
    public int MaxAP;//����ж���
    public int CurAP;//��ǰ�ж���
    public int Armor;//����ֵ
    public int ATKBuff;//�����������һ�ι������ӵ��˺�
    //��ʼ����ɫ����
    public void Init()
    {
        MaxHp = 70;
        CurHp = 70;
        MaxAP = 3;
        CurAP = 3;
        Armor = 0;
        ATKBuff = 0;
    }
    private void Awake()
    {
        Instance = this;
        Debug.Log("FightManager �ѳ�ʼ��");
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

    private void Update()
    {
        if (fightUnit != null)
        {
            Debug.Log($"���� {fightUnit.GetType().Name} �� OnUpdate ����");
            fightUnit.OnUpdate();
        }
        else
        {
            Debug.Log("��ǰս����ԪΪ�գ�δִ�� OnUpdate ����");
        }
    }
}
