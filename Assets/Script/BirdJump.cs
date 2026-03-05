using UnityEngine;
using UnityEngine.SceneManagement;

public class BirdJump : MonoBehaviour
{
    [Header("Flap")]
    public float flapForce = 5f;

    [Header("Rotation")]
    public float maxUpAngle = 30f;
    public float maxDownAngle = -70f;
    public float rotationSmooth = 10f;

    private Rigidbody2D ballRB2D;
    private bool isDead;

    void Start()
    {
        ballRB2D = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        if (isDead) return;

        if (Input.GetMouseButtonDown(0))
        {
            GetComponent<AudioSource>().Play();
            Flap();
        }

        UpdateRotation();
    }

    private void Flap()
    {
        Vector2 velocity = ballRB2D.velocity;
        velocity.y = flapForce;
        ballRB2D.velocity = velocity;
    }

    private void UpdateRotation()
    {
        float verticalVelocity = ballRB2D.velocity.y;
        float t = Mathf.InverseLerp(-10f, 10f, verticalVelocity);
        float targetAngle = Mathf.Lerp(maxDownAngle, maxUpAngle, t);
        Quaternion targetRotation = Quaternion.Euler(0f, 0f, targetAngle);
        transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, rotationSmooth * Time.deltaTime);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isDead = true;
            ballRB2D.velocity = Vector2.zero;
            ballRB2D.simulated = false;
            return;
        }

        if (Score.score > Score.bestScore)
        {
            Score.bestScore = Score.score;
        }

        SceneManager.LoadScene("EndScene");
    }
}