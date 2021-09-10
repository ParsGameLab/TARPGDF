using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using iii_UMVR06_TPSDefenseGame_Subroutines_2;

public class NeedleTrapAtk : MonoBehaviour
{
    private float AtkCdTimer=3.0f;
    public float fbuffspeed = 1.0f;
    private bool bOnTrigger;
    public Animator NeedleTrapAnime;
    public float NeedleAtkDmg = 30f;
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
            AtkCdTimer -= fbuffspeed*Time.deltaTime;
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
        if (other.gameObject.CompareTag("Enemy")&&!bOnTrigger)
        {
            NeedleTrapAnime.SetTrigger("NeedleAtk");
            other.GetComponent<IEnemy_Base>().UnderAttack(NeedleAtkDmg);
            
            bOnTrigger = true;
            //Destroy(other.gameObject);
            

        }
    }
}
