using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public enum PlayerState { WALKING, STEALING, HIDING, ARRESTED }

    [SerializeField] private float moveSpeed = 10f;
    [SerializeField] private float stealDelay = 1f;
    [SerializeField] private Color hiddenColor;
    [SerializeField] private ContactFilter2D contactFilter;
    
    private GameManager gameManager;
    private Rigidbody2D rigidBody;
    private Collider2D playerCollider;
    private SpriteRenderer spriteRenderer;
    private List<Collider2D> colliderOverlaps;
    
    private PlayerState currentState = PlayerState.WALKING;

    void Start()
    {
        gameManager = FindObjectOfType<GameManager>();

        rigidBody = GetComponent<Rigidbody2D>();
        playerCollider = GetComponent<Collider2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();

        colliderOverlaps = new List<Collider2D>();
    }

    public void Move(float direction)
    {
        if (currentState == PlayerState.WALKING)
        {
            rigidBody.MovePosition(new Vector2(transform.position.x, transform.position.y) + Vector2.right * direction * moveSpeed * Time.deltaTime);
        }
    }

    public void ToggleHide()
    {
        if (currentState == PlayerState.WALKING)
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
                    spriteRenderer.color = hiddenColor;
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

        yield return new WaitForSeconds(stealDelay);

        playerCollider.OverlapCollider(contactFilter, colliderOverlaps);
        foreach(Collider2D collider in colliderOverlaps)
        {
            if (collider.GetComponent<Pedestrian>() != null)
            {
                collider.GetComponent<Pedestrian>().Pickpocket();
            }
        }

        currentState = PlayerState.WALKING;
    }
}
