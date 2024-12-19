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
        if (gameStartButton == null)
        {
            Debug.LogError("GameStartButton not found or Register failed!");
            return;
        }

        // 绑定事件处理方法
        gameStartButton.OnClick = onStartGameBtn;

    }

    private void onStartGameBtn(GameObject obj, PointerEventData pData)
    {

        Close();
        //战斗初始化
        FightManager.Instance.ChangeType(FightType.Init);

    }

}

