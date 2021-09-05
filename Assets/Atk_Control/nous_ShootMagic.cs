using System.Collections;
using System.Collections.Generic;
using System.Runtime.Serialization.Formatters;
using System;
using UnityEngine;

public class ShootMagic : MonoBehaviour
{
    public Transform FirePoint;
    public Camera tpsCam;
    public float fmagicspeed=10.0f;
    public GameObject[] Prefabs;//魔法放進陣列裡用第幾個來找要用ㄉ

    private Ray RayMouse;
    private Vector3 direction;
    private Quaternion rotation;

    private Animator manimater;
    private float fTime;
    // Start is called before the first frame update
    void Start()
    {
        manimater = GetComponent<Animator>();
        fTime = 0.0f;
        
    }

    // Update is called once per frame
    void Update()
    {
        Prefabs[0].transform.position += transform.forward * fmagicspeed * Time.deltaTime;
        fTime += Time.deltaTime;
        if (fTime > 3.0f)
        {
            Destroy(Prefabs[0]);
        }
        //if (manimater.GetCurrentAnimatorStateInfo(1).IsName("MagicNorAttack"))
        //{

        //    Instantiate(Prefabs[0], FirePoint.transform.position, FirePoint.transform.rotation);
        //}

    }
    
    public void MagicNorAttack(Vector3 FirePoint,Vector3 targetposition,bool sss)
    {
        Prefabs[0].transform.position = FirePoint;
        Prefabs[0].transform.forward = targetposition;
        
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag.Equals("Enemy"))
        {
            //Destroy(other.gameObject);
            Destroy(Prefabs[0]);
        }
    }
}
