using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//胜利
public class Fight_Win : FightUnit
{
    public override void Init()
    {
        base.Init();
        FightManager.Instance.StopAllCoroutines();
        //显示敌人回合提示
        UIManager.Instance.ShowTip("Victory", Color.yellow, delegate ()
        {
            Debug.Log("Victory");
        });
        Sleep(1);
        UIManager.Instance.GetUI<FightUI>("FightUI").Close();
        // 显示选择页面

        UIManager.Instance.ShowUI<SelectCardUI>("SelectCardUI");
    }
    public override void OnUpdate()
    {
        base.OnUpdate();
    }
    public IEnumerator Sleep(int n)
    {
        yield return new WaitForSeconds(n);
    }
}
