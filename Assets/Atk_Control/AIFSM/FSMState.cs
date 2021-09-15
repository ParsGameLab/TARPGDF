using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum eFSMTransition
{
    NullTransition = 0,
    Go_Idle,
    Go_MoveTo,
    Go_Chase,
    Go_Attack,
    Go_Dead,
}


public enum eFSMStateID
{
    NullStateID = 0,
    IdleStateID,
    MoveToStateID,
    ChaseStateID,
    AttackStateID,
    DeadStateID,
}

public class FSMState {
    public eFSMStateID m_StateID;
    public Dictionary<eFSMTransition, FSMState> m_Map;
    public float m_fCurrentTime;

    public FSMState()
    {
        m_StateID = eFSMStateID.NullStateID;
        m_fCurrentTime = 0.0f;
        m_Map = new Dictionary<eFSMTransition, FSMState>();
    }

    public void AddTransition(eFSMTransition trans, FSMState toState)
    {
        if(m_Map.ContainsKey(trans))
        {
            return;
        }

        m_Map.Add(trans, toState);
    }
    public void DelTransition(eFSMTransition trans)
    {
        if (m_Map.ContainsKey(trans))
        {
            m_Map.Remove(trans);
        }

    }

    public FSMState TransitionTo(eFSMTransition trans)
    {
        if (m_Map.ContainsKey(trans) == false)
        {
            return null;
        }
        return m_Map[trans];
    }

    public virtual void DoBeforeEnter(AIData data)
    {

    }

    public virtual void DoBeforeLeave(AIData data)
    {

    }

    public virtual void Do(AIData data)
    {

    }

    public virtual void CheckCondition(AIData data)
    {
        
    }
}


public class FSMIdleState : FSMState
{

    private float m_fIdleTim;
  

    public FSMIdleState()
    {
        m_StateID = eFSMStateID.IdleStateID;
        m_fIdleTim = Random.Range(1.0f, 3.0f);
        
    }


    public override void DoBeforeEnter(AIData data)
    {
        m_fCurrentTime = 0.0f;
        m_fIdleTim = Random.Range(1.0f, 3.0f);
    }

    public override void DoBeforeLeave(AIData data)
    {

    }

    public override void Do(AIData data)
    {
        m_fCurrentTime += Time.deltaTime;
    }

    public override void CheckCondition(AIData data)
    {
        bool bAttack = false;
        GameObject go = AIFunction.CheckEnemyInSight(data, ref bAttack);
        if (go != null)
        {
            data.m_TargetObject = go;
            if (bAttack)
            {
                data.m_FSMSystem.PerformTransition(eFSMTransition.Go_Attack);
            }
            else
            {
                data.m_FSMSystem.PerformTransition(eFSMTransition.Go_Chase);
            }
            return;
        }
        if (m_fCurrentTime > m_fIdleTim)
        {
            

            data.m_FSMSystem.PerformTransition(eFSMTransition.Go_MoveTo);
        }
    }
}

public class FSMMoveToState : FSMState
{
    private int m_iCurrentWanderPt;
    private int LeavePoint=0;
    //private GameObject[] m_WanderPoints;
    private Vector3[] vPath;//取自己na上的路徑
    private NavigationComponent nv;
    private List<Transform> wpPath;
    //再放入自己現在正確的路徑點
    public FSMMoveToState()
    {
        m_StateID = eFSMStateID.MoveToStateID;
        //m_iCurrentWanderPt = -1;
        //m_WanderPoints = GameObject.FindGameObjectsWithTag("WanderPoint");

    }
    //public void SetPathPoint(Vector3[] vPath)
    //{
    //    this.vPath = vPath;

    //}
    public void SetWp(List<Transform> wpPath)
    {
        this.wpPath = wpPath;
    }
    public void SetNv(NavigationComponent nv)
    {
        this.nv = nv;
    }

    public override void DoBeforeEnter(AIData data)
    {

        //vPath = data.m_Go.GetComponent<NavigationComponent>().Path;

        //int iNewPt = Random.Range(0, m_WanderPoints.Length);
        //if (m_iCurrentWanderPt == iNewPt)
        //{
        //    return;
        //}
        //m_iCurrentWanderPt = iNewPt;
        //data.m_vTarget = m_WanderPoints[m_iCurrentWanderPt].transform.position;//重點在下一個seek點要由這裡A*算出來後再拿點出來
        //data.m_vTarget
        if (LeavePoint != 0)
        {
            m_iCurrentWanderPt = LeavePoint;
        }
        else
        {
            m_iCurrentWanderPt = 0;

        }
        
        data.m_bMove = true;
    }

    public override void DoBeforeLeave(AIData data)
    {
        data.m_Am.SetBool("WalkBool", false);
    }

