using UnityEngine;
using System.Collections;
using System.Collections.Generic;
///
/// 掛載物件：不掛載
/// 作用：物件池類。
/// 注意：
///
public class SubPool
{
    //物件池裡面存放場景中需要的遊戲物體
    List<GameObject> pool = new List<GameObject>();
    //要建立的遊戲物體的預設體
    GameObject prefab;
    //返回預設體的名字，預設體的名字和物件池的名字相同
    //在管理物件池的類裡面，可以通過預設體的名字找到相對應的物件池
    public string Name
    {
        get
        {
            return prefab.name;
        }
    }
    //構造方法
    public SubPool(GameObject mPrefab)
    {
        prefab = mPrefab;//傳遞預設體引數
    }
    //從物件池裡面拿到一個遊戲物體
    public GameObject SubPoolSpawn(Vector3 pos)
    {
        GameObject obj = null;
        //遍歷物件池找預設體
        foreach (GameObject item in pool)
        {
            //如果該物件未啟用，說明該物件沒有被使用
            if (item.activeSelf == false)
            {
                obj = item;
                obj.transform.position = pos;
                obj.transform.rotation = Quaternion.identity;
                break;//跳出該迴圈
            }
        }
        //找不到要用的遊戲物件
        if (obj == null)
        {
            //建立一個新的遊戲物件
            obj = GameObject.Instantiate(prefab, pos, Quaternion.identity) as GameObject;
            //把新建立的遊戲物件放到集合裡面
            pool.Add(obj);
        }
        obj.SetActive(true);//處於啟用狀態
                            //通過子類例項化介面物件，子類的指令碼元件實現繼承
                            //並實現了接口裡面的方法，control裡面存的就是該、
                            //子類所實現的方法

        IControl control = obj.GetComponent<IControl>();
        if (control != null)
        {
            //呼叫遊戲物件實現的方法
            control.Spawn();
        }
        return obj;
    }
    //回收遊戲物件
    public void SubPoolUnSpawn(GameObject obj)
    {
        //如果物件池集合裡面包含了該物體
        if (pool.Contains(obj))
        {
            IControl control = obj.GetComponent<IControl>();
            if (control != null)
            {
                control.UnSpwan();
            }
            obj.SetActive(false);
            obj.transform.position = Vector3.zero;
        }
    }
    //回收所有的遊戲物件
    public void SubPoolUnsPawnAll()
    {
        //遍歷集合中所有的遊戲物件
        foreach (GameObject item in pool)
        {
            if (item.activeSelf)
            {
                SubPoolUnSpawn(item);
            }
        }
    }

    /// <summary>
    /// 某個物件是否在物件池裡面
    /// </summary>
    /// <param name="obj"></param>
    /// <returns></returns>
    public bool Contains(GameObject obj)
    {
        //池子裡面是否有物件
        return pool.Contains(obj);
    }
}