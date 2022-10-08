using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

public enum State
{
    CONTROL_MOVE,
    AUTO_MOVE,
}
[RequireComponent(typeof(CharacterController))]
public class ButterflyController : MonoBehaviour
{
    [Header("Butterfly Movement State")] 
    [SerializeField] float _moveSpeed;
    [SerializeField] State _movementState;
    [SerializeField] CurrentStateUI _currentStateUI;

    [Header("Butterfly Auto Movement")]
    [SerializeField] float _autoMoveSpeed;
    public Transform _range;

    
    private Vector3 endPosition = Vector3.zero;
    private bool _outOfRange;

    private float elapeTime = 3;

    private CharacterController _controller;
    void Start()
    {
        endPosition = RandomPosition();
        _controller = GetComponent<CharacterController>();
    }

    private void FixedUpdate()
    {
        _outOfRange = OutOfPosition();
    }
    void Update()
    {
        switch (_movementState)
        {
            case State.CONTROL_MOVE:
                Controll_Move();
                _currentStateUI.SetControllState(_movementState);
                break;
            case State.AUTO_MOVE:
                bool pending = CheckLimitedTime();
                if (pending)
                    endPosition = RandomPosition();
                Auto_Move();
                _currentStateUI.SetAutoState(_movementState);
                break;
        }
    }

    void Controll_Move()
    {
        float x_move = Input.GetAxis(TagManager.X_Move);
        float y_move = Input.GetAxis(TagManager.Y_Move);

        CheckDiraction(x_move);

         Vector2 moveVec = new Vector2(x_move, y_move);
        _controller.Move(moveVec * _moveSpeed * Time.deltaTime);
    }

    void CheckDiraction(float x)
    {
        if (x < 0)
        {
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.Euler(0, -180, 0), 0.5f);
        }
        else
        {
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.Euler(0, 0, 0), 0.5f);
        }
    }

    void Auto_Move()
    {
        if (_outOfRange)
        {
            CheckDiraction(_controller.velocity.x);
            _controller.Move(endPosition * Time.deltaTime * _autoMoveSpeed);
        }
        else
        {
            elapeTime = 3;
            endPosition = RandomPosition();
        }
    }

    bool CheckLimitedTime()
    {
        elapeTime -= Time.deltaTime;
        if(elapeTime < 0)
        {
            elapeTime = 3;
            return true;
        }
        return false;
    }

    bool OutOfPosition()
    {
        float distance = Vector3.Distance(transform.position, endPosition);
        if (distance > 3)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    Vector3 RandomPosition()
    {
        float range_x = _range.localScale.x - 1.0f;
        float range_y = _range.localScale.y - 1.0f;
        range_x = Random.Range((range_x / 2) * -1.0f, range_x / 2);
        range_y = Random.Range((range_y / 2) * -1.0f, range_y / 2);
        Vector3 randomPosition = new Vector3(range_x, range_y, 0);

        return randomPosition;
    }

}
