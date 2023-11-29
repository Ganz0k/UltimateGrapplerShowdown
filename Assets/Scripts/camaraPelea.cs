using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class camaraPelea : MonoBehaviour
{
    public static Transform target1; // Primer GameObject a seguir
    public static Transform target2; // Segundo GameObject a seguir
    public Vector3 offset;   // Desplazamiento de la c�mara

    [Range(0.1f, 10.0f)]
    public float smoothness = 2.0f; // Suavidad de seguimiento

    private Vector3 midpoint;

    void Start()
    {
        midpoint = GetMidpoint();
    }

    void Update()
    {
        midpoint = GetMidpoint();
    }

    void LateUpdate()
    {
        Vector3 desiredPosition = midpoint + offset;
        transform.position = Vector3.Lerp(transform.position, desiredPosition, smoothness * Time.deltaTime);
    }

    Vector3 GetMidpoint()
    {
        Vector3 midpointPosition = (target1.position + target2.position) / 2f;
        return new Vector3(midpointPosition.x, midpointPosition.y, transform.position.z);
    }
}
