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

    void Start()
    {
        iTween.RotateTo(gocutter, iTween.Hash("y",180,"time",0.45f,"easetype",easeType,"looptype",loopType));
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.CompareTag("Enemy") )
        {
            
            other.GetComponent<IEnemy_Base>().UnderAttack(KnifeAtkDmg);

            
            //Destroy(other.gameObject);


        }
    }

}
