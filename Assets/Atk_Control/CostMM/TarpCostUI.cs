using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TarpCostUI : MonoBehaviour
{
    public TextMeshProUGUI coinText;
    private void Awake()
    {
        //Transform coinTemplate = transform.Find("CoinTemplate");
        //coinText = transform.Find("cointext").GetComponent<TextMeshProUGUI>();
    }
    private void Start()
    {
        UpdateText();
        Player.Instance.OnCoinAmountChange += Instance_OnCoinAmountChanged;
        
    }
    private void Instance_OnCoinAmountChanged(object sender, System.EventArgs e)
    {
        UpdateText();
    }

    private void UpdateText()
    {
        coinText.text = Player.Instance.GetCoinAmount().ToString();
        
    }

}
