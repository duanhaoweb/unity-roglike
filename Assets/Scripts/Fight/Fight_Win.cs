using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//胜利
public class Fight_Win : FightUnit
{
    public override void Init()
    {
        base.Init();
        int i = FightInit.levelIndex;
        // 关闭拖动路径 UI
        UIManager.Instance.CloseUI("LineUI");
        Cursor.visible = true;
        DOTween.KillAll();

        FightManager.Instance.StopAllCoroutines();
        //显示敌人回合提示
        UIManager.Instance.ShowTip("Victory", Color.yellow, delegate ()
        {
            Debug.Log("Victory");
        });

        AudioManager.Instance.PlayEffect("Victory");
        Sleep(1);
        UIManager.Instance.GetUI<FightUI>("FightUI").Close();
        // 显示选择页面
        if(i!=4)
        UIManager.Instance.ShowUI<SelectCardUI>("SelectCardUI");
        else
        {
            UIManager.Instance.ShowUI<Text2UI>("Text2UI");
        }
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
