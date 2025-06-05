using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : CharacterBehaviour
{

    [Header("Animator")]
    [SerializeField] private string _atkTriggerName = "onAttack";

    [SerializeField] private string _IsWalkName = "IsWalk";
   
    [Header("Gameplay")]
    [SerializeField] private int _atkDmg = 1;

    [Header("Physics")]
    [SerializeField] private float _atkDistance = 2.0f;
    [SerializeField] private LayerMask _atkMask;
    [SerializeField] private Transform _rayOrigin;
    [SerializeField] private Transform _target;
    [SerializeField] private float _speed;
    [SerializeField] private float _radius;

    private Ray _atkRay;
    private RaycastHit _atkHit;

    private Animator _animator;

    public GameObject Player;

    protected override void Start()
    {
        _animator = GetComponentInChildren<Animator>();
    }



    protected override void Update()
    {
        base.Update();

        if (_target == null) return;

        if (Vector3.Distance(transform.position, _target.transform.position) <= _radius)
        {

           var lookPos = _target.transform.position - transform.position;
            lookPos.y = 0;
            var rotation = Quaternion.LookRotation(lookPos);
            transform.rotation = Quaternion.RotateTowards(transform.rotation, rotation, 3);

            _animator.SetBool(_IsWalkName, true);
            transform.Translate(Vector3.forward * 2 * Time.deltaTime);
        }
        else
        {
            _animator.SetBool(_IsWalkName, false);

            

        }



    }


    public override void TakeDamage(int dmg)
    {
        _actualHp -= dmg;


        if (_actualHp <= 0 )
        {


            Destroy(gameObject);


        }
        else
        {
            Debug.Log($"{name}: Au. :(");

        }


    }

    public void Attack()
    {

        _animator.SetBool(_IsWalkName, false);
        _animator.SetTrigger(_atkTriggerName);
        _atkRay = new Ray(_rayOrigin.position, transform.forward);

        
        if (Physics.Raycast(_atkRay, out _atkHit, _atkDistance, _atkMask))
        {
            if (_atkHit.collider.TryGetComponent(out Player player))
            {

                player.TakeDamage(_atkDmg);

            }

        }

    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Attack();
        }
        else
        {

            _animator.SetBool(_IsWalkName, false);

        }

        
    }


    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, _radius);
    }



}
