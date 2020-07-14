using System.Collections;
using System.Collections.Generic;
using System.Net;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelController : MonoBehaviour
{
    private static int _nextLevelIndex = 1;
    private BM_Enemy[] _enemies;

    private void OnEnable()
    {
        _enemies = FindObjectsOfType<BM_Enemy>();
        
    }

    private void Update()
    {
        foreach(BM_Enemy enemy in _enemies)
        {
            if(enemy != null)
            {
                return;
            }

            Debug.Log("You killed all enemies");

            _nextLevelIndex++;
            string nextLevelName = "Level" + _nextLevelIndex;
            SceneManager.LoadScene(nextLevelName);
        }
    }
}
