using UnityEngine;

public class DynamicCamera : MonoBehaviour
{
    [SerializeField] Transform target;
    [SerializeField] float tiltAmount;
    [SerializeField] float maxOffset;
    [SerializeField] float shakeAmount;
    [SerializeField] float smoothSpeed;

    Vector3 initialPosition;
    Quaternion initialRotation;

    void Start()
    {
        initialPosition = new Vector3(0, 3.5f, -12f);
        initialRotation = Quaternion.Euler(8f, 0f, 0f);
        transform.localPosition = initialPosition;
        transform.localRotation = initialRotation;
    }

    void LateUpdate()
    {
        Vector3 desiredPosition = target.position;
        Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);
        transform.position = smoothedPosition;

        float tilt = Input.GetAxis("Horizontal") * tiltAmount;
        Quaternion targetRotation = Quaternion.Euler(8, 0, -tilt);
        transform.localRotation = Quaternion.Lerp(transform.localRotation, targetRotation, Time.deltaTime * 5f);

        float offsetZ = 0f;
        if (Input.GetKey(KeyCode.W)) 
        {
            offsetZ = maxOffset;
        } 
        else if (Input.GetKey(KeyCode.S))
        {
            offsetZ = -maxOffset;
        } 
        transform.localPosition = initialPosition + new Vector3(0, 0, offsetZ);

        if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.Space))
        {
            float offsetX = Mathf.PerlinNoise(Time.time * 10f, 0) * shakeAmount;
            float offsetY = Mathf.PerlinNoise(0, Time.time * 10f) * shakeAmount;
            transform.localPosition += new Vector3(offsetX, offsetY, 0);
        }
    }
}