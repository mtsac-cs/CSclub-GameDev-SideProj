using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BM_Enemy : MonoBehaviour
{
    [SerializeField] private GameObject _cloudParticlePrefab;
    private void OnCollisionEnter2D(Collision2D collision)
    {
        bool didHitBird = collision.collider.GetComponent<YellowBird>() != null;
        if (didHitBird)
        {
            Instantiate(_cloudParticlePrefab, transform.position, Quaternion.identity);

            Destroy(gameObject);
            return;
        }

        BM_Enemy enemy = collision.collider.GetComponent<BM_Enemy>();
        if (enemy != null)
        {
            return;

        }

        if(collision.contacts[0].normal.y < -0.5)
        {
            Instantiate(_cloudParticlePrefab, transform.position, Quaternion.identity);
            Destroy(gameObject);
            return;
        }

    }
}
