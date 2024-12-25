using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//ʤ��
public class Fight_Win : FightUnit
{
    public override void Init()
    {
        base.Init();
        FightManager.Instance.StopAllCoroutines();
        //��ʾ���˻غ���ʾ
        UIManager.Instance.ShowTip("Victory", Color.yellow, delegate ()
        {
            Debug.Log("Victory");
        });
        Sleep(1);
        UIManager.Instance.GetUI<FightUI>("FightUI").Close();
        // ��ʾѡ��ҳ��

        UIManager.Instance.ShowUI<SelectCardUI>("SelectCardUI");
    }
    public override void OnUpdate()
    {
        base.OnUpdate();
    }
    public IEnumerator Sleep(int n)
    {
        yield return new WaitForSeconds(n);
    }
}
