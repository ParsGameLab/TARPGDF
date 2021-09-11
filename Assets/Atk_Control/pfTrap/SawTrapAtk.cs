using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using iii_UMVR06_TPSDefenseGame_Subroutines_2;

public class SawTrapAtk : MonoBehaviour
{
    public float SawAtkDmg = 40f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.CompareTag("Enemy"))
        {

            other.GetComponent<IEnemy_Base>().UnderAttack(SawAtkDmg);


            //Destroy(other.gameObject);


        }
    }
}
