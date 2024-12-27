using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// 敌人管理器
/// </summary>
public class EnemyManager
{
    public static EnemyManager Instance = new EnemyManager();

    public List<Enemy> enemyList;//存储战斗中的敌人
    /// <summary>
    /// 加载敌人资源
    /// </summary>
    /// <param name="id"></param>
    public void LoadRes(string id)
    {
        Debug.Log($"[LoadRes] 开始加载关卡数据，关卡 ID: {id}");

        enemyList = new List<Enemy>();

        // 读取关卡表
        Dictionary<string, string> levelData = GameConfigManager.Instance.GetLevlById(id);
        if (levelData == null)
        {
            Debug.LogError($"[LoadRes] 未找到关卡数据，关卡 ID: {id}");
            return;
        }
        Debug.Log($"[LoadRes] 成功读取关卡数据: {string.Join(", ", levelData)}");

        // 敌人 ID 信息
        string[] enemyIds = levelData["EnemyIds"].Split('=');
        Debug.Log($"[LoadRes] 敌人 ID 列表: {string.Join(", ", enemyIds)}");

        // 敌人位置信息
        string[] enemyPos = levelData["Pos"].Split('=');
        Debug.Log($"[LoadRes] 敌人位置列表: {string.Join("; ", enemyPos)}");

        for (int i = 0; i < enemyIds.Length; i++)
        {
            string enemyId = enemyIds[i];
            Debug.Log($"[LoadRes] 正在处理敌人 ID: {enemyId}");

            string[] pos = enemyPos[i].Split(',');
            if (pos.Length != 3)
            {
                Debug.LogError($"[LoadRes] 无效的位置数据: {enemyPos[i]}");
                continue;
            }

            // 敌人位置
            float x = float.Parse(pos[0]);
            float y = float.Parse(pos[1]);
            float z = float.Parse(pos[2]);
            Debug.Log($"[LoadRes] 敌人位置: x={x}, y={y}, z={z}");

            // 根据敌人 ID 获取敌人信息
            Dictionary<string, string> enemyData = GameConfigManager.Instance.GetEnemyById(enemyId);
            if (enemyData == null)
            {
                Debug.LogError($"[LoadRes] 未找到敌人数据，敌人 ID: {enemyId}");
                continue;
            }
            Debug.Log($"[LoadRes] 成功获取敌人数据: {string.Join(", ", enemyData)}");

            // 加载敌人模型
            GameObject prefab = Resources.Load(enemyData["Model"]) as GameObject;
            if (prefab == null)
            {
                Debug.LogError($"[LoadRes] 无法加载敌人模型，路径: {enemyData["Model"]}");
                continue;
            }
            GameObject obj = Object.Instantiate(prefab);
            Debug.Log($"[LoadRes] 成功实例化敌人模型，路径: {enemyData["Model"]}");

            // 添加敌人脚本
            Enemy enemy = obj.AddComponent<Enemy>();
            enemy.Init(enemyData); // 初始化敌人数据
            Debug.Log($"[LoadRes] 敌人已初始化: {enemy}");

            // 存储到敌人列表
            enemyList.Add(enemy);
            obj.transform.position = new Vector3(x, y, z);
            Debug.Log($"[LoadRes] 敌人已添加到列表，并设置位置: x={x}, y={y}, z={z}");
        }

        Debug.Log($"[LoadRes] 所有敌人加载完成，总计: {enemyList.Count} 个敌人");
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
        //行动完后更新所有敌人的行为
        for(int i = 0; i < enemyList.Count;i++)
        {
            enemyList[i].SetAction();
        }
        //且切换到玩家回合
        if (FightManager.Instance.fightUnit is Fight_EnemyTurn)
        {
            FightManager.Instance.ChangeType(FightType.Player);
        }
    }
}
