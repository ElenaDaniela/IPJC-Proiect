using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    
    [SerializeField]
    private Transform playerTransform;
    [SerializeField]
    private float leftLimit;
    [SerializeField]
    private float topLimit;
    [SerializeField]
    private float rightLimit;
    [SerializeField]
    private float bottomLimit;
    
    void Start()
    {
        playerTransform = GameObject.FindGameObjectWithTag ("Player").transform;
    }
    void LateUpdate()
    {
        Vector3 temp = transform.position; // iau pozitia camerei

        temp.x = playerTransform.position.x; // pun pozitia caracterului la camera
        temp.y = playerTransform.position.y;
        
        transform.position = temp; // updatez pozitia camerei
        
        // stript pentru limitarea camerei
        transform.position = new Vector3(
            Mathf.Clamp(transform.position.x, leftLimit, rightLimit),
            Mathf.Clamp(transform.position.y,bottomLimit,topLimit),
            transform.position.z
        );

    }
}
