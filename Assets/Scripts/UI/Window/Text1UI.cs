using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
//��ʼҳ�棨�̳�UIBase��
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
        yield return new WaitForEndOfFrame(); // �ȴ�һ֡��ȷ�����ж���������

        // ս����ʼ��
        FightManager.Instance.ChangeType(FightType.Init);

        // �ӳ�һ֡�ر� UI
        yield return new WaitForEndOfFrame();
        Close(); // ��������г�ʼ�����ٹر� UI
    }

}

