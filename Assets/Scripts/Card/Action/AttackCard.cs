using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class AttackCard : ActionCard
{
    // 被检测到的敌人
    private Enemy hitEnemy;

    /// <summary>
    /// 射线检测是否命中敌人
    /// </summary>
    /// <returns>是否成功命中敌人</returns>
    private bool CheckRayToEnemy()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        // 使用射线检测敌人
        if (Physics.Raycast(ray, out hit, 10000, LayerMask.GetMask("Enemy")))
        {
            Debug.Log("探测到敌人");

            // 尝试获取敌人组件
            if (hit.transform.TryGetComponent<Enemy>(out hitEnemy))
            {
                hitEnemy.OnSelect(); // 敌人被选中

                // 检查是否可以使用卡牌
                if (UseCard())
                {
                    // 显示提示和播放音效
                    UIManager.Instance.ShowTip("进行攻击！", Color.red);
                    AudioManager.Instance.PlayEffect("AttackCard");

                    // 敌人受伤逻辑
                    int val = int.Parse(data["Arg0"]);
                    int hurt = int.Parse(data["Arg1"]);
                    hitEnemy.Hit(val, hurt);
                    UIManager.Instance.GetUI<FightUI>("FightUI").UpdateATKbuff();
                    // 取消选中敌人
                    hitEnemy.OnUnSelect();
                    hitEnemy = null;
                    return true;
                }

                // 如果卡牌未成功使用，取消选中敌人
                hitEnemy.OnUnSelect();
                hitEnemy = null;
            }
        }

        return false;
    }

    /// <summary>
    /// 拖动开始时的逻辑
    /// </summary>
    /// <param name="eventData">拖动事件数据</param>
    public override void OnBeginDrag(PointerEventData eventData)
    {
        base.OnBeginDrag(eventData);
        Cursor.visible = false;
        // 显示拖动路径 UI
        UIManager.Instance.ShowUI<LineUI>("LineUI");
        UIManager.Instance.GetUI<LineUI>("LineUI").SetStartPos(transform.GetComponent<RectTransform>().anchoredPosition);
    }

    /// <summary>
    /// 拖动中的逻辑
    /// </summary>
    /// <param name="eventData">拖动事件数据</param>
    public override void OnDrag(PointerEventData eventData)
    {
        base.OnDrag(eventData);

        // 获取鼠标当前的屏幕位置，并转换为父物体的本地坐标
        Vector2 pos;
        if (RectTransformUtility.ScreenPointToLocalPointInRectangle(
            transform.parent.GetComponent<RectTransform>(),
            eventData.position,
            eventData.pressEventCamera,
            out pos))
        {
            // 动态设置 LineUI 的结束点
            UIManager.Instance.GetUI<LineUI>("LineUI").SetEndPos(pos);
        }
    }


    /// <summary>
    /// 拖动结束时的逻辑
    /// </summary>
    /// <param name="eventData">拖动事件数据</param>
    public override void OnEndDrag(PointerEventData eventData)
    {
        Vector2 pos;

        // 获取拖动结束时的鼠标位置
        if (RectTransformUtility.ScreenPointToLocalPointInRectangle(
            transform.parent.GetComponent<RectTransform>(),
            eventData.position,
            eventData.pressEventCamera,
            out pos))
        {


            // 检测是否命中敌人
            if (!CheckRayToEnemy())
            {
                base.OnEndDrag(eventData); // 如果未命中敌人，调用基类的拖动结束逻辑

            }
        }

        // 关闭拖动路径 UI
        UIManager.Instance.CloseUI("LineUI");
        Cursor.visible = true;
    }

    /// <summary>
    /// 初始化卡牌数据
    /// </summary>
    private void Start()
    {
        UpdateCardUI(); // 更新卡牌 UI
    }

    /// <summary>
    /// 更新卡牌的 UI
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
                // 动态替换占位符
                msgTxt.text = ReplacePlaceholders(data["Des"], data);
            }
            catch (Exception e)
            {
                Debug.LogError($"Error updating card UI: {e.Message}");
                msgTxt.text = data["Des"]; // 或者设置默认值
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

    // 动态替换占位符方法
    private string ReplacePlaceholders(string template, Dictionary<string, string> data)
    {
        for (int i = 0; i < 10; i++) // 假设最多支持 10 个占位符
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
