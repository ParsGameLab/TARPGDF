using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using iii_UMVR06_TPSDefenseGame_Subroutines_2;

public class SawTrapAtk : MonoBehaviour
{
    public float SawAtkDmg = 10f;
    private float AtkCdTimer = 2.0f;
    private bool bOnTrigger;
    public float fbuffspeed = 1.0f;

    // Start is called before the first frame update
    void Start()
    {
        bOnTrigger = false;

    }

    // Update is called once per frame
    void Update()
    {
        if (bOnTrigger)
        {
            AtkCdTimer -= fbuffspeed * Time.deltaTime;
            Debug.Log(AtkCdTimer);
            if (AtkCdTimer < 0f)
            {

                AtkCdTimer = 3.0f;
                bOnTrigger = false;
            }
        }
    }
    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.CompareTag("Enemy") && !bOnTrigger)
        {

            other.GetComponent<IEnemy_Base>().UnderAttack(SawAtkDmg);
            bOnTrigger = true;


            //Destroy(other.gameObject);


        }
    }
}
