using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D), typeof(SpriteRenderer))]
public class PlayerMovement : MonoBehaviour
{
    private const float MinMoveDistance = 0.001f;
    private const float ShellRadius = 0.01f;

    [SerializeField] private AnimationCurve _acceleration;
    [SerializeField] private float _speed = 1f;
    [SerializeField] private float _minGroundNormalY = .65f;
    [SerializeField] private float _gravityModifier = 1f;
    [SerializeField] private LayerMask _layerMask;
    [SerializeField] private int _velocityJump = 5;

    private Vector2 _velocity;
    private Vector2 _targetVelocity;
    private bool _grounded;
    private Vector2 _groundNormal;
    private Rigidbody2D _rigidbody;
    private SpriteRenderer _spriteRenderer;
    private ContactFilter2D _contactFilter;
    private RaycastHit2D[] _hitBuffer = new RaycastHit2D[16];
    private List<RaycastHit2D> _hitBufferList = new List<RaycastHit2D>(16);

    private void Start()
    {
        _contactFilter.useTriggers = false;
        _contactFilter.SetLayerMask(_layerMask);
        _contactFilter.useLayerMask = true;
    }

    private void Update()
    {
        _targetVelocity = new Vector2(_acceleration.Evaluate(Input.GetAxis(GlobalVars.HorizontalAxis)) * _speed, 0);

        if (Input.GetKeyDown(KeyCode.Space) && _grounded)
            _velocity.y = _velocityJump;

        if (Input.GetAxis(GlobalVars.HorizontalAxis) > 0f)
            _spriteRenderer.flipX = false;
        else if (Input.GetAxis(GlobalVars.HorizontalAxis) < 0f)
            _spriteRenderer.flipX = true;
    }

    private void FixedUpdate()
    {
        _velocity += _gravityModifier * Physics2D.gravity * Time.deltaTime;
        _velocity.x = _targetVelocity.x;

        _grounded = false;

        Vector2 deltaPosition = _velocity * Time.deltaTime;
        Vector2 moveAlongGround = new Vector2(_groundNormal.y, -_groundNormal.x);
        Vector2 move = moveAlongGround * deltaPosition.x;

        Move(move, false);

        move = Vector2.up * deltaPosition.y;

        Move(move, true);
    }

    private void OnEnable()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _rigidbody.freezeRotation = true;
        _rigidbody.gravityScale = 0;
    }

    private void Move(Vector2 move, bool yMovement)
    {
        float distance = move.magnitude;

        if (distance > MinMoveDistance)
        {
            int count = _rigidbody.Cast(move, _contactFilter, _hitBuffer, distance + ShellRadius);
            _hitBufferList.Clear();

            for (int i = 0; i < count; i++)
                _hitBufferList.Add(_hitBuffer[i]);

            for (int i = 0; i < _hitBufferList.Count; i++)
            {
                Vector2 currentNormal = _hitBufferList[i].normal;

                if (currentNormal.y > _minGroundNormalY)
                {
                    _grounded = true;

                    if (yMovement)
                    {
                        _groundNormal = currentNormal;
                        currentNormal.x = 0;
                    }
                }

                float projection = Vector2.Dot(_velocity, currentNormal);

                if (projection < 0)
                    _velocity = _velocity - projection * currentNormal;

                float modifiedDistance = _hitBufferList[i].distance - ShellRadius;
                distance = modifiedDistance < distance ? modifiedDistance : distance;
            }
        }

        _rigidbody.position = _rigidbody.position + move.normalized * distance;
    }
}
