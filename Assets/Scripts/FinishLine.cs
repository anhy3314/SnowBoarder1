using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FinishLine : MonoBehaviour
{   
    [SerializeField] float reloadDelay = 0.5f;
    [SerializeField] ParticleSystem finishEffect;
    [SerializeField] int NextLevelScene;

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            finishEffect.Play();
            StartCoroutine(WaitAndLoadLevel(reloadDelay));
        }
    }

    IEnumerator WaitAndLoadLevel(float delay)
    {
        yield return new WaitForSeconds(delay);
        LoadNewLevel(NextLevelScene);
    }

    void LoadNewLevel(int nextLevelScence)
    {
        SceneManager.LoadScene(nextLevelScence);
    }

    void ReloadScene() {
        SceneManager.LoadScene(0);
    }
}
