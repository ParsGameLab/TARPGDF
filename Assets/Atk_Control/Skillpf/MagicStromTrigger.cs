using iii_UMVR06_TPSDefenseGame_Subroutines_2;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagicStromTrigger : MonoBehaviour
{
    public float dmg = 100.0f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter(Collider other)
    {


        if (other.gameObject.CompareTag("Enemy"))
        {

            other.GetComponent<IEnemy_Base>().UnderAttack(dmg);


        }

    }
}
