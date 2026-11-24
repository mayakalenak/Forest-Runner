// I used AI as a resource to allow lane switches

using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    public static PlayerMovement Instance;

    [Header("Movement Settings")]
    [SerializeField] public float forwardSpeed = 10f;
    [SerializeField] private float laneDistance = 6f;
    [SerializeField] private float laneChangeSpeed = 20f;
    [SerializeField] public float speedIncreaseRate = 0.5f;
 
    private int lane = 1; 
    private Vector3 startPos;
    private Animator anim;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
        anim = GetComponent<Animator>();
        startPos = transform.position; 
    }

    void Update()
    {
        float targetX = (lane - 1) * laneDistance;

        float newX = Mathf.Lerp(transform.position.x, targetX, laneChangeSpeed * Time.deltaTime);

        transform.position = new Vector3(newX, transform.position.y, transform.position.z);
        transform.Translate(Vector3.forward * forwardSpeed * Time.deltaTime, Space.World);

        forwardSpeed += speedIncreaseRate * Time.deltaTime;
        
        if (anim != null)
        {
            anim.SetFloat("Speed", forwardSpeed);
        }
    }

    public void OnMoveLeft(InputValue value)
    {
        if (value.isPressed) 
        {
            lane = Mathf.Max(0, lane - 1);
        }
    }

    public void OnMoveRight(InputValue value)
    {
        if (value.isPressed) 
        {
            lane = Mathf.Min(2, lane + 1);
        }
    }

    // "bonus" when hitting a certain gem, the player will get a speed boost
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Gem"))
        {
            if (GameManager.Instance.score % 5 == 0) 
            {
                forwardSpeed += Spawner.Instance.difficulty;
            }
        }
    }

    public void ResetPlayer()
    {
        lane = 1;
        forwardSpeed = 10f;
        transform.position = startPos;
        GameManager.Instance.score = 0;
        CameraFollow.Instance?.ResetPosition();
    }
}