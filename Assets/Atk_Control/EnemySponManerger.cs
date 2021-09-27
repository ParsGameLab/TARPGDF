using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySponManerger : MonoBehaviour
{
    public event EventHandler OnWaveNumberChanged;

    public Transform Spawpoint;
    private Vector3 spawPosition;
    // Start is called before the first frame update
    private float nextWaveSpawTimer;
    private float nextMobSpawTimer;
    private int remainingMobSpawAmount;
    private int remainingEliteMobAmount;
    private int WaveNumber;
    public int SceneWave;
    private int Gnumber;
    public int spawCount;
    private bool CanStart;

    public const string Wolf = "Mobs/pfWolf";
    private string[] PathNameList;//=["","","",""];

    public static EnemySponManerger Instance;
    public float PathFollowWeight = 1f;
    public WayPath Path;
    private List<Boid> m_boids = new List<Boid>(20);
    private void Awake()
    {
        Instance = this;
    }
    void Start()
    {
        CanStart = true;
        Gnumber = 0;
        spawCount = 1;
        WaveNumber = 1;


    }

    // Update is called once per frame
    void Update()
    {
        if (CanStart)
        {
            if (Input.GetKeyDown(KeyCode.G))
            {
                Gnumber += 1;
                spawCount = 1;
                WaveNumber = 1;
                CanStart = false;
            }

        }
        

        if (Gnumber == 1)
        {
            if (WaveNumber <= 3)
            {
                G1();
            }
            else
            {
                nextWaveSpawTimer = 0;
                CanStart = true;
            }       
        }
        else if (Gnumber == 2)
        {
            if (WaveNumber <= 4)
            {
                G2();
            }
            else
            {
                nextWaveSpawTimer = 0;
                CanStart = true;
            }

        }
        else if(Gnumber == 3)
        {

        }
        //每一波分開執行，看Each裡有幾波，再看要做幾次
        
    }
    private void SpawnWave()//每一小波的時間間隔跟數量種類
    {
        nextWaveSpawTimer = 10f;
        spawCount++;
        remainingMobSpawAmount = 1 + 6 * WaveNumber+ 2*Gnumber;//+看Gnumber給精英怪
        remainingEliteMobAmount = 2 * Gnumber;
        WaveNumber++;
        OnWaveNumberChanged?.Invoke(this, EventArgs.Empty);
        //這裡決定要生的怪物string下面帶入

    }
    private void G1()
    {
        nextWaveSpawTimer -= Time.deltaTime;
        if (nextWaveSpawTimer < 0f)
        {
            SpawnWave();
        }
        if (remainingMobSpawAmount > 0)
        {
            nextMobSpawTimer -= Time.deltaTime;
            if (nextMobSpawTimer < 0f)
            {
                nextMobSpawTimer = UnityEngine.Random.Range(0f, 0.3f);
                Create(Wolf);//普通兩波分開兩種各生一半
                //randoncraete
                remainingMobSpawAmount--;
               
            }
        }//remainingEliteMobAmount照寫
    }
    private void G2()
    {
        nextWaveSpawTimer -= Time.deltaTime;
        if (nextWaveSpawTimer < 0f)
        {
            SpawnWave();
        }
        if (remainingMobSpawAmount > 0)
        {
            nextMobSpawTimer -= Time.deltaTime;
            if (nextMobSpawTimer < 0f)
            {
                nextMobSpawTimer = UnityEngine.Random.Range(0f, 0.3f);
                Create(Wolf);
                remainingMobSpawAmount--;
            }
        }
    }
    public int GetWaveNumber()
    {
        return Gnumber;
    }
    public float GetNextWaveSpawnTimer()
    {
        return nextWaveSpawTimer;
    }
    public void Create(string PathName)
    {
        Vector2 p = UnityEngine.Random.insideUnitCircle * 7;
        spawPosition = Spawpoint.position + new Vector3(p.x, 0, p.y);
        Transform pfEnemy = Resources.Load<Transform>(PathName);
        Transform enemyTransform = Instantiate(pfEnemy, spawPosition, Quaternion.identity);
        var boid = enemyTransform.GetComponent<Boid>();
        boid.Position = spawPosition;
        boid.Path = Path;
        m_boids.Add(boid);
    }

    public static float GetPointToSegmentDistanceSqr(Vector2 askPoint, Vector2 p0, Vector2 p1, Vector2 normal, float length, out Vector2 mapPoint, out float projectionLength)
    {
        Vector2 local = askPoint - p0;
        projectionLength = Vector2.Dot(normal, local);
        if (projectionLength < 0)
        {
            mapPoint = p0;
            projectionLength = 0;
            return local.sqrMagnitude;
        }
        if (projectionLength > length)
        {
            mapPoint = p1;
            projectionLength = length;
            return (p1 - askPoint).sqrMagnitude;
        }

        mapPoint = normal * projectionLength + p0;
        return (mapPoint - askPoint).sqrMagnitude;
    }
    public static Vector2 GetPathFollowSteer(Boid boid, WayPath path)
    {
        Vector2 nextPosition = boid.VeryNextPosition, segmentNormal;
        bool isOutOfPathTunnel;
        float distance = path.GetDistanceByMapPoint(nextPosition, out isOutOfPathTunnel, out segmentNormal);//算自己未來的行動
        if (distance >= path.TotalPathLength)
            return -boid.Forward;
        bool isWrongDirection = Vector2.Dot(segmentNormal, boid.Forward) < 0;
        bool needSteerToFollowPath = isOutOfPathTunnel || isWrongDirection;// || (boid.MoveState == UnitMoveState.StandBy);
        if (needSteerToFollowPath)
        {
            float nextPathDistance = distance + (isWrongDirection ? 100f : 1) * boid.Speed * Time.fixedDeltaTime;
            Vector2 targetPoint = path.GetPathPointByDistance(nextPathDistance);
            return GetSeekSteer(boid, targetPoint);
        }
        else
            return Vector2.zero;
    }
    public static Vector2 GetSeekSteer(Boid boid, Vector2 target)
    {
        return ((target - new Vector2(boid.Position.x,boid.Position.z)).normalized - new Vector2(boid.Forward.x,boid.Position.z)).normalized;
    }
}
