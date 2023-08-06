using System;
using UnityEngine;
using static UnityEngine.Networking.UnityWebRequest;

public class EnemyCharacter : Character
{
    public Vector3 TargetPosition { get; private set; } = Vector3.zero;
    
    [SerializeField] private Transform _head;
    [SerializeField] private float _smoothing = 1f;

    private float _velocityMagnitude = 0f;
    private float _averageInterval;
    private float _rotateX = 0f;
    private float _rotateY = 0f;

    private void Start()
    {
        TargetPosition = transform.position;
    }
    private void Update()
    {
        if (_velocityMagnitude > .1f)
        {
            float maxDistance = _velocityMagnitude * Time.deltaTime;
            transform.position = Vector3.MoveTowards(transform.position, TargetPosition, maxDistance);
        }
        else
        {
            transform.position = TargetPosition; // При движении по прямой и вправо-лево в конце рывок
            //transform.position = Vector3.Lerp(TargetPosition, transform.position, _averageInterval); // а теперь начал скользить
        }
    }
    private void FixedUpdate()
    {
        RotateXY(_rotateX, _rotateY);
    }

    public void SetSpeed(float value) => Speed = value;

    public void SetMovement(in Vector3 position, in Vector3 velocity, in float averageInterval, in bool setSit)
    {
        TargetPosition = position + (velocity * averageInterval);
        _velocityMagnitude = velocity.magnitude;
        _averageInterval = averageInterval;
        Velocity = velocity;

        TrySit(setSit);
    }

    public void SetRotateX(float value)
    {
        _rotateX = value;
    }
    public void SetRotateY(float value)
    {
        _rotateY = value;
    }

    public bool TrySit(bool isSit)
    {
        if (isSit)
        {
            sit?.Invoke();
            return true;
        }
        else
        {
            stand?.Invoke();
            return true;
        }
    }

    private void RotateXY(float x, float y)
    {
        _head.localEulerAngles = new Vector3(x, 0, 0);
        transform.localEulerAngles = new Vector3(0, y, 0);
    }
}
