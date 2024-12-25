using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//��Ƭս����ʼ��
public class FightInit : FightUnit
{
    public override void Init()
    {
        //ս����ʼ����ֵ
        FightManager.Instance.Init();
        // �л� BGM
        Debug.Log("�л� BGM������ 'Attack' ����");
        AudioManager.Instance.PlayBGM("Attack");

        //��������
        EnemyManager.Instance.LoadRes("10001");//��ȡ�ؿ�1�ĵ�����Ϣ

        //��ʼ��ս������
        FightCardManager.Instance.Init();
        

        // ��ʾս��ҳ��
        Debug.Log("��ʾս��ҳ�棺���� 'FightUI'");
        UIManager.Instance.ShowUI<FightUI>("FightUI");
        //ʵ����ս������
        UIManager.Instance.GetUI<FightUI>("FightUI").CreateCardItem();
        //��ʼ������
        UIManager.Instance.GetUI<FightUI>("FightUI").UnitBag();
        //ʵ������Ʒ����
        UIManager.Instance.GetUI<FightUI>("FightUI").CreateItemCard();
        //�л�����һغ�
        FightManager.Instance.ChangeType(FightType.Player);
    }
    public override void OnUpdate()
    {
        base.OnUpdate();
    }
}
