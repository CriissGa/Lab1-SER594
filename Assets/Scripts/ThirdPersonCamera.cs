using UnityEngine;

public class ThirdPersonCamera : MonoBehaviour
{
    [SerializeField] private Transform target; // referencia al player que la cámara seguirá (posicion y direccion a la que mira)
    //variables para controlar el comportamiento de la camara
    [SerializeField] private float distance = 5f; // distancia de la cámara al player
    [SerializeField] private float height = 2f; // altura de la cámara respecto al player
    [SerializeField] private float lookHeight = 1f; // altura a la que la cámara mira al player
    [SerializeField] private float followSpeed = 8f; // velocidad de seguimiento de la cámara
    [SerializeField] private float rotationSpeed = 5f; // velocidad de rotación de la cámara
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private void LateUpdate()
    {
        if (target == null)
        {
            return;
        }

        // Seguimos detrás de la orientación del personaje.
        transform.position =
            target.position
            - target.forward * distance
            + Vector3.up * height;

        transform.LookAt(target.position + Vector3.up * lookHeight); // Miramos hacia el personaje.
    }

}
