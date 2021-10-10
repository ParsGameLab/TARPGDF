using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class lastUIevent : MonoBehaviour
{
    // Start is called before the first frame update
    public void PlayUISound()
    {
        SoundManager.Instance.PlaySound(SoundManager.Sound.lastwave);
    }
}
