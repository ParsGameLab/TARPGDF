using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TPScontt : MonoBehaviour
{
    
    // Start is called before the first frame update
    public Transform mLookAtPoint;


    public float mFollowDistance=3.0f;
    public float mMaxFollowDistance =1.5f;
    public float mMinFollowDistance = 0.9f;
    
    





    private float mVerticalDegree;
    public float mVerticalLimitUp = 20.0f;
    public float mVerticalLimitDown = 50.0f;

    private Vector3 mHorizontalV;
    public float rotateSensitivity=6.0f;
    public float fcamrecaverspeed=20.0f;
    public LayerMask mCheckLayer;
    float upV;
    void Start()
    {
        Vector3 vDir = mLookAtPoint.position - transform.position;
        mHorizontalV = vDir;
        mHorizontalV.y = 0.0f;
        mHorizontalV.Normalize();


    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void LateUpdate()
    {
        float xMov = Input.GetAxis("Mouse X");
        float yMov = Input.GetAxis("Mouse Y");
        mHorizontalV = Quaternion.AngleAxis(xMov * rotateSensitivity, Vector3.up) * mHorizontalV;
        Vector3 rotationAxis = Vector3.Cross(mHorizontalV, Vector3.up);
        mVerticalDegree -= yMov * rotateSensitivity;
        if (mVerticalDegree < -mVerticalLimitUp)
        {
            mVerticalDegree = -mVerticalLimitUp;
        }
        else if (mVerticalDegree > mVerticalLimitDown)
        {
            mVerticalDegree = mVerticalLimitDown;
        }
        Vector3 vFinalDir = Quaternion.AngleAxis(mVerticalDegree, rotationAxis) * mHorizontalV;//�̫���v���¦V����V�A���T����v�����
        vFinalDir.Normalize();
        Vector3 vFinalPos = mLookAtPoint.position + vFinalDir * mFollowDistance;
        Vector3 vDir = mLookAtPoint.position - vFinalPos;

        vDir.Normalize();


        RaycastHit rh;
        RaycastHit nwrh;
        Ray r = new Ray(mLookAtPoint.position, -vDir);
        Vector3 vHit;
        
        Vector3 newpoint;
        bool lerpcheck = false;

        float newdis;



        if (Physics.SphereCast(r, 0.1f, out rh, mFollowDistance, mCheckLayer))
        {

            vFinalPos = mLookAtPoint.position - vDir * (rh.distance - 0.1f);

            if (rh.distance < mMinFollowDistance)
            {

                upV = Mathf.Sqrt(mMaxFollowDistance * mMaxFollowDistance - rh.distance * rh.distance);//+= Vector3.up*upV;
                newpoint = rh.point + Vector3.up * upV;//�[����new Vector3(0, upV, 0)
                newdis = Vector3.Distance(mLookAtPoint.position, newpoint);
                vDir = mLookAtPoint.position - newpoint;//�u�O���e���b�쥻�ݪ���V�A�{�b�n��q�s���I�ݪ���V�]�w�n
                vDir.Normalize();
                vHit = mLookAtPoint.position - vDir * (newdis - 0.1f);
                vFinalPos =vHit;//Vector3.Lerp(vFinalPos ,vHit, Time.deltaTime *fcamrecaverspeed)
                lerpcheck = true;
                Debug.Log("here");
                Ray newr = new Ray(mLookAtPoint.position, -vDir);
                if (!Physics.SphereCast(newr, 0.1f, out nwrh, mFollowDistance, mCheckLayer))
                {


                    //vFinalPos = mLookAtPoint.position - vHit * (nwrh.distance - 0.1f);
                    vFinalPos = mLookAtPoint.position - vDir * mFollowDistance;
                    //vDir = - vHit;

                    //vDir.Normalize();


                }
                
                


            }



        }
        if (lerpcheck) 
        { 
            transform.position = Vector3.Lerp(transform.position, vFinalPos, Time.deltaTime * fcamrecaverspeed);
        }
        else
        {
            transform.position = vFinalPos;

        }
        
        //transform.position = Vector3.Lerp(transform.position, vFinalPos, Time.deltaTime * fcamrecaverspeed);
        transform.forward = vDir;



    }


}
