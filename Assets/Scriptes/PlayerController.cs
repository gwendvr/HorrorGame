using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    public float _moveSpeed = 5f, _reduceSpeedIfCrouching = 1f;
    public Animator _Animator;
    public SpriteRenderer _sprite;
    public bool _isMoving = false, _isStandUp = true;

    [SerializeField] Rigidbody2D rb;
    PlayerAction _playerInput;
    Vector2 moveInput;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        _Animator = GetComponent<Animator>();
        _sprite = GetComponent<SpriteRenderer>();
        _playerInput = new PlayerAction();
        _playerInput.Enable();
    }
    void Update()
    {
        if (moveInput.x != 0)_Animator.SetBool("IsMoving", true);
        else _Animator.SetBool("IsMoving", false);
        if (_isStandUp) _Animator.SetBool("IsUp", true);
        else _Animator.SetBool("IsUp", false);
    }

    void FixedUpdate()
    {
        rb.MovePosition(rb.position + moveInput * _moveSpeed * Time.deltaTime * _reduceSpeedIfCrouching);
    }

    void OnMove(InputValue value)
    {
        moveInput = value.Get<Vector2>();
        if (moveInput.x < 0) _sprite.flipX = true;
        else if (moveInput.x > 0) _sprite.flipX = false;
    }
    void OnCrouch()
    {
        _isStandUp = !_isStandUp;
        if (_isStandUp)_reduceSpeedIfCrouching = 1f;
        else _reduceSpeedIfCrouching = 0.33f;
    }
}
