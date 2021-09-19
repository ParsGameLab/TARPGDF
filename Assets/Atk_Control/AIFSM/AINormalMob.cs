using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AINormalMob : MonoBehaviour
{
    public AIData m_Data;
    FSMSystem m_FSM;
    float deadtine = 0;
    
    //private CharacterController m_AICc;
    // Use this for initialization
    void Start()
    {
        m_FSM = new FSMSystem(m_Data);
        m_Data.m_Go = this.gameObject;
        m_Data.m_FSMSystem = m_FSM;
        m_Data.m_Am= GetComponentInChildren<Animator>();
        m_Data.m_Cc = GetComponent<CharacterController>();
        m_Data.m_player = GameObject.FindWithTag("Player");
        //m_Data.m_nv = GetComponent<NavigationComponent>();/*GetComponent(typeof(NavigationComponent)) as NavigationComponent;*/



        FSMMoveToState mtstate = new FSMMoveToState();
        mtstate.SetWp(GameObject.Find("WayPoint").GetComponent<WayPointMg>().WayPointPath);
        mtstate.SetNv(m_Data.m_Go.GetComponent<NavigationComponent>());
        //mtstate.SetPathPoint(m_Data.m_Go.GetComponent<NavigationComponent>().Path);
        FSMChaseState chasestate = new FSMChaseState();
        FSMAttackState attackstate = new FSMAttackState();
        

        //mtstate.AddTransition(eFSMTransition.Go_Idle, idlestate);
        mtstate.AddTransition(eFSMTransition.Go_Chase, chasestate);
        mtstate.AddTransition(eFSMTransition.Go_Attack, attackstate);

        //chasestate.AddTransition(eFSMTransition.Go_Idle, idlestate);
        chasestate.AddTransition(eFSMTransition.Go_MoveTo, mtstate);
        chasestate.AddTransition(eFSMTransition.Go_Attack, attackstate);

        attackstate.AddTransition(eFSMTransition.Go_MoveTo, mtstate);
        attackstate.AddTransition(eFSMTransition.Go_Chase, chasestate);

        FSMDeadState dstate = new FSMDeadState();
        m_FSM.AddGlobalTransition(eFSMTransition.Go_Dead, dstate);
        Debug.Log("add state");
        m_FSM.AddState(mtstate);
        m_FSM.AddState(chasestate);
        m_FSM.AddState(attackstate);
        m_FSM.AddState(dstate);

    }

    // Update is called once per frame
    void Update()
    {

        m_FSM.DoState();
        

        if (m_Data.m_Am.GetCurrentAnimatorStateInfo(0).IsName("Die"))
        {
            
            m_FSM.PerformGlobalTransition(eFSMTransition.Go_Dead);
        }

    }
    public void OnDead()
    {
        Destroy(this.gameObject);
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
