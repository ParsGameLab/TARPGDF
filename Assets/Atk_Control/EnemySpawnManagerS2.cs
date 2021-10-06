using DigitalRuby.ThunderAndLightning;
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
    public List<Transform> allenemylist= new List<Transform>();

    private int EnemyAmountL;
    private int EnemyAmountR;
    private bool IsBonusGived;
    public GameObject BonusEffect;
    public int Wave1Bonus = 1200;
    public int Wave2Bonus = 1500;
    public int Wave3Bonus = 2000;
    public int Wave4Bonus = 2500;

    public GameObject LightEffect;
    public Transform LightEffectstartposR;
    public Transform LightEffectstartposL;
    public Transform LightEnd;

    public bool S2Clear;

    private float G1W1 = 8f;
    private float G1W2 = 9f;
    private float G1W3 = 10f;
    private float G1W4 = 8f;
    private float G1W5 = 8f;
    private float G2W1 = 10f;
    private float G2W2 = 10f;
    private float G2W3 = 6f;
    private float G2W4 = 15f;
    private float G2W5 = 12f;
    private float G2W6 = 12f;
    private float G3W1 = 14f;
    private float G3W2 = 16f;
    private float G3W3 = 12f;
    private float G3W4 = 15f;
    private float G3W5 = 17f;
    private float G4W1 = 10f;
    private float G4W2 = 11f;
    private float G4W3 = 15f;
    private float G4W4 = 12f; 
    private float G4W5 = 13f;
    private float G4W6 = 14f;
    private float G5W1 = 15f;
    private float G5W2 = 13f;
    private float G5W3 = 11f;
    private float G5W4 = 10f;
    private float G5W5 = 9f;
    private float G5W6 = 14f;
    private float G5W7 = 10f;

    private void Awake()
    {
        BonusEffect.SetActive(false);
        Instance = this;
        S2Clear = false;
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
                    EnemyAmountL = 15;
                    currectcounter = 2;
                }

                break;
            case 2:
                EnemyGeneraterL(PathNameList[3]);
                G1W2 -= Time.deltaTime;
                if (G1W2 < 0f)
                {
                    EnemyAmountL = 16;
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
                    EnemyAmountL = 10;
                    currectcounter = 5;
                }
                break;
            case 5:
                EnemyGeneraterL(PathNameList[0]);
                G1W5 -= Time.deltaTime;
                if (G1W5 < 0f)
                {
                    currectcounter = 6;
                }
                break;

            case 6:
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
            case 7:
                EnemyGeneraterR(PathNameList[2]);
                G2W1 -= Time.deltaTime;
                if (G2W1 < 0f)
                {
                    EnemyAmountR = 20;
                    currectcounter = 8;
                }
                break;
            case 8:
                EnemyGeneraterR(PathNameList[2]);
                G2W2 -= Time.deltaTime;
                if (G2W2 < 0f)
                {
                    EnemyAmountR = 6;
                    currectcounter = 9;
                }
                break;
            case 9:
                EnemyGeneraterR(PathNameListElite[0]);
                G2W3 -= Time.deltaTime;
                if (G2W3 < 0f)
                {
                    EnemyAmountR = 12;
                    currectcounter = 10;
                }
                break;
            case 10:
                EnemyGeneraterR(PathNameList[0]);
                G2W4 -= Time.deltaTime;
                if (G2W4 < 0f)
                {
                    EnemyAmountR = 7;
                    currectcounter = 11;
                }
                break;
            case 11:
                EnemyGeneraterR(PathNameListElite[1]);
                G2W5 -= Time.deltaTime;
                if (G2W5 < 0f)
                {
                    EnemyAmountR = 17;
                    currectcounter = 12;
                }
                break;
            case 12:
                EnemyGeneraterR(PathNameList[3]);
                G2W6 -= Time.deltaTime;
                if (G2W6 < 0f)
                {

                    currectcounter = 13;
                }
                break;
            case 13:
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
            case 14:
                
                EnemyGeneraterR(PathNameList[0]);
                G3W1 -= Time.deltaTime;
                if (G3W1 < 0f)
                {
                    EnemyAmountR = 20;
                    
                    currectcounter = 15;
                }
                break;
            case 15:
               
                EnemyGeneraterR(PathNameList[1]);
                G3W2 -= Time.deltaTime;
                if (G3W2 < 0f)
                {
                    
                    EnemyAmountL = 20;
                    currectcounter = 16;
                }
                break;
            case 16:
                EnemyGeneraterL(PathNameList[0]);
                
                G3W3 -= Time.deltaTime;
                if (G3W3 < 0f)
                {
                    
                    EnemyAmountL = 19;
                    currectcounter = 17;
                }
                break;
            case 17:
                EnemyGeneraterL(PathNameList[2]);
                
                G3W4 -= Time.deltaTime;
                if (G3W4 < 0f)
                {
                    EnemyAmountR = 15;
                    EnemyAmountL = 15;
                    currectcounter = 18;
                }
                break;
            case 18:
                EnemyGeneraterL(PathNameList[3]);
                EnemyGeneraterR(PathNameList[3]);
                G3W5 -= Time.deltaTime;
                if (G3W5 < 0f)
                {
                    EnemyAmountR = 7;
                    EnemyAmountL = 8;
                    currectcounter = 19;
                }
                break;
            case 19:
                EnemyGeneraterL(PathNameListElite[0]);
                EnemyGeneraterR(PathNameListElite[1]);
                G3W5 -= Time.deltaTime;
                if (G3W5 < 0f)
                {
                    currectcounter = 20;
                }
                break;
            case 20:
                if (CheckEnemyClear() == true)
                {
                    if (IsBonusGived == false)
                    {
                        BonusEffect.SetActive(true);
                        Player.Instance.AddCoinAmount(Wave3Bonus);
                        IsBonusGived = true;
                    }
                    CanStart = true;
                }
                break;
            case 21:
                EnemyGeneraterL(PathNameList[0]);
                EnemyGeneraterR(PathNameList[0]);
                G4W1 -= Time.deltaTime;
                if (G4W1 < 0f)
                {
                    EnemyAmountR = 16;
                    EnemyAmountL = 18;
                    currectcounter = 22;
                }
                break;
            case 22:
                EnemyGeneraterL(PathNameList[2]);
                EnemyGeneraterR(PathNameList[2]);
                G4W2 -= Time.deltaTime;
                if (G4W2 < 0f)
                {
                    EnemyAmountR = 6;
                    EnemyAmountL = 20;
                    currectcounter = 23;
                }
                break;
            case 23:
                EnemyGeneraterL(PathNameList[3]);
                EnemyGeneraterR(PathNameListElite[0]);
                G4W3 -= Time.deltaTime;
                if (G4W3 < 0f)
                {
                    EnemyAmountR = 20;
                    EnemyAmountL = 8;
                    currectcounter = 24;
                }
                break;
            case 24:
                EnemyGeneraterL(PathNameListElite[1]);
                EnemyGeneraterR(PathNameList[0]);
                G4W4 -= Time.deltaTime;
                if (G4W4 < 0f)
                {
                    EnemyAmountR = 9;
                    EnemyAmountL = 20;
                    currectcounter = 25;
                }
                break;
            case 25:
                EnemyGeneraterL(PathNameList[3]);
                EnemyGeneraterR(PathNameListElite[0]);
                G4W5 -= Time.deltaTime;
                if (G4W5 < 0f)
                {
                    EnemyAmountR = 10;
                    EnemyAmountL = 20;
                    currectcounter = 26;
                }
                break;
            case 26:
                EnemyGeneraterL(PathNameList[2]);
                EnemyGeneraterR(PathNameListElite[1]);
                G4W6 -= Time.deltaTime;
                if (G4W6 < 0f)
                {
                    currectcounter = 27;
                }
                break;
            case 27:
                if (CheckEnemyClear() == true)
                {
                    if (IsBonusGived == false)
                    {
                        BonusEffect.SetActive(true);
                        Player.Instance.AddCoinAmount(Wave4Bonus);
                        IsBonusGived = true;
                    }
                    CanStart = true;
                }
                break;
            case 28:
                EnemyGeneraterL(PathNameList[2]);
                EnemyGeneraterR(PathNameListElite[1]);
                G5W1 -= Time.deltaTime;
                if (G5W1 < 0f)
                {
                    EnemyAmountR = 20;
                    EnemyAmountL = 23;
                    currectcounter = 29;
                }
                break;
            case 29:
                EnemyGeneraterL(PathNameList[2]);
                EnemyGeneraterR(PathNameList[3]);
                G5W2 -= Time.deltaTime;
                if (G5W2 < 0f)
                {
                    EnemyAmountR = 10;
                    EnemyAmountL = 26;
                    currectcounter = 30;
                }
                break;
            case 30:
                EnemyGeneraterL(PathNameList[0]);
                EnemyGeneraterR(PathNameListElite[1]);
                G5W3 -= Time.deltaTime;
                if (G5W3 < 0f)
                {
                    EnemyAmountR = 29;
                    EnemyAmountL = 23;
                    currectcounter = 31;
                }
                break;
            case 31:
                EnemyGeneraterL(PathNameList[2]);
                EnemyGeneraterR(PathNameList[3]);
                G5W4 -= Time.deltaTime;
                if (G5W4 < 0f)
                {
                    EnemyAmountR = 10;
                    EnemyAmountL = 10;
                    currectcounter = 32;
                }
                break;
            case 32:
                EnemyGeneraterL(PathNameListElite[1]);
                EnemyGeneraterR(PathNameListElite[0]);
                G5W5 -= Time.deltaTime;
                if (G5W5 < 0f)
                {
                    EnemyAmountR = 26;
                    EnemyAmountL = 20;
                    currectcounter = 33;
                }
                break;
            case 33:
                EnemyGeneraterL(PathNameList[2]);
                EnemyGeneraterR(PathNameList[2]);
                G5W6 -= Time.deltaTime;
                if (G5W6 < 0f)
                {
                    EnemyAmountR = 10;
                    EnemyAmountL = 10;
                    currectcounter = 34;
                }
                break;
            case 34:
                EnemyGeneraterL(PathNameListElite[1]);
                EnemyGeneraterR(PathNameListElite[0]);
                G5W7 -= Time.deltaTime;
                if (G5W7 < 0f)
                {
                    currectcounter = 35;
                }
                break;
            case 35:
                if (CheckEnemyClear() == true)
                {
                    S2Clear = true;

                }
                break;



            // 此為預設 當上面的case都沒達成時則會判斷
            default:
                Debug.Log("生完ㄌ");
                break;
        }


    }
    public bool IsS2Clear
    {
        get { return S2Clear; }
    }
    public void EnemyGeneraterL(string mob)
    {
        if (EnemyAmountL > 0)
        {
            nextMobSpawTimer -= Time.deltaTime;
            if (nextMobSpawTimer < 0f)
            {
                nextMobSpawTimer = UnityEngine.Random.Range(0.2f, 0.9f);

                CreateL(mob);
                EnemyAmountL--;

            }

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
            nextMobSpawTimer2 -= Time.deltaTime;
            if (nextMobSpawTimer2 < 0f)
            {
                nextMobSpawTimer2 = UnityEngine.Random.Range(0.1f, 0.7f);
                CreateR(mob);
                EnemyAmountR--;

            }
            
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
        GameObject Lightn = Instantiate(LightEffect);
        Lightn.GetComponent<LightningBoltPrefabScript>().Source.transform.position = LightEffectstartposL.position;
        Lightn.GetComponent<LightningBoltPrefabScript>().Destination.transform.position = spawPosition;
        enemyTransform.GetComponent<AINormalMob>().m_eMobRL = AINormalMob.eMobRL.L;
        enemylistL.Add(enemyTransform);
        allenemylist.Add(enemyTransform);
        Destroy(Lightn, 0.5f);

    }
    public void CreateR(string PathName)
    {
        Vector2 p = UnityEngine.Random.insideUnitCircle * 7;
        spawPosition = SpawpointR.position + new Vector3(p.x, 0, p.y);//圈圈生成

        Transform pfEnemy = Resources.Load<Transform>(PathName);
        Transform enemyTransform = Instantiate(pfEnemy, spawPosition, Quaternion.identity);
        GameObject Lightn = Instantiate(LightEffect);
        Lightn.GetComponent<LightningBoltPrefabScript>().Source.transform.position = LightEffectstartposR.position;
        Lightn.GetComponent<LightningBoltPrefabScript>().Destination.transform.position = spawPosition;
        enemyTransform.GetComponent<AINormalMob>().m_eMobRL = AINormalMob.eMobRL.R;
        enemylistR.Add(enemyTransform);
        allenemylist.Add(enemyTransform);
        Destroy(Lightn, 0.5f);
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
