using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Rigidbody))]
public class Player : CharacterBehaviour
{

    [Header("Animator")]
    [SerializeField] private string _airBoolName = "isOnAir";
    [SerializeField] private string _jumpTriggerName = "onJump";
    [SerializeField] private string _atkTriggerName = "onAttack";
    [SerializeField] private string _intTriggerName = "onInteract";
    [SerializeField] private string _intCrouchTriggerName = "onCrouchInteract";
    [SerializeField] private string _inSprintName = "Sprint";
    [SerializeField] private string _crouchBoolName = "isOnGround";
    [SerializeField] private string _crouchTriggerName = "onCrouch";
    
    [SerializeField] private string _xAxisName = "xAxis";
    [SerializeField] private string _zAxisName = "zAxis";

    [Header("Gameplay")]
    [SerializeField] private int _atkDmg = 10;
   
    [Header("Inputs")]
    [SerializeField] private KeyCode _jumpKey = KeyCode.Space;
    [SerializeField] private KeyCode _atkKey = KeyCode.Mouse0;
    [SerializeField] private KeyCode _intKey = KeyCode.E;
    [SerializeField] private KeyCode _sprintKey = KeyCode.LeftShift;
    [SerializeField] private KeyCode _crouchKey = KeyCode.LeftControl;

    [Header("Physics")]
    [SerializeField] private float _groundRayDistance = 1.9f;
    [SerializeField] private LayerMask _groundRayMask;
    [SerializeField] private float _atkDistance = 2.0f;
    [SerializeField] private LayerMask _atkMask;
    [SerializeField] private float _intDistance = 5.0f;
    [SerializeField] private float _intRadius = 1.0f;
    [SerializeField] private LayerMask _intMask;
    [SerializeField] private float _jumpForce = 2.0f;
    [SerializeField] private float _moveSpeed = 3.5f;
    [SerializeField] private float _sprintMultiplier = 2.0f; 
    [SerializeField] private float _crouchSpeed = 1.5f;
    [SerializeField] private Transform _rayOrigin;
    [SerializeField] private float currentMoveSpeed;

    [Header("Vida")]
    [SerializeField] public float Life = 10;

    [Header("Trampas")]
    [SerializeField] private float MoveAlquitran = 0.75f;
    [SerializeField] private bool RangoLlave = false;
    [SerializeField] private bool TengoLlave = false;
    [SerializeField] private bool CollisionPuerta = false;


    private bool _isGrounded = false;
    float TiempoAtaque;
    public float TiempoLimiteAtaque;

    public GameObject Llave;
    public GameObject Puerta;
    public GameObject Flecha;
    public GameObject EnemigoCapsula;
    public GameObject EnemySpawn;

    public float lifeState;

    private Vector3 _dir = new(), _posOffset = new();

    private Animator _animator;
    private Rigidbody _rb;

    private Ray _atkRay, _intRay, _groundRay;
    private RaycastHit _atkHit, _intHit;
    


    protected override void Awake()
    {
        base.Awake();

        _rb = GetComponent<Rigidbody>();
        
    }

    protected override void Start()
    {
        _animator = GetComponentInChildren<Animator>();

        currentMoveSpeed = _moveSpeed;
        
           
    }

    protected override void Update()
    {
        if (RangoLlave && Input.GetKeyDown(KeyCode.E))
        {
            Destroy(Llave);

            Debug.Log("Tengo la llave");

            TengoLlave = true;

            EnemySpawn.SetActive(true);

        }
        if (Life == 0)
        {
            Morir();
        }


        if (CollisionPuerta == true && Input.GetKeyDown(KeyCode.E))
        {
            if (TengoLlave == true)
            {
                Destroy(Puerta);

                Debug.Log("Puerta abierta");
            }
            else
            {
                Debug.Log("Falta una llave");
            }
        }

        base.Update();

        _dir.x = Input.GetAxis("Horizontal");
        _dir.z = Input.GetAxis("Vertical");   

        _animator.SetFloat(_xAxisName, _dir.x);
        _animator.SetFloat(_zAxisName, _dir.z);


        _isGrounded = IsGrounded();

        _animator.SetBool(_airBoolName, !_isGrounded);

        if (Input.GetKey(_jumpKey) && _isGrounded)
        {
            _animator.SetTrigger(_jumpTriggerName);
            Jump();
           
        }
        else if (Input.GetKeyDown(_intKey))
        {
           
            _animator.SetTrigger(_intTriggerName);
        }
        else if (Input.GetKeyDown(_atkKey))
        {
            
            _animator.SetTrigger(_atkTriggerName);
        }


        else if (Input.GetKeyDown(_crouchKey))
        {
           
            _animator.SetTrigger(_crouchTriggerName);
            _animator.SetBool(_crouchBoolName, true);

            if (Input.GetKeyDown(_crouchKey) && Input.GetKeyDown(_intKey))
            {

                _animator.SetTrigger(_intCrouchTriggerName);
            }
        }

        else if (!Input.GetKey(_crouchKey))
        {
            _animator.SetBool(_crouchBoolName, false);
        }

        if (Input.GetKeyDown(_sprintKey) && (_dir != Vector3.zero))
        {
           
            _animator.SetBool(_inSprintName, true);
            
        }

        else if (!Input.GetKey(_sprintKey) || (_dir == Vector3.zero))
        {
            _animator.SetBool(_inSprintName, false);

        }
    }

