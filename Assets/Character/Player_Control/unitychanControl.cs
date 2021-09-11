using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class unitychanControl : MonoBehaviour
{
    public Transform tpsCamera;
    public float fspeed = 5.0f;
    public float frunspeed = 8.0f;
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
        
    }

    // Update is called once per frame
    void Update()
    {
        
        InGround = Physics.CheckSphere(GroundCheck.position, CheckRaius, flayermask);
        if (InGround && vVelocity.y < 0)
        {
            vVelocity.y = 0;
        }
        float fH = Input.GetAxis("Horizontal");
        float fV = Input.GetAxis("Vertical");
        fspeed = 5.0f;
        Vector3 vCamDir = tpsCamera.forward;
        vCamDir.y = 0;

        //transform.forward= vCamDir;
        transform.forward = Vector3.Lerp(transform.forward, vCamDir, Time.deltaTime*fowardspeed);


        Vector2 Mxy = new Vector2(fH, fV);
        //Mxy.x=Mathf.Clamp(fH,)

        Walking();
        if (isWalking)
        {
            manimater.SetFloat("MoveSpeed", Mathf.Clamp(Mxy.y, -0.5f,0.5f));
            manimater.SetFloat("MoveLR", Mathf.Clamp(Mxy.x, -0.5f, 0.5f));//走路動畫

        }
        else
        {
            manimater.SetFloat("MoveSpeed", Mxy.y);
            manimater.SetFloat("MoveLR", Mxy.x);
            fspeed = frunspeed;

        }
        Vector3 fMoveVAmount = transform.forward * fV * fspeed;
        Vector3 fMoveHAmount = transform.right * fH * fspeed;
        Vector3 vMove = fMoveVAmount + fMoveHAmount;
        //vMove += Physics.gravity;
        //transform.position = transform.position + transform.forward * fMoveVAmount + transform.right * fMoveHAmount;//no cc
        //mControl.Move();
        
        vVelocity.y += fgravity * Time.deltaTime;
        
        Jumping();
        
        mControl.Move(vMove*Time.deltaTime+vVelocity * Time.deltaTime);
        
        
        




        //transform.Rotate(0.0f, fH, 0.0f);//我要的是滑鼠轉人跟著轉(人轉的動畫)，人左右走(左右走的動畫)攝影機會拍人

    }
    void Walking()
    {
        if (Input.GetKeyDown(KeyCode.LeftShift))
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
