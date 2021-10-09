using iii_UMVR06_TPSDefenseGame_Subroutines_2;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagicSpell : MonoBehaviour
{
    private float fTime;
    public float fmagicspeed = 10.0f;
    [SerializeField]
    private GameObject spellDecal;
    public float Dmg=30f;
    public Vector3 target { get; set; }
    public bool hit {get;set;}
    public LayerMask hitLayer;

    public GameObject SpellExplode;

    // Start is called before the first frame update
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
        //gameObject.SetActive(true);



    }
    //private void OnCollisionEnter(Collision other)
    //{
    //    ContactPoint contact = other.GetContact(0);
    //    GameObject.Instantiate(spellDecal, contact.point, Quaternion.LookRotation(contact.normal));
    //    Destroy(gameObject);
    //}
    private void OnTriggerEnter(Collider other)
    {


        //if (other.gameObject.CompareTag("Enemy"))
        //{

        //    other.GetComponent<IEnemy_Base>().UnderAttack(Dmg);
        //    //Destroy(other.gameObject);
        //    Destroy(gameObject);
        //}
        if (other.gameObject.CompareTag("EnemyHit")) 
        {
            
            other.GetComponentInParent<IEnemy_Base>().UnderAttack(Dmg);
            if (Random.Range(1, 10) < 4)
            {
                other.GetComponentInParent<IEnemy_Base>().PlayGetHit();
            }           

            Instantiate(SpellExplode, transform.position, transform.rotation);

            Destroy(gameObject);
            
        }
            
        Destroy(gameObject,10f);
    }
    
}
