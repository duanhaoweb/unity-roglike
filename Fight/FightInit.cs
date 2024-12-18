using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//卡片战斗初始化
public class FightInit : FightUnit
{
    public override void Init()
    {
        //初始化战斗数据
        FightManager.Instance.Init();
        // 切换 BGM
            Debug.Log("切换 BGM：播放 'Attack' 音乐");
            AudioManager.Instance.PlayBGM("Attack");

        //敌人生成
        EnemyManager.Instance.LoadRes("10001");//读取关卡1的敌人信息

        //初始化战斗卡片
        FightCardManager.Instance.Init();
        // 显示战斗页面
        Debug.Log("显示战斗页面：加载 'FightUI'");
            UIManager.Instance.ShowUI<FightUI>("FightUI");

    }
    public override void OnUpdate()
    {
        base.OnUpdate();
    }
}
