using iii_UMVR06_TPSDefenseGame_Subroutines_2;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagicBallTriger : MonoBehaviour
{
    public float dmg=50.0f;
    private float fTime;
    public float fmagicspeed = 10.0f;
    void Start()
    {
        fTime = 0.0f;
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = transform.position + transform.forward * fmagicspeed * Time.deltaTime;

        //transform.position = Vector3.MoveTowards(transform.position, target, fmagicspeed * Time.deltaTime);
        //if (!hit && Vector3.Distance(transform.position, target) < 0.01f)
        //{
        //    Destroy(gameObject);
        //}
        //fTime += Time.deltaTime;
        //if (fTime > 3.0f)
        //{
        //    Destroy(gameObject);
        //}
    }
    public void MagicBallAttack(Vector3 FirePoint, Vector3 targetposition)
    {

        transform.position = FirePoint;
        transform.forward = targetposition;
        //gameObject.SetActive(true);



    }
    private void OnTriggerEnter(Collider other)
        {


            if (other.gameObject.CompareTag("Enemy"))
            {

                other.GetComponent<IEnemy_Base>().UnderAttack(dmg);
                other.GetComponent<AINormalMob>().m_Data.State = AIData.eMobState.slowdown;

            }

        }
}
