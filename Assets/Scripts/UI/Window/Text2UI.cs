using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
//��ʼҳ�棨�̳�UIBase��
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
        //ս����ʼ��
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

