using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//整个游戏配置表的管理器
public class GameConfigManager
{
    public static GameConfigManager Instance =new GameConfigManager();
    private GameConfigData cardData;//卡片表
    private GameConfigData enemyData;//敌人表
    private GameConfigData levelData;//关卡表

    private TextAsset textAsset;

    //初始化配置文件（txt文件 存储在内存中）

    public void Init()
    {
        textAsset = Resources.Load<TextAsset>("Data/card");
        cardData=new GameConfigData(textAsset.text);

        // 检查文件是否成功加载
        if (textAsset == null)
        {
            Debug.LogError("加载 card.txt 失败！请检查文件是否放置在 Resources/Data 文件夹中，并且文件名是 card.txt。");
            return;
        }

        Debug.Log("成功加载 card.txt 文件内容：");
        Debug.Log(textAsset.text); // 输出文件内容，便于检查格式是否正确


    
    textAsset = Resources.Load<TextAsset>("Data/enemy");
        enemyData = new GameConfigData(textAsset.text);

        textAsset = Resources.Load<TextAsset>("Data/level");
        levelData = new GameConfigData(textAsset.text);
    }
    public List<Dictionary<string,string>>GetCardLines()
    {
        return cardData.GetLines();
    }
    public List<Dictionary<string, string>> GetEnemyLines()
    {
        return enemyData.GetLines();
    }
    public List<Dictionary<string, string>> GetLevelLines()
    {
        return levelData.GetLines();
    }
    public Dictionary<string,string> GetCardById(string id)
    {
        return cardData.GetOneById(id);
    }
    public Dictionary<string, string> GetEnemyById(string id)
    {
        return enemyData.GetOneById(id);
    }
    public Dictionary<string, string> GetLevlById(string id)
    {
        return levelData.GetOneById(id);
    }
}
