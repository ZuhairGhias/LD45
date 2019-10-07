using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

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
    [SerializeField] private float heatCooldownModifier = 1.2f;
    [SerializeField] private float pickpocketHeat = 0.15f;

    [Header("Upgrade Modifiers")]
    [SerializeField] private float stealAmountMultiplier = 5.5f;

    [SerializeField] private float stealDelayMultiplier = 0.75f;

    [SerializeField] private float moveSpeedMultiplier = 1.25f;

    [SerializeField] private float hideTimeMultiplier = 1.5f;

    [Header("Game Over Settings")]
    [SerializeField] private Image blackScreen;
    [SerializeField] private TextMeshProUGUI continuePrompt;
    [SerializeField] private float blackoutDelay;
    [SerializeField] private float continueDelay;
    [SerializeField] private AudioSource soundtrackSource;
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private AudioClip goteem;
    [SerializeField] private AudioClip gunReload;
    [SerializeField] private AudioClip gunshot;

    private GameState currentState = GameState.PREPARING;
    private int currentRound = 1;
    private float timeRemaining;
    PlayerController player;

    private void Awake()
    {
        Pedestrian.OnPickpocket += StealMoney;
    }

    private void Start()
    {
        player = FindObjectOfType<PlayerController>();

        Inventory.Reset();
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
            shopCanvas.gameObject.SetActive(false);
            currentHeat = 0f;

            spawnSystem.StartSpawning();
            StartCoroutine(CooldownHeat());

            if (Inventory.HasBoots) player.moveSpeedMultiplier = moveSpeedMultiplier;
            if (Inventory.HasGloves) player.stealDelayMultiplier = stealDelayMultiplier;
            if (Inventory.HasGuide) player.stealAmountMultiplier = stealAmountMultiplier;
            if (Inventory.HasHoodie) player.hideTimeMultiplier = hideTimeMultiplier;
        }
    }

    public void EndRound(bool success)
    {
        spawnSystem.StopSpawning();

        if (success)
        {
            shopCanvas.gameObject.SetActive(true);
            currentState = GameState.UPGRADING;
            currentRound++;
        }
        else
        {
            currentState = GameState.GAMEOVER;
            StartCoroutine(GameOverRoutine());
        }
    }

    private void StealMoney(int moneyStolen)
    {
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

                // FindGameObjectWithTag is very spooky when in the Update method so let's avoid it in the future
                // if(GameObject.FindGameObjectWithTag("NPC") == null) EndRound(true);

                if (!spawnSystem.HasNPCs())
                {
                    EndRound(true);
                }
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
            float multiplier = 1f;
            if (Inventory.HasBadge) multiplier = heatCooldownModifier;
            AdjustHeat(-heatPassiveCooldown * Time.deltaTime * heatCooldownModifier);

            yield return null;
        }
    }

    private IEnumerator GameOverRoutine()
    {
        audioSource.clip = gunReload;
        audioSource.Play();

        audioSource.PlayOneShot(goteem);

        yield return new WaitForSeconds(blackoutDelay);

        blackScreen.gameObject.SetActive(true);
        audioSource.clip = gunshot;
        audioSource.Play();
        soundtrackSource.Stop();

        yield return new WaitForSeconds(continueDelay);

        continuePrompt.gameObject.SetActive(true);
        while (true)
        {
            if (Input.GetKeyDown("space"))
            {
                SceneManager.LoadScene("GameScene");
            }

            yield return null;
        }
    }

    public void RevolverBought()
    {
        SceneManager.LoadScene("IntroScene");
    }
}
