using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewRadial : MonoBehaviour
{
    public static void Radial(Vector3 _originPoint,GameObject _Object, int _numberOfObjects, float _angle, float _radius, float _rotation, float _rotationOffset)
    {

        if (_angle != 360)
        {
            _numberOfObjects -= 1;
        }

        float angleStep = _angle / _numberOfObjects;
        float angle = 0f;

        if (_angle == 360)
        {
            _numberOfObjects -= 1;
        }

        for (int i = 0; i <= _numberOfObjects; i++)
        {
            //spawnpoint
            //m_StartPoint = _gameObject.transform.position;

            float xPosition = _originPoint.x + Mathf.Sin(((angle + _rotation) * Mathf.PI) / 180) * _radius;
            float zPosition = _originPoint.z + Mathf.Cos(((angle + _rotation) * Mathf.PI) / 180) * _radius;

            Vector3 radialVector = new Vector3(xPosition, 0, zPosition);

            //Direction

            float xPositionDir = _originPoint.x + Mathf.Sin(((angle + _rotation + _rotationOffset) * Mathf.PI) / 180) * (_radius + 1);
            float zPositionDir = _originPoint.z + Mathf.Cos(((angle + _rotation + _rotationOffset) * Mathf.PI) / 180) * (_radius + 1);

            Vector3 dirVector = new Vector3(xPositionDir, 0, zPositionDir);

            //Instantiate(m_bullet, radialVector, Quaternion.LookRotation(dirVector));
            Pooling.PoolThis(_Object, radialVector, Quaternion.LookRotation(dirVector));

            angle += angleStep;
        }
    }

    public static void RadialV2(Vector3 _originPoint, GameObject _Object, int _numberOfObjects, float _angle, float _radius, float _rotation, float _rotationOffset)
    {

        if (_angle != 360)
        {
            _numberOfObjects -= 1;
        }

        float angleStep = _angle / _numberOfObjects;
        float angle = 0f;

        if (_angle == 360)
        {
            _numberOfObjects -= 1;
        }

        for (int i = 0; i <= _numberOfObjects; i++)
        {
            //spawnpoint
            //m_StartPoint = _gameObject.transform.position;

            float xPosition = _originPoint.x + Mathf.Sin(((angle + _rotation) * Mathf.PI) / 180) * _radius;
            float yPosition = _originPoint.y + Mathf.Cos(((angle + _rotation) * Mathf.PI) / 180) * _radius;

            Vector3 radialVector = new Vector3(xPosition, yPosition, 0);

            //Direction

            float xPositionDir = _originPoint.x + Mathf.Sin(((angle + _rotation + _rotationOffset) * Mathf.PI) / 180) * (_radius + 1);
            float yPositionDir = _originPoint.y + Mathf.Cos(((angle + _rotation + _rotationOffset) * Mathf.PI) / 180) * (_radius + 1);

            Vector3 dirVector = new Vector3(xPositionDir, yPositionDir, 0);

            //Instantiate(m_bullet, radialVector, Quaternion.LookRotation(dirVector));
            Pooling.PoolThis(_Object, radialVector, Quaternion.LookRotation(dirVector));

            angle += angleStep;
        }
    }

    public static void RadialV2(Vector3 _originPoint,GameObject _Object, int _numberOfObjects, float _angle, float _radius, float _rotation, float _rotationOffset, Transform _parent)
    {

        if (_angle != 360)
        {
            _numberOfObjects -= 1;
        }

        float angleStep = _angle / _numberOfObjects;
        float angle = 0f;

        if (_angle == 360)
        {
            _numberOfObjects -= 1;
        }

        for (int i = 0; i <= _numberOfObjects; i++)
        {
            //spawnpoint
            //m_StartPoint = _gameObject.transform.position;

            float xPosition = _originPoint.x + Mathf.Sin(((angle + _rotation) * Mathf.PI) / 180) * _radius;
            float yPosition = _originPoint.y + Mathf.Cos(((angle + _rotation) * Mathf.PI) / 180) * _radius;

            Vector3 radialVector = new Vector3(xPosition, yPosition, 0);

            //Direction

            float xPositionDir = _originPoint.x + Mathf.Sin(((angle + _rotation + _rotationOffset) * Mathf.PI) / 180) * (_radius + 1);
            float yPositionDir = _originPoint.y + Mathf.Cos(((angle + _rotation + _rotationOffset) * Mathf.PI) / 180) * (_radius + 1);

            Vector3 dirVector = new Vector3(xPositionDir, yPositionDir, 0);

            //Instantiate(m_bullet, radialVector, Quaternion.LookRotation(dirVector));
            GameObject obj = Pooling.PoolThis(_Object, radialVector, Quaternion.LookRotation(dirVector));

            obj.transform.SetParent(_parent);

            angle += angleStep;
        }
    }

    public static void RadialV2(Vector3 _originPoint, GameObject _Object, int _numberOfObjects, float _angle, float _radius, float _rotation, float _rotationOffset, Transform _parent, float _speed)
    {

        if (_angle != 360)
        {
            _numberOfObjects -= 1;
        }

        float angleStep = _angle / _numberOfObjects;
        float angle = 0f;

        if (_angle == 360)
        {
            _numberOfObjects -= 1;
        }

        for (int i = 0; i <= _numberOfObjects; i++)
        {
            //spawnpoint
            //m_StartPoint = _gameObject.transform.position;

            float xPosition = _originPoint.x + Mathf.Sin(((angle + _rotation) * Mathf.PI) / 180) * _radius;
            float yPosition = _originPoint.y + Mathf.Cos(((angle + _rotation) * Mathf.PI) / 180) * _radius;

            Vector3 radialVector = new Vector3(xPosition, yPosition, 0);

            //Direction

            float xPositionDir = _originPoint.x + Mathf.Sin(((angle + _rotation + _rotationOffset) * Mathf.PI) / 180) * (_radius + 1);
            float yPositionDir = _originPoint.y + Mathf.Cos(((angle + _rotation + _rotationOffset) * Mathf.PI) / 180) * (_radius + 1);

            Vector3 dirVector = new Vector3(xPositionDir, yPositionDir, 0);

            //Instantiate(m_bullet, radialVector, Quaternion.LookRotation(dirVector));
            GameObject obj = Pooling.PoolThis(_Object, radialVector, Quaternion.LookRotation(dirVector));
            obj.transform.SetParent(_parent);
            obj.GetComponent<Enemy>().speed = _speed;


            angle += angleStep;
        }
    }

    public static void RadialGizmo(Vector3 _originPoint, GameObject _Object, int _numberOfObjects, float _angle, float _radius, float _rotation, float _rotationOffset)
    {

        if (_angle != 360)
        {
            _numberOfObjects -= 1;
        }

        float angleStep = _angle / _numberOfObjects;
        float angle = 0f;

        if (_angle == 360)
        {
            _numberOfObjects -= 1;
        }

        for (int i = 0; i <= _numberOfObjects; i++)
        {
            //spawnpoint
            //m_StartPoint = _gameObject.transform.position;

            float xPosition = _originPoint.x + Mathf.Sin(((angle + _rotation) * Mathf.PI) / 180) * _radius;
            float zPosition = _originPoint.z + Mathf.Cos(((angle + _rotation) * Mathf.PI) / 180) * _radius;

            Vector3 radialVector = new Vector3(xPosition, 0, zPosition);

            //Direction

            float xPositionDir = _originPoint.x + Mathf.Sin(((angle + _rotation + _rotationOffset) * Mathf.PI) / 180) * (_radius + 1);
            float zPositionDir = _originPoint.z + Mathf.Cos(((angle + _rotation + _rotationOffset) * Mathf.PI) / 180) * (_radius + 1);

            Vector3 dirVector = new Vector3(xPositionDir, 0, zPositionDir);

            Gizmos.color = new Color(0, 1, 0, 1f);
            Gizmos.DrawSphere(radialVector, 0.1f);
            Gizmos.color = Color.cyan;
            Gizmos.DrawRay(radialVector, dirVector);
            angle += angleStep;
        }
    }
}
