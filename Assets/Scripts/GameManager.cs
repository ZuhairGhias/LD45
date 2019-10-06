using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public enum GameState { PREPARING, INPROGRESS, UPGRADING, GAMEOVER }

    [Header("Game System")]
    [SerializeField] private SpawnSystem spawnSystem;
    [SerializeField] private Canvas shopCanvas;

    [Header("Timer Settings")]
    [SerializeField] private TimerUI timerUI;
    [SerializeField] private float timeRemaining = 20f;

    [Header("Heat Bar Settings")]
    [SerializeField] private HeatBarUI heatBarUI;
    [SerializeField] private float currentHeat = 0f;
    [SerializeField] private float heatPassiveCooldown = 0.1f;
    [SerializeField] private float pickpocketHeat = 0.15f;

    private GameState currentState = GameState.PREPARING;
    private int currentRound = 0;

    private void Awake()
    {
        Pedestrian.OnPickpocket += StealMoney;
    }

    private void Start()
    {
        StartRound();
    }

    private void Update()
    {
        UpdateTimer();
    }

    public float GetCurrentHeat()
    {
        return currentHeat;
    }

    public void StartRound()
    {
        if (currentState != GameState.INPROGRESS)
        {
            currentState = GameState.INPROGRESS;
            currentRound++;

            spawnSystem.StartSpawning();
            StartCoroutine(CooldownHeat());
        }
    }

    public void EndRound(bool success)
    {
        spawnSystem.StopSpawning();

        if (success)
        {
            Debug.Log("[GameManager] Round complete, upgrading phase");

            shopCanvas.enabled = true;
            currentState = GameState.UPGRADING;
            currentRound++;
        }
        else
        {
            Debug.Log("[GameManager] Game over, you lose");

            currentState = GameState.GAMEOVER;
        }
    }

    private void StealMoney(int moneyStolen)
    {
        Debug.Log("[GameManager] $" + moneyStolen + " stolen!");

        if (moneyStolen > 0)
        {
            Inventory.Money += moneyStolen;
            AdjustHeat(pickpocketHeat);
        }
    }

    private void AdjustHeat(float delta)
    {
        currentHeat = Mathf.Clamp(currentHeat + delta, 0f, 1f);
        heatBarUI.UpdateHeat(currentHeat);
    }

    private void UpdateTimer()
    {
        if (currentState == GameState.INPROGRESS)
        {
            timeRemaining -= Time.deltaTime;

            if (timeRemaining <= 0f)
            {
                EndRound(true);
            }

            timerUI.UpdateTimer(timeRemaining);
        }
    }

    private IEnumerator CooldownHeat()
    {
        while (currentState == GameState.INPROGRESS)
        {
            AdjustHeat(-heatPassiveCooldown * Time.deltaTime);

            yield return null;
        }
    }
}
