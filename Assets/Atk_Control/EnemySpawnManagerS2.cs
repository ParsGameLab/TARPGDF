using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawnManagerS2 : MonoBehaviour
{
    
    public event EventHandler OnWaveNumberChangedS2;

    public Transform SpawpointR;
    public Transform SpawpointL;
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
    private int Gnumber;
    public int spawCount;
    private bool CanStart;
    public GameObject Gbutton;

    public string[] PathNameList;//=["","","",""];
    public string[] PathNameListElite;
    private string useMob;
    private string useMob2;
    private string useElite;

    public static EnemySpawnManagerS2 Instance;

    public GameObject WinUI;

    private float ReUseEnemyTimer;
    private int currectcounter = 0;
    public List<Transform> enemylistR = new List<Transform>();
    public List<Transform> enemylistL = new List<Transform>();

    private int EnemyAmountL;
    private int EnemyAmountR;
    private bool IsBonusGived;
    public GameObject BonusEffect;
    public int Wave1Bonus = 1000;
    public int Wave2Bonus = 1200;

    private float G1W1 = 8f;
    private float G1W2 = 9f;
    private float G1W3 = 8f;
    private float G1W4 = 8f;
    private float G2W1 = 10f;
    private float G2W2 = 10f;
    private float G2W3 = 6f;
    private float G2W4 = 15f;
    private float G2W5 = 12f;
    private float G2W6 = 12f;
    private float G3W1 = 14f;
    private float G3W2 = 10f;
    private float G3W3 = 12f;
    private float G3W4 = 6f;
    private float G3W5 = 15f;
    private float G3W6 = 18f;

    private void Awake()
    {
        BonusEffect.SetActive(false);
        Instance = this;
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
                EnemyAmountL = 10;
                EnemyAmountR = 10;
                currectcounter++;
                Gnumber += 1;
                CanStart = false;
                Gbutton.SetActive(false);
                IsBonusGived = false;
                OnWaveNumberChangedS2?.Invoke(this, EventArgs.Empty);
            }

        }
        
        switch (currectcounter)
        {
            case 1:
                EnemyGeneraterL(PathNameList[0]);
                G1W1 -= Time.deltaTime;
                if (G1W1 < 0f)
                {
                    EnemyAmountL = 11;
                    currectcounter = 2;
                }

                break;
            case 2:
                EnemyGeneraterL(PathNameList[3]);
                G1W2 -= Time.deltaTime;
                if (G1W2 < 0f)
                {
                    EnemyAmountL = 12;
                    currectcounter = 3;
                }
                break;
            case 3:
                EnemyGeneraterL(PathNameList[1]);
                G1W3 -= Time.deltaTime;
                if (G1W3 < 0f)
                {
                    EnemyAmountL = 6;
                    currectcounter = 4;
                }
                break;
            case 4:
                EnemyGeneraterL(PathNameListElite[0]);
                G1W4 -= Time.deltaTime;
                if (G1W4 < 0f)
                {
                    currectcounter = 5;
                }
                break;
            case 5:
                if (CheckEnemyClear() == true)
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
                EnemyGeneraterR(PathNameList[2]);
                G2W1 -= Time.deltaTime;
                if (G2W1 < 0f)
                {
                    EnemyAmountR = 20;
                    currectcounter = 7;
                }
                break;
            case 7:
                EnemyGeneraterR(PathNameList[2]);
                G2W2 -= Time.deltaTime;
                if (G2W2 < 0f)
                {
                    EnemyAmountR = 6;
                    currectcounter = 8;
                }
                break;
            case 8:
                EnemyGeneraterR(PathNameListElite[0]);
                G2W3 -= Time.deltaTime;
                if (G2W3 < 0f)
                {
                    EnemyAmountR = 12;
                    currectcounter = 9;
                }
                break;
            case 9:
                EnemyGeneraterR(PathNameList[0]);
                G2W4 -= Time.deltaTime;
                if (G2W4 < 0f)
                {
                    EnemyAmountR = 7;
                    currectcounter = 10;
                }
                break;
            case 10:
                EnemyGeneraterR(PathNameListElite[1]);
                G2W5 -= Time.deltaTime;
                if (G2W5 < 0f)
                {
                    EnemyAmountR = 17;
                    currectcounter = 11;
                }
                break;
            case 11:
                EnemyGeneraterR(PathNameList[3]);
                G2W6 -= Time.deltaTime;
                if (G2W6 < 0f)
                {

                    currectcounter = 12;
                }
                break;
            case 12:
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
            case 13:
                EnemyGeneraterL(PathNameList[3]);
                EnemyGeneraterR(PathNameList[0]);
                G3W1 -= Time.deltaTime;
                if (G3W1 < 0f)
                {
                    EnemyAmountR = 4;
                    EnemyAmountL = 12;
                    currectcounter = 14;
                }
                break;
            case 14:
                EnemyGeneraterL(PathNameList[0]);
                EnemyGeneraterR(PathNameListElite[0]);
                G3W2 -= Time.deltaTime;
                if (G3W2 < 0f)
                {
                    EnemyAmountR = 10;
                    EnemyAmountL = 5;
                    currectcounter = 15;
                }
                break;
            case 15:
                EnemyGeneraterL(PathNameListElite[1]);
                EnemyGeneraterR(PathNameList[2]);
                G3W3 -= Time.deltaTime;
                if (G3W3 < 0f)
                {
                    EnemyAmountR = 10;
                    EnemyAmountL = 6;
                    currectcounter = 16;
                }
                break;
            case 16:
                EnemyGeneraterL(PathNameListElite[0]);
                EnemyGeneraterR(PathNameList[1]);
                G3W4 -= Time.deltaTime;
                if (G3W4 < 0f)
                {
                    EnemyAmountR = 5;
                    EnemyAmountL = 10;
                    currectcounter = 17;
                }
                break;
            case 17:
                EnemyGeneraterL(PathNameList[3]);
                EnemyGeneraterR(PathNameList[0]);
                G3W5 -= Time.deltaTime;
                if (G3W5 < 0f)
                {
                    EnemyAmountR = 6;
                    EnemyAmountL = 10;
                    currectcounter = 18;
                }
                break;
            case 18:
                EnemyGeneraterL(PathNameList[0]);
                EnemyGeneraterR(PathNameListElite[1]);
                G3W5 -= Time.deltaTime;
                if (G3W5 < 0f)
                {
                    EnemyAmountR = 10;
                    EnemyAmountL = 10;
                    currectcounter = 19;
                }
                break;
            case 19:
                EnemyGeneraterL(PathNameList[1]);
                EnemyGeneraterR(PathNameList[2]);
                G3W6 -= Time.deltaTime;
                if (G3W6 < 0f)
                {   
                    currectcounter = 10;
                }
                break;
            case 20:
                if (CheckEnemyClear() == true)
                {
                    //通關選單打開
                    Debug.Log("通關選單打開");
                }
                break;


            // 此為預設 當上面的case都沒達成時則會判斷
            default:
                Debug.Log("生完ㄌ");
                break;
        }


    }
    public void EnemyGeneraterL(string mob)
    {
        if (EnemyAmountL > 0)
        {
            CreateL(mob);
            EnemyAmountL--;
        }
        else
        {
            EnemyAmountL = 0;
        }
        
    }
    public void EnemyGeneraterR(string mob)
    {
        if (EnemyAmountR> 0)
        {
            CreateR(mob);
            EnemyAmountR--;
        }
        else
        {
            EnemyAmountR = 0;
        }

    }
    public bool CheckEnemyClear()
    {
        if (enemylistR.Count == 0&& enemylistL.Count == 0)
        { return true; }
        else
        {
            return false;
        }
       
    }
    public void CreateL(string PathName)
    {
        Vector2 p = UnityEngine.Random.insideUnitCircle * 7;
        spawPosition = SpawpointL.position + new Vector3(p.x, 0, p.y);//圈圈生成

        Transform pfEnemy = Resources.Load<Transform>(PathName);
        Transform enemyTransform = Instantiate(pfEnemy, spawPosition, Quaternion.identity);
        enemyTransform.GetComponent<AINormalMob>().m_eMobRL = AINormalMob.eMobRL.L;
        enemylistL.Add(enemyTransform);

    }
    public void CreateR(string PathName)
    {
        Vector2 p = UnityEngine.Random.insideUnitCircle * 7;
        spawPosition = SpawpointR.position + new Vector3(p.x, 0, p.y);//圈圈生成

        Transform pfEnemy = Resources.Load<Transform>(PathName);
        Transform enemyTransform = Instantiate(pfEnemy, spawPosition, Quaternion.identity);
        enemyTransform.GetComponent<AINormalMob>().m_eMobRL = AINormalMob.eMobRL.R;
        enemylistR.Add(enemyTransform);

    }

    public int GetWaveNumber()
    {
        return Gnumber;
    }
    public float GetNextWaveSpawnTimer()
    {
        return nextWaveSpawTimer;
    }
    
}
