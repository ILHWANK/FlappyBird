using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BirdJump : MonoBehaviour
{

    Rigidbody2D rb;
    public float jumpPower;

    public float shotVelocity;
    public float shotAngle;

    private bool isGround = true;
    private bool isCenter = false;
    private float totalTime = 0f;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            GetComponent<AudioSource>().Play();
            rb.velocity = Vector2.up * jumpPower;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        // 그라운드에 착지
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGround = true;
            //총 걸린 시간
            Debug.Log("Totaltime: " + totalTime);
            // 초기위치가 -8에서 시작 했기 때문에 +8로 보정 (총 이동거리)
            Debug.Log("TotalMeter: " + (transform.position.x + 8));

            Verification();
        }
        else if(!collision.gameObject.CompareTag("Ground"))
        {
            if (Score.score > Score.bestScore)
            {
                Score.bestScore = Score.score;
            }

            SceneManager.LoadScene("EndScene");
        }
    }

    private void Verification()
    {
        // 공식을 적용한 계산값 확인.
        Debug.Log("=== Verification ===");

        // 총걸린 시간은 2t
        // 2 * V* sin(theta)/g 
        float totalTime = 2 * shotVelocity * Mathf.Sin(shotAngle * Mathf.Deg2Rad) / 9.81f;
        // 최고 높이 (V*sin(theta))^2 / (2g)
        float centerHeight = Mathf.Pow(shotVelocity * Mathf.Sin(shotAngle * Mathf.Deg2Rad), 2) / (2 * 9.81f); //
        // 총 날아간 거리 2*V^2*sin(theta)*cos(theta)/g = > 2sin(theta)cos(theta) == sing(2theta) => v^2/g*sin(2*theta)
        float totalMeter = Mathf.Pow(shotVelocity, 2) / 9.81f * Mathf.Sin(2 * shotAngle * Mathf.Deg2Rad); // 

        Debug.Log("Totaltime: " + totalTime);
        Debug.Log("CenterHeight: " + centerHeight);
        Debug.Log("TotalMeter: " + totalMeter);

        // Simulation 값과 Verification 결과의 오차가 발생하는 이유
        // Project Settings - Physics 2D의 속성값에 따라 계산 결과가 다르기 때문이다.
    }

}
