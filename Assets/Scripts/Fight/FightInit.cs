using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 卡片战斗初始化
public class FightInit : FightUnit
{
    public static int levelIndex = 3u;

    public override void Init()
    {

        // 以下为战斗初始化数值逻辑

        // 战斗初始化数值
        FightManager.Instance.Init();

        // 切换 BGM
        AudioManager.Instance.PlayBGM("AttackBGM");
        AudioManager.Instance.PlayEffect("Battle-start");

        // 敌人生成
        EnemyManager.Instance.LoadRes($"1000{levelIndex}"); // 读取关卡1的敌人信息
        levelIndex++;

        // 初始化战斗卡牌
        FightCardManager.Instance.Init();

        // 显示战斗页面
        Debug.Log("显示战斗页面：加载 'FightUI'");
        UIManager.Instance.ShowUI<FightUI>("FightUI");

        // 实例化战斗卡牌
        UIManager.Instance.GetUI<FightUI>("FightUI").CreateCardItem();

        // 初始化背包
        UIManager.Instance.GetUI<FightUI>("FightUI").UnitBag();

        // 实例化物品卡牌
        UIManager.Instance.GetUI<FightUI>("FightUI").CreateItemCard();

        // 切换到玩家回合
        FightManager.Instance.ChangeType(FightType.Player);
    }

    public override void OnUpdate()
    {
        base.OnUpdate();
    }


}
