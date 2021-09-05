using UnityEngine;
using System.Collections;

public class PathfindingManager : MonoBehaviour {
	
	private GameObject m_TempObject;
	private GameObject m_PathfindingTerrain;
	private GameObject m_Camera;
	
	// Use this for initialization
	void Start () {
		m_TempObject = GameObject.Find("Capsule");
		m_Camera = GameObject.Find("Main Camera");
		m_PathfindingTerrain = GameObject.Find ("Grid");
		PathfindingGridComponent pg = m_PathfindingTerrain.GetComponent(typeof(PathfindingGridComponent)) as PathfindingGridComponent;//場上找建好的網格
		NavigationComponent nv = m_TempObject.GetComponent(typeof(NavigationComponent)) as NavigationComponent;
		nv.Init(pg.FindingGrid);//這裡把建好的網個帶入//順序
	
	}
	
	// Update is called once per frame
	void Update () {
		if(Input.GetMouseButtonDown(0)) {
			Vector3 vec = Input.mousePosition;
			Ray ray = m_Camera.GetComponent<Camera>().ScreenPointToRay(vec);
			RaycastHit hitInfo;
			if(Physics.Raycast(ray, out hitInfo, 2000.0f)) {
				NavigationComponent nv = m_TempObject.GetComponent(typeof(NavigationComponent)) as NavigationComponent;
				nv.MoveToPosition(hitInfo.point);
			}
			
		}
		
	}
	
	
}
