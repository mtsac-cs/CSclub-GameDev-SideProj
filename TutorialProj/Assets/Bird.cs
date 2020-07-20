//using System.Collections;
//using System.Collections.Generic;
using System;
using System.ComponentModel;
using System.Security.Cryptography;
using System.Threading;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Bird : MonoBehaviour
{
    private Vector3 _initialPosition;
    private bool _birdWasLaunched;
    private float _timeSittingAround;
    private float _gravityScale;
    public GameObject _pointPrefab;
    public GameObject[] _points;
    public int _numberOfPoints;

    [SerializeField] private float _launchPower = 200;

    private void Awake()
    {
        _initialPosition = transform.position;
        _birdWasLaunched = false;
        _timeSittingAround = 0;
        _gravityScale = 1;
        _numberOfPoints = 10;
        _points = new GameObject[_numberOfPoints];

        for (int i = 0; i < _numberOfPoints; i++)
        {
            _points[i] = Instantiate(_pointPrefab, transform.position, Quaternion.identity);
        }
    }
    private void Update()
    {
        float lowerBound = -20;
        float upperBound = 20;
        float leftBound = -20;
        float rightBound = 20;

        GetComponent<LineRenderer>().SetPosition(0, transform.position);
        GetComponent<LineRenderer>().SetPosition(1, _initialPosition);

        if (_birdWasLaunched && GetComponent<Rigidbody2D>().velocity.magnitude <= 0.1)
        {
            _timeSittingAround += Time.deltaTime;
        }

        if (transform.position.y > upperBound || transform.position.y < lowerBound || transform.position.x < leftBound || transform.position.x > rightBound || _timeSittingAround > 3)
        {
            string currentSceneName = SceneManager.GetActiveScene().name;
            SceneManager.LoadScene(currentSceneName);
        }
    }

    private void OnMouseDown()
    {
        GetComponent<LineRenderer>().enabled = true;
        GetComponent<TrailRenderer>().enabled = false;
        GetComponent<SpriteRenderer>().color = Color.red;
    }
    private void OnMouseUp()
    {
        GetComponent<LineRenderer>().enabled = false;
        GetComponent<TrailRenderer>().enabled = true;
        GetComponent<SpriteRenderer>().color = Color.white;

        Vector2 directionToInitialPosition = _initialPosition - transform.position;
        GetComponent<Rigidbody2D>().AddForce(directionToInitialPosition * _launchPower);
        GetComponent<Rigidbody2D>().gravityScale = _gravityScale;
        _birdWasLaunched = true;
    }
    private void OnMouseDrag()
    {
        Vector3 newPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        transform.position = new Vector3(newPosition.x, newPosition.y, 0);
        Vector2 directionToInitialPosition = _initialPosition - transform.position;
        for (int i = 0; i < _points.Length; i++)
        {
            _points[i].transform.position = PointPosition(i * 0.01f, directionToInitialPosition);
        }

    }
    Vector2 PointPosition(float t, Vector2 directionToInitialPosition)
    {
        //GetComponent<Rigidbody2D>().gravityScale = 1;
        Vector2 currentPointPos = (Vector2)transform.position + (directionToInitialPosition.normalized * _launchPower * t) + 0.5f * Physics2D.gravity * (t*t);
        Debug.Log(0.5f * Physics2D.gravity * (t * t));
        //GetComponent<Rigidbody2D>().gravityScale = 0;
        return currentPointPos;
    }
}
