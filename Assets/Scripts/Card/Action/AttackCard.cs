using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class AttackCard : ActionCard
{
    // ����⵽�ĵ���
    private Enemy hitEnemy;

    /// <summary>
    /// ���߼���Ƿ����е���
    /// </summary>
    /// <returns>�Ƿ�ɹ����е���</returns>
    private bool CheckRayToEnemy()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        // ʹ�����߼�����
        if (Physics.Raycast(ray, out hit, 10000, LayerMask.GetMask("Enemy")))
        {
            Debug.Log("̽�⵽����");

            // ���Ի�ȡ�������
            if (hit.transform.TryGetComponent<Enemy>(out hitEnemy))
            {
                hitEnemy.OnSelect(); // ���˱�ѡ��

                // ����Ƿ����ʹ�ÿ���
                if (UseCard())
                {
                    // ��ʾ��ʾ�Ͳ�����Ч
                    UIManager.Instance.ShowTip("���й�����", Color.red);
                    AudioManager.Instance.PlayEffect("AttackCard");

                    // ���������߼�
                    int val = int.Parse(data["Arg0"]);
                    int hurt = int.Parse(data["Arg1"]);
                    hitEnemy.Hit(val, hurt);
                    UIManager.Instance.GetUI<FightUI>("FightUI").UpdateATKbuff();
                    // ȡ��ѡ�е���
                    hitEnemy.OnUnSelect();
                    hitEnemy = null;
                    return true;
                }

                // �������δ�ɹ�ʹ�ã�ȡ��ѡ�е���
                hitEnemy.OnUnSelect();
                hitEnemy = null;
            }
        }

        return false;
    }

    /// <summary>
    /// �϶���ʼʱ���߼�
    /// </summary>
    /// <param name="eventData">�϶��¼�����</param>
    public override void OnBeginDrag(PointerEventData eventData)
    {
        base.OnBeginDrag(eventData);
        Cursor.visible = false;
        // ��ʾ�϶�·�� UI
        UIManager.Instance.ShowUI<LineUI>("LineUI");
        UIManager.Instance.GetUI<LineUI>("LineUI").SetStartPos(transform.GetComponent<RectTransform>().anchoredPosition);
    }

    /// <summary>
    /// �϶��е��߼�
    /// </summary>
    /// <param name="eventData">�϶��¼�����</param>
    public override void OnDrag(PointerEventData eventData)
    {
        base.OnDrag(eventData);

        // ��ȡ��굱ǰ����Ļλ�ã���ת��Ϊ������ı�������
        Vector2 pos;
        if (RectTransformUtility.ScreenPointToLocalPointInRectangle(
            transform.parent.GetComponent<RectTransform>(),
            eventData.position,
            eventData.pressEventCamera,
            out pos))
        {
            // ��̬���� LineUI �Ľ�����
            UIManager.Instance.GetUI<LineUI>("LineUI").SetEndPos(pos);
        }
    }


    /// <summary>
    /// �϶�����ʱ���߼�
    /// </summary>
    /// <param name="eventData">�϶��¼�����</param>
    public override void OnEndDrag(PointerEventData eventData)
    {
        Vector2 pos;

        // ��ȡ�϶�����ʱ�����λ��
        if (RectTransformUtility.ScreenPointToLocalPointInRectangle(
            transform.parent.GetComponent<RectTransform>(),
            eventData.position,
            eventData.pressEventCamera,
            out pos))
        {


            // ����Ƿ����е���
            if (!CheckRayToEnemy())
            {
                base.OnEndDrag(eventData); // ���δ���е��ˣ����û�����϶������߼�

            }
        }

        // �ر��϶�·�� UI
        UIManager.Instance.CloseUI("LineUI");
        Cursor.visible = true;
    }

    /// <summary>
    /// ��ʼ����������
    /// </summary>
    private void Start()
    {
        UpdateCardUI(); // ���¿��� UI
    }

    /// <summary>
    /// ���¿��Ƶ� UI
    /// </summary>
    private void UpdateCardUI()
    {
        var bgImage = transform.Find("bg").GetComponent<Image>();
        if (bgImage != null)
        {
            bgImage.sprite = Resources.Load<Sprite>(data["BgImage"]);
        }

        var iconImage = transform.Find("bg/icon").GetComponent<Image>();
        if (iconImage != null)
        {
            iconImage.sprite = Resources.Load<Sprite>(data["Image"]);
        }

        var msgTxt = transform.Find("bg/msgTxt").GetComponent<Text>();
        if (msgTxt != null)
        {
            try
            {
                // ��̬�滻ռλ��
                msgTxt.text = ReplacePlaceholders(data["Des"], data);
            }
            catch (Exception e)
            {
                Debug.LogError($"Error updating card UI: {e.Message}");
                msgTxt.text = data["Des"]; // ��������Ĭ��ֵ
            }
        }

        var nameTxt = transform.Find("bg/nameTxt").GetComponent<Text>();
        if (nameTxt != null)
        {
            nameTxt.text = data["Name"];
        }

        var expendTxt = transform.Find("bg/useTxt").GetComponent<Text>();
        if (expendTxt != null)
        {
            expendTxt.text = data["Expend"];
        }

        var typeTxt = transform.Find("bg/Text").GetComponent<Text>();
        if (typeTxt != null)
        {
            typeTxt.text = GameConfigManager.Instance.GetCardTypeById(data["Type"])["Name"];
        }
    }

    // ��̬�滻ռλ������
    private string ReplacePlaceholders(string template, Dictionary<string, string> data)
    {
        for (int i = 0; i < 10; i++) // �������֧�� 10 ��ռλ��
        {
            string key = $"Arg{i}";
            if (data.ContainsKey(key))
            {
                template = template.Replace($"{{{i}}}", data[key]);
            }
            else
            {
                break;
            }
        }
        return template;
    }

}
