using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class YellowBird : MonoBehaviour
{

    private Vector3 _intitialPosition;
    private bool _birdWasLaunched;
    private float _timeSittingAround;

    [SerializeField] private float _launchPower = 350;
    

    private void Awake()
    {
        _intitialPosition = transform.position;
    }


    private void Update()
    {

        if(_birdWasLaunched && GetComponent<Rigidbody2D>().velocity.magnitude <= .1)
        {
            _timeSittingAround += Time.deltaTime;
        }

        if (transform.position.y > 10 || 
            transform.position.y < -10 || 
            transform.position.x > 20 || 
            transform.position.x < -30 || 
            _timeSittingAround >= 3)
        {

            string currentSceneName = UnityEngine.SceneManagement.SceneManager.GetActiveScene().name;
            UnityEngine.SceneManagement.SceneManager.LoadScene(currentSceneName);
        }
    }

    private void OnMouseDown()
    {
        GetComponent<SpriteRenderer>().color = Color.red;
    }

    private void OnMouseUp()
    {
        GetComponent<SpriteRenderer>().color = Color.white;

         Vector2 DirectionToInitialPosition = _intitialPosition - transform.position;
         GetComponent<Rigidbody2D>().AddForce(DirectionToInitialPosition * _launchPower);

        GetComponent<Rigidbody2D>().gravityScale = 1;

        _birdWasLaunched = true;
    }

    private void OnMouseDrag()
    {

        Vector3 newPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        transform.position = new Vector3(newPosition.x, newPosition.y);
    }
}
