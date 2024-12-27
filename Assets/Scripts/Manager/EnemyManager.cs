using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// ���˹�����
/// </summary>
public class EnemyManager
{
    public static EnemyManager Instance = new EnemyManager();

    public List<Enemy> enemyList;//�洢ս���еĵ���
    /// <summary>
    /// ���ص�����Դ
    /// </summary>
    /// <param name="id"></param>
    public void LoadRes(string id)
    {
        Debug.Log($"[LoadRes] ��ʼ���عؿ����ݣ��ؿ� ID: {id}");

        enemyList = new List<Enemy>();

        // ��ȡ�ؿ���
        Dictionary<string, string> levelData = GameConfigManager.Instance.GetLevlById(id);
        if (levelData == null)
        {
            Debug.LogError($"[LoadRes] δ�ҵ��ؿ����ݣ��ؿ� ID: {id}");
            return;
        }
        Debug.Log($"[LoadRes] �ɹ���ȡ�ؿ�����: {string.Join(", ", levelData)}");

        // ���� ID ��Ϣ
        string[] enemyIds = levelData["EnemyIds"].Split('=');
        Debug.Log($"[LoadRes] ���� ID �б�: {string.Join(", ", enemyIds)}");

        // ����λ����Ϣ
        string[] enemyPos = levelData["Pos"].Split('=');
        Debug.Log($"[LoadRes] ����λ���б�: {string.Join("; ", enemyPos)}");

        for (int i = 0; i < enemyIds.Length; i++)
        {
            string enemyId = enemyIds[i];
            Debug.Log($"[LoadRes] ���ڴ������ ID: {enemyId}");

            string[] pos = enemyPos[i].Split(',');
            if (pos.Length != 3)
            {
                Debug.LogError($"[LoadRes] ��Ч��λ������: {enemyPos[i]}");
                continue;
            }

            // ����λ��
            float x = float.Parse(pos[0]);
            float y = float.Parse(pos[1]);
            float z = float.Parse(pos[2]);
            Debug.Log($"[LoadRes] ����λ��: x={x}, y={y}, z={z}");

            // ���ݵ��� ID ��ȡ������Ϣ
            Dictionary<string, string> enemyData = GameConfigManager.Instance.GetEnemyById(enemyId);
            if (enemyData == null)
            {
                Debug.LogError($"[LoadRes] δ�ҵ��������ݣ����� ID: {enemyId}");
                continue;
            }
            Debug.Log($"[LoadRes] �ɹ���ȡ��������: {string.Join(", ", enemyData)}");

            // ���ص���ģ��
            GameObject prefab = Resources.Load(enemyData["Model"]) as GameObject;
            if (prefab == null)
            {
                Debug.LogError($"[LoadRes] �޷����ص���ģ�ͣ�·��: {enemyData["Model"]}");
                continue;
            }
            GameObject obj = Object.Instantiate(prefab);
            Debug.Log($"[LoadRes] �ɹ�ʵ��������ģ�ͣ�·��: {enemyData["Model"]}");

            // ��ӵ��˽ű�
            Enemy enemy = obj.AddComponent<Enemy>();
            enemy.Init(enemyData); // ��ʼ����������
            Debug.Log($"[LoadRes] �����ѳ�ʼ��: {enemy}");

            // �洢�������б�
            enemyList.Add(enemy);
            obj.transform.position = new Vector3(x, y, z);
            Debug.Log($"[LoadRes] ��������ӵ��б�������λ��: x={x}, y={y}, z={z}");
        }

        Debug.Log($"[LoadRes] ���е��˼�����ɣ��ܼ�: {enemyList.Count} ������");
    }

    public void DeleteEnemy(Enemy enemy)
    {
        enemyList.Remove(enemy);
    }
    public IEnumerator DoAllEnemyAction()
    {
        for(int i = 0;i < enemyList.Count;i++)
        {
            yield return FightManager.Instance.StartCoroutine(enemyList[i].DoAction());
        }
        //�ж����������е��˵���Ϊ
        for(int i = 0; i < enemyList.Count;i++)
        {
            enemyList[i].SetAction();
        }
        //���л�����һغ�
        if (FightManager.Instance.fightUnit is Fight_EnemyTurn)
        {
            FightManager.Instance.ChangeType(FightType.Player);
        }
    }
}
