using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public enum GameState { PREPARING, INPROGRESS, UPGRADING, GAMEOVER }

    [Header("Game System")]
    [SerializeField] private SpawnSystem spawnSystem;
    [SerializeField] private Canvas shopCanvas;
    [SerializeField] private DynamicSky dynamicSky;

    [Header("Timer Settings")]
    [SerializeField] private TimerUI timerUI;
    [SerializeField] private float roundTimer = 20f;

    [Header("Heat Bar Settings")]
    [SerializeField] private HeatBarUI heatBarUI;
    [SerializeField] private float currentHeat = 0f;
    [SerializeField] private float heatPassiveCooldown = 0.1f;
    [SerializeField] private float pickpocketHeat = 0.15f;

    [Header("Upgrade Modifiers")]
    [SerializeField] private float stealAmountMultiplier = 4f;

    [SerializeField] private float stealDelayMultiplier = 0.75f;

    [SerializeField] private float moveSpeedMultiplier = 1.25f;

    private GameState currentState = GameState.PREPARING;
    private int currentRound = 1;
    private float timeRemaining;

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
            Debug.Log("[GameManager] Starting round " + currentRound.ToString());

            currentState = GameState.INPROGRESS;
            timeRemaining = roundTimer;
            shopCanvas.enabled = false;

            spawnSystem.StartSpawning();
            StartCoroutine(CooldownHeat());

            PlayerController player = GameManager.FindObjectOfType<PlayerController>();
            if (Inventory.HasBoots) player.moveSpeedMultiplier = moveSpeedMultiplier;
            if (Inventory.HasGloves) player.stealDelayMultiplier = stealDelayMultiplier;
            if (Inventory.HasGuide) player.stealAmountMultiplier = stealAmountMultiplier;

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
                spawnSystem.StopSpawning();
                if(GameObject.FindGameObjectWithTag("NPC") == null) EndRound(true);
            }
            timeRemaining = Mathf.Clamp(timeRemaining, 0f, Mathf.Infinity);
            dynamicSky.UpdateSky(timeRemaining / roundTimer);
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
