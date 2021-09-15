using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SteeringBehavior
{
    static public void aaa(float a)
    {
        a = 1.0f;
    }
    static public void Move(AIData data)
    {
        if (data.m_bMove == false)
        {
            return;
        }
        Transform t = data.m_Go.transform;//因為我物體本身的rf都拿去算seekㄌ//所以我用目前物體的總合的RF去移動

        Vector3 cPos = data.m_Go.transform.position;
        Vector3 vR = t.right;
        Vector3 vOriF = t.forward;
        Vector3 vF = data.m_vCurrentVector;//前進的方向

        if(data.m_fTempTurnForce > data.m_fMaxRot)
        {
            data.m_fTempTurnForce = data.m_fMaxRot;
        } else if(data.m_fTempTurnForce < -data.m_fMaxRot)
        {
            data.m_fTempTurnForce = -data.m_fMaxRot;
        }
        
        vF = vF + vR * data.m_fTempTurnForce;//用外力來使他轉向//為神摸要用道r，因為我門的加速度需要方向
        vF.Normalize();//沒均化她不會漸變
        t.forward = vF;

        

        data.m_Speed = data.m_Speed + data.m_fMoveForce * Time.deltaTime;
        //Debug.Log("speed" + data.m_Speed);
        if (data.m_Speed < 0.01f)
        {
            data.m_Speed = 0.01f;
        } else if(data.m_Speed > data.m_fMaxSpeed)
        {
            data.m_Speed = data.m_fMaxSpeed;
        }

        if (data.m_bCol == false)//如果剛剛避開ㄌ，但怕他//施力遠離障礙後會去seek//只有seek的時候會跑這裡
        {
            Debug.Log("CheckCollision");
            if (SteeringBehavior.CheckCollision(data))//會重複seek跟avoid
            {
                Debug.Log("CheckCollision true");
                t.forward = vOriF;//如果在seek時有障礙先保持前進方向，下一次在avoid
            }
            else
            {
                Debug.Log("CheckCollision true");
            }
        }
        else//在迴避ㄌ
        {
            if (data.m_Speed < 0.02f)
            {
                if (data.m_fTempTurnForce > 0)//減速轉彎中
                {
                    t.forward = vR;
                }
                else
                {
                    t.forward = -vR;
                }

            }
        }

        //cPos = cPos + t.forward * data.m_Speed;
        //t.position = cPos;
        data.m_Cc.SimpleMove(t.forward * data.m_Speed*20);
    }

    static public bool CheckCollision(AIData data)//看有沒有障礙//看障礙
    {
        List<Obstacle> m_AvoidTargets = Main.m_Instance.GetObstacles();
        if (m_AvoidTargets == null)
        {
            return false;
        }
        Transform ct = data.m_Go.transform;
        Vector3 cPos = ct.position;
        Vector3 cForward = ct.forward;
        Vector3 vec;

        float fDist = 0.0f;
        float fDot = 0.0f;
        int iCount = m_AvoidTargets.Count;
        for (int i = 0; i < iCount; i++)
        {
            vec = m_AvoidTargets[i].transform.position - cPos;
            vec.y = 0.0f;
            fDist = vec.magnitude;
            if (fDist > data.m_fProbeLength + m_AvoidTargets[i].m_fRadius)
            {
                m_AvoidTargets[i].m_eState = Obstacle.eState.OUTSIDE_TEST;
                continue;
            }

            vec.Normalize();
            fDot = Vector3.Dot(vec, cForward);
            if (fDot < 0)
            {
                m_AvoidTargets[i].m_eState = Obstacle.eState.OUTSIDE_TEST;
                continue;
            }
            m_AvoidTargets[i].m_eState = Obstacle.eState.INSIDE_TEST;
            float fProjDist = fDist * fDot;
            float fDotDist = Mathf.Sqrt(fDist * fDist - fProjDist * fProjDist);
            if (fDotDist > m_AvoidTargets[i].m_fRadius + data.m_fRadius)
            {
                continue;//就只是黃色
            }

            return true;//有紅色迴避


        }
        return false;
    }


    static public bool CollisionAvoid(AIData data)//緊急迴避//迴避動作
    {
        List<Obstacle> m_AvoidTargets = Main.m_Instance.GetObstacles();
        Transform ct = data.m_Go.transform;
        Vector3 cPos = ct.position;
        Vector3 cForward = ct.forward;
        data.m_vCurrentVector = cForward;
        Vector3 vec;
        float fFinalDotDist;
        float fFinalProjDist;
        Vector3 vFinalVec = Vector3.forward;
        Obstacle oFinal = null;
        float fDist = 0.0f;
        float fDot = 0.0f;
        float fFinalDot = 0.0f;
        int iCount = m_AvoidTargets.Count;

        float fMinDist = 10000.0f;
        for (int i = 0; i < iCount; i++)
        {
            vec = m_AvoidTargets[i].transform.position - cPos;
            vec.y = 0.0f;
            fDist = vec.magnitude;
            if (fDist > data.m_fProbeLength + m_AvoidTargets[i].m_fRadius)
            {
                m_AvoidTargets[i].m_eState = Obstacle.eState.OUTSIDE_TEST;
                continue;
            }

            vec.Normalize();
            fDot = Vector3.Dot(vec, cForward);
            if (fDot < 0)
            {
                m_AvoidTargets[i].m_eState = Obstacle.eState.OUTSIDE_TEST;//背後
                continue;
            }
            else if (fDot > 1.0f)//最大設定
            {
                fDot = 1.0f;//跟我行進路線一樣
            }
            m_AvoidTargets[i].m_eState = Obstacle.eState.INSIDE_TEST;
            float fProjDist = fDist * fDot;
            float fDotDist = Mathf.Sqrt(fDist * fDist - fProjDist * fProjDist);
            if (fDotDist > m_AvoidTargets[i].m_fRadius + data.m_fRadius)
            {
                continue;//把黃色的在這裡結束
            }//之後的都是紅色

            if (fDist < fMinDist)//早最近的當目標
            {
                fMinDist = fDist;
                fFinalDotDist = fDotDist;
                fFinalProjDist = fProjDist;
                vFinalVec = vec;
                oFinal = m_AvoidTargets[i];//要避免碰撞的物體
                fFinalDot = fDot;
            }

        }

        if (oFinal != null)//最近且探針有東西
        {
            Vector3 vCross = Vector3.Cross(cForward, vFinalVec);
            float fTurnMag = Mathf.Sqrt(1.0f - fFinalDot * fFinalDot);//知道ㄌ這個角的cos值，求sin//求對邊
            if (vCross.y > 0.0f)//東西在右邊，我給的sin力要往反方向失利
            {
                fTurnMag = -fTurnMag;
            }
            data.m_fTempTurnForce = fTurnMag;//right*正會右轉，所以要乘負的

            float fTotalLen = data.m_fProbeLength + oFinal.m_fRadius;//警戒範圍
            float fRatio = fMinDist / fTotalLen;//跟目標物的長度比，越小越近，越大越遠//越大越接近一，越小越近0
            if (fRatio > 1.0f)
            {
                fRatio = 1.0f;//防呆
            }
            fRatio = 1.0f - fRatio;//越近力量越大
            data.m_fMoveForce = -fRatio;//減速力越大
            oFinal.m_eState = Obstacle.eState.COL_TEST;
            data.m_bCol = true;
            data.m_bMove = true;
            return true;
        }
        data.m_bCol = false;
        return false;
    }

    static public bool Flee(AIData data)
    {
        Vector3 cPos = data.m_Go.transform.position;
        Vector3 vec = data.m_vTarget - cPos;
        vec.y = 0.0f;
        float fDist = vec.magnitude;
        data.m_fTempTurnForce = 0.0f;
        if (data.m_fProbeLength < fDist)//當距離大於警戒範圍的時候，他就都不會旋轉，因為他會直接挑離迴圈不會進到下面
            //當他的警戒範圍大於距離的時候，就會開始轉彎，轉彎匯進下面迴圈，因為他不成立
        {
            if(data.m_Speed > 0.01f)
            {
                //當他又開始逃離的時候，又進來ㄌ
                
                data.m_fMoveForce = -1.0f;//開始把加出去的速度慢慢降下
                Debug.LogError(data.m_fMoveForce);
            } 
            
            data.m_bMove = true;
            return false;
        }

        Vector3 vf = data.m_Go.transform.forward;
        Vector3 vr = data.m_Go.transform.right;
        data.m_vCurrentVector = vf;
        vec.Normalize();
        float fDotF = Vector3.Dot(vf, vec);//一開始進到這裡即開始減速+==>-
        if (fDotF < -0.96f)
        {
            fDotF = -1.0f;
            data.m_vCurrentVector = -vec;
            //  data.m_Go.transform.forward = -vec;
            data.m_fTempTurnForce = 0.0f;
            data.m_fRot = 0.0f;
        }
        else
        {
            if (fDotF > 1.0f)
            {
                fDotF = 1.0f;
            }
            float fDotR = Vector3.Dot(vr, vec);

            if (fDotF > 0.0f)
            {

                if (fDotR > 0.0f)
                {
                    fDotR = 1.0f;
                }
                else
                {
                    fDotR = -1.0f;
                }

            }
            Debug.Log(fDotR);
            data.m_fTempTurnForce = -fDotR;//已經近來警戒範圍，所以才有-1

            // data.m_fTempTurnForce *= 0.1f;


        }

        data.m_fMoveForce = -fDotF;//當開始轉到正確方向時，負的開始得正
        data.m_bMove = true;
        return true;
    }

    static public bool Seek(AIData data)//為了到達目標控制加速度
    {
        Vector3 cPos = data.m_Go.transform.position;
        Vector3 vec = data.m_vTarget - cPos;
        vec.y = 0.0f;
        float fDist = vec.magnitude;
        if (fDist < data.m_Speed + 0.001f)//結果幾乎為0的情況下，不能=0否則會抽動
        {
            Vector3 vFinal = data.m_vTarget;
            vFinal.y = cPos.y;
            data.m_Go.transform.position = vFinal;
            data.m_fMoveForce = 0.0f;
            data.m_fTempTurnForce = 0.0f;
            data.m_Speed = 0.0f;
            data.m_bMove = false;
            return false;
        }
        Vector3 vf = data.m_Go.transform.forward;
        Vector3 vr = data.m_Go.transform.right;
        data.m_vCurrentVector = vf;
        vec.Normalize();
        float fDotF = Vector3.Dot(vf, vec);
        if(fDotF > 0.96f)//夾角為0直接面對她走//還沒考慮左右轉//這裡都在用已經n的質來判斷情況
        {
            fDotF = 1.0f;
            data.m_vCurrentVector = vec;
            data.m_fTempTurnForce = 0.0f;
            data.m_fRot = 0.0f;
        }
        else//先判斷需不需要轉彎，在轉彎
        {
            if (fDotF < -1.0f)//統一口徑
            {
                fDotF = -1.0f;
            }
            float fDotR = Vector3.Dot(vr, vec);//轉彎的值

            if (fDotF < 0.0f)//再F面向不同於目標的情況下
            {
               
                if (fDotR > 0.0f)//大於0給右轉
                {
                    fDotR = 1.0f;//固定給1
                } else
                {
                    fDotR = -1.0f;
                }
               
            } 
            if(fDist < 3.0f)
            {
                fDotR *= (1.0f-(fDist / 3.0f))+ 1.0f;//這轉向的力道取決於距離的多寡，距離越小轉越快
            }
            data.m_fTempTurnForce = fDotR;//會越來越小

        }

        if(fDist < 3.0f)
        {
            Debug.Log(data.m_Speed);
            if(data.m_Speed > 0.1f)//太近ㄌ，速度又超大
            {
                data.m_fMoveForce = -(1.0f - fDist/3.0f)*5.0f;//開始給你扣速度
            } else
            {
                data.m_fMoveForce = fDotF*100.0f;//小於的時候沒有加速度那就會瘋狂減速
            }
            
        } else
        {
            data.m_fMoveForce = fDotF*100.0f;
        }


       
        data.m_bMove = true;
        return true;
    }
}
