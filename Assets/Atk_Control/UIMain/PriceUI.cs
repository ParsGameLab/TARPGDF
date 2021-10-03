using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PriceUI : MonoBehaviour
{
    public TextMeshProUGUI needlepricetext;
    public TextMeshProUGUI knifepricetext;
    public TextMeshProUGUI sawpricetext;
    public TextMeshProUGUI hammerpricetext;

    public PlacedObjectTypeSO needleplacedObjectTypeSO;
    public PlacedObjectTypeSO knifeplacedObjectTypeSO;
    public PlacedObjectTypeSO sawplacedObjectTypeSO;
    public PlacedObjectTypeSO hammerplacedObjectTypeSO;

    private Color used;

    private ITrapCustomer trapBuyer;


    private void Awake()
    {
        //needlepricetext.text = needleplacedObjectTypeSO.GetCost.ToString();
        //knifericetext.text = knifeplacedObjectTypeSO.GetCost.ToString();
        //sawpricetext.text = sawplacedObjectTypeSO.GetCost.ToString();
        //hammerpricetext.text = hammerplacedObjectTypeSO.GetCost.ToString();

        //needlepricetext.color= normal;
        //knifericetext.color = normal;
        //sawpricetext.color = normal;
        //hammerpricetext.color = normal;
        needlepricetext = transform.Find("needleprice").GetComponent<TextMeshProUGUI>();
        knifepricetext = transform.Find("knifeprice").GetComponent<TextMeshProUGUI>();
        sawpricetext = transform.Find("sawprice").GetComponent<TextMeshProUGUI>();
        hammerpricetext = transform.Find("hammerprice").GetComponent<TextMeshProUGUI>();
        used=hammerpricetext.color;//顏色爆了?

    }
    void Start()
    {
        trapBuyer = GameObject.FindWithTag("Player").GetComponent<ITrapCustomer>();
        SetText(needlepricetext, needleplacedObjectTypeSO);
        SetText(knifepricetext, knifeplacedObjectTypeSO);
        SetText(sawpricetext, sawplacedObjectTypeSO);
        SetText(hammerpricetext, hammerplacedObjectTypeSO);
    }

    // Update is called once per frame
    void Update()
    {
        CanColor(needlepricetext, needleplacedObjectTypeSO);  
        CanColor(knifepricetext, knifeplacedObjectTypeSO);
        CanColor(sawpricetext, sawplacedObjectTypeSO);
        CanColor(hammerpricetext, hammerplacedObjectTypeSO);

        

    }
    public void CanColor(TextMeshProUGUI text, PlacedObjectTypeSO pso)
    {
        //text.color = normal;
        if (trapBuyer.CanBuyTrap(pso.GetCost))
        {
            //text.color = normal;
            text.text = pso.GetCost.ToString();
            text.color = used;
            Debug.Log("TEXT Nor");
            return;
        }
        else
        {
            Debug.Log("TEXT RED");
            //text.text = pso.GetCost.ToString();
            text.text = pso.GetCost.ToString();
            text.color = Color.red;
            //變色
        }
    }
    public void CanTextColor(Text text, PlacedObjectTypeSO pso)
    {
        //text.color = normal;
        //text.color = normal;
        if (trapBuyer.CanBuyTrap(pso.GetCost))
        {
            text.color = used;
            Debug.Log("TEXT Nor");
            return;
        }
        else
        {
            Debug.Log("TEXT RED");
            text.text = pso.GetCost.ToString();
            text.color = Color.red;
            //變色
        }
    }
    public void SetText(TextMeshProUGUI text, PlacedObjectTypeSO pso)
    {
        text.text = pso.GetCost.ToString();
    }
}
