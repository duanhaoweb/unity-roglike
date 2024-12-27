using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
//开始页面（继承UIBase）
public class LoginUI : UIBase
{
    private void Awake()
    {
        var gameStartButton = Register("GameStartButton");
        var gameExitButton = Register("quitBtn");
        if (gameStartButton == null)
        {
            Debug.LogError("GameStartButton not found or Register failed!");
            return;
        }

        // 绑定事件处理方法
        gameStartButton.OnClick = onStartGameBtn;
        gameExitButton.OnClick = onExitGameBtn;
    }

    private void onStartGameBtn(GameObject obj, PointerEventData pData)
    {

        Close();

        //显示loginUI,创建的脚本名字记得跟预制体物体的名字一样
        UIManager.Instance.ShowUI<Text1UI>("Text1UI");


    }
    private void onExitGameBtn(GameObject obj, PointerEventData pData)
    {

        Close();
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

