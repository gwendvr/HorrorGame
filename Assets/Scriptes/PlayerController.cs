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
    public bool _isWalking = false;
    public bool _isCrouching = false;
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
        rb.MovePosition(rb.position + moveInput * _moveSpeed * Time.deltaTime);
    }
    void OnMove(InputValue value)
    {
        moveInput = value.Get<Vector2>();
    }
}
