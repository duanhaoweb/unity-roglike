using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
//开始页面（继承UIBase）
public class Text1UI : UIBase
{
    private void Awake()
    {
        int i = FightInit.levelIndex;

        var gameReturnButton = Register("returnBtn");

        gameReturnButton.OnClick = onReturnGameBtn1;
    }



    public void onReturnGameBtn1(GameObject obj, PointerEventData pData)
    {
        StartCoroutine(ReturnToGameCoroutine());
    }

    private IEnumerator ReturnToGameCoroutine()
    {
        yield return new WaitForEndOfFrame(); // 等待一帧，确保所有对象加载完成

        // 战斗初始化
        FightManager.Instance.ChangeType(FightType.Init);

        // 延迟一帧关闭 UI
        yield return new WaitForEndOfFrame();
        Close(); // 在完成所有初始化后再关闭 UI
    }

}

