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
    private void LateUpdate() // unity primero ejectuta Update, luego FixedUpdate y luego LateUpdate (para que la camara se mueva despues de que el jugador se mueva)
    {
        if (target == null)
        {
            return; // si no hay player, no hacemos nada
        }

        //posicion deseada  = Player - detras + arriba
        Vector3 desiredPosition = target.position - target.forward * distance + Vector3.up * height; // calculamos la posicion deseada de la camara
        //uso de lerp para suavizar el movimiento de la camara
        transform.position = Vector3.Lerp(transform.position, desiredPosition, followSpeed * Time.deltaTime); // interpolamos la posicion de la camara hacia la posicion deseada

        Vector3 lookPoint= target.position + Vector3.up * lookHeight; // calculamos el punto al que la camara debe mirar

        Quaternion desiredRotation = Quaternion.LookRotation(lookPoint - transform.position); // calculamos la rotacion deseada de la camara
        transform.rotation = Quaternion.Slerp(transform.rotation, desiredRotation, rotationSpeed * Time.deltaTime); // interpolamos la rotacion de la camara hacia la rotacion deseada
    
    }

}
