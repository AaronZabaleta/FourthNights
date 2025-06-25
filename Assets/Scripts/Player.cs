using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Rigidbody))]
public class Player : Character, IInteractable
{
    [Header("Animator")]
    [SerializeField] private string jumpTriggerName = "onJump";
    [SerializeField] private string intTriggerName = "onInteract";
    [SerializeField] private string intCrouchTriggerName = "onCrouchInteract";
    [SerializeField] private string crouchTriggerName = "onCrouch";
    [SerializeField] private string inSprintName = "Sprint";
    [SerializeField] private string airBoolName = "isOnAir";
    [SerializeField] private string crouchBoolName = "isOnGround";
    [SerializeField] private string xAxisName = "xAxis";
    [SerializeField] private string zAxisName = "zAxis";

    [Header("Inputs")]
    [SerializeField] private KeyCode jumpKey = KeyCode.Space;
    [SerializeField] private KeyCode intKey = KeyCode.E;
    [SerializeField] private KeyCode sprintKey = KeyCode.LeftShift;
    [SerializeField] private KeyCode crouchKey = KeyCode.LeftControl;

    [Header("Physics")]
    [SerializeField] private PlayerLightController lightController;
    [SerializeField] private float groundRayDistance = 1.9f;
    [SerializeField] private LayerMask groundRayMask;
    [SerializeField] private float intDistance = 5.0f;
    [SerializeField] private float intRadius = 1.0f;
    [SerializeField] private LayerMask intMask;
    [SerializeField] private float jumpForce = 2.0f;
    [SerializeField] private float moveSpeed = 3.5f;
    [SerializeField] private float sprintMultiplier = 2.0f;
    [SerializeField] private float crouchSpeed = 1.5f;
    [SerializeField] private Transform rayOrigin;

    private Animator animator;
    private Rigidbody rb;
    private Vector3 dir;
    private float currentMoveSpeed;
    private bool isGrounded;
    private bool canMove = true;

    private void Start()
    {
        animator = GetComponentInChildren<Animator>();
        rb = GetComponent<Rigidbody>();
        currentMoveSpeed = moveSpeed;
    }

    private void Update()
    {
        if (!healthComponent.IsAlive) return;
        HandleInputs();

        if (Input.GetKeyDown(KeyCode.F))
        {
            lightController.StartRecharge();
            canMove = false;
        }
        if (Input.GetKeyUp(KeyCode.F))
        {
            lightController.StopRecharge();
            canMove = true;
        }

        if (canMove)
        {
            Move(); 
        }
    }

    private void FixedUpdate()
    {
        if (canMove && dir.sqrMagnitude > 0)
            Move();

        if (!isGrounded)
            rb.AddForce(Vector3.down * 20f);
    }

    private void HandleInputs()
    {
        dir.x = Input.GetAxis("Horizontal");
        dir.z = Input.GetAxis("Vertical");

        animator.SetFloat(xAxisName, dir.x);
        animator.SetFloat(zAxisName, dir.z);

        isGrounded = CheckGrounded();
        animator.SetBool(airBoolName, !isGrounded);

        if (Input.GetKey(jumpKey) && isGrounded)
        {
            animator.SetTrigger(jumpTriggerName);
            Jump();
        }

        if (Input.GetKeyDown(intKey))
        {
            animator.SetTrigger(intTriggerName);
            OnInteract();
        }

        if (Input.GetKeyDown(crouchKey))
        {
            animator.SetTrigger(crouchTriggerName);
            animator.SetBool(crouchBoolName, true);

            if (Input.GetKeyDown(intKey))
                animator.SetTrigger(intCrouchTriggerName);
        }
        else if (!Input.GetKey(crouchKey))
        {
            animator.SetBool(crouchBoolName, false);
        }

        animator.SetBool(inSprintName, Input.GetKey(sprintKey) && dir != Vector3.zero);
    }

    public void OnInteract()
    {
        if (Physics.SphereCast(rayOrigin.position, intRadius, transform.forward, out RaycastHit hit, intDistance, intMask))
        {
            Debug.Log("Objeto detectado por SphereCast: " + hit.collider.name);

            if (hit.collider.TryGetComponent(out IInteractable interactable))
            {
                Debug.Log("Interactable encontrado, llamando OnInteract()");
                interactable.OnInteract();
            }
            else
            {
                Debug.LogWarning("El objeto no tiene IInteractable");
            }
        }
    }

    private void Move()
    {
        currentMoveSpeed = Input.GetKey(sprintKey) ? sprintMultiplier :
                           Input.GetKey(crouchKey) ? crouchSpeed :
                           moveSpeed;

        Vector3 moveDir = transform.forward * dir.z + transform.right * dir.x;
        rb.MovePosition(transform.position + moveDir.normalized * currentMoveSpeed * Time.fixedDeltaTime);
    }

    private void Jump()
    {
        rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
    }

    private bool CheckGrounded()
    {
        Vector3 rayStart = transform.position + Vector3.up * 0.1f;
        return Physics.Raycast(rayStart, Vector3.down, groundRayDistance, groundRayMask);
    }

    public void ModifySpeed(float factor)
    {
        moveSpeed *= factor;
    }

    public void ResetSpeed()
    {
        moveSpeed = 3.5f; 
    }

    private void OnDrawGizmos()
    {
        if (rayOrigin == null) return;

        Gizmos.color = Color.yellow;
        Vector3 direction = transform.forward * intDistance;
        Gizmos.DrawWireSphere(rayOrigin.position + direction, intRadius);
    }
}

