using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
//开始页面（继承UIBase）
public class SelectCardUI : UIBase
{
    private void Awake()
    {
        var gameButton1 = Register("content/Btn1");
        var gameButton2 = Register("content/Btn2");
        var gameButton3 = Register("content/Btn3");
        var gameReturnButton = Register("content/returnBtn");

        // 绑定事件处理方法
        gameButton1.OnClick = onStartGameBtn1;
        gameButton1.OnClick = onStartGameBtn2;
        gameButton1.OnClick = onStartGameBtn3;
        gameReturnButton.OnClick = onReturnGameBtn;
    }

    private void onStartGameBtn1(GameObject obj, PointerEventData pData)
    {

        Close();

        //进入地图页面
       // UIManager.Instance.ShowUI<FightUI>("FightUI");
    }
    private void onStartGameBtn2(GameObject obj, PointerEventData pData)
    {

        Close();

        FightManager.MaxHP += 5;
        //进入地图页面
        // UIManager.Instance.ShowUI<FightUI>("FightUI");
    }
    private void onStartGameBtn3(GameObject obj, PointerEventData pData)
    {

        Close();
        //进入背包页面
        // UIManager.Instance.ShowUI<FightUI>("FightUI");

    }
    private void onReturnGameBtn(GameObject obj, PointerEventData pData)
    {

        Close();
        //进入地图页面
        // UIManager.Instance.ShowUI<FightUI>("FightUI");
    }
}

