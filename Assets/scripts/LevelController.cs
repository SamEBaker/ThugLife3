using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelController : MonoBehaviour
{
    // Start is called before the first frame update
    Monster[] _monsters;

    [SerializeField] string _nextlevelName;
    
    void OnEnable()
    {
        _monsters = FindObjectsOfType<Monster>();
    }

    // Update is called once per frame
    void Update()
    {
        if (MonstersDead())
            GoToNextLevel();
    }
    
    void GoToNextLevel()
    {
        SceneManager.LoadScene(_nextlevelName);
    }
    bool MonstersDead()
    {
        foreach (var monster in _monsters)
        {
            if(monster.gameObject.activeSelf)
                return false;
        }
        
        return true;
    }
}

