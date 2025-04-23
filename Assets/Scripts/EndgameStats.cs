using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using TMPro;

public class EndgameStats : MonoBehaviour
{
    // Stats being tracked
    private int waveGottenTo;
    private int totalTicketsAccrued;
    private int totalZombiesKilled;

    [SerializeField] private TextMeshProUGUI wave;
    [SerializeField] private TextMeshProUGUI zombiesKilled;
    [SerializeField] private TextMeshProUGUI ticketsGained;

    public void StatsUpdate()
    {
        WaveStat();
        TotalKilledStat();
        TotalTicketsStat();
    }
    
    // Get the wave gotten to
    void WaveStat()
    {
        waveGottenTo = ZombieManager.waveCount;

        wave.text = waveGottenTo.ToString();
    }

    // Get the total Zombies killed
    void TotalKilledStat()
    {
        totalZombiesKilled = ZombieManager.totalZombiesKilled;

        zombiesKilled.text = totalZombiesKilled.ToString();
    }

    // Get the total tickets gained
    void TotalTicketsStat()
    {
        totalTicketsAccrued = ticketGiverScript.totTicketsGained;

        ticketsGained.text = totalTicketsAccrued.ToString();
    }
}
