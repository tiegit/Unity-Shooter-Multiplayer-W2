using System;
using UnityEngine;

public class PlayerGun : Gun
{
    [SerializeField] private Transform _bulletPoint;
    [SerializeField] private float _bulletSpeed;
    [SerializeField] private float _shootDelay;
    private float _lastShootTime;

    public bool TryShoot(out ShootInfo info)
    {
        info = new ShootInfo();

        if (Time.time - _lastShootTime < _shootDelay) return false;

        Vector3 position = _bulletPoint.position;
        Vector3 velocity = _bulletPoint.forward * _bulletSpeed;
        Quaternion rotation = _bulletPoint.rotation; // может пригодится для передачи поворота на сервер

        _lastShootTime = Time.time;
        Instantiate(_bulletPrefab, position, rotation).Init(velocity); // Instantiate возвращает ссылку на объект который создал
        shoot?.Invoke();  //событие для аниматора

        info.pX = position.x;
        info.pY = position.y;
        info.pZ = position.z;

        info.vX = velocity.x;
        info.vY = velocity.y;
        info.vZ = velocity.z;

        info.rX = rotation.x;
        info.rY = rotation.y;
        info.rZ = rotation.z;

        return true;
    }
}
