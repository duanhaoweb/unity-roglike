using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//界面基类
public class UIBase : MonoBehaviour
{
    //注册事件
    public UIEventTrigger Register(string name)
    {
        // 尝试查找子对象
        Transform tf = transform.Find(name);

        // 添加调试信息
        if (tf == null)
        {
            Debug.LogError($"Register failed: Transform '{name}' not found under '{transform.name}'.");
            return null;
        }
        else
        {
            Debug.Log($"Transform '{name}' found successfully under '{transform.name}'.");
        }

        // 尝试获取 UIEventTrigger 组件
        var eventTrigger = UIEventTrigger.Get(tf.gameObject);
        if (eventTrigger == null)
        {
            Debug.LogError($"Register failed: UIEventTrigger not found on '{name}'.");
            return null;
        }
        else
        {
            Debug.Log($"UIEventTrigger successfully obtained on '{name}'.");
        }

        return eventTrigger;
    }

    //显示
    public virtual void Show()
    {
        gameObject.SetActive(true);
    }
    //隐藏
    public virtual void Hide()
    {
        gameObject.SetActive(false);
    }
    //关闭界面(销毁)
    public virtual void Close()
    {
        UIManager.Instance.CloseUI(gameObject.name);
    }
}
