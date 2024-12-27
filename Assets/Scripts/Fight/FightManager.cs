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

    public static int MaxHP=10;//最大血量
    public static int CurrentHP=10;//当前血量

    public int MaxPowerCount = 0;//最大卡牌费用（能量）
    public int CurrentPowerCount = 0;//当前能量

    public int DefenseCount = 0;//盾（护甲）值
    public int ATKBuff = 0;//增伤值
    
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

    ///敌人回合玩家受伤
    public void GetPlayerHit(int hit)
    {
        //先扣护盾值
        if(DefenseCount>=hit)
        {
            DefenseCount -= hit;
            //播放动画及音效
            AudioManager.Instance.PlayEffect("Hurt-Defense");
        }
        else
        {
            hit = hit - DefenseCount;
            DefenseCount = 0;
            CurrentHP -= hit;
            //播放动画及音效
            AudioManager.Instance.PlayEffect("Hurt");
            if ( CurrentHP<= 0)
            {
                CurrentHP = 0;
                //游戏失败
                ChangeType(FightType.Loss);

            }

        }
        //更新下一轮页面
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
            Debug.Log("当前战斗单元为空，未执行 OnUpdate 操作");
        }
    }
}
