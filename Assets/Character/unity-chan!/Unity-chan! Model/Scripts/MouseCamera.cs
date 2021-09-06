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

        if (Input.GetMouseButton(1))//���U�k��� �i�J�Ĥ@�H�ٵ���
        {           

            mouse_X = Input.GetAxis("Mouse X");
            mouse_Y = Input.GetAxis("Mouse Y");
           

            LimitAngleX(40);//�����            
            
            transform.RotateAround(transform.position, transform.right, -1 * mouse_Y * Time.deltaTime * 300);//Y�b��������
        }
        else
        {
            this.transform.localEulerAngles = new Vector3(0, 0, 0);//��v���_��
            this.transform.localPosition = new Vector3(0, 1.33f,0);            
        }
    }
        private void LimitAngleX(float angle)//����X�b����
        {
            vect = this.transform.localEulerAngles;//��e�۾�x�b���઺����(0~360)

            xcream = IsPosNum(vect.x);            

            if (xcream > angle)
                this.transform.localEulerAngles = new Vector3(angle, vect.y, 0);
            else if (xcream < -angle)
                this.transform.localEulerAngles = new Vector3(-angle, vect.y, 0);
            else
                transform.RotateAround(transform.position, transform.up, mouse_X * Time.deltaTime * 300);//X�b��������
        }

        private void LimitAngleY(float angle)//����Y�b����
        {
            vect = this.transform.eulerAngles;//��e�۾�y�b���઺����(0~360)

        ycream = IsPosNum(vect.y);

            if (ycream > angle)
                this.transform.localEulerAngles = new Vector3(vect.x, angle, 0);
            else if (ycream < -angle)
                this.transform.localEulerAngles = new Vector3(vect.x, -angle, 0);
        }


        private float IsPosNum(float x)//���ץ��W��
        {
            x -= 180;
            if (x < 0)
                return x + 180;
            else
                return x - 180;
        }
    

}
