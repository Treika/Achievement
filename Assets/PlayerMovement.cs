using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PlayerMovement : MonoBehaviour {

    private Rigidbody2D _rigidBody;
    private AchievementManager _achievements;
    public BoxCollider2D FeetCollider;
    public LayerMask groundLayers;
    public float jumpForce;
    private float decayRate = 4;
    private Vector3 _change;
    public float Speed;
    private Animator _animator;
    private bool _isGrounded = true;
    private GameObject _player;

    private float _rightScale;

    [HideInInspector]
    public DateTime lastMovement;

    [HideInInspector]
    public Vector2 CurrentVelocity;

    // Use this for initialization
    void Start()
    {
        _rigidBody = GetComponent<Rigidbody2D>();
        _achievements = GetComponentInParent<AchievementManager>();
        _animator = GetComponent<Animator>();
        lastMovement = DateTime.UtcNow;
        _rightScale = transform.localScale.x;

    }

    // Update is called once per frame
    void Update () {

        _change = Vector3.zero;
        _change.x = Input.GetAxisRaw("Horizontal");
        //_playerAction.Action(currentSceneType);
        Action();
        UpdateAnimationAndMove();
        CurrentVelocity = _rigidBody.velocity;
        _animator.SetBool("IsGrounded", _isGrounded);
        _animator.SetFloat("xVelocity", _change.x);

        SetDirection();
    }

    private void SetDirection()
    {

        var factor = _rigidBody.velocity.x >= 0 ? 1 : -1;
        transform.localScale = new Vector2(_rightScale * factor, transform.localScale.y);

    }

    private void FixedUpdate()
    {
        var hit = Physics2D.OverlapBoxAll(new Vector2(FeetCollider.bounds.center.x, FeetCollider.bounds.center.y), FeetCollider.bounds.size, 0, groundLayers);
        _isGrounded = (hit.Where(x => x.tag != "Player").Any());
    }

    public void Action()
    {
        if (_achievements.CanJump && Input.GetKeyDown(KeyCode.Space) && _isGrounded)
        {
            StartCoroutine(DoJump());
        }
    }

    private void UpdateAnimationAndMove()
    {
        MoveCharacter();
    }

    private void MoveCharacter()
    {
        _change = HorizontalMovement(_change);
        _rigidBody.AddForce(_change * Speed * Time.deltaTime);
    }

    public Vector3 HorizontalMovement(Vector3 movement)
    {
        movement.x = _achievements.CanMoveLeft ? movement.x : Mathf.Clamp(movement.x, 0, movement.x);
        movement.x = _achievements.CanMoveRight ? movement.x : Mathf.Clamp(movement.x, movement.x, 0);

        if (movement != Vector3.zero)
        {
            lastMovement = DateTime.UtcNow;
        }
        return movement;
    }

    IEnumerator DoJump()
    {
        //the initial jump
        _rigidBody.AddForce(Vector2.up * jumpForce);
        yield return null;

        //can be any value, this is a start ascending force
        float currentForce = jumpForce;

        while (Input.GetKey(KeyCode.Space) && currentForce > 0)
        {
            _rigidBody.AddForce(Vector2.up * currentForce);

            currentForce -= decayRate * Time.deltaTime;
            yield return null;
        }
    }
}
