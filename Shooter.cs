using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shooter : MonoBehaviour
{
    public GameObject _arrow;
    public float _launchForce;
    public Transform _shotPoint;

    public GameObject _point;
    GameObject[] _points;
    public int _numberOfPoints;
    public float _spaceBetweenPoints;
    Vector2 _direction;

    private void Start()
    {
        _points = new GameObject[_numberOfPoints];
        for (int i = 0; i < _numberOfPoints; i++)
        {
            _points[i] = Instantiate(_point, _shotPoint.position, Quaternion.identity);
        }
    }

    void Update()
    {
        Vector2 _bowPos = transform.position;
        Vector2 _mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        _direction = _mousePos - _bowPos;
        transform.right = _direction;

        if (Input.GetMouseButtonDown(0))
        {
            Shoot();
        }

        for (int i = 0; i < _numberOfPoints; i++)
        {
            _points[i].transform.position = PointPosition(i * _spaceBetweenPoints);
        }
    }

    void Shoot()
    {
        GameObject _newArrow =  Instantiate(_arrow, _shotPoint.position, _shotPoint.rotation);
        _newArrow.GetComponent<Rigidbody2D>().velocity = transform.right * _launchForce;
    }

    Vector2 PointPosition(float t)
    {
        Vector2 _pos = (Vector2)_shotPoint.position +
                                (_direction.normalized * _launchForce * t)
                                + .5f * Physics2D.gravity * (t * t);
        return _pos;
    }
}
