using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Traps : MonoBehaviour
{



    [Header("Animator")]
    [SerializeField] private string _explocionName = "onExplocion";
    [SerializeField] private string _explocionTrigName = "ExplocionTrig";


    [Header("Gameplay")]
    [SerializeField] private int _atkDmg = 10000000;

    [Header("Physics")]
    [SerializeField] private float _atkDistance = 2.0f;
    [SerializeField] private LayerMask _atkMask;
    [SerializeField] private Transform _rayOrigin;
    [SerializeField] private float _radius;

    public AudioClip BigExplocion;

    private Ray _atkRay;
    private RaycastHit _atkHit;

    private Animator _animator;

    private AudioSource _audioSource;

    public GameObject Player, Enemy;

    private void Awake()
    {
        _animator = GetComponent<Animator>();

        _audioSource = gameObject.AddComponent<AudioSource>();
    }


    public void Attack()
    {



        _atkRay = new Ray(_rayOrigin.position, transform.forward);


        if (Physics.Raycast(_atkRay, out _atkHit, _atkDistance, _atkMask))
        {
            if (_atkHit.collider.TryGetComponent(out CharacterBehaviour entity))
            {

                entity.TakeDamage(_atkDmg);

            }
            if (_atkHit.collider.TryGetComponent(out Player player))
            {

                player.TakeDamage(_atkDmg);

            }

        }

    }


    private void OnCollisionEnter(Collision collision)
    {
       

        if (collision.gameObject.CompareTag("Player") || collision.gameObject.CompareTag("Enemy"))
        {
            //_animator.SetBool(_explocionName, true);

            Attack();

            /* if (_animator != null)
             {
                 _animator.SetTrigger(_explocionName);
             }*/

            if (BigExplocion != null)
            {
                _audioSource.PlayOneShot(BigExplocion);
            }

            Destroy(gameObject, 1.5f);
        }
    }
}
