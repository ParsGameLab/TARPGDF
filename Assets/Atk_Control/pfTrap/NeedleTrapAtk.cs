using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using iii_UMVR06_TPSDefenseGame_Subroutines_2;

public class NeedleTrapAtk : MonoBehaviour
{
    private float AtkCdTimer=7.0f;
    public float fbuffspeed = 1.0f;
    private bool bOnTrigger;
    public Animator NeedleTrapAnime;
    public float NeedleAtkDmg = 30f;

    public GameObject TrapNeedleEffect;

    public AudioSource audioSource;

    // Start is called before the first frame update
    void Start()
    {
        bOnTrigger = false;
        //audioSource = GetComponentInChildren<AudioSource>();
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
            
                AtkCdTimer = 7.0f;
                bOnTrigger = false;
            }
        }

        
        
    }
    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.CompareTag("EnemyHit") && !bOnTrigger)
        {
            NeedleTrapAnime.SetTrigger("NeedleAtk");
            audioSource.Play();
            other.GetComponentInParent<IEnemy_Base>().UnderAttack(NeedleAtkDmg);
            other.GetComponentInParent<IEnemy_Base>().PlayGetHit();

            bOnTrigger = true;
            //Destroy(other.gameObject);

            var collisionPoint = other.ClosestPoint(transform.position); 
            Instantiate(TrapNeedleEffect,new Vector3(collisionPoint.x,collisionPoint.y+1, collisionPoint.z), transform.rotation);
        }
    }
}
