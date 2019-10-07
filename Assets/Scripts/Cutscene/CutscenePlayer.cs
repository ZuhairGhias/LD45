using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CutscenePlayer : MonoBehaviour
{
    public enum PlayerState { WAITING, ALIVE, DEAD }

    [SerializeField] private Animator animator;
    [SerializeField] private SpriteRenderer spriteRenderer;
    [SerializeField] private float moveSpeed = 4f;

    private Rigidbody2D rigidBody;
    private Collider2D playerCollider;
    private float direction = 1f;

    private PlayerState currentState = PlayerState.WAITING;

    private void Start()
    {
        rigidBody = GetComponent<Rigidbody2D>();
        playerCollider = GetComponent<Collider2D>();
    }

    private void Update()
    {
        if (currentState == PlayerState.ALIVE)
        {
            direction = Input.GetAxis("Horizontal");
            rigidBody.MovePosition(new Vector2(transform.position.x, transform.position.y) + Vector2.right * direction * moveSpeed * Time.fixedDeltaTime);

            if (direction > 0)
            {
                spriteRenderer.flipX = false;
            }
            else if (direction < 0)
            {
                spriteRenderer.flipX = true;
            }

            animator.SetFloat("walkSpeed", Mathf.Abs(direction));
        }
    }

    public void EnablePlayer()
    {
        currentState = PlayerState.ALIVE;
    }

    public void KillPlayer()
    {
        currentState = PlayerState.DEAD;
        animator.SetFloat("walkSpeed", 0f);
    }
}
