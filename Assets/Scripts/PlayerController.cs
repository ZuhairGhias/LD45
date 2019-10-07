using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    public enum PlayerState { WALKING, STEALING, HIDING, ARRESTED }

    [Header("General Settings")]
    [SerializeField] private float moveSpeed = 10f;
    [SerializeField] private float stealDelay = 1f;
    [SerializeField] private ContactFilter2D contactFilter;
    [SerializeField] private LootEffect lootEffect;

    [Header("Hiding Settings")]
    [SerializeField] private float hideMaxTime = 5f;
    [SerializeField] private Color hiddenPlayerColor;
    [SerializeField] private Canvas cooldownCanvas;
    [SerializeField] private Image cooldownBar;

    [Header("Animator Settings")]
    [SerializeField] Animator animator;
    [SerializeField] SpriteRenderer sr;


    private GameManager gameManager;
    private Rigidbody2D rigidBody;
    private Collider2D playerCollider;
    private SpriteRenderer spriteRenderer;
    private List<Collider2D> colliderOverlaps;
    private float currentHidingCooldown;
    private bool cooldownExpired = false;

    [HideInInspector] public float stealAmountMultiplier { get; set;}
    [HideInInspector] public float stealDelayMultiplier { get; set; }
    [HideInInspector] public float moveSpeedMultiplier { get; set; }
    [HideInInspector] public float hideTimeMultiplier { get; set; }

    private PlayerState currentState = PlayerState.WALKING;

    void Start()
    {
        stealAmountMultiplier = 1;
        stealDelayMultiplier = 1;
        moveSpeedMultiplier = 1;
        hideTimeMultiplier = 1;

        gameManager = FindObjectOfType<GameManager>();

        rigidBody = GetComponent<Rigidbody2D>();
        playerCollider = GetComponent<Collider2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();

        colliderOverlaps = new List<Collider2D>();

        currentHidingCooldown = hideMaxTime;

        animator.SetFloat("stealTime", 1/stealDelay);
    }

    private void Update()
    {
        UpdateCooldown();
    }

    private void UpdateCooldown()
    {

        if (currentState == PlayerState.HIDING)
        {
            currentHidingCooldown -= Time.deltaTime;
        }
        else
        {
            currentHidingCooldown += Time.deltaTime;
        }

        currentHidingCooldown = Mathf.Clamp(currentHidingCooldown, 0f, hideMaxTime * hideTimeMultiplier);
        cooldownBar.fillAmount = currentHidingCooldown / (hideMaxTime * hideTimeMultiplier);

        if (currentHidingCooldown == 0)
        {
            ToggleHide();

            cooldownExpired = true;
            cooldownBar.color = Color.red;
        }
        else if (currentHidingCooldown == hideMaxTime * hideTimeMultiplier)
        {
            cooldownExpired = false;
            cooldownBar.color = Color.white;
            cooldownCanvas.enabled = false;
        }
        else
        {
            cooldownCanvas.enabled = true;
        }
    }

    public void Move(float direction)
    {
        if (currentState == PlayerState.WALKING)
        {
            rigidBody.MovePosition(new Vector2(transform.position.x, transform.position.y) + Vector2.right * direction * moveSpeed * moveSpeedMultiplier* Time.deltaTime);
            if(direction > 0)
            {
                sr.flipX = false;
            }
            else if(direction < 0)
            {
                sr.flipX = true;
            }
            animator.SetFloat("walkSpeed", Mathf.Abs(direction));
        }
    }

    public void ToggleHide()
    {
        if (currentState == PlayerState.WALKING && !cooldownExpired)
        {
            // Check if the player is standing in an alleyway
            playerCollider.OverlapCollider(contactFilter, colliderOverlaps);
            foreach (Collider2D collider in colliderOverlaps)
            {
                if (collider.CompareTag("Alleyway"))
                {
                    currentState = PlayerState.HIDING;

                    transform.position = transform.position + Vector3.up * 0.5f;
                    spriteRenderer.color = hiddenPlayerColor;
                    playerCollider.enabled = false;
                    
                }
            }
            animator.SetFloat("walkSpeed", 0);
        }
        else if (currentState == PlayerState.HIDING)
        {
            transform.position = transform.position + Vector3.down * 0.5f;
            spriteRenderer.color = Color.white;
            playerCollider.enabled = true;

            currentState = PlayerState.WALKING;
        }
    }

    public void Arrest()
    {
        animator.SetFloat("walkSpeed", 0);
        currentState = PlayerState.ARRESTED;
        playerCollider.enabled = false;

        gameManager.EndRound(false);
    }

    public void Steal()
    {
        if (currentState == PlayerState.WALKING)
        {
            StartCoroutine(StealRoutine());
        }
    }

    private IEnumerator StealRoutine()
    {
        currentState = PlayerState.STEALING;
        animator.SetBool("isStealing", true);

        yield return new WaitForSeconds(stealDelay * stealDelayMultiplier);
        
        int totalMoneyStolen = 0;
        int pedestrianCount = 0;

        playerCollider.OverlapCollider(contactFilter, colliderOverlaps);
        foreach(Collider2D collider in colliderOverlaps)
        {
            if (collider.GetComponent<Pedestrian>() != null)
            {
                int moneyStolen = collider.GetComponent<Pedestrian>().Pickpocket(stealAmountMultiplier);

                if (moneyStolen > 0)
                {
                    pedestrianCount++;
                    totalMoneyStolen += moneyStolen;
                }
            }
        }

        // If the player steals from more than 1 person at once, multiply their loot
        if (pedestrianCount > 1)
        {
            totalMoneyStolen *= (pedestrianCount + 1);
        }

        if (totalMoneyStolen > 0)
        {
            LootEffect effect = Instantiate(lootEffect, transform.position, Quaternion.identity);
            effect.SetText("$" + totalMoneyStolen);
        }

        if (currentState == PlayerState.STEALING)
        {
            currentState = PlayerState.WALKING;
        }

        animator.SetBool("isStealing", false);
    }
}
