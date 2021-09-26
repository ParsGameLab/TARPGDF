using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

public class EnemyWaveUI : MonoBehaviour
{
    [SerializeField] private EnemySponManerger enemySponManger;
    private TextMeshProUGUI waveNumberText;
    private TextMeshProUGUI waveMessageText;

    private void Awake()
    {
        waveNumberText=transform.Find("WaveNumberText").GetComponent<TextMeshProUGUI>();
        waveMessageText=transform.Find("WaveMessageText").GetComponent<TextMeshProUGUI>();
    }
    private void Start()
    {
        enemySponManger.OnWaveNumberChanged += EnemyWaveManager_OnWaveNumberChaged;
    }

    private void EnemyWaveManager_OnWaveNumberChaged(object sender, EventArgs e)
    {
        SetWaveNumberText("Wave"+enemySponManger.GetWaveNumber());
    }

    private void Update()
    {
        float nextWaveSpawnTimer = enemySponManger.GetNextWaveSpawnTimer();
        if (nextWaveSpawnTimer <= 0f)
        {
            SetMessageText("");
        }
        else
        {
            SetMessageText("Next Wave in" + nextWaveSpawnTimer.ToString("F1") + "s");
        }
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
