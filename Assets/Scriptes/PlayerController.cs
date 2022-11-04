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
    public AudioSource _audio;


    CreatureController CC;
    [SerializeField] Rigidbody2D rb;
    PlayerAction _playerInput;
    Vector2 moveInput;
    CapsuleCollider2D _Collider;

    void Start()
    {
        CC = FindObjectOfType<CreatureController>();
        rb = GetComponent<Rigidbody2D>();
        _Collider = GetComponent<CapsuleCollider2D>();
        _Animator = GetComponent<Animator>();
        _sprite = GetComponent<SpriteRenderer>();
        _playerInput = new PlayerAction();
        _playerInput.Enable();
        _audio = GetComponent<AudioSource>();
    }
    void Update()
    {
        if (moveInput.x != 0)
        {
            _Animator.SetBool("IsMoving", true);
        }
        else
        {
            _Animator.SetBool("IsMoving", false);
        }
        if (_isStandUp)
        {
            _Animator.SetBool("IsUp", true);
        }
        else
        {
            _Animator.SetBool("IsUp", false);
        }
    }

    void FixedUpdate()
    {
        rb.MovePosition(rb.position + moveInput * _moveSpeed * Time.deltaTime * _reduceSpeedIfCrouching);

        //FootSteps Sound;
    }

    void OnMove(InputValue value)
    {
        _isMoving = !_isMoving;
        moveInput = value.Get<Vector2>();
        if (moveInput.x < 0)
        {
            _sprite.flipX = true;
        }
        else if (moveInput.x > 0)
        {
            _sprite.flipX = false;
        }

        //Foot steps audio
        if (_isStandUp && _isMoving)
        {
            _audio.Play();
        }
        else
        {
            _audio.Stop();
        }
    }

    void OnCrouch()
    {
        _isStandUp = !_isStandUp;
        if (_isStandUp)
        {
            _reduceSpeedIfCrouching = 1f;

            // Foot steps audio
            if (_isMoving)
            {
                _audio.Play();
            }
        }
        else
        {
            _reduceSpeedIfCrouching = 0.33f;

            // Foot steps audio
            _audio.Stop();
        }
    }

    void UpdateCollider()
    {
        if (_isStandUp)
        {
            _Collider.size = new Vector2(1.4f, 3.25f);
            _Collider.offset = new Vector2(0.01f, 0.1f);
            _Collider.direction = CapsuleDirection2D.Vertical;
        }
        else
        {
            _Collider.size = new Vector2(3.15f, 1.5f);
            _Collider.offset = new Vector2(0.03f, -0.8f);
            _Collider.direction = CapsuleDirection2D.Horizontal;
        }
    }
}
