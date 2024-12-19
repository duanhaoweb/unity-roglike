using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// ���˹�����
/// </summary>
public class EnemyManager
{
    public static EnemyManager Instance = new EnemyManager();

    private List<Enemy> enemyList;//�洢ս���еĵ���
    /// <summary>
    /// ���ص�����Դ
    /// </summary>
    /// <param name="id"></param>
    public void LoadRes(string id)
    {
        enemyList = new List<Enemy>();
        //Id	Name	EnemyIds	Pos
        //10001	  1	     10001	  0,0,0
        //��ȡ�ؿ���
        Dictionary<string,string> levelData =GameConfigManager.Instance.GetLevlById(id);
        //����id��Ϣ
        string[] enemyIds = levelData["EnemyIds"].Split('=');

        string[] enemyPos = levelData["Pos"].Split('=');//����λ����Ϣ
        for(int i = 0; i < enemyIds.Length; i++)
        {
            string enemyId = enemyIds[i];
            string[] pos = enemyPos[i].Split(',');

            //����λ��
            float x = float.Parse(pos[0]);
            float y = float.Parse(pos[1]);
            float z = float.Parse(pos[2]);
            //���ݵ���id���ÿ�����˵���Ϣ
            Dictionary<string,string>enemyData=GameConfigManager.Instance.GetEnemyById(enemyId);
            GameObject obj = Object.Instantiate(Resources.Load(enemyData["Model"])) as GameObject;//����Դ·�����ض�Ӧ�ĵ���ģ��

            Enemy enemy = obj.AddComponent<Enemy>();//��ӵ��˽ű�
            enemy.Init(enemyData);//�洢������Ϣ
            //�洢������
            enemyList.Add(enemy);
            obj.transform.position = new Vector3(x, y, z);
        }
    }
}
