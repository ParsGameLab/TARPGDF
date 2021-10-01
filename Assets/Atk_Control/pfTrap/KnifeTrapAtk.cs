using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using iii_UMVR06_TPSDefenseGame_Subroutines_2;

public class KnifeTrapAtk : MonoBehaviour
{
    // Start is called before the first frame update
    public iTween.EaseType easeType;
    public iTween.LoopType loopType;
    public GameObject gocutter;
    public float KnifeAtkDmg = 0.1f;

    public GameObject Effect;
    public Transform StartPositionRotation;
    public float DestroyAfter = 2;

    private float AtkCdTimer = 7.0f;
    public float fbuffspeed = 1.0f;

    void Start()
    {
        iTween.RotateTo(gocutter, iTween.Hash("y",180,"time",0.45f,"easetype",easeType,"looptype",loopType));
    }

    // Update is called once per frame
    void Update()
    {
        iTween.RotateTo(gocutter, iTween.Hash("y", 180, "time", 0.45f, "easetype", easeType, "looptype", loopType));
        AtkCdTimer -= fbuffspeed * Time.deltaTime;
        if (AtkCdTimer < 0f)
        {
            KnifeAtk();
            AtkCdTimer = 7.0f;
        }

    }
    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.CompareTag("EnemyHit"))
        {
            
            other.GetComponentInParent<IEnemy_Base>().UnderAttack(KnifeAtkDmg);
            other.GetComponentInParent<AINormalMob>().m_Data.State = AIData.eMobState.smallslowdown;


            //Destroy(other.gameObject);


        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("EnemyHit"))
        {

            other.GetComponentInParent<AINormalMob>().m_Data.State = AIData.eMobState.normal;


            //Destroy(other.gameObject);


        }
    }
    public void KnifeAtk()
    {
        var instance = Instantiate(Effect, StartPositionRotation.position, Quaternion.identity);
        Destroy(instance, DestroyAfter);
    }

}
