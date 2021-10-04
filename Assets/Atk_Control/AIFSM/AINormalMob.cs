using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AINormalMob : MonoBehaviour
{
    public enum eMobRL
    {
        oneway,
        R,
        L,
    }
    public eMobRL m_eMobRL;

    public AIData m_Data;
    FSMSystem m_FSM;
    private float slowtime = 2f;
    
    Collider m_Collider;
    private float ftimer;
    private float smallftimer;
    private Animation anim;
    public GameObject slowFX;
    public GameObject debuff;
    public int coinAmount;
    bool CountAlready;
    private float normaxspeed;
    private bool inslowdown;


    //public static AINormalMob Create(Vector3 position)
    //{
    //    Transform pfEnemy = Resources.Load<Transform>("Mobs/pfWolf");
    //    Transform enemyTransform = Instantiate(pfEnemy, position, Quaternion.identity);

    //    AINormalMob NormalMob = enemyTransform.GetComponent<AINormalMob>();
    //    return NormalMob;

    //}
    //private CharacterController m_AICc;
    // Use this for initialization
    void Start()
    {
        CountAlready = false;
        m_FSM = new FSMSystem(m_Data);
        m_Data.m_Go = this.gameObject;
        m_Data.m_FSMSystem = m_FSM;
        m_Data.m_Am= GetComponentInChildren<Animator>();
        m_Data.m_Cc = GetComponent<CharacterController>();
        m_Data.m_player = GameObject.FindWithTag("Player");
        m_Data.m_core = GameObject.FindWithTag("Core");
        m_Collider = this.gameObject.GetComponent<Collider>();
        anim = m_Data.m_Am.GetComponentInChildren<Animation>();
        //m_Data.m_nv = GetComponent<NavigationComponent>();/*GetComponent(typeof(NavigationComponent)) as NavigationComponent;*/
        m_Data.State = AIData.eMobState.normal;


        FSMMoveToState mtstate = new FSMMoveToState();
        if (m_eMobRL == eMobRL.oneway)//EnemySponManerger.Instance.enemylist ==null&& EnemySponManerger.Instance.enemylist.Contains(this.transform)
        {
            mtstate.SetWp(GameObject.Find("WayPoint").GetComponent<WayPointMg>().WayPointPath);
            
        }//其餘是否在RL陣列李，有就用左右路
        else if (m_eMobRL == eMobRL.L)//EnemySpawnManagerS2.Instance.enemylistL.Contains(this.transform)
        {
            mtstate.SetWp(GameObject.Find("WayPointL").GetComponent<WayPointMg>().WayPointPath);
            
        }
        else if (m_eMobRL == eMobRL.R)//EnemySpawnManagerS2.Instance.enemylistR.Contains(this.transform)
        {
            mtstate.SetWp(GameObject.Find("WayPointR").GetComponent<WayPointMg>().WayPointPath);
           
        }
        //mtstate.SetWp(GameObject.Find("WayPoint").GetComponent<WayPointMg>().WayPointPath);
        mtstate.SetNv(m_Data.m_Go.GetComponent<NavigationComponent>());
        //mtstate.SetSelfBoid(m_Data.m_Go.GetComponent<Boid>());
        //mtstate.SetPathPoint(m_Data.m_Go.GetComponent<NavigationComponent>().Path);
        FSMChaseState chasestate = new FSMChaseState();
        FSMAttackState attackstate = new FSMAttackState();
        FSMAttackCoreState attackecoerestate = new FSMAttackCoreState();
        FSMGetHitState hitstate = new FSMGetHitState();
        attackecoerestate.SetCore(m_Data.m_core);


        //mtstate.AddTransition(eFSMTransition.Go_Idle, idlestate);
        mtstate.AddTransition(eFSMTransition.Go_Chase, chasestate);
        mtstate.AddTransition(eFSMTransition.Go_Attack, attackstate);
        mtstate.AddTransition(eFSMTransition.Go_AttackCore, attackecoerestate);
        mtstate.AddTransition(eFSMTransition.Go_GetHit, hitstate);
        //chasestate.AddTransition(eFSMTransition.Go_Idle, idlestate);
        chasestate.AddTransition(eFSMTransition.Go_MoveTo, mtstate);
        chasestate.AddTransition(eFSMTransition.Go_Attack, attackstate);
        chasestate.AddTransition(eFSMTransition.Go_GetHit, hitstate);

        attackstate.AddTransition(eFSMTransition.Go_MoveTo, mtstate);
        attackstate.AddTransition(eFSMTransition.Go_Chase, chasestate);
        attackstate.AddTransition(eFSMTransition.Go_Attack, attackstate);

        hitstate.AddTransition(eFSMTransition.Go_MoveTo, mtstate);
        hitstate.AddTransition(eFSMTransition.Go_Chase, chasestate);
        hitstate.AddTransition(eFSMTransition.Go_Attack, attackstate);
        hitstate.AddTransition(eFSMTransition.Go_AttackCore, attackecoerestate);

        attackecoerestate.AddTransition(eFSMTransition.Go_GetHit, hitstate);

        FSMDeadState dstate = new FSMDeadState();
        m_FSM.AddGlobalTransition(eFSMTransition.Go_Dead, dstate);
        
        Debug.Log("add state");
        m_FSM.AddState(mtstate);
        m_FSM.AddState(chasestate);
        m_FSM.AddState(attackstate);
        m_FSM.AddState(attackecoerestate);
        m_FSM.AddState(dstate);
        m_FSM.AddState(hitstate);

        normaxspeed = m_Data.m_fMaxSpeed;
        inslowdown = false;
    }

    // Update is called once per frame
    void Update()
    {

        m_FSM.DoState();
        //if (m_Data.m_Am.GetCurrentAnimatorStateInfo(0).IsName("GetHit"))
        //{
        //    m_FSM.PerformGlobalTransition(eFSMTransition.Go_GetHit);
        //}
        if(m_Data.State == AIData.eMobState.dizzy)
        {
            m_Data.m_Am.SetTrigger("Dizzy");
            
        }

        

        if (m_Data.State == AIData.eMobState.slowdown)
        {
            inslowdown = true;
            slowFX.SetActive(true);
            m_Data.m_Am.SetFloat("Speed", 0.5f);
            float slowspeed = normaxspeed * 0.5f;
            if (slowspeed <= 0.05) { slowspeed = 0.05f; }
            
            m_Data.m_fMaxSpeed = slowspeed;

            if (ftimer > slowtime)
            {
                inslowdown = false;
                m_Data.m_fMaxSpeed = normaxspeed;
                ftimer = 0f;
                m_Data.m_Am.SetFloat("Speed", 1f);
                slowFX.SetActive(false);
                m_Data.State = AIData.eMobState.normal;
                

            }
            ftimer += Time.deltaTime;
        }else if (m_Data.State == AIData.eMobState.smallslowdown&&!inslowdown)
        {
            debuff.SetActive(true);
            float smallslowspeed = normaxspeed * 0.3f;
            if (smallslowspeed <= 0.05) { smallslowspeed = 0.05f; }
            m_Data.m_fMaxSpeed = smallslowspeed;
            m_Data.m_Am.SetFloat("Speed", 0.3f);
        }else
        {
            debuff.SetActive(false);
            m_Data.m_fMaxSpeed = normaxspeed;
            m_Data.m_Am.SetFloat("Speed", 1f);
            slowFX.SetActive(false);
            m_Data.State = AIData.eMobState.normal;

        }

        if (m_Data.m_Am.GetCurrentAnimatorStateInfo(0).IsName("Dizzy"))
        {
            m_Data.m_fMaxSpeed = 0;
        }
        

        if (m_Data.m_Am.GetCurrentAnimatorStateInfo(0).IsName("Die"))
        {
            
            m_FSM.PerformGlobalTransition(eFSMTransition.Go_Dead);

            //this.gameObject.GetComponent<CharacterController>().enabled = false;
            if (CountAlready==false)
            {
                Player.Instance.AddCoinAmount(coinAmount);
                UiMainforCoin.Instance().SpawnFloatingText(transform, coinAmount.ToString());
                CountAlready = true;

            }

            if(m_eMobRL == eMobRL.oneway)
            {
                EnemySponManerger.Instance.enemylist.Remove(this.transform);
            }else if(m_eMobRL == eMobRL.L)
            {
                EnemySpawnManagerS2.Instance.enemylistL.Remove(this.transform);
                EnemySpawnManagerS2.Instance.allenemylist.Remove(this.transform);
            }
            else if(m_eMobRL == eMobRL.R)
            {
                EnemySpawnManagerS2.Instance.enemylistR.Remove(this.transform);
                EnemySpawnManagerS2.Instance.allenemylist.Remove(this.transform);
            }
            
            m_Collider.enabled = false;
            this.gameObject.GetComponentInChildren<DissolveSphere>().enabled = true;
            Destroy(this.gameObject, 3f);

        }

    }
    public void SetAnimationSpeed(Animation ani,string name,float speed)
    {
        if (null == ani) return;
        AnimationState state = ani[name];
        if (!state) state.speed = speed;
    }

    public Vector3 GetTagetPosition()
    {
        return m_Data.GetTarget();
    }
    

    private void OnDrawGizmos()
    {
        if (m_Data == null || m_FSM == null)
        {
            return;
        }
        Gizmos.color = Color.cyan;
        Gizmos.DrawLine(this.transform.position, this.transform.position + this.transform.forward * 2.0f);
        if (m_FSM.CurrentStateID == eFSMStateID.IdleStateID)
        {
            Gizmos.color = Color.green;
        }
        else if (m_FSM.CurrentStateID == eFSMStateID.MoveToStateID)
        {
            Gizmos.color = Color.blue;
            Gizmos.DrawLine(this.transform.position, m_Data.m_vTarget);
        }
        else if (m_FSM.CurrentStateID == eFSMStateID.ChaseStateID)
        {
            Gizmos.color = Color.yellow;
            Gizmos.DrawLine(this.transform.position, m_Data.m_vTarget);
        }
        else if (m_FSM.CurrentStateID == eFSMStateID.AttackStateID)
        {
            Gizmos.color = Color.red;
        }
        else if (m_FSM.CurrentStateID == eFSMStateID.DeadStateID)
        {
            Gizmos.color = Color.gray;
        }
        Gizmos.DrawWireSphere(this.transform.position, m_Data.m_fSight);

        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(this.transform.position, m_Data.m_fAttackRange);
    }

    
}
