using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(BoxCollider2D))]
public class Butterfly_2DController : MonoBehaviour
{
    [Header("Butterfly Movement State")]
    [SerializeField] float _moveSpeed;
    [SerializeField] State _movementState;
    [SerializeField] CurrentStateUI _currentStateUI;

    [Header("Butterfly Auto Movement")]
    [SerializeField] float _autoMoveSpeed;
    public Transform _range;

    Vector3 endPosition = Vector3.zero;
    bool _outOfRange;
    float elapeTime = 3;
    private Rigidbody2D _rigidbody2D;
    private BoxCollider2D _collider2D;
    // Start is called before the first frame update
    void Start()
    {
        endPosition = RandomPosition();
        _rigidbody2D = GetComponent<Rigidbody2D>();
        _collider2D = GetComponent<BoxCollider2D>();
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
                Auto_Move();
                _currentStateUI.SetAutoState(_movementState);
                break;
        }
    }

    void Controll_Move()
    {
        float x_move = Input.GetAxisRaw(TagManager.X_Move);
        float y_move = Input.GetAxisRaw(TagManager.Y_Move);

        Vector2 moveVec = new Vector2(x_move, y_move) * _moveSpeed;
        _rigidbody2D.velocity = moveVec;
    }

    void Auto_Move()
    {
        if (_outOfRange)
        {
            transform.position = Vector2.MoveTowards(transform.position, endPosition, Time.deltaTime * _autoMoveSpeed);
            if (CheckLimitedTime())
                endPosition = RandomPosition();
        }
        else
            endPosition = RandomPosition();
    }

    bool CheckLimitedTime()
    {
        float prevtime = elapeTime - Time.deltaTime;
        if (prevtime < 0)
        {
            prevtime = 0;
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
