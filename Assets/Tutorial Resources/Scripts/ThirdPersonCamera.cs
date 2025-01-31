using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThirdPersonCamera : MonoBehaviour
{
    public Transform target; // Objeto objetivo a seguir
    public float distance = 5.0f; // Distancia de la c�mara al objeto objetivo
    public float height = 2.0f; // Altura de la c�mara respecto al objeto objetivo
    public float smoothSpeed = 10.0f; // Velocidad de suavizado del movimiento de la c�mara

    private Vector3 offset; // Distancia inicial entre la c�mara y el objeto objetivo

    void Start()
    {
        offset = new Vector3(0, height, -distance); // Calcula el offset inicial
    }

    void LateUpdate()
    {
        Vector3 desiredPosition = target.position + offset; // Calcula la posici�n deseada de la c�mara
        Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed * Time.deltaTime); // Aplica suavizado al movimiento de la c�mara
        transform.position = smoothedPosition; // Actualiza la posici�n de la c�mara

        transform.LookAt(target); // Mira siempre hacia el objeto objetivo
    }
}