// I used AI as a resource to find a way to allow the visuals of the game to flow while the lanes switch

using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public static CameraFollow Instance;

    [SerializeField] private Transform target;
    [SerializeField] private float smoothSpeed = 0.125f;
    [SerializeField] private Vector3 offset = new Vector3(0, 5, -8);
    [SerializeField] private float rotationSmoothness = 5f;

    private void Awake()
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
    }

    void Update()
    {
        if (target == null) return;

        // Desired camera position 
        Vector3 desiredPosition = target.position + offset;
        
        // Smooth position
        Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);
        
        transform.position = new Vector3(
            smoothedPosition.x,
            smoothedPosition.y,
            smoothedPosition.z
        );

        // Smooth camera rotation at lane changes
        Vector3 lookTarget = target.position + Vector3.forward * 5;
        Quaternion targetRotation = Quaternion.LookRotation(lookTarget - transform.position);
        transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotationSmoothness * Time.deltaTime);
    }

    public void ResetPosition()
    {
        if (target != null)
        {
            transform.position = target.position + offset;
            Vector3 lookTarget = target.position + Vector3.forward * 5;
            transform.rotation = Quaternion.LookRotation(lookTarget - transform.position);
        }
    }
}