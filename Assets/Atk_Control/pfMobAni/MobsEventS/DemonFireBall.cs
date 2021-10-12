using iii_UMVR06_TPSDefenseGame_Subroutines_2;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DemonFireBall : MonoBehaviour
{
    // Start is called before the first frame update
    public float dmg = 40.0f;
    private float fTime;
    public float fmagicspeed = 15.0f;
    private AudioSource audioSource;
    

    void Start()
    {
        fTime = 0.0f;
        audioSource = GetComponent<AudioSource>();
        audioSource.Play();
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = transform.position + transform.forward * fmagicspeed * Time.deltaTime;

    }
    public void FireBallAttack(Vector3 FirePoint, Vector3 targetposition)
    {

        transform.position = FirePoint;
        transform.forward = targetposition;
        //gameObject.SetActive(true);



    }
    private void OnTriggerEnter(Collider other)
    {


        if (other.gameObject.CompareTag("Player"))
        {
            audioSource.clip= SoundManager.Instance.GetSoundClip(SoundManager.Sound.fireimpact);
            audioSource.Play();
            audioSource.loop = false;
            other.GetComponent<_2_StatHandler_UnityChan>().UnderEnemyAttack(dmg);
            other.GetComponent<MobGetHitEvent>().GetHit();
            //other.GetComponent<unitychanControl>().PlayHit();
            //other.GetComponentInParent<AINormalMob>().m_Data.State = AIData.eMobState.slowdown;


        }
        if (other.gameObject.CompareTag("Core"))
        {
            other.GetComponent<_2_LevelCoreHandler>().UnderEnemyAttack(dmg);
        }

    }
}
