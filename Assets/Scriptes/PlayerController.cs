using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    public float _moveSpeed = 5f;
    public float _reduceSpeedIfCrouching = 1f;
    public Animator _Animator;
    public SpriteRenderer _sprite;
    public bool _isMoving = false;
    public bool _isStandUp = true;
    public Rigidbody2D rb;

    private PlayerAction _playerInput;
    private Vector2 moveInput;


    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        _Animator = GetComponent<Animator>();
        _sprite = GetComponent<SpriteRenderer>();
        _playerInput = new PlayerAction();
        _playerInput.Enable();
    }

    // Update is called once per frame
    void Update()
    {

    }

    void FixedUpdate()
    {
        rb.MovePosition(rb.position + moveInput * _moveSpeed * Time.deltaTime * _reduceSpeedIfCrouching);
    }

    void OnMove(InputValue value)
    {
        moveInput = value.Get<Vector2>();
        _isMoving = !_isMoving;
        AnimationPlayer();
    }

    void OnCrouch()
    {
        _isStandUp = !_isStandUp;
        if (_isStandUp)
        {
            _reduceSpeedIfCrouching = 1f;
        }
        else
        {
            _reduceSpeedIfCrouching = 0.33f;
        }

        AnimationPlayer();
    }

    private void AnimationPlayer()
    {
        if (_isStandUp)
        {
            if (_isMoving)
            {
                _Animator.SetTrigger("Walk");
            }
            else
            {
                _Animator.SetTrigger("StandUp");
            }
        }
        else
        {
            if (_isMoving)
            {
                _Animator.SetTrigger("Crouch");
            }
            else
            {
                _Animator.SetTrigger("StandCrouch");
            }
        }

    }



}
