using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
//开始页面（继承UIBase）
public class Text2UI : UIBase
{
    private void Awake()
    {
        int i = FightInit.levelIndex;

        var gameReturnButton = Register("returnBtn");

        gameReturnButton.OnClick = onReturnGameBtn1;
    }


    private void onReturnGameBtn1(GameObject obj, PointerEventData pData)
    {

        Close();
        //战斗初始化
       ExitGame();

    }
    public void ExitGame()
    {

#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false; // 在编辑器中停止运行
#else
        Application.Quit(); // 在打包后的游戏中退出
#endif
    }
}

