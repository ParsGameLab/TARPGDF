using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;
///
/// 掛在物件：C#類，不掛載
/// 作用：物件池管理類，減少程式碼耦合度
/// 注意：不繼承MonoBehaviour
///
public class PoolControl
{

    private static PoolControl instances;//單例模式

    private PoolControl()
    {
    }

    public static PoolControl Instance
    {
        get
        {
            if (instances == null)
            {
                instances = new PoolControl();
            }
            return instances;
        }
    }
    //建立字典，根據預設體名字找到相對應的物件
    Dictionary<string, SubPool> poolDic =
        new Dictionary<string, SubPool>();

    /// <summary>
    /// 得到物件池中的遊戲物件
    /// </summary>
    /// <param name="name">預設體的名字</param>
    /// <returns></returns>
    public GameObject Spawn(string name, Vector3 position)
    {
        SubPool subpool;
        //如果字典包含該key的值
        if (!poolDic.ContainsKey(name))
        {
            //在字典裡面新增一個物件池
            RegisterSubPools(name);
        }
        subpool = poolDic[name];
        return subpool.SubPoolSpawn(position);
    }

    /// <summary>
    ///  新增物件池的方法
    /// </summary名字
    void RegisterSubPools(string name)
    {
        //通過名字載入預設體
        GameObject mprefab = Resources.Load(name) as GameObject;
        //根據預設體建立對應的物件池
        SubPool subpool = new SubPool(mprefab);
        //將物件池新增到字典中
        poolDic.Add(name, subpool);
    }

    ///回收物件的方法
    public void UnSpwan(GameObject obj)
    {
        //遍歷所有的物件池看那個物件池包含該遊戲物體
        foreach (SubPool item in poolDic.Values)
        {
            //如果該物件池包含了該物體
            if (item.Contains(obj))
            {
                //呼叫該物件池回收遊戲物體的方法
                item.SubPoolUnSpawn(obj);
                break;
            }
        }
    }
}
