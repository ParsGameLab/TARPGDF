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
    

    public GameObject TrapKnifeEffect;
    public GameObject BigTrapKnifeEffect;
    public bool AtkAnimation;
    int x = 0;

    public AudioSource audioSource;
    bool EnemyInSide;

    void Start()
    {
        iTween.RotateTo(gocutter, iTween.Hash("y",180,"time",0.45f,"easetype",easeType,"looptype",loopType));
        //audioSource = GetComponentInChildren<AudioSource>();
        audioSource.clip = SoundManager.Instance.GetSoundClip(SoundManager.Sound.KnifeTrapNor);
        audioSource.Play();
        EnemyInSide = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (EnemyInSide == false)
        {
            audioSource.clip = SoundManager.Instance.GetSoundClip(SoundManager.Sound.KnifeTrapNor);
            if (audioSource.isPlaying) { return; }
            audioSource.Play();
        }
        else
        {
            audioSource.clip = SoundManager.Instance.GetSoundClip(SoundManager.Sound.KnifeTrapAtk);
            if (audioSource.isPlaying) { return; }
            audioSource.Play();
        }
        iTween.RotateTo(gocutter, iTween.Hash("y", 180, "time", 0.45f, "easetype", easeType, "looptype", loopType));
        AtkCdTimer -= fbuffspeed * Time.deltaTime;
        if (AtkCdTimer < 0f)
        {
            KnifeAtk();
            AtkCdTimer = 7.0f;
            AtkAnimation = true;
        }

    }
    private void OnTriggerStay(Collider other)
    {
        

        if (other.gameObject.CompareTag("EnemyHit"))
        {
            EnemyInSide = true;
            

            other.GetComponentInParent<IEnemy_Base>().UnderAttack(KnifeAtkDmg);
            other.GetComponentInParent<AINormalMob>().m_Data.State = AIData.eMobState.smallslowdown;

                           
                //Destroy(other.gameObject);
            if (x == 30)
            {
                if (AtkAnimation == true)
                {
                    var collisionPoint = other.ClosestPoint(transform.position);
                    Instantiate(BigTrapKnifeEffect, new Vector3(collisionPoint.x, collisionPoint.y + 1, collisionPoint.z), transform.rotation);
                    x = 0;
                    AtkAnimation = false;
                }
                else
                {
                    var collisionPoint = other.ClosestPoint(transform.position);
                    Instantiate(TrapKnifeEffect, new Vector3(collisionPoint.x, collisionPoint.y + 1, collisionPoint.z), transform.rotation);
                    x = 0;
                }

            }
            else
                x++;

        }
        else
        {
            EnemyInSide = false;
        }
    }
    private void OnTriggerExit(Collider other)//
    {
        if (other.gameObject.CompareTag("EnemyHit"))
        {
            EnemyInSide = false;
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
