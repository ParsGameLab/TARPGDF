using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PortalFun2 : MonoBehaviour
{
    public Transform targetdoor;
    public GameObject circleEffect;
    public GameObject targetEffect;
    public GameObject targetPortEffect;
    public GameObject QHint;
    private bool CanPort;
    private GameObject m_player;
    private float Timer = 2f;
    public Transform player;
    private CharacterController CCC;
    // Start is called before the first frame update
    void Start()
    {
        CanPort = false;
        m_player = Main.m_Instance.GetPlayer();
    }

    // Update is called once per frame
    void LateUpdate()
    {
        if (CanPort == true)
        {
            circleEffect.SetActive(true);
            targetEffect.SetActive(true);
            QHint.SetActive(true);
            print("Press Q to teleport");
            if (Input.GetKeyDown(KeyCode.Q))
            {
                CCC.enabled = false;
                //player.transform.position = targetdoor.transform.position;
                m_player.transform.position = targetdoor.transform.position;
                CCC.enabled = true;
                //Player.Instance.PortCharacter(targetdoor.transform.position);
                var instance = Instantiate(targetPortEffect, targetdoor.position, targetdoor.rotation);
                Destroy(instance, 2f);
                CanPort = false;
            }
        }
        else
        {
            QHint.SetActive(false);
            targetEffect.SetActive(false);
            circleEffect.SetActive(false);
        }
    }
    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            CanPort = true;
            CCC = other.gameObject.GetComponent<CharacterController>();
        }
    }
    void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            CanPort = false;

        }
    }
}
