using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySponManerger : MonoBehaviour
{
    public Transform Spawpoint;
    // Start is called before the first frame update
    float timelimit;
    float nextTime;
    void Start()
    {
        SpawnWave();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void SpawnWave()
    {
        for(int i = 0; i < 2; i++)
        {
            Create(Spawpoint.position);
        }
    }
    public void Create(Vector3 position)
    {
        Vector2 p = Random.insideUnitCircle * 5;
        Vector3 spawpoint=position+new Vector3(p.x,0,p.y);
        Transform pfEnemy = Resources.Load<Transform>("pfWolf");
        Transform enemyTransform=Instantiate(pfEnemy, spawpoint, Quaternion.identity);
    }
}
