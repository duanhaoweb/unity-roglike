using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
//��ʼҳ�棨�̳�UIBase��
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

        // ���¼��������
        gameStartButton.OnClick = onStartGameBtn;

    }

    private void onStartGameBtn(GameObject obj, PointerEventData pData)
    {

        Close();
        //ս����ʼ��
        FightManager.Instance.ChangeType(FightType.Init);

    }

}

