using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using iii_UMVR06_TPSDefenseGame_Subroutines_2;

public class SawTrapAtk : MonoBehaviour
{
    public float SawAtkDmg = 10f;
    private float AtkCdTimer = 4.0f;
    private bool bOnTrigger;
    public float fbuffspeed = 1.0f;

    public GameObject SawTrapEffect;    

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

                AtkCdTimer = 4.0f;
                bOnTrigger = false;
            }
        }
    }
    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.CompareTag("EnemyHit") && !bOnTrigger)
        {

            other.GetComponentInParent<IEnemy_Base>().UnderAttack(SawAtkDmg);
            other.GetComponentInParent<IEnemy_Base>().PlayGetHit();
            bOnTrigger = true;

            
            var collisionPoint = other.ClosestPoint(transform.position);
            Instantiate(SawTrapEffect, new Vector3(collisionPoint.x, collisionPoint.y + 1, collisionPoint.z), transform.rotation);
             

        }
    }
}
