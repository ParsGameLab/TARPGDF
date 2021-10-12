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

    public GameObject LatkUI;
    public GameObject RatkUI;




    public bool S2Clear;

    private float G1W1 = 9f;
    private float G1W2 = 9f;
    private float G1W3 = 10f;
    private float G1W4 = 10f;
    private float G1W5 = 12f;
    private float G1W6 = 15f;

    private float G2W1 = 10f;
    private float G2W2 = 10f;
    private float G2W3 = 10f;
    private float G2W4 = 12f;
    private float G2W5 = 9f;
    private float G2W6 = 15f;

    private float G3W1 = 11f;
    private float G3W2 = 14f;
    private float G3W3 = 11f;
    private float G3W4 = 14f;
    private float G3W5 = 10f;
    private float G3W6 = 14f; 
    private float G3W7 = 14f;


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
            if (Gnumber == 0)
            {
                LatkUI.SetActive(true);

            }
            if (Gnumber == 1)
            {
                RatkUI.SetActive(true);
            }
            if (Gnumber == 2)
            {
                LatkUI.SetActive(true);
                RatkUI.SetActive(true);
            }
            Gbutton.SetActive(true);
            if (Input.GetKeyDown(KeyCode.G))
            {
                if (Gnumber == 0)
                {
                    BattleBGM.Instance.PlaySound();
                    
                }
                StartCoroutine(MusicManager.Instance.FadeMusic(1.5f, 0));
                StartCoroutine(BattleBGM.Instance.FadeMusic(2f, 0.15f));
                BonusEffect.SetActive(false);
                if (Gnumber == 0)
                {
                    EnemyAmountL = 15;
                    
                }
                if (Gnumber == 1)
                {
                    EnemyAmountR = 17;
                }
                if (Gnumber == 2)
                {
                    EnemyAmountL = 20;
                    EnemyAmountR = 20;
                }
                
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
                OnWaveNumberChangedS2?.Invoke(this, EventArgs.Empty);
            }

        }
        else
        {
            LatkUI.SetActive(false);
            RatkUI.SetActive(false);
        }

        switch (currectcounter)
        {
            case 1:
                EnemyGeneraterL(PathNameList[0], UnityEngine.Random.Range(0.2f, 0.7f));
                G1W1 -= Time.deltaTime;
                if (G1W1 < 0f)
                {
                    EnemyAmountL = 10;
                    currectcounter = 2;
                }

                break;
            case 2:
                EnemyGeneraterL(PathNameList[3], UnityEngine.Random.Range(0.2f, 0.7f));
                G1W2 -= Time.deltaTime;
                if (G1W2 < 0f)
                {
                    EnemyAmountL = 3;
                    currectcounter = 3;
                }
                break;
            case 3:
                EnemyGeneraterL(PathNameListElite[1], UnityEngine.Random.Range(1.5f, 3.5f));
                G1W3 -= Time.deltaTime;
                if (G1W3 < 0f)
                {
                    EnemyAmountL = 17;
                    currectcounter = 4;
                }
                break;
            case 4:
                EnemyGeneraterL(PathNameList[2], UnityEngine.Random.Range(0.3f, 0.7f));
                G1W4 -= Time.deltaTime;
                if (G1W4 < 0f)
                {
                    EnemyAmountL = 15;
                    currectcounter = 5;
                }
                break;
            case 5:
                EnemyGeneraterL(PathNameList[1], UnityEngine.Random.Range(0.3f, 0.7f));
                G1W5 -= Time.deltaTime;
                if (G1W5 < 0f)
                {
                    EnemyAmountL = 5;
                    currectcounter = 6;
                }
                break;
            case 6:
                EnemyGeneraterL(PathNameListElite[1], UnityEngine.Random.Range(2.5f, 3.5f));
                G1W6 -= Time.deltaTime;
                if (G1W6 < 0f)
                {
                    currectcounter = 7;
                }
                break;
            case 7:
                if (CheckEnemyClear() == true)
                {
                    if (IsBonusGived == false)
                    {
                        StartCoroutine(BattleBGM.Instance.FadeMusic(1f, 0));
                        BonusEffect.SetActive(true);
                        StartCoroutine(MusicManager.Instance.FadeMusic(2f, 0.35f));
                        SoundManager.Instance.PlaySound(SoundManager.Sound.Horn);
                        Player.Instance.AddCoinAmount(Wave3Bonus);
                        SupportCannonUI.SetActive(true);
                        GameObject c1 = Cannon[0];
                        GameObject c2 = Cannon[1];
                        GameObject Buildsmoke1 = GameObject.Instantiate(BuildCannonEffect, Cannon[0].transform.position, Quaternion.identity);
                        GameObject Buildsmoke2 = GameObject.Instantiate(BuildCannonEffect, Cannon[1].transform.position, Quaternion.identity);
                        StartCoroutine(BuildCannon(c1, c2, Buildsmoke1, Buildsmoke2));
                        IsBonusGived = true;
                    }
                    CanStart = true;
                }
                break;
            case 8:
                EnemyGeneraterR(PathNameList[2], UnityEngine.Random.Range(0.2f, 0.7f));
                G2W1 -= Time.deltaTime;
                if (G2W1 < 0f)
                {
                    EnemyAmountR = 20;
                    currectcounter = 9;
                }
                break;
            case 9:
                EnemyGeneraterR(PathNameList[2], UnityEngine.Random.Range(0.2f, 0.7f));
                G2W2 -= Time.deltaTime;
                if (G2W2 < 0f)
                {
                    EnemyAmountR = 3;
                    currectcounter = 10;
                }
                break;
            case 10:
                EnemyGeneraterR(PathNameListElite[0], UnityEngine.Random.Range(2f, 3.5f));
                G2W3 -= Time.deltaTime;
                if (G2W3 < 0f)
                {
                    EnemyAmountR = 18;
                    currectcounter = 11;
                }
                break;
            case 11:
                EnemyGeneraterR(PathNameList[0], UnityEngine.Random.Range(0.2f, 0.7f));
                G2W4 -= Time.deltaTime;
                if (G2W4 < 0f)
                {
                    EnemyAmountR = 19;
                    currectcounter = 12;
                }
                break;
            case 12:
                EnemyGeneraterR(PathNameList[3], UnityEngine.Random.Range(0.2f, 0.7f));
                G2W5 -= Time.deltaTime;
                if (G2W5 < 0f)
                {
                    EnemyAmountR = 7;
                    currectcounter = 13;
                }
                break;
            case 13:
                EnemyGeneraterR(PathNameListElite[1], UnityEngine.Random.Range(2.5f, 3.5f));
                G2W6 -= Time.deltaTime;
                if (G2W6 < 0f)
                {

                    currectcounter = 14;
                }
                break;
            case 14:
                if (CheckEnemyClear() == true)
                {
                    if (IsBonusGived == false)
                    {
                        StartCoroutine(BattleBGM.Instance.FadeMusic(1f, 0));
                        BonusEffect.SetActive(true);
                        StartCoroutine(MusicManager.Instance.FadeMusic(2f, 0.35f));
                        SoundManager.Instance.PlaySound(SoundManager.Sound.Horn);
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
            case 15:

                EnemyGeneraterR(PathNameList[0], UnityEngine.Random.Range(0.3f, 0.9f));
                G3W1 -= Time.deltaTime;
                if (G3W1 < 0f)
                {
                    EnemyAmountR = 25;
                    currectcounter = 16;
                }
                break;
            case 16:

                EnemyGeneraterR(PathNameList[3],UnityEngine.Random.Range(0.3f, 0.9f));
                G3W2 -= Time.deltaTime;
                if (G3W2 < 0f)
                {

                    EnemyAmountL = 26;
                    currectcounter = 17;
                }
                break;
            case 17:
                EnemyGeneraterL(PathNameList[1],UnityEngine.Random.Range(0.2f, 0.7f));

                G3W3 -= Time.deltaTime;
                if (G3W3 < 0f)
                {

                    EnemyAmountL = 25;
                    currectcounter = 18;
                }
                break;
            case 18:
                EnemyGeneraterL(PathNameList[2],UnityEngine.Random.Range(0.2f, 0.7f));

                G3W4 -= Time.deltaTime;
                if (G3W4 < 0f)
                {
                    EnemyAmountR = 5;
                    EnemyAmountL = 25;
                    currectcounter = 19;
                }
                break;
            case 19:
                EnemyGeneraterL(PathNameList[2], UnityEngine.Random.Range(0.2f, 0.7f));
                EnemyGeneraterR(PathNameListElite[1], UnityEngine.Random.Range(2f, 3.5f));
                G3W5 -= Time.deltaTime;
                if (G3W5 < 0f)
                {
                    EnemyAmountR = 25;
                    EnemyAmountL = 5;
                    currectcounter = 20;
                }
                break;
            case 20:
                EnemyGeneraterL(PathNameListElite[0], UnityEngine.Random.Range(2f, 3.5f));
                EnemyGeneraterR(PathNameList[2], UnityEngine.Random.Range(0.2f, 0.7f));
                G3W6 -= Time.deltaTime;
                if (G3W6 < 0f)
                {
                    EnemyAmountR = 7;
                    EnemyAmountL = 7;
                    currectcounter = 21;
                }
                break;
            case 21:
                EnemyGeneraterL(PathNameListElite[1], UnityEngine.Random.Range(1f, 3.5f));
                EnemyGeneraterR(PathNameListElite[0], UnityEngine.Random.Range(1f, 3.5f));
                G3W7 -= Time.deltaTime;
                if (G3W7 < 0f)
                {
                    currectcounter = 22;
                }
                break;
            case 22:
                if (CheckEnemyClear() == true)
                {
                    Main.m_Instance.GetPlayer().GetComponent<unitychanControl>().PlayWinPose();
                    StartCoroutine(WaitWin());
                    if (IsBonusGived == false)
                    {
                        StartCoroutine(BattleBGM.Instance.FadeMusic(1f, 0));
                        IsBonusGived = true;
                    }


                }
                break;
            
            default:
                Debug.Log("生完ㄌ");
                break;
        }
        if (Input.GetKeyDown(KeyCode.F10))
        {
            currectcounter = 22;
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
    //public void EnemyGeneraterL(string mob)
    //{
    //    if (EnemyAmountL > 0)
    //    {
    //        nextMobSpawTimer -= Time.deltaTime;
    //        if (nextMobSpawTimer < 0f)
    //        {
    //            nextMobSpawTimer = UnityEngine.Random.Range(0.2f, 0.9f);

    //            CreateL(mob);
    //            EnemyAmountL--;

    //        }

    //    }
    //    else
    //    {
    //        EnemyAmountL = 0;
    //    }

    //}
    //public void EnemyGeneraterR(string mob)
    //{
    //    if (EnemyAmountR > 0)
    //    {
    //        nextMobSpawTimer2 -= Time.deltaTime;
    //        if (nextMobSpawTimer2 < 0f)
    //        {
    //            nextMobSpawTimer2 = UnityEngine.Random.Range(0.1f, 0.7f);
    //            CreateR(mob);
    //            EnemyAmountR--;

    //        }

    //    }
    //    else
    //    {
    //        EnemyAmountR = 0;
    //    }

    //}
    public void EnemyGeneraterL(string mob, float timer)
    {
        if (EnemyAmountL > 0)
        {
            nextMobSpawTimer -= Time.deltaTime;
            if (nextMobSpawTimer < 0f)
            {
                nextMobSpawTimer = timer; /*UnityEngine.Random.Range(0.2f, 0.9f);*/

                CreateL(mob);
                EnemyAmountL--;

            }

        }
        else
        {
            EnemyAmountL = 0;
        }

    }
    public void EnemyGeneraterR(string mob, float timer)
    {
        if (EnemyAmountR > 0)
        {
            nextMobSpawTimer2 -= Time.deltaTime;
            if (nextMobSpawTimer2 < 0f)
            {
                nextMobSpawTimer2 = timer;
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
        if (enemylistR.Count == 0 && enemylistL.Count == 0)
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
