using System.Collections;
using System.Collections.Generic;
//using UnityEditor.ShortcutManagement;
using UnityEngine;

using UnityEngine.InputSystem;
using UnityEngine.InputSystem.LowLevel;

public class PlayerController : MonoBehaviour
{

    public PlayerAction _playerAction;
    Vector2 _moveVec;

    [SerializeField]
    private float moveSpeed = 2.0f;

    public Camera _playerCamera;

    Rigidbody _rb;
    private bool isGround = true;
    [SerializeField]
    public float _jumpSpeed = 2.0f;
    [SerializeField]
    public float _distanceToGround = 0.07f;

    private float _facingRot = 90.0f;

    public Transform _spawnPos;

    public List<brick> _bricks = new List<brick>();

    public Transform LevelBrick;

    iCommand _command;

    void Start()
    {
        _playerAction = PlayerInputController.Instance._playerAction;

        _rb = GetComponent<Rigidbody>();

        _playerAction.PlayerControl.Move.performed += context => _moveVec = context.ReadValue<Vector2>();
        _playerAction.PlayerControl.Move.canceled += context => _moveVec = Vector2.zero;

        _playerAction.PlayerControl.Jump.performed += context => PlayerJump();
        _playerAction.PlayerControl.Attack.performed += context => PlayerAttack();

        foreach (Transform brickTransform in LevelBrick)
        {
            brick singleBrick = brickTransform.GetComponent<brick>();

            _bricks.Add(singleBrick);
        }
    }

    // Update is called once per frame
    void Update()
    {
        PlayerMove();

        this.transform.position = new Vector3(this.transform.position.x, this.transform.position.y, 0.0f);
        this.transform.rotation = Quaternion.Euler(0.0f, _facingRot, 0.0f);

        isGround = Physics.Raycast(transform.position, -Vector3.up, _distanceToGround);

        _playerCamera.transform.position = new Vector3(this.transform.position.x, this.transform.position.y, -10.0f);


    }

    private void OnTriggerEnter(Collider other)
    {
        switch(other.tag)
        {
            case "Bee":
                HealthManager.Instance.ChangeHealth(-10);
                break;

            case "Skull":
                HealthManager.Instance.ChangeHealth(-50);
                break;

            case "DropPlane":
                this.transform.position = _spawnPos.position;
                break;

            default:
                break;
        }
    }

    private void PlayerMove()
    {
        float toMove;
        if(_moveVec.x < 0.0f)
        {
            _facingRot = -90.0f;
            toMove = _moveVec.x * -1.0f;
        }
        else if(_moveVec.x > 0.0f)
        {
            _facingRot = 90.0f;
            toMove = _moveVec.x;
        }
        else
        {
            toMove = _moveVec.x;
        }    
        
        this.transform.Translate(Vector3.forward * toMove * Time.deltaTime * moveSpeed, Space.Self);
        this.transform.Translate(Vector3.up * _moveVec.y * Time.deltaTime * moveSpeed, Space.Self);
    }

    private void PlayerJump()
    {
        if(isGround)
        {
            _rb.velocity = new Vector2(_rb.velocity.x, _jumpSpeed);
            isGround = false;
        }
    }

    private void PlayerAttack()
    {
        for(int i = 0; i < _bricks.Count; i++)
        {
            if (Vector3.Distance(_bricks[i].transform.position, this.transform.position) < 1.0)
            {
                _command = new BrakeBrickCommand(_bricks[i].transform.position, _bricks[i].transform);
                CommandInvoker.AddCommand(_command);
                GameObject.Destroy(_bricks[i].gameObject);
            }
        }
    }
}
