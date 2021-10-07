using DigitalRuby.ThunderAndLightning;
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
    private float nextMobSpawTimer2;
    private float nextMobSpawTimerElite;
    private int remainingMobSpawAmount;
    private int remainingMobSpawAmountType2;
    private int remainingEliteMobAmount;
    private int WaveNumber;
    public int SceneWave;
    private int Gnumber;
    public int spawCount;
    private bool CanStart;
    public GameObject Gbutton;

    public const string Wolf = "Mobs/pfWolf";
    public string[] PathNameList;//=["","","",""];
    public string[] PathNameListElite;
    private string useMob;
    private string useMob2;
    private string useElite;

    public static EnemySponManerger Instance;
    public float PathFollowWeight = 1f;
    public WayPath Path;
    private List<Boid> m_boids = new List<Boid>(20);

    public GameObject lastUI;

    private float ReUseEnemyTimer;
    private int currectcounter=0;
    public List<Transform> enemylist = new List<Transform>();

    private int EnemyAmount;
    private bool IsBonusGived;
    public GameObject BonusEffect;
    public int Wave1Bonus=500;
    public int Wave2Bonus = 700;

    public GameObject LightEffect;
    public Transform LightEffectstartpos;
    public Transform LightEnd;

    public bool S1Clear;

    private float G1W1 = 5f;
    private float G1W2 = 6f;
    private float G1W3 = 7f;
    private float G1W4 = 8f;
    private float G2W1 = 3f;
    private float G2W2 = 7f;
    private float G2W3 = 6f;
    private float G2W4 = 8f;
    private float G3W1 = 6f;
    private float G3W2 = 10f;
    private float G3W3 = 7f;
    private float G3W4 = 9f;

    private void Awake()
    {
        BonusEffect.SetActive(false);
        Instance = this;
        S1Clear = false;
        lastUI.SetActive(false);


    }
    void Start()
    {
        //PathNameList =["Mobs/pfWolf", "Mobs/pfLizard", "Mobs/pfRatAssassin", "Mobs/pfSpecter"];
        CanStart = true;
        Gnumber = 0;
        spawCount = 0;
        WaveNumber = 1;


    }

    // Update is called once per frame
    void Update()
    {
        
        if (CanStart)
        {
            Gbutton.SetActive(true);
            if (Input.GetKeyDown(KeyCode.G))
            {
                BonusEffect.SetActive(false);
                EnemyAmount = 5;
                currectcounter++;
                Gnumber += 1;
                if (Gnumber == 3)
                {
                    lastUI.SetActive(true);
                    StartCoroutine(lastWaveDispear());
                }
                CanStart = false;
                Gbutton.SetActive(false);
                IsBonusGived = false;
                OnWaveNumberChanged?.Invoke(this, EventArgs.Empty);
            }

        }
        //if (CanStart)
        //{
        //    Gbutton.SetActive(true);
        //    if (Input.GetKeyDown(KeyCode.G))
        //    {
        //        Gnumber += 1;
        //        spawCount += 1;

        //        WaveNumber = 1;
        //        CanStart = false;
        //        Gbutton.SetActive(false);
        //    }

        //}


        //if (Gnumber == 1)
        //{
        //    if (WaveNumber <= 2)
        //    {
        //        G1();
        //    }
        //    else
        //    {
        //        nextWaveSpawTimer = 0;
        //        CanStart = true;
        //    }       
        //}
        //else if (Gnumber == 2)
        //{
        //    if (WaveNumber <= 3)
        //    {
        //        G1();
        //    }
        //    else
        //    {
        //        nextWaveSpawTimer = 0;
        //        CanStart = true;
        //    }

        //}
        //else if(Gnumber == 3)
        //{
        //    if (WaveNumber <= 4)
        //    {
        //        G1();
        //    }
        //    else
        //    {
        //        nextWaveSpawTimer = 0;
        //        CanStart = true;
        //    }
        //    if (CheckEnemyClear())
        //    {
        //        WinUI.SetActive(true);
        //    }

        //}
        //每一波分開執行，看Each裡有幾波，再看要做幾次

        switch (currectcounter)
        {
            case 1:
                EnemyGenerater(PathNameList[0]);
                G1W1 -= Time.deltaTime;
                if (G1W1 < 0f)
                {
                    EnemyAmount = 8;
                    currectcounter =2;
                }
                
                break;
            case 2:
                EnemyGenerater(PathNameList[0]);
                G1W2 -= Time.deltaTime;
                if (G1W2 < 0f)
                {
                    EnemyAmount = 6;
                    currectcounter =3;
                }
                break;
            case 3:
                EnemyGenerater(PathNameList[1]);
                G1W3 -= Time.deltaTime;
                if (G1W3 < 0f)
                {
                    EnemyAmount = 9;
                    currectcounter = 4;
                }
                break;
            case 4:
                EnemyGenerater(PathNameList[1]);
                G1W4 -= Time.deltaTime;
                if (G1W4 < 0f)
                {
                    currectcounter=7;
                }
                break;
            case 5:
                if (CheckEnemyClear()==true)
                {
                    if (IsBonusGived == false)
                    {
                        BonusEffect.SetActive(true);
                        Player.Instance.AddCoinAmount(Wave1Bonus);
                        IsBonusGived = true;
                    }
                    CanStart = true;
                }
                break;
            case 6:
                EnemyGenerater(PathNameList[2]);
                G2W1 -= Time.deltaTime;
                if (G2W1 < 0f)
                {
                    EnemyAmount = 15;
                    currectcounter =7;
                }
                break;
            case 7:
                EnemyGenerater(PathNameList[2]);
                G2W2 -= Time.deltaTime;
                if (G2W2 < 0f)
                {
                    EnemyAmount = 9;
                    currectcounter =8;
                }
                break;
            case 8:
                EnemyGenerater(PathNameList[0]);
                G2W3 -= Time.deltaTime;
                if (G2W3 < 0f)
                {
                    EnemyAmount = 10;
                    currectcounter = 9;
                }
                break;
            case 9:
                EnemyGenerater(PathNameList[0]);
                G2W4 -= Time.deltaTime;
                if (G2W4 < 0f)
                {
                    
                    currectcounter = 11;
                }
                break;
            case 10:
                if (CheckEnemyClear() == true)
                {
                    if (IsBonusGived == false)
                    {
                        BonusEffect.SetActive(true);
                        Player.Instance.AddCoinAmount(Wave2Bonus);
                        IsBonusGived = true;
                    }
                    CanStart = true;
                }
                break;
            case 11:
                EnemyGenerater(PathNameList[3]);
                G3W1 -= Time.deltaTime;
                if (G3W1 < 0f)
                {
                    EnemyAmount = 12;
                    currectcounter = 12;
                }
                break;
            case 12:
                EnemyGenerater(PathNameList[0]);
                G3W2 -= Time.deltaTime;
                if (G3W2 < 0f)
                {
                    EnemyAmount = 13;
                    currectcounter = 13;
                }
                break;
            case 13:
                EnemyGenerater(PathNameList[2]);
                G3W3 -= Time.deltaTime;
                if (G3W3 < 0f)
                {
                    EnemyAmount = 16;
                    currectcounter = 14;
                }
                break;
            case 14:
                EnemyGenerater(PathNameList[1]);
                G3W4 -= Time.deltaTime;
                if (G3W4 < 0f)
                {
                    currectcounter = 18;
                }
                break;
            case 15:
                if (CheckEnemyClear() == true)
                {
                    Main.m_Instance.GetPlayer().GetComponent<unitychanControl>().PlayWinPose();
                    StartCoroutine(WaitWin());
                    Debug.Log("通關選單打開");
                }
                break;


            // 此為預設 當上面的case都沒達成時則會判斷
            default:
                Debug.Log("生完ㄌ");
                break;
        }
        if (Input.GetKeyDown(KeyCode.F10))
        {
            currectcounter = 15;
        }
        
    }
    IEnumerator WaitWin()
    {
        yield return new WaitForSeconds(4f);
        S1Clear = true;
    }
    IEnumerator lastWaveDispear()
    {
        yield return new WaitForSeconds(3f);
        lastUI.SetActive(false);
    }

    public void EnemyGenerater(string mob)
    {
        if (EnemyAmount > 0)
        {
            Create(mob);
            EnemyAmount--;
        }
        else
        {
            EnemyAmount = 0;
        }
        //if (mobAmount == 0&& timer <= 0f)
        //{
        //    //ReUseEnemyTimer = 0f;
        //    currectcounter++;
        //}
    }

    //public void EnemySpawnTimer(float time, string mob, int mobAmount)
    //{
    //    ReUseEnemyTimer -= Time.deltaTime;
    //    if (ReUseEnemyTimer < 0f)
    //    {
    //        ReUseEnemyTimer = time;
    //        EnemyGenerater(mob, mobAmount);
    //    }


    //}
    public bool CheckEnemyClear()
    {
        if (enemylist.Count==0) 
        { return true; }
        else
        {
            return false;
        }
        //foreach(Transform go in enemylist)
        //{
        //    if (go == null)
        //    {
        //        return true;
        //    }
        //    else
        //    {
        //        return false;
        //    }

        //}
        //return false;
    }
    private void SpawnWave()//每一小波的時間間隔跟數量種類
    {
        nextWaveSpawTimer = 20f;
        
        remainingMobSpawAmount = 3 + 1 * WaveNumber+ 1*Gnumber+ 1*spawCount;//+看Gnumber給精英怪
        remainingMobSpawAmountType2 = 3 + 1 * WaveNumber + 1 * Gnumber + 1 * spawCount;
        remainingEliteMobAmount = 1 * Gnumber;
        WaveNumber++;
        useMob = PathNameList[UnityEngine.Random.Range(0, PathNameList.Length)];
        useMob2 = PathNameList[UnityEngine.Random.Range(0, PathNameList.Length)];
        useElite = PathNameListElite[UnityEngine.Random.Range(0, PathNameListElite.Length)];
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
                Create(useMob);//普通兩波分開兩種各生一半
                //randoncraete
                remainingMobSpawAmount--;
               
            }
        }//remainingEliteMobAmount照寫
        if (remainingMobSpawAmountType2 > 0)
        {
            nextMobSpawTimer2 -= Time.deltaTime;
            if (nextMobSpawTimer2 < 0f)
            {
                nextMobSpawTimer2 = UnityEngine.Random.Range(0.2f, 0.4f);
                Create(useMob2);//普通兩波分開兩種各生一半
                //randoncraete
                remainingMobSpawAmountType2--;

            }
        }
        if (remainingEliteMobAmount > 0)
        {
            
            nextMobSpawTimerElite -= Time.deltaTime;
            if (nextMobSpawTimerElite < 0f)
            {
                nextMobSpawTimerElite = UnityEngine.Random.Range(3f, 4f);
                Create(useElite);//普通兩波分開兩種各生一半
                //randoncraete
                remainingEliteMobAmount--;

            }
        }

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
                Create(useMob);
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
        spawPosition = Spawpoint.position + new Vector3(p.x, 0, p.y);//圈圈生成
        
        Transform pfEnemy = Resources.Load<Transform>(PathName);
        Transform enemyTransform = Instantiate(pfEnemy, spawPosition, Quaternion.identity);
        
        
        GameObject Lightn = Instantiate(LightEffect);
        Lightn.GetComponent<LightningBoltPrefabScript>().Source.transform.position = LightEffectstartpos.position;
        Lightn.GetComponent<LightningBoltPrefabScript>().Destination.transform.position = spawPosition;

        enemyTransform.GetComponent<AINormalMob>().m_eMobRL = AINormalMob.eMobRL.oneway;
        enemylist.Add(enemyTransform);

        Destroy(Lightn, 0.3f);
        //var boid = enemyTransform.GetComponent<Boid>();
        //boid.Position = spawPosition;
        //boid.Path = Path;
        //m_boids.Add(boid);
    }

    public bool IsS1Clear
    {
        get { return S1Clear; }
    }
    public void ReSetS1Clear()
    {
        S1Clear = false;
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
