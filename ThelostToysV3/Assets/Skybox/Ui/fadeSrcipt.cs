using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class fadeSrcipt : MonoBehaviour {

    private Animator m_animator;
    private string _nextScene;

    private void Start()
    {
        m_animator = GetComponent<Animator>();
    }

    public void fade_triger(string nextScene)
    {
        m_animator.SetTrigger("next");
        _nextScene = nextScene;
    }

    private void goto_Scene()
    {
        SceneManager.LoadScene(_nextScene);
    }
}
