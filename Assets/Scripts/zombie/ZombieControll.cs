using UnityEngine;
using UnityEngine.AI;

public class ZombieControll : MonoBehaviour
{
    [Header("main Parameters")]
    [SerializeField] private float _zombieMaxHealth;
    [SerializeField] private float _zombieCurrentHealth;

    public float damage;

    [SerializeField] private float _chaseDistance;
    private float _distanceToPlayer;

    public float zombieDamage;
    [SerializeField] private bool _zombieIsDead;
    [SerializeField] private bool _playerIsDetected;
    private bool _screamed;

    [Header("References")]
    [SerializeField] private GameObject _player;
    private Animator _animator;
    private NavMeshAgent _agent;

    [SerializeField] private GameObject _rightHandAtack;
    [SerializeField] private GameObject _leftHandAtack;

    private void Start()
    {
        _animator = GetComponent<Animator>();
        _agent = GetComponent<NavMeshAgent>();
        _playerIsDetected = false;
        _zombieIsDead = false;
        _screamed = false;
        _zombieCurrentHealth = _zombieMaxHealth;
        DisableDamageColliders();


    }




    private void Update()
    {
        if (!_zombieIsDead)
        {
            if (_player != null)
            {
                _distanceToPlayer = Vector3.Distance(gameObject.transform.position, _player.transform.position);

                if (_distanceToPlayer < _chaseDistance)
                {
                    _animator.SetBool("idle", false);
                    _animator.SetBool("run", false);
                    _animator.SetBool("scream", true);
                    _animator.SetBool("atack1", false);
                    _animator.SetBool("kicking", false);
                    _animator.SetBool("punch", false);
                    _animator.SetBool("death", false);
                    _animator.SetBool("hit", false);

                    if (_playerIsDetected)
                    {
                        FightLogic();
                    }
                    
                }
                else if (_distanceToPlayer > _chaseDistance)
                {
                    IdelingLogic();
                }
            }
        }
        

    }


    private void IdelingLogic()
    {
        _animator.SetBool("idle", true); //t
        _animator.SetBool("run", false);
        _animator.SetBool("scream", false);
        _animator.SetBool("atack1", false);
        _animator.SetBool("kicking", false);
        _animator.SetBool("punch", false);
        _animator.SetBool("death", false);
        _animator.SetBool("hit", false);
    }
    private void FightLogic()
    {
        

        if (_distanceToPlayer <= _agent.stoppingDistance)
        {
            _animator.SetBool("idle", false);
            _animator.SetBool("run", false);
            _animator.SetBool("scream", false);
            _animator.SetBool("atack1", true); //t
            _animator.SetBool("kicking", false);
            _animator.SetBool("punch", false);
            _animator.SetBool("death", false);
            _animator.SetBool("hit", false);
        }
        if (_distanceToPlayer >= _agent.stoppingDistance)
        {
            _agent.SetDestination(_player.transform.position);

            _animator.SetBool("idle", false);
            _animator.SetBool("run", true); //t
            _animator.SetBool("scream", false);
            _animator.SetBool("atack1", false);
            _animator.SetBool("kicking", false);
            _animator.SetBool("punch", false);
            _animator.SetBool("death", false);
            _animator.SetBool("hit", false);
        }
    }
    private void DeathLogic()
    {
        DisableDamageColliders();
        _zombieIsDead = true;
        _agent.Stop();

        _animator.SetBool("idle", false);
        _animator.SetBool("run", false);
        _animator.SetBool("scream", false);
        _animator.SetBool("atack1", false);
        _animator.SetBool("kicking", false);
        _animator.SetBool("punch", false);
        _animator.SetBool("death", true); //t
        _animator.SetBool("hit", false);

    }

    private void ClaimDamage(float damageToClaim)
    {
        _animator.SetBool("hit", true); // idk, this anim do not want to play itself. 
        _animator.SetBool("idle", false);
        _animator.SetBool("run", false);
        _animator.SetBool("scream", false);
        _animator.SetBool("atack1", false);
        _animator.SetBool("kicking", false);
        _animator.SetBool("punch", false);
        _animator.SetBool("death", false);
        _zombieCurrentHealth -= damageToClaim;
        if (_zombieCurrentHealth <= 0)
        {
            DeathLogic();
        }
    }


    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.CompareTag("weapon"))
        {
            var _sword = collision.gameObject.GetComponent<Sword>();
            if (_sword != null)
            {
                ClaimDamage(_sword.weaponDamage);
            }
        }
    }
    public void ZombieScreamed()
    {
        _playerIsDetected = true;
    }


    public void AnableDamageColliders()
    {
        _rightHandAtack.SetActive(true);
        _leftHandAtack.SetActive(true);
    }
    public void DisableDamageColliders()
    {
        _rightHandAtack.SetActive(false);
        _leftHandAtack.SetActive(false);
    }
}
