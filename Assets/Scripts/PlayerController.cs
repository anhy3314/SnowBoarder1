    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;

    public class PlayerController : MonoBehaviour
    {
        Rigidbody2D rb2D;
        [SerializeField] float torqueAmount = 1f;
        [SerializeField] float boostSpeed = 30f;
        [SerializeField] float baseSpeed = 20f;
        [SerializeField] float jumpForce = 10f;
        [SerializeField] float powerUpSpeed = 50f;
        [SerializeField] float powerUpDuration = 5f;
        SurfaceEffector2D surfaceEffector2D;
        bool isGrounded = true;
        bool isInvincible = false;

        void Start()
        {
            rb2D = GetComponent<Rigidbody2D>();
            surfaceEffector2D = FindFirstObjectByType<SurfaceEffector2D>();

            if (surfaceEffector2D == null)
             {
                    Debug.LogError("🚨 Không tìm thấy SurfaceEffector2D! Hãy kiểm tra xem nó có tồn tại trong Scene không.");
             }
            else
            {
                Debug.Log("✅ Tìm thấy SurfaceEffector2D thành công!" + surfaceEffector2D);
            }
    }

        void Update()
        {
            rotatePlayer();
            respondToBoost();
            respondToJump();
        if (surfaceEffector2D == null)
        {
            Debug.LogWarning("⚠ Mất tham chiếu SurfaceEffector2D! Đang tìm lại...");
            surfaceEffector2D = FindFirstObjectByType<SurfaceEffector2D>();

            if (surfaceEffector2D == null)
            {
                Debug.LogError("🚨 Không tìm thấy SurfaceEffector2D trong Scene! Tăng tốc không thể hoạt động.");
            }
            else
            {
                Debug.Log("✅ Tìm thấy SurfaceEffector2D thành công!");
            }
        }
    }

        void respondToBoost()
        {
            if(surfaceEffector2D == null)
            {
                Debug.LogWarning("⚠ surfaceEffector2D không tồn tại! Đang bỏ qua boost.");
                return;
            }
            if (Input.GetKey(KeyCode.DownArrow))
            {
                surfaceEffector2D.speed = boostSpeed;
            }
            else
            {
                surfaceEffector2D.speed = baseSpeed;
            }
        }

        void rotatePlayer()
        {
            if (Input.GetKey(KeyCode.LeftArrow))
            {
                rb2D.AddTorque(torqueAmount);
            }
            else if (Input.GetKey(KeyCode.RightArrow))
            {
                rb2D.AddTorque(-torqueAmount);
            }
        }

        void respondToJump()
        {
            if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
            {
                rb2D.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
                isGrounded = false;
            }
        }

        void OnCollisionEnter2D(Collision2D collision)
        {
            isGrounded = true;
        }

        void OnTriggerEnter2D(Collider2D other)
        {
            if (other.CompareTag("SpeedBoost"))
            {
                Debug.Log("🚀 Nhặt vật phẩm tăng tốc!");
                StartCoroutine(ActivateSpeedBoost());
                Destroy(other.gameObject);
            }
            else if (other.CompareTag("Invincibility"))
            {
                StartCoroutine(ActivateInvincibility());
                Destroy(other.gameObject);
            }
        }

    IEnumerator ActivateSpeedBoost()
    {
        if (surfaceEffector2D == null)
        {
            Debug.LogError("🚨 Không tìm thấy SurfaceEffector2D! Không thể tăng tốc.");
            yield break;
        }

        Debug.Log("🏎 Trước khi tăng tốc: " + surfaceEffector2D.speed);

        // 🔥 Tăng tốc lên powerUpSpeed thay vì 100 (dùng giá trị có sẵn)
        surfaceEffector2D.speed = powerUpSpeed;

        Debug.Log("🔥 Đã tăng tốc lên: " + surfaceEffector2D.speed);

        // ⚠ Tạm thời vô hiệu hóa `respondToBoost()` để tránh bị đặt lại tốc độ
        float savedBaseSpeed = baseSpeed;
        baseSpeed = powerUpSpeed; // Giữ tốc độ mới trong thời gian buff

        yield return new WaitForSeconds(powerUpDuration);

        // 🛑 Quay lại tốc độ bình thường sau khi buff kết thúc
        surfaceEffector2D.speed = savedBaseSpeed;
        baseSpeed = savedBaseSpeed; // Phục hồi tốc độ cơ bản

        Debug.Log("🛑 Hết buff, tốc độ trở lại: " + surfaceEffector2D.speed);
    }





    IEnumerator ActivateInvincibility()
        {
            isInvincible = true;
            yield return new WaitForSeconds(powerUpDuration);
            isInvincible = false;
        }
    }
