using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillAnimationEvent : MonoBehaviour
{
    // Start is called before the first frame update
    public EffectInfo[] Effects;
    public LayerMask hitEnemy;

    [System.Serializable]

    
    public class EffectInfo
    {
        public GameObject Effect;
        public Transform StartPositionRotation;
        public float DestroyAfter = 10;
        public bool UseLocalPosition = true;
    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void VatkCombat(int EffectNumber)
    {
        if (Effects == null || Effects.Length <= EffectNumber)
        {
            Debug.LogError("Incorrect effect number or effect is null");
        }

        var instance = Instantiate(Effects[EffectNumber].Effect, Effects[EffectNumber].StartPositionRotation.position, Effects[EffectNumber].StartPositionRotation.rotation);

        if (Effects[EffectNumber].UseLocalPosition)
        {
            instance.transform.parent = Effects[EffectNumber].StartPositionRotation.transform;
            instance.transform.localPosition = Vector3.zero;
            instance.transform.localRotation = new Quaternion();
        }
        Destroy(instance, Effects[EffectNumber].DestroyAfter);

    }
    public void SkillAtkBall(int EffectNumber)
    {
        Transform CameraTrans = Camera.main.transform;

        RaycastHit rh;
        GameObject goMagicBall = GameObject.Instantiate(Effects[EffectNumber].Effect, Effects[EffectNumber].StartPositionRotation.position, Effects[EffectNumber].StartPositionRotation.rotation);
        MagicBallTriger magicball = goMagicBall.GetComponent<MagicBallTriger>();
        if (Physics.Raycast(CameraTrans.position, CameraTrans.forward, out rh, hitEnemy))//Physics.Raycast(r, out rh, 1000.0f /*hitEnemy*/)
        {
            magicball.MagicBallAttack(Effects[EffectNumber].StartPositionRotation.position, rh.point);//use
        }
        else
        {

            Vector3 vTarget = CameraTrans.forward * 1000.0f;
            magicball.MagicBallAttack(Effects[EffectNumber].StartPositionRotation.position, vTarget);//use

            Debug.Log("hhhhhhhhh");
        }
        Destroy(goMagicBall, Effects[EffectNumber].DestroyAfter);
    }
    
}
