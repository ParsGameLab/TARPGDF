using UnityEngine;
using System.Collections;

//[RequireComponent(typeof(Rigidbody))]
public class NavigationComponent : MonoBehaviour 
{	
	private PathFindingGrid m_NavigationTerrain;
	private AStar m_AStarPlanner;
	private AStar.eFindingStatus m_Status;
	private Vector3 [] vPath;
	private bool bUpdateBlock;

	private GameObject m_EnemyGo;
	private GameObject m_PathfindingTerrain;
	public GameObject m_MainCore;




	public PathFindingGrid NavigationGrid		
	{
		set { m_NavigationTerrain = value; }
		get { return m_NavigationTerrain; }
	}
	public AStar AStarPlanner
	{
		get { return m_AStarPlanner; }
	}
	public Vector3[] Path
    {
		get { return vPath; }
	}

	void Awake()
	{
		m_NavigationTerrain = null;
		m_AStarPlanner= new AStar();
		m_Status = AStar.eFindingStatus.Waiting;
		bUpdateBlock = false;
		m_EnemyGo = this.gameObject;
		m_MainCore= GameObject.Find("MainCore");

	}
    private void Start()
    {
		m_PathfindingTerrain = GameObject.Find("Grid");
		PathfindingGridComponent pg = m_PathfindingTerrain.GetComponent(typeof(PathfindingGridComponent)) as PathfindingGridComponent;
		Init(pg.FindingGrid);
	}
    public void Init(PathFindingGrid grid)//第一個把Grid帶給大家的人
	{
		m_NavigationTerrain = grid;
		m_AStarPlanner.Init(m_NavigationTerrain);
	}
	
	
	public bool MoveToPosition(Vector3 targetPosition)//幫算出道路線的人
	{
		int iStartIndex = m_NavigationTerrain.GetNodeIndex(this.transform.position);
		int iDestIndex = m_NavigationTerrain.GetNodeIndex(targetPosition);
		Debug.Log("Start : " + iStartIndex.ToString());
		Debug.Log("Dest : " + iDestIndex.ToString());
		m_AStarPlanner.InitCalculation(iStartIndex, iDestIndex);
		m_AStarPlanner.PerformCalculation(10000);
		
		if(m_AStarPlanner.FindingStatus == AStar.eFindingStatus.Succeed) {
			int iCount = m_AStarPlanner.SolutionPath.Count;
			int index = 0;
			vPath = new Vector3[m_AStarPlanner.SolutionPath.Count];
			foreach(PathNode node in m_AStarPlanner.SolutionPath) {
				vPath[index] = m_NavigationTerrain.GetNodePosition(node.Index);
				index++;
			}
		}
		
		return true;
	}
	
	
	void Update()
	{
		//MoveToPosition(m_MainCore.transform.position);

		if (bUpdateBlock == false) {
			m_NavigationTerrain.SyncNodeState(m_AStarPlanner.NodePool);
			bUpdateBlock = true;
		}
		if(m_AStarPlanner.FindingStatus == AStar.eFindingStatus.Succeed) {
			AStar.DebugDrawPath(vPath, Color.cyan);
		}
		
	}
	

}
