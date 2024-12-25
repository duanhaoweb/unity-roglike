using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
//��ʼҳ�棨�̳�UIBase��
public class SelectCardUI : UIBase
{
    private void Awake()
    {
        var gameButton1 = Register("content/Btn1");
        var gameButton2 = Register("content/Btn2");
        var gameButton3 = Register("content/Btn3");
        var gameReturnButton = Register("content/returnBtn");

        // ���¼�������
        gameButton1.OnClick = onStartGameBtn1;
        gameButton1.OnClick = onStartGameBtn2;
        gameButton1.OnClick = onStartGameBtn3;
        gameReturnButton.OnClick = onReturnGameBtn;
    }

    private void onStartGameBtn1(GameObject obj, PointerEventData pData)
    {

        Close();

        //�����ͼҳ��
       // UIManager.Instance.ShowUI<FightUI>("FightUI");
    }
    private void onStartGameBtn2(GameObject obj, PointerEventData pData)
    {

        Close();

        FightManager.MaxHP += 5;
        //�����ͼҳ��
        // UIManager.Instance.ShowUI<FightUI>("FightUI");
    }
    private void onStartGameBtn3(GameObject obj, PointerEventData pData)
    {

        Close();
        //���뱳��ҳ��
        // UIManager.Instance.ShowUI<FightUI>("FightUI");

    }
    private void onReturnGameBtn(GameObject obj, PointerEventData pData)
    {

        Close();
        //�����ͼҳ��
        // UIManager.Instance.ShowUI<FightUI>("FightUI");
    }
}

