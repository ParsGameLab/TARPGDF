using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[System.Serializable]
public class AIData  {

    public enum eMobState
    {
        normal,
        slowdown,
        dizzy,
        smallslowdown,
        death,
    }
    public eMobState m_MobState;

    public float m_fRadius;
    public float m_fProbeLength;   
    public float m_Speed;
    public float m_fMaxSpeed;
    public float m_fRot;
    public float m_fMaxRot;
    public GameObject m_Go;
    public Transform eye;


    public float m_fSight;
    public float m_fAttackRange;

    [HideInInspector]
    public float m_fAttackTime;
    public float m_fHp;
    [HideInInspector]
    public GameObject m_TargetObject;

    [HideInInspector]
    public Vector3 m_vTarget;
    [HideInInspector]
    public Vector3 m_vCurrentVector;
    [HideInInspector]
    public float m_fTempTurnForce;
    [HideInInspector]
    public float m_fMoveForce;
    [HideInInspector]
    public bool m_bMove;
    [HideInInspector]
    public CharacterController m_Cc;
    [HideInInspector]
    public Animator m_Am;
    [HideInInspector]
    public NavigationComponent m_nv;
    [HideInInspector]
    public GameObject m_player;
    [HideInInspector]
    public GameObject m_core;
    
    

    [HideInInspector]
    public bool m_bCol;

    [HideInInspector]
    public FSMSystem m_FSMSystem;

    public BT.cBTSystem m_BTSystem;

    public eMobState State
    {
        get { return m_MobState; }
        set { m_MobState = value; }

    }
    public Vector3 GetTarget()
    {
        return m_vTarget;
    }
    public GameObject GetPlayer()
    {
        return m_player;
    }
}


public class AIFunction
{
    public static GameObject CheckEnemyInSight(AIData data, ref bool bAttack)
    {
        //bool Haswell;
        //Haswell = false;

        GameObject go = data.GetPlayer();
        Vector3 v = go.transform.position - data.m_Go.transform.position;

        
        //Ray DetectRay = new Ray(data.eye.position, go.transform.position);
        //Debug.DrawLine(data.m_Go.transform.position, go.transform.position,Color.red);
        //bool Haswell = Physics.Raycast(DetectRay,1000f,1<<LayerMask.NameToLayer("PlayerUnderAtk"));
        //Debug.LogWarning("Haswell" + Haswell);
        //if (Physics.Raycast(DetectRay, out hitInfo, data.m_fSight))
        //{
        //    if (hitInfo.collider == go)
        //    {
        //        Haswell = true;
        //    }
        //}

        float fDist = v.magnitude;
        if (fDist < data.m_fAttackRange /*&& Haswell == true*/)//攻擊範圍內
        {
            bAttack = true;
            return go;
        }
        else if (fDist < data.m_fSight /*&& Haswell == true*/)//追擊範圍內
        {
            bAttack = false;
            return go;
        }
        return null;
    }

    public static bool CheckTargetEnemyInSight(AIData data, GameObject target, ref bool bAttack)
    {
        //bool Haswell;
        //Haswell = false;
        GameObject go = target;
        Vector3 v = go.transform.position - data.m_Go.transform.position;

        //Ray DetectRay = new Ray(data.eye.position, go.transform.position);
        //bool Haswell = Physics.Raycast(DetectRay, 1000f, 1 << LayerMask.NameToLayer("PlayerUnderAtk"));
        //RaycastHit hitInfo;
        //Ray DetectRay = new Ray(data.m_Go.transform.position, data.m_Go.transform.position- go.transform.position);
        //if (Physics.Raycast(DetectRay, out hitInfo, data.m_fSight))
        //{
        //    if (hitInfo.collider == go)
        //    {
        //        Haswell = true;
        //    }
        //}



        float fDist = v.magnitude;
        if (fDist < data.m_fAttackRange /*&& Haswell == true)*/)
        {
            bAttack = true;
            return true;
        }
        else if (fDist < data.m_fSight/* && Haswell == true*/)
        {
            bAttack = false;
            return true;
        }
        return false;
    } 
    public static GameObject CheckCoreInSight(AIData data, ref bool bAttack)
    {
        //bool Haswell;
        //Haswell = false;

        GameObject go = data.m_core;
        Vector3 v = go.transform.position - data.m_Go.transform.position;

        //RaycastHit hitInfo;
        //Ray DetectRay = new Ray(data.m_Go.transform.position, v.normalized * data.m_fSight);
        //if (Physics.Raycast(DetectRay, out hitInfo, data.m_fSight))
        //{
        //    if (hitInfo.transform == go.transform)
        //    {
        //        Haswell = true;
        //    }
        //}

        float fDist = v.magnitude;
        if (fDist < data.m_fSight)//攻擊範圍內
        {
            bAttack = true;
            return go;
        }
        return null;
    }

    public static string GetCurrentPlayingAnimationClip(GameObject go)

    {
        Animation amigo = go.GetComponent<Animation>();
        if (go == null)
        {
            return string.Empty;
        }
        foreach (AnimationState anim in amigo)
        {
            if (amigo.IsPlaying(anim.name))
            {
                return anim.name;
            }
        }
        return string.Empty;
    }
}