using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TpGridManager : MonoBehaviour
{
    private GameObject m_PathfindingTerrain;
    private GameObject m_WeaponController;
    // Start is called before the first frame update
    void Start()
    {
        m_PathfindingTerrain = GameObject.Find("Grid");
        m_WeaponController= GameObject.Find("SdUnitychan");
        PathfindingGridComponent pg = m_PathfindingTerrain.GetComponent(typeof(PathfindingGridComponent)) as PathfindingGridComponent;//³õ¤W§ä
        WeaponController wc = m_WeaponController.GetComponent(typeof(WeaponController)) as WeaponController;
        wc.Init(pg.FindingGrid);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
