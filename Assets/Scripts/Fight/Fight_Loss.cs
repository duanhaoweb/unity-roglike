using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//ʧ��
public class Fight_Loss : FightUnit
{
    public override void Init()
    {
        base.Init();
        FightManager.Instance.StopAllCoroutines();
        //��ʾ���˻غ���ʾ
        UIManager.Instance.ShowTip("GameOver", Color.red, delegate ()
        {
            Debug.Log("gameover");
        });
        Sleep(2);
        ExitGame();
    }
    public override void OnUpdate()
    {
        base.OnUpdate();
    }
    public IEnumerator Sleep(int n)
    {
        yield return new WaitForSeconds(n);
    }
    public   void ExitGame()
    {
      
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false; // �ڱ༭����ֹͣ����
#else
        Application.Quit(); // �ڴ�������Ϸ���˳�
#endif
    }
}
