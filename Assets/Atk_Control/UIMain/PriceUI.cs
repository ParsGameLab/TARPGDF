using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PriceUI : MonoBehaviour
{
    public TextMeshProUGUI needlepricetext;
    public TextMeshProUGUI knifericetext;
    public TextMeshProUGUI sawpricetext;
    public TextMeshProUGUI hammerpricetext;

    public PlacedObjectTypeSO needleplacedObjectTypeSO;
    public PlacedObjectTypeSO knifeplacedObjectTypeSO;
    public PlacedObjectTypeSO sawplacedObjectTypeSO;
    public PlacedObjectTypeSO hammerplacedObjectTypeSO;

    public Color red;
    public Color normal;

    private ITrapCustomer trapBuyer;


    private void Awake()
    {
        //needlepricetext.text = needleplacedObjectTypeSO.GetCost.ToString();
        //knifericetext.text = knifeplacedObjectTypeSO.GetCost.ToString();
        //sawpricetext.text = sawplacedObjectTypeSO.GetCost.ToString();
        //hammerpricetext.text = hammerplacedObjectTypeSO.GetCost.ToString();

        needlepricetext.color= normal;
        knifericetext.color = normal;
        sawpricetext.color = normal;
        hammerpricetext.color = normal;
    }
    void Start()
    {
        trapBuyer = GameObject.FindWithTag("Player").GetComponent<ITrapCustomer>();
    }

    // Update is called once per frame
    void Update()
    {
        CanColor(needlepricetext, needleplacedObjectTypeSO);
        CanColor(knifericetext, knifeplacedObjectTypeSO);
        CanColor(sawpricetext, sawplacedObjectTypeSO);
        CanColor(hammerpricetext, hammerplacedObjectTypeSO);
    }
    public void CanColor(TextMeshProUGUI text, PlacedObjectTypeSO pso)
    {
        if (trapBuyer.CanBuyTrap(pso.GetCost))
        {
            text.color = normal;
            return;
        }
        else
        {
            text.color = red;
            //Εά¦β
        }
    }
}
