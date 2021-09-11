using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class MouseCamera : MonoBehaviour
{
    public float mouse_X;
    public float mouse_Y;   
    //public float mouse_scroll;    
    
    public Vector3 vect;
    private float xcream;
    private float ycream;


    //Transform jumpPos;

    // Start is called before the first frame update
    void Start()
    {
        mouse_X = 0;
        mouse_Y = 0;
    }

    // Update is called once per frame
    void Update()
    {        

        if (Input.GetMouseButton(1))//按下右鍵時 進入第一人稱視角
        {           

            mouse_X = Input.GetAxis("Mouse X");
            mouse_Y = Input.GetAxis("Mouse Y");
           

            LimitAngleX(40);//限制角度            
            
            transform.RotateAround(transform.position, transform.right, -1 * mouse_Y * Time.deltaTime * 300);//Y軸視角旋轉
        }
        else
        {
            this.transform.localEulerAngles = new Vector3(0, 0, 0);//攝影機復位
            this.transform.localPosition = new Vector3(0, 1.33f,0);            
        }
    }
        private void LimitAngleX(float angle)//限制X軸角度
        {
            vect = this.transform.localEulerAngles;//當前相機x軸旋轉的角度(0~360)

            xcream = IsPosNum(vect.x);            

            if (xcream > angle)
                this.transform.localEulerAngles = new Vector3(angle, vect.y, 0);
            else if (xcream < -angle)
                this.transform.localEulerAngles = new Vector3(-angle, vect.y, 0);
            else
                transform.RotateAround(transform.position, transform.up, mouse_X * Time.deltaTime * 300);//X軸視角旋轉
        }

        private void LimitAngleY(float angle)//限制Y軸角度
        {
            vect = this.transform.eulerAngles;//當前相機y軸旋轉的角度(0~360)

        ycream = IsPosNum(vect.y);

            if (ycream > angle)
                this.transform.localEulerAngles = new Vector3(vect.x, angle, 0);
            else if (ycream < -angle)
                this.transform.localEulerAngles = new Vector3(vect.x, -angle, 0);
        }


        private float IsPosNum(float x)//角度正規化
        {
            x -= 180;
            if (x < 0)
                return x + 180;
            else
                return x - 180;
        }
    

}
