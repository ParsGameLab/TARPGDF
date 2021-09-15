using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Main : MonoBehaviour {

    public static Main m_Instance;

    private List<Obstacle> m_Obstacles;//宣告一個裝物件的陣列形式
    public GameObject m_Player;//帶入我或NPC
    private void Awake()
    {
        m_Instance = this;
    }

    // Use this for initialization
    void Start () {
        m_Obstacles = new List<Obstacle>();
        GameObject[] gos = GameObject.FindGameObjectsWithTag("Obstacle");
        if (gos != null || gos.Length > 0)
        {
            Debug.Log(gos.Length);
            foreach (GameObject go in gos)
            {
                m_Obstacles.Add(go.GetComponent<Obstacle>());//開始ㄌ，就把障礙物的位置輸入
            }
        }
    }

    public GameObject GetPlayer()
    {
        return m_Player;
    }

    public List<Obstacle> GetObstacles()
    {
        return m_Obstacles;//會回傳物件的方法//專門給障礙物避免用的
    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
