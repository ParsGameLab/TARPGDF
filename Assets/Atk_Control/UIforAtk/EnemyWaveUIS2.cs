using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class EnemyWaveUIS2 : MonoBehaviour
{
    
    [SerializeField] private EnemySpawnManagerS2 enemySponMangerS2;
    private TextMeshProUGUI waveNumberText;
    private TextMeshProUGUI waveMessageText;

    private void Awake()
    {
        waveNumberText = transform.Find("WaveNumberText").GetComponent<TextMeshProUGUI>();
        waveMessageText = transform.Find("WaveMessageText").GetComponent<TextMeshProUGUI>();
    }
    private void Start()
    {
        
        enemySponMangerS2.OnWaveNumberChangedS2 += EnemySponMangerS2_OnWaveNumberChanged;
    }

    private void EnemySponMangerS2_OnWaveNumberChanged(object sender, EventArgs e)
    {
        SetWaveNumberText(enemySponMangerS2.GetWaveNumber() + "/3");
    }

    

    private void Update()
    {
        //float nextWaveSpawnTimer = enemySponManger.GetNextWaveSpawnTimer();
        //if (nextWaveSpawnTimer <= 0f)
        //{
        //    SetMessageText("");
        //}
        //else
        //{
        //    SetMessageText("Next Wave in" + nextWaveSpawnTimer.ToString("F1") + "s");
        //}
    }
    private void SetMessageText(string message)
    {
        waveMessageText.SetText(message);
    }
    private void SetWaveNumberText(string text)
    {
        waveNumberText.SetText(text);
    }
}
