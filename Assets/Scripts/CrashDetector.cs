using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CrashDetector : MonoBehaviour
{
    CircleCollider2D playerHead;
    [SerializeField] float reloadDelay = 0.5f;
    [SerializeField] ParticleSystem crashEffect;

    void Start() {
        playerHead = GetComponent<CircleCollider2D>();
    }
    void OnCollisionEnter2D(Collision2D other) {
        if (other.gameObject.tag == "Ground" && playerHead.IsTouching(other.collider)) {
            crashEffect.Play();
            Invoke("ReloadScene", reloadDelay);
        }
    }

    void ReloadScene() {
        SceneManager.LoadScene(0);
    }
}
