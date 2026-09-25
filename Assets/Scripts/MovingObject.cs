using UnityEngine;

public class MovingObject : MonoBehaviour
{
    [SerializeField] private bool shouldMove = false; //shouldmove decide si el objeto debe moverse
    [SerializeField] private Vector3 movementAxis = Vector3.right; //define el eje del movimiento
    [SerializeField] private float movementDistance = 5f; //define la distancia que puede alejarse de la p inicial
    [SerializeField] private float movementSpeed = 2f; //define la velocidad a la que se mueve el objeto
    [SerializeField] private Vector3 rotationSpeed =
        new Vector3(0f, 90f, 0f); // gira 90grados por segundo alrededor de Y

    private Vector3 startPosition; //guarda la posición inicial del objeto
    private int movementDirection = 1; //define la dirección del movimiento (1 para adelante, -1 para atrás)
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private void Start() //start se ejecuta cuando inicia el juego
    {
        startPosition = transform.position; //guarda la posición inicial del objeto, y cada objeto guarda su posicion inicial
    }

    // Update is called once per frame
    private void Update() //ejecutandose cada frame
    {
        RotateObject();
        if (shouldMove) //solo los que tengan shouldMove activado se moverán con moveobject
        {
            MoveObject();
        }
    }

    private void RotateObject()
    {
        transform.Rotate(rotationSpeed * Time.deltaTime); //t.r cambia la rotacion actual y multiplicamos por deltatime para que la velocidad sea estable aunque cambien los frames
    }

    private void MoveObject()
    {
        Vector3 direction = movementAxis * movementDirection;
        transform.position += direction * movementSpeed * Time.deltaTime; //direction (a donde se mueve), movementSpeed (velocidad),movementDirection (positivo o negativo), Time.deltaTime (ajunte de tiempo entre frames)
        float distanceFromStart = Vector3.Distance(startPosition, transform.position); //calcula la distancia entre la posición actual y la posición inicial
            if (distanceFromStart >= movementDistance) //si la distancia desde la posición inicial es mayor o igual a la distancia de movimiento, cambia la dirección
            {   
                Vector3 offset = transform.position - startPosition; //calcula el desplazamiento desde la posición inicial
                transform.position = startPosition + offset.normalized * movementDistance; //ajusta la posición para que no se aleje más de la distancia de movimiento

                movementDirection *= -1; //invierte la dirección del movimiento
            }
    }
}
