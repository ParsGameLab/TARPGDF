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
    public LayerMask mCheckLayer;
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
        Vector3 vFinalDir = Quaternion.AngleAxis(mVerticalDegree, rotationAxis) * mHorizontalV;//最後攝影機朝向的方向，正確的攝影機方位
        vFinalDir.Normalize();
        Vector3 vFinalPos = mLookAtPoint.position + vFinalDir * mFollowDistance;
        Vector3 vDir = mLookAtPoint.position - vFinalPos;

        vDir.Normalize();


        RaycastHit rh;
        RaycastHit nwrh;
        Ray r = new Ray(mLookAtPoint.position, -vDir);
        Vector3 vHit;
        float upV;
        Vector3 newpoint;


        float newdis;



        if (Physics.SphereCast(r, 0.1f, out rh, mFollowDistance, mCheckLayer))
        {

            vFinalPos = mLookAtPoint.position - vDir * (rh.distance - 0.1f);

            if (rh.distance < mMinFollowDistance)
            {

                upV = Mathf.Sqrt(mMaxFollowDistance * mMaxFollowDistance - rh.distance * rh.distance);//+= Vector3.up*upV;
                newpoint = rh.point + new Vector3(0, upV, 0);//加的對
                newdis = Vector3.Distance(mLookAtPoint.position, newpoint);
                vDir = mLookAtPoint.position - newpoint;//只是之前都在原本看的方向，現在要把從新的點看的方向設定好
                vDir.Normalize();
                vHit = mLookAtPoint.position - vDir * (newdis - 0.1f);
                vFinalPos = vHit;

                Debug.Log("here");
                Ray newr = new Ray(mLookAtPoint.position, -vHit.normalized);
                if (!Physics.SphereCast(newr, 0.1f, out nwrh, mFollowDistance, mCheckLayer))
                {


                    //vFinalPos = mLookAtPoint.position - vHit * (nwrh.distance - 0.1f);
                    vFinalPos = mLookAtPoint.position + vDir * mFollowDistance;
                    //vDir = - vHit;

                    //vDir.Normalize();


                }


            }



        }
        transform.position = vFinalPos;
        transform.forward = vDir;



    }


}
