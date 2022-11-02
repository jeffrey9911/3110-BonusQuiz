using System.Collections;
using System.Collections.Generic;
//using UnityEditor.ShortcutManagement;
using UnityEngine;

using UnityEngine.InputSystem;
using UnityEngine.InputSystem.LowLevel;

public class PlayerController : MonoBehaviour
{
    public static PlayerController Instance;

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

    private void Awake()
    {
        if (!Instance)
            Instance = this;
    }

    void Start()
    {
        _playerAction = PlayerInputController.Instance._playerAction;

        _rb = GetComponent<Rigidbody>();

        _playerAction.PlayerControl.Move.performed += context => _moveVec = context.ReadValue<Vector2>();
        _playerAction.PlayerControl.Move.canceled += context => _moveVec = Vector2.zero;

        _playerAction.PlayerControl.Jump.performed += context => PlayerJump();
        _playerAction.PlayerControl.Attack.performed += context => PlayerAttack();

        SearchBricks();
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
        GameObject _brickToDestroy = null;
        int index = 0;
        for(int i = 0; i < _bricks.Count; i++)
        {
            if (Vector3.Distance(_bricks[i].transform.position, this.transform.position) < 1.0)
            {
                if(_brickToDestroy == null)
                {
                    _brickToDestroy = _bricks[i].gameObject;
                    index = i;
                }
                else if(Vector3.Distance(_bricks[i].transform.position, this.transform.position) < Vector3.Distance(_brickToDestroy.transform.position, this.transform.position))
                {
                    _brickToDestroy = _bricks[i].gameObject;
                    index = i;
                }
            }
        }

        if(_brickToDestroy != null)
        {
            _command = new BrakeBrickCommand(_brickToDestroy.transform.position, _brickToDestroy.transform);
            CommandInvoker.AddCommand(_command);

            _brickToDestroy.SetActive(false);

            _bricks.RemoveAt(index);
        }
    }

    public void SearchBricks()
    {
        foreach (Transform brickTransform in LevelBrick)
        {
            brick singleBrick = brickTransform.GetComponent<brick>();

            _bricks.Add(singleBrick);
        }
    }
}
