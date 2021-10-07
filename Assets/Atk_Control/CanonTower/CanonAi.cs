using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanonAi : MonoBehaviour
{
    public float AttackRange;
    public bool bAttack;
    public float fAttackTime = 5.0f;
    private float fCurrentTime = 0.0f;
    public float fowardspeed=2f;
    public GameObject targetRange;
    public Transform Cannon;

    public GameObject Effect;
    public GameObject Effectsmoke;
    public Transform FirePosition;
    public float DestroyAfter = 2;

    private Vector3 updown;


    // Start is called before the first frame update
    void Start()
    {
        bAttack = false;
        fCurrentTime = 0.0f;
    }

    // Update is called once per frame
    void Update()
    {
        Transform go = CheckEnemyInRanged();
        if (go != null)
        {
            if (fCurrentTime > fAttackTime)
            {
                fCurrentTime = 0.0f;
                SoundManager.Instance.PlaySound(SoundManager.Sound.Cannon);
                CanonAttack(go);
            }
            fCurrentTime += Time.deltaTime;
        }
        return;
    }
    public Transform CheckEnemyInRanged()
    {
        List<Transform> golist = EnemySpawnManagerS2.Instance.allenemylist;
        foreach(Transform go in golist)
        {
            Vector3 v = go.transform.position - targetRange.transform.position;
            float fDist = v.magnitude;
            if (fDist < AttackRange)
            {
                bAttack = true;
                return go;
            }
        }

        bAttack = false;
        return null;
    }
    public void CanonAttack(Transform go)
    {
        Vector3 dirforward = go.position-transform.position ;
        Vector3 Dir = dirforward;
        updown = new Vector3(dirforward.x,0,0);
        dirforward.y = 0;

        Cannon.forward = Vector3.Lerp(Cannon.forward, dirforward, Time.deltaTime * fowardspeed);
        Fire(go, Dir);
    }
    public void Fire(Transform go,Vector3 fireDir)
    {
        var instance = Instantiate(Effectsmoke, FirePosition.position, FirePosition.rotation);
        GameObject firego = GameObject.Instantiate(Effect);
        CanonShell goshell = firego.GetComponent<CanonShell>();
        goshell.CanonShellAttack(FirePosition.position, fireDir);
        Destroy(instance, 1f);
        Destroy(firego, DestroyAfter);
    }
    public Vector3 GetUpDown()
    {
        return updown;
    }
    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(targetRange.transform.position, AttackRange);

        Gizmos.color = Color.red;
    }
}
