using iii_UMVR06_TPSDefenseGame_Subroutines_2;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagicStromTrigger : MonoBehaviour
{
    public float dmg = 100.0f;
    public float speed=10;
    public GameObject MagicStromEffect;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.localScale = transform.localScale+new Vector3(1.5f, 1.5f, 1.5f) * Time.deltaTime*speed;
    }
    private void OnTriggerEnter(Collider other)
    {


        if (other.gameObject.CompareTag("EnemyHit"))
        {

            other.GetComponentInParent<IEnemy_Base>().UnderAttack(dmg);
            other.GetComponentInParent<AINormalMob>().m_Data.State = AIData.eMobState.smallslowdown;
            StartCoroutine(OutSlow(other.gameObject));

            //var collisionPoint = other.ClosestPoint(transform.position);
            Instantiate(MagicStromEffect,new Vector3( other.transform.position.x, other.transform.position.y+1, other.transform.position.z), transform.rotation);

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

    IEnumerator OutSlow(GameObject mobs)
    {
        
        yield return new WaitForSeconds(3f);
        if (mobs == null) { }
        mobs.GetComponentInParent<AINormalMob>().m_Data.State = AIData.eMobState.normal;

    }
}
