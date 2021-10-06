using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using iii_UMVR06_TPSDefenseGame_Subroutines_2;

public class unitychanControl : MonoBehaviour
{
    public _2_StatHandler_UnityChan statHandler_UnityChan;

    public Transform tpsCamera;
    public float fspeed = 3.0f;
    public float frunspeed = 5.0f;
    public Transform GroundCheck;
    public float CheckRaius = 0.2f;
    public LayerMask flayermask;
    
    private Animator manimater;
    private CharacterController mControl;
    bool isWalking = true;
    private bool InGround;
    Vector3 vVelocity=Vector3.zero;
    [SerializeField] private float jumpHeight=1;
    [SerializeField] private float fgravity=-9.8f;
    
    public float fowardspeed=1.0f;

    private AnimatorStateInfo animStateInfo;

    public Transform targetR;
    public Transform targetL;

    public bool CanPort;
    public Vector3 PortPos;

    

    /// <summary>
    /// ATk用參數
    /// </summary>



    // Start is called before the first frame update
    private void Awake()
    {
        manimater = GetComponent<Animator>();
        mControl = GetComponent<CharacterController>();
    }
    void Start()
    {
        isWalking = true;
        animStateInfo = manimater.GetCurrentAnimatorStateInfo(0);
        CanPort = false;
    }

    // Update is called once per frame
    void Update()
    {
        
        

        InGround = Physics.CheckSphere(GroundCheck.position, CheckRaius, flayermask);
        if (InGround && vVelocity.y < 0)
        {
            vVelocity.y = 0;
        }
        if (statHandler_UnityChan.IsDeath) { return; }

        float fH = Input.GetAxis("Horizontal");
        float fV = Input.GetAxis("Vertical");
        if (manimater.GetCurrentAnimatorStateInfo(0).IsName("Force")|| manimater.GetCurrentAnimatorStateInfo(0).IsName("SmallSkill") || manimater.GetCurrentAnimatorStateInfo(0).IsName("BigSkill")|| manimater.GetCurrentAnimatorStateInfo(0).IsName("BackCore"))
        {
            fH = 0;
            fV = 0;

        }
        

        
        
        
        Vector3 vCamDir = tpsCamera.forward;
        vCamDir.y = 0;

        //transform.forward= vCamDir;
        transform.forward = Vector3.Lerp(transform.forward, vCamDir, Time.deltaTime*fowardspeed);
        Vector2 Mxy = new Vector2(fH, fV);
        
        //Mxy.x=Mathf.Clamp(fH,)
        float speed = fspeed;

        Vector3 output = Vector3.zero;
        output.x = fH * Mathf.Sqrt(1 - (fV * fV) / 2.0f);
        output.z = fV * Mathf.Sqrt(1 - (fH * fH) / 2.0f);


        output = output.x * transform.right + output.z * transform.forward;
        output.y = 0;
        speed = fspeed;
        manimater.SetFloat("MoveSpeed", Mathf.Clamp(Mxy.y, -0.5f,0.5f));
        manimater.SetFloat("MoveLR", Mathf.Clamp(Mxy.x, -0.5f, 0.5f));//走路動畫
        //Walking();
        //if (isWalking)
        //{
        //    speed = fspeed;
        //    manimater.SetFloat("MoveSpeed", Mathf.Clamp(Mxy.y, -0.5f,0.5f));
        //    manimater.SetFloat("MoveLR", Mathf.Clamp(Mxy.x, -0.5f, 0.5f));//走路動畫

        //}
        //else
        //{
        //    speed = frunspeed;
        //    manimater.SetFloat("MoveSpeed", Mxy.y);
        //    manimater.SetFloat("MoveLR", Mxy.x);


        //}
        if (Input.GetKey(KeyCode.B)){
            manimater.SetTrigger("BackCore");
        }
        if (Input.GetKey(KeyCode.LeftShift))
        {
            speed = frunspeed;
            manimater.SetFloat("MoveSpeed", Mxy.y);
            manimater.SetFloat("MoveLR", Mxy.x);

            //isWalking = !isWalking;
        }

        Vector3 fMoveVAmount = transform.forward * fV * speed;
        Vector3 fMoveHAmount = transform.right * fH * speed;
        Vector3 vMove = fMoveVAmount + fMoveHAmount;
        //vMove += Physics.gravity;
        //transform.position = transform.position + transform.forward * fMoveVAmount + transform.right * fMoveHAmount;//no cc
        //mControl.Move();
        
        vVelocity.y += fgravity * Time.deltaTime;
        
        Jumping();

        //mControl.Move(vMove * Time.deltaTime + vVelocity * Time.deltaTime);
        mControl.Move(output * Time.deltaTime* speed + vVelocity * Time.deltaTime);




        //if (CanPort)
        //{
        //    if (Input.GetKeyDown(KeyCode.Q))
        //    {
        //        print("Press Q inThis");
        //        transform.position = PortPos;
        //        CanPort = false;

        //    }
        //}


        //transform.Rotate(0.0f, fH, 0.0f);//我要的是滑鼠轉人跟著轉(人轉的動畫)，人左右走(左右走的動畫)攝影機會拍人

    }
    //public void SetPort(bool CantelePort,Vector3 target)
    //{
    //    CanPort = CantelePort;
    //    PortPos = target;
    //}
    public void PlayHit()
    {
        if (statHandler_UnityChan.IsDeath == true)
        {
            return;
        }
        else
        {
            manimater.SetTrigger("BeAtk");

        }
       
    }
    public void PortCharacterR()
    {
        this.transform.position = targetR.position;
    }
    public void PortCharacterL()
    {
        this.transform.position = targetL.position;
    }
    void Walking()
    {
        if (Input.GetKey(KeyCode.LeftShift))
        {

            isWalking = !isWalking;
        }

    }
    
    void Jumping()
    {
        if (Input.GetKeyDown(KeyCode.Space)&& InGround)
        {

            //manimater.SetBool("Jump", true);
            manimater.SetTrigger("TriggerJJ");
            vVelocity.y = Mathf.Sqrt(jumpHeight*-2*fgravity);
            //vVelocity.y = jumpHeight;
            
        }
        else
        {
            //manimater.SetBool("Jump", false);
            //manimater.ResetTrigger("TriggerJJ");
           
        }
        
    }
    
    
    
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawLine(tpsCamera.position, tpsCamera.forward*1000.0f);
    }


}
