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
    
    [Header("Hiding Settings")]
    [SerializeField] private float hideMaxTime = 5f;
    [SerializeField] private Color hiddenPlayerColor;
    [SerializeField] private Canvas cooldownCanvas;
    [SerializeField] private Image cooldownBar;

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

    private PlayerState currentState = PlayerState.WALKING;

    void Start()
    {
        stealAmountMultiplier = 1;
        stealDelayMultiplier = 1;
        moveSpeedMultiplier = 1;

        gameManager = FindObjectOfType<GameManager>();

        rigidBody = GetComponent<Rigidbody2D>();
        playerCollider = GetComponent<Collider2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();

        colliderOverlaps = new List<Collider2D>();

        currentHidingCooldown = hideMaxTime;
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

        currentHidingCooldown = Mathf.Clamp(currentHidingCooldown, 0f, hideMaxTime);
        cooldownBar.fillAmount = currentHidingCooldown / hideMaxTime;

        if (currentHidingCooldown == 0)
        {
            ToggleHide();

            cooldownExpired = true;
            cooldownBar.color = Color.red;
        }
        else if (currentHidingCooldown == hideMaxTime)
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
                    Debug.Log("[PlayerController] Player is hiding");

                    currentState = PlayerState.HIDING;

                    transform.position = transform.position + Vector3.up * 0.5f;
                    spriteRenderer.color = hiddenPlayerColor;
                    playerCollider.enabled = false;
                    
                }
            }
        }
        else if (currentState == PlayerState.HIDING)
        {
            Debug.Log("[PlayerController] Player is coming out of hiding");

            transform.position = transform.position + Vector3.down * 0.5f;
            spriteRenderer.color = Color.white;
            playerCollider.enabled = true;

            currentState = PlayerState.WALKING;
        }
    }

    public void Arrest()
    {
        currentState = PlayerState.ARRESTED;
        playerCollider.enabled = false;

        gameManager.EndRound(false);
        
        Debug.Log("[PlayerController] Player has been arrested");
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
        Debug.Log("[PlayerController] Player is attempting to steal...");
        currentState = PlayerState.STEALING;

        yield return new WaitForSeconds(stealDelay * stealDelayMultiplier);

        playerCollider.OverlapCollider(contactFilter, colliderOverlaps);
        foreach(Collider2D collider in colliderOverlaps)
        {
            if (collider.GetComponent<Pedestrian>() != null)
            {
                int moneyStolen = collider.GetComponent<Pedestrian>().Pickpocket(stealAmountMultiplier);
            }
        }
        if (currentState == PlayerState.STEALING) currentState = PlayerState.WALKING;
    }
}
