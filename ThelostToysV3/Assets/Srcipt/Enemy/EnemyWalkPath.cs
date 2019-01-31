using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(EnemyBehavior))]
[RequireComponent(typeof(enemyController))]
public class EnemyWalkPath : MonoBehaviour
{
    private EnemyBehavior m_behabior;
    public Transform[] paths;
    public float time_betaweet_path;
    private float timer;
    private int index_path = 0;
    private enemyController m_controller;

    private void Start()
    {
        m_behabior = GetComponent<EnemyBehavior>();
        m_controller = GetComponent<enemyController>();
        timer = time_betaweet_path;
    }

    private void Update()
    {
        Debug.Log(timer);
        Debug.Log(index_path);
        if(m_behabior.target == null)
        {
            if(timer <= 0)
            {
                //next path
                next_path();
            }
            else
            {
                timer -= Time.deltaTime;
            }
        }
    }

    void next_path()
    {
        timer = time_betaweet_path;
        index_path++;

        if(index_path >= paths.Length)
        {
            index_path = 0;
        }

        m_controller.setWalk(paths[index_path]);
    }
}
