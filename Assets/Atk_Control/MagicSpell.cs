using iii_UMVR06_TPSDefenseGame_Subroutines_2;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagicSpell : MonoBehaviour
{
    private float fTime;
    public float fmagicspeed = 10.0f;
    // Start is called before the first frame update
    void Start()
    {
        fTime = 0.0f;
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = transform.position+transform.forward * fmagicspeed * Time.deltaTime;
        fTime += Time.deltaTime;
        if (fTime > 3.0f)
        {
            Destroy(gameObject);
        }
    }
    public void MagicNorAttack(Vector3 FirePoint, Vector3 targetposition)
    {
        
            transform.position = FirePoint;
            transform.forward = targetposition;
            gameObject.SetActive(true);

        
        
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Enemy"))
        {
            other.GetComponent<IEnemy_Base>().UnderAttack(30f);
            //Destroy(other.gameObject);
            Destroy(gameObject);
        }
    }
    
}
