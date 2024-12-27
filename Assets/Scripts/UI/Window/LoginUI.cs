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
        var gameExitButton = Register("quitBtn");
        if (gameStartButton == null)
        {
            Debug.LogError("GameStartButton not found or Register failed!");
            return;
        }

        // ���¼�������
        gameStartButton.OnClick = onStartGameBtn;
        gameExitButton.OnClick = onExitGameBtn;
    }

    private void onStartGameBtn(GameObject obj, PointerEventData pData)
    {

        Close();

        //��ʾloginUI,�����Ľű����ּǵø�Ԥ�������������һ��
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
        UnityEditor.EditorApplication.isPlaying = false; // �ڱ༭����ֹͣ����
#else
        Application.Quit(); // �ڴ�������Ϸ���˳�
#endif
    }

}

