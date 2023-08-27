using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float moveSpeed;

    [SerializeField] private Vector2 direction;

    private Rigidbody2D rb2D;

    private bool isMoving;

    private float moveX;

    private float moveY;

    private Animator animator;

    [SerializeField] private LayerMask interactableLayer;

    private void Awake()
    {
        animator = GetComponent<Animator>();
        rb2D = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {


        moveX = Input.GetAxisRaw("Horizontal");
        moveY = Input.GetAxisRaw("Vertical");
        animator.SetFloat("moveX", moveX);
        animator.SetFloat("moveY", moveY);

        if (moveX != 0 || moveY != 0)
        {
            animator.SetFloat("lastX", moveX);
            animator.SetFloat("lastY", moveY);
        }

        animator.SetBool("isMoving", isMoving);

        direction = new Vector2(moveX, moveY).normalized;

        if (Input.GetKeyDown(KeyCode.Z))
        {
            Interact();
        }
    }

    private void Interact()
    {
        var facingdir = new Vector3(animator.GetFloat("lastX"), animator.GetFloat("lastY"));
        var interactPos = transform.position + facingdir * 0.1f;

        var collider = Physics2D.OverlapCircle(interactPos, 0.2f, interactableLayer);
        if (collider != null)
        {
            collider.GetComponent<Interactable>()?.Interact();
        }
    }

    private void FixedUpdate()
    {
        rb2D.MovePosition(rb2D.position + direction * moveSpeed * Time.fixedDeltaTime);
    }
}
