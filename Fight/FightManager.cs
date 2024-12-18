using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 战斗枚举
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
/// 战斗管理器
/// </summary>
public class FightManager : MonoBehaviour
{
    public static FightManager Instance;
    public FightUnit fightUnit; // 战斗单元

    public int MaxHp;//最大血量
    public int CurHp;//当前血量
    public int MaxAP;//最大行动点
    public int CurAP;//当前行动点
    public int Armor;//护甲值
    public int ATKBuff;//获得力量后下一次攻击增加的伤害
    //初始化角色数据
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
        Debug.Log("FightManager 已初始化");
    }

    // 切换战斗类型
    public void ChangeType(FightType type)
    {
        Debug.Log($"切换战斗类型为: {type}");

        switch (type)
        {
            case FightType.None:
                Debug.Log("切换到 None 类型，未进行任何操作。");
                fightUnit = null;
                break;
            case FightType.Init:
                Debug.Log("切换到 Init 类型，创建新的 FightUnit。");
                fightUnit = new FightInit();
                break;
            case FightType.Player:
                Debug.Log("切换到 Player 类型，进入玩家回合。");
                fightUnit = new Fight_PlayerTurn();
                break;
            case FightType.Enemy:
                Debug.Log("切换到 Enemy 类型，进入敌人回合。");
                fightUnit = new Fight_EnemyTurn();
                break;
            case FightType.Win:
                Debug.Log("切换到 Win 类型，玩家胜利。");
                fightUnit = new Fight_Win();
                break;
            case FightType.Loss:
                Debug.Log("切换到 Loss 类型，玩家失败。");
                fightUnit = new Fight_Loss();
                break;
        }

        if (fightUnit != null)
        {
            Debug.Log($"初始化战斗单元: {fightUnit.GetType().Name}");
            fightUnit.Init();
        }
        else
        {
            Debug.LogWarning("当前战斗单元为 null，未执行初始化操作。");
        }
    }

    private void Update()
    {
        if (fightUnit != null)
        {
            Debug.Log($"调用 {fightUnit.GetType().Name} 的 OnUpdate 方法");
            fightUnit.OnUpdate();
        }
        else
        {
            Debug.Log("当前战斗单元为空，未执行 OnUpdate 操作");
        }
    }
}
