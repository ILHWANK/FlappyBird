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
    private bool _isDead;
    private AudioSource _audioSource;

    private void Start()
    {
        _audioSource = GetComponent<AudioSource>();
        ballRB2D = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        if (_isDead) return;

        if (Input.GetMouseButtonDown(0))
        {
            _audioSource.Play();
            Flap();
        }

        UpdateRotation();
    }

    private void Flap()
    {
        var velocity = ballRB2D.velocity;
        velocity.y = flapForce;
        ballRB2D.velocity = velocity;
    }

    private void UpdateRotation()
    {
        var verticalVelocity = ballRB2D.velocity.y;
        var t = Mathf.InverseLerp(-10f, 10f, verticalVelocity);
        var targetAngle = Mathf.Lerp(maxDownAngle, maxUpAngle, t);
        Quaternion targetRotation = Quaternion.Euler(0f, 0f, targetAngle);
        transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, rotationSmooth * Time.deltaTime);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (_isDead)
        {
            return;
        }

        _isDead = true;
        ballRB2D.velocity = Vector2.zero;
        ballRB2D.simulated = false;

        if (Score.score > Score.bestScore)
        {
            Score.bestScore = Score.score;
        }

        SceneManager.LoadScene("EndScene");
    }
}