using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// UI页面管理器
public class UIManager : MonoBehaviour
{
    public static UIManager Instance;
    private Transform canvasTf; // 画布的变换组件
    private List<UIBase> uiList; // 存储加载过的页面

    private void Awake()
    {
        Instance = this;
        // 找到世界中的画布
        canvasTf = GameObject.Find("Canvas").transform;
        uiList = new List<UIBase>();
    }

    // 显示
    public UIBase ShowUI<T>(string uiName) where T : UIBase
    {
        UIBase ui = Find(uiName);
        if (ui == null)
        {
            // 如果列表中没有，加载
            GameObject obj = Instantiate(Resources.Load("UI/" + uiName), canvasTf) as GameObject;
            obj.name = uiName;
            // 添加需要的脚本
            ui = obj.AddComponent<T>();
            // 添加到列表进行存储
            uiList.Add(ui);
        }
        else
        {
            // 显示
            ui.Show();
        }
        return ui;
    }

    // 隐藏
    public void HideUI(string uiName)
    {
        UIBase ui = Find(uiName);
        if (ui != null)
        {
            ui.Hide();
        }
    }

    // 关闭所有页面
    public void CloseALLUI()
    {
        for (int i = uiList.Count - 1; i >= 0; i--)
        {
            Destroy(uiList[i].gameObject);
        }
        uiList.Clear();
    }

    // 关闭某个页面
    public void CloseUI(string uiName)
    {
        UIBase ui = Find(uiName);
        if (ui != null)
        {
            uiList.Remove(ui);
            Destroy(ui.gameObject);
        }
    }

    // 从列表中找到名字对应的页面脚本
    public UIBase Find(string uiName)
    {
        for (int i = 0; i < uiList.Count; i++) // 修正越界问题
        {
            if (uiList[i].name == uiName)
            {
                return uiList[i];
            }
        }
        return null;
    }

    //获得某个界面的脚本
    public T GetUI<T>(string uiName) where T : UIBase
    {
        UIBase uI = Find(uiName);
        if(uI != null)
        {
            return uI.GetComponent<T>();
        }
        return null;
    }
}
