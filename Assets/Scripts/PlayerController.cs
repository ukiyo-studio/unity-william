using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private const string Horizontal = "Horizontal";
    private const string Vertical = "Vertical";
    
    private const float MoveSpeed = 2f;
    private Rigidbody2D _rigidBody;
    private Vector2 _movement;
    private Animator _animator;
    private static readonly int AnimHorizontal = Animator.StringToHash("Horizontal");
    private static readonly int AnimVertical = Animator.StringToHash("Vertical");
    private static readonly int Speed = Animator.StringToHash("Speed");

    // Update is called once per frame
    private void Awake()
    {
        _animator = GetComponent<Animator>();
        _rigidBody = GetComponent<Rigidbody2D>();
    }

    void Update()
    {

        PlayerMovement();
        PlayerAnimation();

    }

    private void FixedUpdate()
    {
        _rigidBody.MovePosition(_rigidBody.position + _movement * (MoveSpeed * Time.fixedDeltaTime));
    }

    private void PlayerMovement()
    {
        _movement.x =  Input.GetAxis(Horizontal);
        _movement.y =  Input.GetAxis(Vertical);
    }

    private void PlayerAnimation()
    {
        _animator.SetFloat(AnimHorizontal, _movement.x);
        _animator.SetFloat(AnimVertical, _movement.y);
        _animator.SetFloat(Speed, _movement.sqrMagnitude);
    }
}