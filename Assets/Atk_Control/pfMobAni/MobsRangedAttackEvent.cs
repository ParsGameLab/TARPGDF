using iii_UMVR06_TPSDefenseGame_Subroutines_2;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MobsRangedAttackEvent : MonoBehaviour
{
    public GameObject Effect;
    public Transform StartPositionRotation;
    public float DestroyAfter = 4;
    public LayerMask hitEnemy;
    public AINormalMob AIMob;


    public void SkillAtkBall()
    {
        
        Vector3 targetposition = AIMob.GetTagetPosition();
        //SoundManager.Instance.PlaySound(SoundManager.Sound.fire);
        RaycastHit rh;
        //GameObject pfgo = Resources.Load<GameObject>("FloatingCoin");
        GameObject goMagicBall = GameObject.Instantiate(Effect);
        DemonFireBall magicball = goMagicBall.GetComponent<DemonFireBall>();
        //if (Physics.Raycast(StartPositionRotation.position, targetposition, out rh))//Physics.Raycast(r, out rh, 1000.0f /*hitEnemy*/)
        //{
        //    magicball.FireBallAttack(StartPositionRotation.position, rh.point);//use
        //}
        //else
        //{

        //    Vector3 vTarget = targetposition * 1000.0f;
        //    magicball.FireBallAttack(StartPositionRotation.position, vTarget);//use  
        //}
        magicball.FireBallAttack(StartPositionRotation.position, transform.forward);
        Destroy(goMagicBall, DestroyAfter);
    }

}
