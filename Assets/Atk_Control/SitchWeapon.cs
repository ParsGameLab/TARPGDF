using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SitchWeapon : MonoBehaviour
{
    public List<Transform> WeaponBox = new List<Transform>();//�Ҧ������������Z������o
    public List<Transform> Weapons = new List<Transform>();//�ثe�ҥΪ��Z��
    public int currentWeapon;
    public bool canChagerWp;
    

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
        SwitchWpController();
        
    }

    private void SwitchPlayerWp(int index)
    {
        if (Weapons.Count == 1)
        {
            return;
        }
        else
        {
            for(int i=0;i< Weapons.Count; i++)
            {
                if (i == index)
                {
                    Weapons[i].gameObject.SetActive(true);
                }
                else
                {
                    Weapons[i].gameObject.SetActive(false);
                }

            }

        }
        
    }
    private void SwitchWpController()
    {
        if (Weapons.Count == 1) return;
        else
        {
            if(Input.GetAxisRaw("Mouse ScrollWheel") > 0 )//���Z�����U��&& canChagerWp
            {
                
                
                currentWeapon += 1;
                if (currentWeapon == Weapons.Count) return;
                
                SwitchPlayerWp(currentWeapon);
            }
            else if (Input.GetAxisRaw("Mouse ScrollWheel") < 0 && currentWeapon!=0) //���W�u�A���V�W��&& canChagerWp
            {
                currentWeapon -= 1;
                if (currentWeapon == 0)
                SwitchPlayerWp(currentWeapon);
            }



        }

        currentWeapon = Mathf.Clamp(currentWeapon, 0, 1);
    }
    public GameObject GetCurrectWp()
    {
        //int i = currentWeapon;
        //if(i> Weapons.Count)
        //{
        //    i = 1;
        //}
        currentWeapon = Mathf.Clamp(currentWeapon, 0, 1);
        return Weapons[currentWeapon].gameObject;
    }
    //public void UpdateHp(float fHp)
    //{
    //    mProperties.fHp = fHp;
    //    weaPonCallback(fHp / mProperties.fMaxHp);
    //}
}
