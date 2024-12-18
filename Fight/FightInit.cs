using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//��Ƭս����ʼ��
public class FightInit : FightUnit
{
    public override void Init()
    {
        //��ʼ��ս������
        FightManager.Instance.Init();
        // �л� BGM
            Debug.Log("�л� BGM������ 'Attack' ����");
            AudioManager.Instance.PlayBGM("Attack");

        //��������
        EnemyManager.Instance.LoadRes("10001");//��ȡ�ؿ�1�ĵ�����Ϣ

        //��ʼ��ս����Ƭ
        FightCardManager.Instance.Init();
        // ��ʾս��ҳ��
        Debug.Log("��ʾս��ҳ�棺���� 'FightUI'");
            UIManager.Instance.ShowUI<FightUI>("FightUI");

    }
    public override void OnUpdate()
    {
        base.OnUpdate();
    }
}
