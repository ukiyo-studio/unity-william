using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private const string Horizontal = "Horizontal";
    private const string Vertical = "Vertical";

    private const float MoveSpeed = 2f;
    private bool _isMoving;
    private Rigidbody2D _rigidBody;
    private Vector2 _movement;
    private Animator _animator;
    private static readonly int AnimHorizontal = Animator.StringToHash("Horizontal");
    private static readonly int AnimVertical = Animator.StringToHash("Vertical");
    private static readonly int Speed = Animator.StringToHash("Speed");
    [SerializeField] private LayerMask solidObjectsLayer;

    // Update is called once per frame
    private void Awake()
    {
        _animator = GetComponent<Animator>();
        _rigidBody = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        PlayerMovementInput();
        PlayerAnimation();
    }

    private void FixedUpdate()
    {
        PlayerMovement();
    }

    /// <summary>
    /// Player movement logic
    /// </summary>
    private void PlayerMovement()
    {
        // get our target position. This is where we want to move towards
        Vector2 targetPosition = _rigidBody.position + _movement * (MoveSpeed * Time.fixedDeltaTime);

        // Check if we are going to collide with an object before we can move
        // if we are not going to collide with an object then we can move to our target position
        if (!CollidingWithObject(targetPosition))
        {
            _rigidBody.MovePosition(targetPosition);
        }
    }

    /// <summary>
    /// Get player movement (this is where we want to go)
    /// </summary>
    private void PlayerMovementInput()
    {
        _movement.x = Input.GetAxis(Horizontal);
        _movement.y = Input.GetAxis(Vertical);
        _isMoving = _movement.sqrMagnitude > 0;
    }

    /// <summary>
    /// Handle player animation depending on movement
    /// </summary>
    private void PlayerAnimation()
    {
        _animator.SetFloat(AnimHorizontal, _movement.x);
        _animator.SetFloat(AnimVertical, _movement.y);
        _animator.SetFloat(Speed, _movement.sqrMagnitude);
    }

    /// <summary>
    /// Check if we are going to walk into something
    /// </summary>
    /// <param name="targetPosition"></param>
    /// <returns></returns>
    private bool CollidingWithObject(Vector3 targetPosition)
    {
        var other = Physics2D.OverlapCircle(targetPosition, 0.2f, solidObjectsLayer);


        return other != null;
    }
}