    public override void Do(AIData data)
    {
        bool stop=false;
        //nv.MoveToPosition(nv.m_MainCore.transform.position);
        data.m_Am.SetBool("WalkBool",true);
        //m_iCurrentWanderPt = 0;
        if (SteeringBehavior.CollisionAvoid(data) == false)
        {

            //vPath = nv.Path;
            //int iFinal = vPath.Length - 1;
            //int i;
            //for (i = iFinal; i >= m_iCurrentWanderPt; i--)
            //{
            //    Vector3 sPos = vPath[i];
            //    Vector3 cPos = data.m_Go.transform.position;
            //    if (Physics.Linecast(cPos, sPos, 1 << LayerMask.NameToLayer("wall")))
            //    {
            //        continue;
            //    }
            //    m_iCurrentWanderPt = i;
            //    data.m_vTarget = sPos;

            //    break;
            //}

            //int iFinal = wpPath.Count - 1;
            //int i;
            //for (i = iFinal; i >= m_iCurrentWanderPt; i--)
            //{
            //    Vector3 sPos = wpPath[i].position;
            //    sPos.y = 0;
            //    Vector3 cPos = data.m_Go.transform.position;
            //    if (Physics.Linecast(cPos, sPos, 1 << LayerMask.NameToLayer("wall")))
            //    {
            //        continue;
            //    }
            //    m_iCurrentWanderPt = i;
            //    data.m_vTarget = sPos;

            //    break;
            //}




            Vector3 sPos = wpPath[m_iCurrentWanderPt].position;
            
            Vector3 cPos = data.m_Go.transform.position;
            
            float dis = Vector3.Distance(wpPath[m_iCurrentWanderPt].position , cPos);
            if (m_iCurrentWanderPt == wpPath.Count - 1)
            {
                stop = true;
                m_iCurrentWanderPt = wpPath.Count - 1;
                Debug.Log("m_iCurrentWanderPt" + m_iCurrentWanderPt);
            }
            if (dis<=1.0f&& !stop)
            {
                m_iCurrentWanderPt ++;
            }

            LeavePoint = m_iCurrentWanderPt;


            data.m_vTarget = sPos;

            SteeringBehavior.Seek(data);

        }

        SteeringBehavior.Move(data);

        //等等待CC進去給他操作
    }

    public override void CheckCondition(AIData data)
    {
        bool bAttack = false;
        GameObject go = AIFunction.CheckEnemyInSight(data, ref bAttack);//玩家在警戒範圍內
        if (go != null)
        {
            data.m_TargetObject = go;
            if (bAttack)
            {
                data.m_FSMSystem.PerformTransition(eFSMTransition.Go_Attack);
            }
            else
            {
                data.m_FSMSystem.PerformTransition(eFSMTransition.Go_Chase);
            }
            return;
        }
        //我需要把核心帶入放在AIDATA這裡就是去寫go這樣的方法去找核心if(核心有){打核心}
        //if (data.m_bMove == false)
        //{
        //    data.m_FSMSystem.PerformTransition(eFSMTransition.Go_Idle);
        //}
    }
}

public class FSMChaseState : FSMState
{
    public FSMChaseState()
    {
        m_StateID = eFSMStateID.ChaseStateID;
    }


    public override void DoBeforeEnter(AIData data)
    {

    }

    public override void DoBeforeLeave(AIData data)
    {
        data.m_Am.SetBool("RunBool", false);
    }

    public override void Do(AIData data)
    {
        data.m_Am.SetBool("RunBool", true);
        data.m_vTarget = data.m_TargetObject.transform.position;
        if (SteeringBehavior.CollisionAvoid(data) == false)
        {
            SteeringBehavior.Seek(data);
        }

        SteeringBehavior.Move(data);
    }

    public override void CheckCondition(AIData data)
    {
        bool bAttack = false;
        bool bCheck = AIFunction.CheckTargetEnemyInSight(data, data.m_TargetObject, ref bAttack);//插在敵人是check還是有人在旁邊
        if (bCheck == false)
        {
            data.m_FSMSystem.PerformTransition(eFSMTransition.Go_MoveTo);
            return;
        }
        if (bAttack)
        {
            data.m_FSMSystem.PerformTransition(eFSMTransition.Go_Attack);
        }
    }
}


public class FSMAttackState : FSMState
{
    private float fAttackTime = 0.0f;
    public FSMAttackState()
    {
        m_StateID = eFSMStateID.AttackStateID;
    }


    public override void DoBeforeEnter(AIData data)
    {
        fAttackTime = Random.Range(1.0f, 3.0f);
        m_fCurrentTime = 0.0f;
    }

    public override void DoBeforeLeave(AIData data)
    {
        data.m_Am.SetBool("BoolAtk", false);
    }


    public override void Do(AIData data)
    {
        // Check Animation complete.
        //...
        //Vector3 dirfowrd = data.m_vTarget - data.m_Go.transform.position;
        //data.m_vTarget = dirfowrd;
        
        if (m_fCurrentTime > fAttackTime)
        {
            m_fCurrentTime = 0.0f;
            // Do attack.
            data.m_Am.SetBool("BoolAtk", true);
        }
        m_fCurrentTime += Time.deltaTime;
    }

    public override void CheckCondition(AIData data)
    {
        bool bAttack = false;
        bool bCheck = AIFunction.CheckTargetEnemyInSight(data, data.m_TargetObject, ref bAttack);
        if (bCheck == false)
        {
            data.m_FSMSystem.PerformTransition(eFSMTransition.Go_MoveTo);
            return;
        }
        if (bAttack == false)
        {
            data.m_FSMSystem.PerformTransition(eFSMTransition.Go_Chase);
            return;
        }
    }
}



public class FSMDeadState : FSMState
{
    public FSMDeadState()
    {
        m_StateID = eFSMStateID.DeadStateID;
    }


    public override void DoBeforeEnter(AIData data)
    {

    }

    public override void DoBeforeLeave(AIData data)
    {

    }

    public override void Do(AIData data)
    {
        //校滅我自己
        Debug.Log("Do Dead State");
    }

    public override void CheckCondition(AIData data)
    {

    }
}