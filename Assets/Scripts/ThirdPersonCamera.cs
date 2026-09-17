using UnityEngine;

public class ThirdPersonCamera : MonoBehaviour
{
    [SerializeField] private Transform target; // referencia al objetivo que la cámara seguirá
    [SerializeField] private float distance = 5f; // distancia de la cámara al objetivo
    [SerializeField] private float height = 2f; // altura de la cámara respecto al objetivo
    [SerializeField] private float lookHeigth = 1f; // altura a la que la cámara mira al objetivo
    [SerializeField] private float followSpeed = 8f; // velocidad de seguimiento de la cámara
    [SerializeField] private float rotationSpeed = 5f; // velocidad de rotación de la cámara
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private void LateUpdate()
    {
        if (target == null)
        {
            return; // si no hay objetivo, no hacemos nada
        }
    }

    
}
