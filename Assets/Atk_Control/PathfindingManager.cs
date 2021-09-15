using UnityEngine;
using System.Collections;

public class PathfindingManager : MonoBehaviour {
	
	private GameObject m_EnemyGo;
	private GameObject m_PathfindingTerrain;
	public GameObject m_MainCore;
    


    // Use this for initialization
    private void Awake()
    {
		
		
		

	}
    void Start () 
	{
        
        //m_EnemyGo = GameObject.FindWithTag("Enemy");
        //m_PathfindingTerrain = GameObject.Find("Grid");
        //PathfindingGridComponent pg = m_PathfindingTerrain.GetComponent(typeof(PathfindingGridComponent)) as PathfindingGridComponent;//場上找建好的網格
        //NavigationComponent nv = m_EnemyGo.GetComponent(typeof(NavigationComponent)) as NavigationComponent;//do
        //nv.Init(pg.FindingGrid);//這裡把建好的網個帶入//順序
        //nv.MoveToPosition(m_MainCore.transform.position);



    }
	
	// Update is called once per frame
	void Update () 
	{
        //if (Input.GetMouseButtonDown(0))
        //{
        //    Vector3 vec = Input.mousePosition;
        //    Ray ray = m_Camera.GetComponent<Camera>().ScreenPointToRay(vec);
        //    RaycastHit hitInfo;
        //    if (Physics.Raycast(ray, out hitInfo, 2000.0f))
        //    {
        //        NavigationComponent nv = m_EnemyGo.GetComponent(typeof(NavigationComponent)) as NavigationComponent;
        //        nv.MoveToPosition(hitInfo.point);
        //    }

        //}
        //NavigationComponent nv = m_EnemyGo.GetComponent(typeof(NavigationComponent)) as NavigationComponent;
        //nv.MoveToPosition(m_MainCore.transform.position);

    }


}
