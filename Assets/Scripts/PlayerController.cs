using Unity.VisualScripting;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    [SerializeField] private float _currentSpeed;
    private bool _forvard;


    private Animator animator;

    void Start()
    {
        animator = GetComponent<Animator>();
        _forvard = true;

        //IF SOMEONE FUCKING TAKES vSYNC OUT OF HERE -BITCH I'LL RIP OFF YOUR FUCKING HANDS !!!!!
        QualitySettings.vSyncCount = 1;
        //DO NOT TOUCH !!


        Cursor.lockState = CursorLockMode.Locked;
    }

    void Update()
    {
        Move();
        AnimationUpdate();
    }

    void Move()
    {
        _currentSpeed = Input.GetAxis("Horizontal");
        animator.SetFloat("speed", _currentSpeed);
        if (_currentSpeed > 0)
        {
            if (!_forvard)
            {
                transform.rotation = Quaternion.Euler(0, 0, 0);
                _forvard = true;
            }
            
        }
        else if (_currentSpeed < 0)
        {
            if (_forvard)
            {
                transform.rotation = Quaternion.Euler(0, 180, 0);
                _forvard = false;
            }
            
        }
    }

    private void AnimationUpdate()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (_currentSpeed == 0)
            {
                animator.SetBool("atack_1", true);
                animator.SetBool("atack_2", false);
            }
            else if (_currentSpeed != 0)
            {
                animator.SetBool("atack_1", false);
                animator.SetBool("atack_2", true);
            }
            
        }
        else
        {
            animator.SetBool("atack_1", false);
            animator.SetBool("atack_2", false);
        }
    }
}
