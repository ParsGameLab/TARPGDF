using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillChargeSet : MonoBehaviour
{
    public Animator amime;
    private AnimatorStateInfo animStateInfo;
    // Start is called before the first frame update
    void Start()
    {
        //amime = GetComponentInParent<Animator>();
        animStateInfo = amime.GetCurrentAnimatorStateInfo(0);
        gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (animStateInfo.IsName("Force"))
        {
            gameObject.SetActive(true);
        }
        else
        {
            gameObject.SetActive(false);

        }
        
    }
}
