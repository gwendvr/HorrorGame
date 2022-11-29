using System.Collections;
using System.Collections.Generic;
using UnityEditor.UI;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    //Public 
    public float _moveSpeed = 5f, _reduceSpeedIfCrouching = 1f, _blockIfHidden = 1f;
    public Animator _Animator;
    public SpriteRenderer _sprite;
    public bool _isMoving = false, _isStandUp = true;
    public AudioSource _audio;
    public bool _isHidden = false;
    public HudController _Hud;
    public GameObject _Knife;
    public bool _DidacticielOn = false;


    //Private
    menuController _menu;
    private bool _isDead = false;
    Color _PlayerColor;
    bool _canHide = false, _canTake = false;
    CreatureController CC;
    private Rigidbody2D rb;
    PlayerAction _playerInput;
    Vector2 moveInput;
    CapsuleCollider2D _Collider;

    //Interactions
    private bool _isLocker = false, _isKnife = false;
    private bool _haveAKnife = false;

    void Start()
    {
        _menu = FindObjectOfType<menuController>();
        _PlayerColor = _sprite.color;
        CC = FindObjectOfType<CreatureController>();
        rb = GetComponent<Rigidbody2D>();
        _Collider = GetComponent<CapsuleCollider2D>();
        _Animator = GetComponent<Animator>();
        _sprite = GetComponent<SpriteRenderer>();
        _playerInput = new PlayerAction();
        _playerInput.Enable();
        _audio = GetComponent<AudioSource>();

        _Animator.SetBool("IsMoving", false);
        _Animator.SetBool("IsUp", false);
        _Animator.SetBool("IsHide", true);

    }
    void Update()
    {
        if (moveInput.x != 0)
        {
            _Animator.SetBool("IsMoving", true);
            _Animator.SetBool("IsHide", false);
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
            _Animator.SetBool("IsHide", false);
        }
    }

    void FixedUpdate()
    {
        rb.MovePosition(rb.position + moveInput * _moveSpeed * Time.deltaTime * _reduceSpeedIfCrouching * _blockIfHidden);
    }

    void OnMove(InputValue value)
    {
        if (!_DidacticielOn)
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
        

    }

    void OnCrouch()
    {
        if (!_DidacticielOn)
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

    }

    void UpdateCollider()
    {
        if (_isHidden)
        {
            _Collider.size = new Vector2(1.4f, 1.5f);
            _Collider.offset = new Vector2(0f, 1.2f);
            _Collider.direction = CapsuleDirection2D.Horizontal;
        }
        else if (_isStandUp)
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

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Hideable"))
        {
            _isLocker = true;
            _canHide = true;
        }
        if (collision.CompareTag("Knife"))
        {
            _canTake = true;
            _isKnife = true;
        }
        if (collision.CompareTag("Win"))
        {
            _menu.BackToMenu();
        }
        if (collision.CompareTag("GetCrazyStep1"))
        {
            _Hud.ShowCrazy1();
        }
        if (collision.CompareTag("GetCrazyStep2"))
        {
            _Hud.ShowCrazy2();
        }
        if (collision.CompareTag("GetCrazyStep3"))
        {
            _Hud.ShowCrazy3();
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Hideable"))
        {
            _canHide = false;
            _isLocker = false;
        }

        if (collision.CompareTag("Knife"))
        {
            _isKnife = false;
        }

        if (collision.CompareTag("GetCrazyStep1"))
        {
            _Hud.HideCrazy1();
        }
        if (collision.CompareTag("GetCrazyStep2"))
        {
            _Hud.HideCrazy2();
        }
        if (collision.CompareTag("GetCrazyStep3"))
        {
            _Hud.HideCrazy3();
        }
    }
    //se cache dans une armoir ou autre
    void OnInteract()
    {
        if (_isLocker)
        {
            if (_isHidden)
            {
                _blockIfHidden = 1f;
                _sprite.color = _PlayerColor;
                _isHidden = false;
                _canHide = true;
                _Animator.SetBool("IsHide", true);
                UpdateCollider();
            }

            else if (_canHide)
            {
                _blockIfHidden = 0f;
                _sprite.color = new Color(0, 0, 0, 0);
                _canHide = false;
                _isHidden = true;
                UpdateCollider();
            }
        }
        if (_isKnife)
        {
            _haveAKnife = true;
            _Hud.ShowKnifeInventory();
            Destroy(_Knife);
        }
        if (_DidacticielOn)
        {

        }
    }

    //system knife
    public bool HitACreature()
    {
        bool _stillalive;
        if (_haveAKnife)
        {
            _stillalive = true;
            _haveAKnife = false;
            _Hud.HideKnifeInventory();
        }
        else
        {
            _stillalive = false;
            _isDead = true;
        }
        return _stillalive;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Deadly"))
        {
            _menu.Die();
        }
    }
    public void IsOnDidacticiel()
    {
        _DidacticielOn = true;
    }

    public void IsntOnDidacticiel()
    {
        _DidacticielOn = false;
    }
}
