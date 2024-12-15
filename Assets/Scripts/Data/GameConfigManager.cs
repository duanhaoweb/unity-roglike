using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//������Ϸ���ñ�Ĺ�����
public class GameConfigManager
{
    public static GameConfigManager Instance =new GameConfigManager();
    private GameConfigData cardData;//��Ƭ��
    private GameConfigData enemyData;//���˱�
    private GameConfigData levelData;//�ؿ���

    private TextAsset textAsset;

    //��ʼ�������ļ���txt�ļ� �洢���ڴ��У�

    public void Init()
    {
        textAsset = Resources.Load<TextAsset>("Data/card");
        cardData=new GameConfigData(textAsset.text);

        // ����ļ��Ƿ�ɹ�����
        if (textAsset == null)
        {
            Debug.LogError("���� card.txt ʧ�ܣ������ļ��Ƿ������ Resources/Data �ļ����У������ļ����� card.txt��");
            return;
        }

        Debug.Log("�ɹ����� card.txt �ļ����ݣ�");
        Debug.Log(textAsset.text); // ����ļ����ݣ����ڼ���ʽ�Ƿ���ȷ


    
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
