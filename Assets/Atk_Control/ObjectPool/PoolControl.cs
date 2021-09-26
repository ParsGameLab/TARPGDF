using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;
///
/// ���b����GC#���A������
/// �@�ΡG������޲z���A��ֵ{���X���X��
/// �`�N�G���~��MonoBehaviour
///
public class PoolControl
{

    private static PoolControl instances;//��ҼҦ�

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
    //�إߦr��A�ھڹw�]��W�r���۹���������
    Dictionary<string, SubPool> poolDic =
        new Dictionary<string, SubPool>();

    /// <summary>
    /// �o�쪫��������C������
    /// </summary>
    /// <param name="name">�w�]�骺�W�r</param>
    /// <returns></returns>
    public GameObject Spawn(string name, Vector3 position)
    {
        SubPool subpool;
        //�p�G�r��]�t��key����
        if (!poolDic.ContainsKey(name))
        {
            //�b�r��̭��s�W�@�Ӫ����
            RegisterSubPools(name);
        }
        subpool = poolDic[name];
        return subpool.SubPoolSpawn(position);
    }

    /// <summary>
    ///  �s�W���������k
    /// </summary�W�r
    void RegisterSubPools(string name)
    {
        //�q�L�W�r���J�w�]��
        GameObject mprefab = Resources.Load(name) as GameObject;
        //�ھڹw�]��إ߹����������
        SubPool subpool = new SubPool(mprefab);
        //�N������s�W��r�夤
        poolDic.Add(name, subpool);
    }

    ///�^�����󪺤�k
    public void UnSpwan(GameObject obj)
    {
        //�M���Ҧ���������ݨ��Ӫ�����]�t�ӹC������
        foreach (SubPool item in poolDic.Values)
        {
            //�p�G�Ӫ�����]�t�F�Ӫ���
            if (item.Contains(obj))
            {
                //�I�s�Ӫ�����^���C�����骺��k
                item.SubPoolUnSpawn(obj);
                break;
            }
        }
    }
}
