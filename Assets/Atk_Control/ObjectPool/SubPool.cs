using UnityEngine;
using System.Collections;
using System.Collections.Generic;
///
/// ��������G������
/// �@�ΡG��������C
/// �`�N�G
///
public class SubPool
{
    //������̭��s��������ݭn���C������
    List<GameObject> pool = new List<GameObject>();
    //�n�إߪ��C�����骺�w�]��
    GameObject prefab;
    //��^�w�]�骺�W�r�A�w�]�骺�W�r�M��������W�r�ۦP
    //�b�޲z����������̭��A�i�H�q�L�w�]�骺�W�r���۹����������
    public string Name
    {
        get
        {
            return prefab.name;
        }
    }
    //�c�y��k
    public SubPool(GameObject mPrefab)
    {
        prefab = mPrefab;//�ǻ��w�]��޼�
    }
    //�q������̭�����@�ӹC������
    public GameObject SubPoolSpawn(Vector3 pos)
    {
        GameObject obj = null;
        //�M���������w�]��
        foreach (GameObject item in pool)
        {
            //�p�G�Ӫ��󥼱ҥΡA�����Ӫ���S���Q�ϥ�
            if (item.activeSelf == false)
            {
                obj = item;
                obj.transform.position = pos;
                obj.transform.rotation = Quaternion.identity;
                break;//���X�Ӱj��
            }
        }
        //�䤣��n�Ϊ��C������
        if (obj == null)
        {
            //�إߤ@�ӷs���C������
            obj = GameObject.Instantiate(prefab, pos, Quaternion.identity) as GameObject;
            //��s�إߪ��C�������춰�X�̭�
            pool.Add(obj);
        }
        obj.SetActive(true);//�B��ҥΪ��A
                            //�q�L�l���Ҷ��Ƥ�������A�l�������O�X�����{�~��
                            //�ù�{�F���f�̭�����k�Acontrol�̭��s���N�O�ӡB
                            //�l���ҹ�{����k

        IControl control = obj.GetComponent<IControl>();
        if (control != null)
        {
            //�I�s�C�������{����k
            control.Spawn();
        }
        return obj;
    }
    //�^���C������
    public void SubPoolUnSpawn(GameObject obj)
    {
        //�p�G��������X�̭��]�t�F�Ӫ���
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
    //�^���Ҧ����C������
    public void SubPoolUnsPawnAll()
    {
        //�M�����X���Ҧ����C������
        foreach (GameObject item in pool)
        {
            if (item.activeSelf)
            {
                SubPoolUnSpawn(item);
            }
        }
    }

    /// <summary>
    /// �Y�Ӫ���O�_�b������̭�
    /// </summary>
    /// <param name="obj"></param>
    /// <returns></returns>
    public bool Contains(GameObject obj)
    {
        //���l�̭��O�_������
        return pool.Contains(obj);
    }
}