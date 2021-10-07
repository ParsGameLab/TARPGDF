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

    public GameObject lastUI;

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

    public GameObject SupportCannonUI;
    public List<GameObject> Cannon = new List<GameObject>();
    public GameObject BuildCannonEffect;

    

    public bool S2Clear;

    private float G1W1 = 8f;
    private float G1W2 = 9f;
    private float G1W3 = 10f;
    private float G1W4 = 8f;
    private float G1W5 = 8f;
    private float G2W1 = 10f;
    private float G2W2 = 10f;
    private float G2W3 = 10f;
    private float G2W4 = 19f;
    private float G2W5 = 12f;
    private float G2W6 = 12f;
    private float G3W1 = 19f;
    private float G3W2 = 16f;
    private float G3W3 = 12f;
    private float G3W4 = 19f;
    private float G3W5 = 17f;
    private float G4W1 = 10f;
    private float G4W2 = 16f;
    private float G4W3 = 15f;
    private float G4W4 = 12f; 
    private float G4W5 = 19f;
    private float G4W6 = 18f;
    private float G5W1 = 22f;
    private float G5W2 = 21f;
    private float G5W3 = 23f;
    private float G5W4 = 21f;
    private float G5W5 = 20f;
    private float G5W6 = 10f;
    private float G5W7 = 20f;

    private void Awake()
    {
        BonusEffect.SetActive(false);
        Instance = this;
        S2Clear = false;
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
                EnemyAmountL = 15;
                EnemyAmountR = 15;
                currectcounter++;
                Gnumber += 1;
                if (Gnumber == 5)
                {
                    lastUI.SetActive(true);
                    StartCoroutine(lastWaveDispear());
                }
                CanStart = false;
                Gbutton.SetActive(false);
                IsBonusGived = false;
                OnWaveNumberChangedS2?.Invoke(this, EventArgs.Empty);
            }

        }
        
        switch (currectcounter)
        {
            case 1:
                EnemyGeneraterL(PathNameList[0],TimeRange(0.2f,0.9f));
                G1W1 -= Time.deltaTime;
                if (G1W1 < 0f)
                {
                    EnemyAmountL = 19;
                    currectcounter = 2;
                }

                break;
            case 2:
                EnemyGeneraterL(PathNameList[3], TimeRange(0.2f, 0.9f));
                G1W2 -= Time.deltaTime;
                if (G1W2 < 0f)
                {
                    EnemyAmountL = 16;
                    currectcounter = 3;
                }
                break;
            case 3:
                EnemyGeneraterL(PathNameList[1], TimeRange(0.2f, 0.9f));
                G1W3 -= Time.deltaTime;
                if (G1W3 < 0f)
                {
                    EnemyAmountL = 7;
                    currectcounter = 4;
                }
                break;
            case 4:
                EnemyGeneraterL(PathNameListElite[0], TimeRange(3f, 5f));
                G1W4 -= Time.deltaTime;
                if (G1W4 < 0f)
                {
                    EnemyAmountL = 17;
                    currectcounter = 5;
                }
                break;
            case 5:
                EnemyGeneraterL(PathNameList[0], TimeRange(0.1f, 0.9f));
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
                EnemyGeneraterR(PathNameList[2], TimeRange(0.2f, 0.9f));
                G2W1 -= Time.deltaTime;
                if (G2W1 < 0f)
                {
                    EnemyAmountR = 20;
                    currectcounter = 8;
                }
                break;
            case 8:
                EnemyGeneraterR(PathNameList[2], TimeRange(0.2f, 0.9f));
                G2W2 -= Time.deltaTime;
                if (G2W2 < 0f)
                {
                    EnemyAmountR = 6;
                    currectcounter = 9;
                }
                break;
            case 9:
                EnemyGeneraterR(PathNameListElite[0], TimeRange(3.5f, 4.5f));
                G2W3 -= Time.deltaTime;
                if (G2W3 < 0f)
                {
                    EnemyAmountR = 12;
                    currectcounter = 10;
                }
                break;
            case 10:
                EnemyGeneraterR(PathNameList[0], TimeRange(0.2f, 0.9f));
                G2W4 -= Time.deltaTime;
                if (G2W4 < 0f)
                {
                    EnemyAmountR = 7;
                    currectcounter = 11;
                }
                break;
            case 11:
                EnemyGeneraterR(PathNameListElite[1], TimeRange(2.5f, 4.5f));
                G2W5 -= Time.deltaTime;
                if (G2W5 < 0f)
                {
                    EnemyAmountR = 17;
                    currectcounter = 12;
                }
                break;
            case 12:
                EnemyGeneraterR(PathNameList[3], TimeRange(0.2f, 0.9f));
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
                
                EnemyGeneraterR(PathNameList[0], TimeRange(0.3f, 0.9f));
                G3W1 -= Time.deltaTime;
                if (G3W1 < 0f)
                {
                    EnemyAmountR = 25;
                    
                    currectcounter = 15;
                }
                break;
            case 15:
               
                EnemyGeneraterR(PathNameList[1], TimeRange(0.2f, 0.9f));
                G3W2 -= Time.deltaTime;
                if (G3W2 < 0f)
                {
                    
                    EnemyAmountL = 29;
                    currectcounter = 16;
                }
                break;
            case 16:
                EnemyGeneraterL(PathNameList[0], TimeRange(0.2f, 0.9f));
                
                G3W3 -= Time.deltaTime;
                if (G3W3 < 0f)
                {
                    
                    EnemyAmountL = 28;
                    currectcounter = 17;
                }
                break;
            case 17:
                EnemyGeneraterL(PathNameList[2], TimeRange(0.1f, 0.6f));
                
                G3W4 -= Time.deltaTime;
                if (G3W4 < 0f)
                {
                    EnemyAmountR = 20;
                    EnemyAmountL = 20;
                    currectcounter = 18;
                }
                break;
            case 18:
                EnemyGeneraterL(PathNameList[3], TimeRange(0.3f, 0.9f));
                EnemyGeneraterR(PathNameList[3], TimeRange(0.3f, 0.9f));
                G3W5 -= Time.deltaTime;
                if (G3W5 < 0f)
                {
                    EnemyAmountR = 10;
                    EnemyAmountL = 12;
                    currectcounter = 19;
                }
                break;
            case 19:
                EnemyGeneraterL(PathNameListElite[0], TimeRange(4f, 5.5f));
                EnemyGeneraterR(PathNameListElite[1], TimeRange(3.5f, 6f));
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
                        SupportCannonUI.SetActive(true);
                        GameObject c1 = Cannon[0];
                        GameObject c2 = Cannon[1];
                        GameObject Buildsmoke1 = GameObject.Instantiate(BuildCannonEffect, Cannon[0].transform.position,Quaternion.identity);
                        GameObject Buildsmoke2 = GameObject.Instantiate(BuildCannonEffect, Cannon[1].transform.position, Quaternion.identity);
                        StartCoroutine(BuildCannon(c1,c2,Buildsmoke1, Buildsmoke2));
                        IsBonusGived = true;
                    }
                    CanStart = true;
                }
                break;
            case 21:
                EnemyGeneraterL(PathNameList[0], TimeRange(0.2f, 0.9f));
                EnemyGeneraterR(PathNameList[0], TimeRange(0.2f, 0.9f));
                G4W1 -= Time.deltaTime;
                if (G4W1 < 0f)
                {
                    EnemyAmountR = 25;
                    EnemyAmountL = 21;
                    currectcounter = 22;
                }
                break;
            case 22:
                EnemyGeneraterL(PathNameList[2], TimeRange(0.4f, 0.9f));
                EnemyGeneraterR(PathNameList[2], TimeRange(0.4f, 0.9f));
                G4W2 -= Time.deltaTime;
                if (G4W2 < 0f)
                {
                    EnemyAmountR = 13;
                    EnemyAmountL = 28;
                    currectcounter = 23;
                }
                break;
            case 23:
                EnemyGeneraterL(PathNameList[3], TimeRange(0.2f, 0.9f));
                EnemyGeneraterR(PathNameListElite[0], TimeRange(3f, 6f));
                G4W3 -= Time.deltaTime;
                if (G4W3 < 0f)
                {
                    EnemyAmountR = 30;
                    EnemyAmountL = 16;
                    currectcounter = 24;
                }
                break;
            case 24:
                EnemyGeneraterL(PathNameListElite[1], TimeRange(2.5f, 4f));
                EnemyGeneraterR(PathNameList[0], TimeRange(0.2f, 0.9f));
                G4W4 -= Time.deltaTime;
                if (G4W4 < 0f)
                {
                    EnemyAmountR = 16;
                    EnemyAmountL = 35;
                    currectcounter = 25;
                }
                break;
            case 25:
                EnemyGeneraterL(PathNameList[3], TimeRange(0.3f, 0.9f));
                EnemyGeneraterR(PathNameListElite[0], TimeRange(3f, 5f));
                G4W5 -= Time.deltaTime;
                if (G4W5 < 0f)
                {
                    EnemyAmountR = 18;
                    EnemyAmountL = 40;
                    currectcounter = 26;
                }
                break;
            case 26:
                EnemyGeneraterL(PathNameList[2], TimeRange(0.2f, 0.9f));
                EnemyGeneraterR(PathNameListElite[1], TimeRange(3.5f, 5.5f));
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
                        GameObject c1 = Cannon[2];
                        GameObject c2 = Cannon[3];
                        GameObject Buildsmoke1 = GameObject.Instantiate(BuildCannonEffect, Cannon[2].transform.position, Quaternion.identity);
                        GameObject Buildsmoke2 = GameObject.Instantiate(BuildCannonEffect, Cannon[3].transform.position, Quaternion.identity);
                        StartCoroutine(BuildCannon(c1, c2, Buildsmoke1, Buildsmoke2));
                        IsBonusGived = true;
                    }
                    CanStart = true;
                }
                break;
            case 28:
                EnemyGeneraterL(PathNameList[2], TimeRange(0.2f, 0.9f));
                EnemyGeneraterR(PathNameListElite[1], TimeRange(3.3f, 5.5f));
                G5W1 -= Time.deltaTime;
                if (G5W1 < 0f)
                {
                    EnemyAmountR = 30;
                    EnemyAmountL = 30;
                    currectcounter = 29;
                }
                break;
            case 29:
                EnemyGeneraterL(PathNameList[2], TimeRange(0.2f, 0.9f));
                EnemyGeneraterR(PathNameList[3], TimeRange(0.2f, 0.9f));
                G5W2 -= Time.deltaTime;
                if (G5W2 < 0f)
                {
                    EnemyAmountR = 15;
                    EnemyAmountL = 30;
                    currectcounter = 30;
                }
                break;
            case 30:
                EnemyGeneraterL(PathNameList[0], TimeRange(0.2f, 0.9f));
                EnemyGeneraterR(PathNameListElite[1], TimeRange(3.5f, 4.5f));
                G5W3 -= Time.deltaTime;
                if (G5W3 < 0f)
                {
                    EnemyAmountR = 35;
                    EnemyAmountL = 35;
                    currectcounter = 31;
                }
                break;
            case 31:
                EnemyGeneraterL(PathNameList[2], TimeRange(0.2f, 0.9f));
                EnemyGeneraterR(PathNameList[3], TimeRange(0.2f, 0.9f));
                G5W4 -= Time.deltaTime;
                if (G5W4 < 0f)
                {
                    EnemyAmountR = 22;
                    EnemyAmountL = 22;
                    currectcounter = 32;
                }
                break;
            case 32:
                EnemyGeneraterL(PathNameListElite[1], TimeRange(2.8f, 6.1f));
                EnemyGeneraterR(PathNameListElite[0],TimeRange(3.5f, 5.5f));
                G5W5 -= Time.deltaTime;
                if (G5W5 < 0f)
                {
                    EnemyAmountR = 50;
                    EnemyAmountL = 50;
                    currectcounter = 33;
                }
                break;
            case 33:
                EnemyGeneraterL(PathNameList[2], TimeRange(0.2f, 0.5f));
                EnemyGeneraterR(PathNameList[2], TimeRange(0.2f, 0.5f));
                G5W6 -= Time.deltaTime;
                if (G5W6 < 0f)
                {
                    EnemyAmountR = 30;
                    EnemyAmountL = 30;
                    currectcounter = 34;
                }
                break;
            case 34:
                EnemyGeneraterL(PathNameListElite[1], TimeRange(1f, 4.5f));
                EnemyGeneraterR(PathNameListElite[0], TimeRange(1f, 3f));
                G5W7 -= Time.deltaTime;
                if (G5W7 < 0f)
                {
                    currectcounter = 35;
                }
                break;
            case 35:
                if (CheckEnemyClear() == true)
                {
                    Main.m_Instance.GetPlayer().GetComponent<unitychanControl>().PlayWinPose();
                    StartCoroutine(WaitWin());

                }
                break;



            // 此為預設 當上面的case都沒達成時則會判斷
            default:
                Debug.Log("生完ㄌ");
                break;
        }
        if (Input.GetKeyDown(KeyCode.F10))
        {
            currectcounter = 20;
        }

    }
    IEnumerator WaitWin()
    {
        yield return new WaitForSeconds(4f);
        S2Clear = true;
    }
    IEnumerator lastWaveDispear()
    {
        yield return new WaitForSeconds(3f);
        lastUI.SetActive(false);
    }
    IEnumerator BuildCannon(GameObject c1, GameObject c2, GameObject Buildsmoke1, GameObject Buildsmoke2)
    {
        yield return new WaitForSeconds(5f);
        c1.SetActive(true);
        Destroy(Buildsmoke1);
        c2.SetActive(true);
        Destroy(Buildsmoke2);
        SupportCannonUI.SetActive(false);

    }
    public bool IsS2Clear
    {
        get { return S2Clear; }
    }
    public void EnemyGeneraterL(string mob, float time)
    {
        if (EnemyAmountL > 0)
        {
            nextMobSpawTimer -= Time.deltaTime;
            if (nextMobSpawTimer < 0f)
            {
                nextMobSpawTimer = time;

                CreateL(mob);
                EnemyAmountL--;

            }

        }
        else
        {
            EnemyAmountL = 0;
        }
        
    }
    public float TimeRange(float t1,float t2)
    {
        return UnityEngine.Random.Range(t1, t2);
    }
    public void EnemyGeneraterR(string mob,float time)
    {
        if (EnemyAmountR> 0)
        {
            nextMobSpawTimer2 -= Time.deltaTime;
            if (nextMobSpawTimer2 < 0f)
            {
                nextMobSpawTimer2 = time;
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
