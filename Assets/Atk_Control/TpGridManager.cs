using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TpGridManager : MonoBehaviour
{
    //private GameObject m_EnemyGo;
    //public GameObject m_MainCore;


    private GameObject m_PathfindingTerrain;
    private GameObject m_WeaponController;
    // Start is called before the first frame update
    void Start()
    {
        //m_EnemyGo = GameObject.FindWithTag("Enemy");
        m_PathfindingTerrain = GameObject.Find("Grid");
        m_WeaponController= GameObject.Find("BigUnityChan");
        PathfindingGridComponent pg = m_PathfindingTerrain.GetComponent(typeof(PathfindingGridComponent)) as PathfindingGridComponent;//場上找
        WeaponController wc = m_WeaponController.GetComponent(typeof(WeaponController)) as WeaponController;
        //NavigationComponent nv = m_EnemyGo.GetComponent(typeof(NavigationComponent)) as NavigationComponent;
        //nv.Init(pg.FindingGrid);//這裡把建好的網個帶入//順序
        //nv.MoveToPosition(m_MainCore.transform.position);
        wc.Init(pg.FindingGrid);
        
       
    }

    // Update is called once per frame
    void Update()
    {
        //NavigationComponent nv = m_EnemyGo.GetComponent(typeof(NavigationComponent)) as NavigationComponent;
        //nv.MoveToPosition(m_MainCore.transform.position);
    }
}
