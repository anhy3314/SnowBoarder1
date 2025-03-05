using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CrashDetector : MonoBehaviour
{
    CircleCollider2D playerHead;
    [SerializeField] float reloadDelay = 0.5f;
    [SerializeField] ParticleSystem crashEffect;

    public GameObject gameOverScreen;

    void Start() {
        playerHead = GetComponent<CircleCollider2D>();
        gameOverScreen.SetActive(false);
    }

    void OnCollisionEnter2D(Collision2D other) {
        if (other.gameObject.tag == "Ground" && playerHead.IsTouching(other.collider)) {
            crashEffect.Play();
            GameOverScreen();
        }
    }

    void GameOverScreen()
    {
        gameOverScreen.SetActive(true);
    }

    void ReloadScene() {
        SceneManager.LoadScene(0);
    }
}
