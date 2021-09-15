using UnityEngine;
using System.Collections;
using System;

public class PathfindingGridComponent : TerrainComponent {
	struct sTexelData
	{
		public Color m_aCol;           // texel color.
	}

	public float 			m_fCellSize = 1.0f;
	public int 				m_nRows = 10;
	public int 				m_nColumns = 10;
	
	// Debug.
	public bool 			m_bDebug = true;
	public Color			m_DebugColor = Color.white;

	private Color m_BlackColor = new Color(0.0f, 0.0f, 0.0f, 1.0f);
	public Color m_RedColor = new Color(1.0f, 0.0f, 0.0f, 1.0f);
	public Texture2D m_MaskTexture ;
	


	public PathFindingGrid FindingGrid
	{
		get { return m_TerrainRepresentation as PathFindingGrid; }//自己建構的網格
	}
	
	// Use this for initialization
	void Awake () {
		m_TerrainRepresentation = new PathFindingGrid();//上面做他的是，網格交給基底寫得自己做//其實已經在建構作好ㄌ
		
		FindingGrid.Init(transform.position, m_nRows, m_nColumns, m_fCellSize);
		


	}
	
	void Start () {
		RefreshCellState();
		SetStartCellState();
	}	
	
	// Update is called once per frame
	void Update () 
	{
		
		
	}
	
	void RefreshCellState()//這裡再把網個不能走的地方設定好
	{
		FindingGrid.ResetCellBlockState();
		
		ObstacleComponent[] ObstacleArray = (ObstacleComponent[])GameObject.FindObjectsOfType(typeof(ObstacleComponent));
		foreach (ObstacleComponent obstacle in ObstacleArray)
		{	
			if(obstacle.GetComponent<Collider>() == null) {
				continue;
			}	
			Bounds bounds = obstacle.GetComponent<Collider>().bounds;
			FindingGrid.SetCellStateInRect(bounds, -1);

		}
		
		
	}
	void SetStartCellState()
    {
		FindingGrid.ResetCellBlockState();
		int i, j;
		Color col = m_BlackColor;
		Color colr = m_RedColor;


		for (i = 0; i < m_MaskTexture.width; i++)
		{
			for (j = 0; j < m_MaskTexture.height; j++)
			{
				//if(m_MaskTexture.GetPixel(i, j) == Color.white || m_MaskTexture.GetPixel(i, j) == colr)
    //            {
				//	FindingGrid.SetTexCellStateInStart(i, j, -1);

				//}
				if(m_MaskTexture.GetPixel(i,j)== col/*|| m_MaskTexture.GetPixel(i, j) == colr*/)
                {
					FindingGrid.SetTexCellStateInStart(i, j,-1);//把黑色的都設定成B
					FindingGrid.SetTrapCellAllState(i, j, 1);

				}
				if(m_MaskTexture.GetPixel(i, j) == colr)
                {
					FindingGrid.SetTrapCellAllState(i, j, 1);//少一行
															
				}
				
				
			}
		}

    }
	
	
	void OnDrawGizmos()
	{
		Gizmos.color = m_DebugColor;
		if(m_bDebug) {
			BaseGrid.DebugDraw(transform.position, m_nRows, m_nColumns, m_fCellSize, Gizmos.color);
			if(FindingGrid != null) {
				Vector3 cellPos;
				Vector3 size;
				for(int i = 0; i < FindingGrid.GetNodesNumber(); i++) {
					if(FindingGrid.IsNodeBlocked(i)) {
						cellPos = FindingGrid.GetCellCenter(i);
						size = new Vector3(FindingGrid.CellSize, 0.3f, FindingGrid.CellSize);
						UnityEngine.Gizmos.DrawCube(cellPos, size);
					}
				}
			}
			//PathFindingGrid.DebugDrawBlock(FindingGrid);
		}
	}
	
}
