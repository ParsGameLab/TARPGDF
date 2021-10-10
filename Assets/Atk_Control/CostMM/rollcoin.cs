using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class rollcoin : MonoBehaviour
{
    public iTween.EaseType easeType;
    public iTween.LoopType loopType;
    // Start is called before the first frame update
    void Update()
    {
        iTween.RotateTo(transform.gameObject, iTween.Hash("y", 180, "time", 1.5f, "easetype", easeType, "looptype", loopType));
    }

    private void OnEnable()
    {
        Destroy(gameObject, 5f);
    }
}
