using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//失败
public class Fight_Loss : FightUnit
{
    public override void Init()
    {
        base.Init();
        FightManager.Instance.StopAllCoroutines();
        //显示敌人回合提示
        UIManager.Instance.ShowTip("GameOver", Color.red, delegate ()
        {
            Debug.Log("gameover");
        });
        Sleep(2);
        ExitGame();
    }
    public override void OnUpdate()
    {
        base.OnUpdate();
    }
    public IEnumerator Sleep(int n)
    {
        yield return new WaitForSeconds(n);
    }
    public   void ExitGame()
    {
      
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false; // 在编辑器中停止运行
#else
        Application.Quit(); // 在打包后的游戏中退出
#endif
    }
}