    protected override void FixedUpdate()
    {
        base.FixedUpdate();

        if ( _dir.sqrMagnitude != 0.0f)
        {
            Movement(_dir);
        }
        if (!_isGrounded)
        {
            _rb.AddForce(Vector3.down * 20f); 
        }

    }

    public void Attack()
    {
        
        _atkRay = new Ray(_rayOrigin.position, transform.forward);

        if (Physics.Raycast(_atkRay, out _atkHit, _atkDistance, _atkMask))
        {
            if(_atkHit.collider.TryGetComponent(out CharacterBehaviour entity))
            {
                
                entity.TakeDamage(_atkDmg);
            }
        }
    }

    public void Interact()
    {
        Debug.LogWarning("ta.");

        _intRay = new Ray(_rayOrigin.position, transform.forward);

        if (Physics.SphereCast(_intRay, _intRadius, out _intHit, _intDistance, _intMask))
        {
           

            Interactable interactable = _intHit.collider.GetComponentInParent<Interactable>();

            if (interactable != null)
            {
               
                interactable.OnInteract();
            }
           
        }
       
    }

    private bool IsGrounded()
    {
        _posOffset = new Vector3(transform.position.x, transform.position.y + 0.1f, transform.position.z);
        _groundRay = new Ray(_posOffset, -transform.up);

        Debug.DrawRay(_groundRay.origin, _groundRay.direction * _groundRayDistance, Color.red);

        return Physics.Raycast(_groundRay, _groundRayDistance, _groundRayMask);
    }

    private void Jump()
    {

        _rb.AddForce(transform.up * _jumpForce, ForceMode.Impulse);

    }
    

    
    private void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.name == "Flecha")
        {
            Life -= 2;

            Debug.Log("-Auch- Tenes " + Life + " de vida");
        }
        if (collision.gameObject.name == "EnemigoCapsula")
        {
            TiempoAtaque = Time.deltaTime;

            if (TiempoAtaque >= TiempoLimiteAtaque)
            {
                Life -= 2;
                Debug.Log("Recibo danio Tenes " + Life + " de vida");
                TiempoAtaque = 0;
            }
        }

        if (collision.CompareTag("PuertaNivel")) 
        {
            CollisionPuerta = true;

            Debug.Log("Abrir Puerta, Presionar 'E'");
        }

        if (collision.CompareTag("Llave"))
        {
            RangoLlave = true;

            Debug.Log("Agarrar Llave, Pulsar 'E'");
        }

        if (collision.CompareTag("Pinches"))
        {
            Life -= 2;
            Debug.Log("-Ay me pinche- Tenes " + Life + " de vida");
        }
   
        if (collision.CompareTag("ZonaLenta"))
        {
            currentMoveSpeed = MoveAlquitran;
        }
    }

    private void OnTriggerExit(Collider collision)
    {
        if (collision.CompareTag("Llave"))
        {
            RangoLlave = false;
        }

        if (collision.CompareTag("ZonaLenta"))
        {
            currentMoveSpeed = _moveSpeed;
        }
    }

    private void Movement(Vector3 dir)
    {

        if (_isGrounded)
        {
            if (Input.GetKey(_sprintKey)) 
            {
                 
                currentMoveSpeed = _sprintMultiplier;
            }
            else if (Input.GetKey(_crouchKey))  
            {
                
                currentMoveSpeed = _crouchSpeed;
            }
            else { currentMoveSpeed = _moveSpeed; }
           
        }

        Vector3 moveDir = transform.forward * dir.z + transform.right * dir.x;
        _rb.MovePosition(transform.position + moveDir.normalized * currentMoveSpeed * Time.fixedDeltaTime);
    }
    void Morir()
    {
        lifeState = 0;
    }

    public override void TakeDamage(float dmg)
    {
        _actualHp -= Mathf.RoundToInt(dmg);  

        lifeState = _actualHp;

        if (_actualHp <= 0)
        {
            Destroy(gameObject);
        }
        else if (_isAlive == false)
        {
            Destroy(gameObject);
        }
        else
        {
            Debug.Log($"{name}: Au. :(");
        }
    }


    private void OnDrawGizmos()
    {
        if (_isGrounded)
        {
            Gizmos.color = Color.green;
        }
        else
        {
            Gizmos.color = Color.red;
        }
        Gizmos.DrawLine(_groundRay.origin, _groundRay.direction * _groundRayDistance);

    }

    private void OnDrawGizmosSelected()
    {
        if (_rayOrigin == null) return;

        Gizmos.color = Color.blue;
        Gizmos.DrawRay(_rayOrigin.position, _rayOrigin.forward * _intDistance);
        Gizmos.DrawWireSphere(_rayOrigin.position + _rayOrigin.forward * _intDistance, _intRadius);
    }
}
