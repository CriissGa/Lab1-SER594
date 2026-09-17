using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem; // uso del teclado

public class PlayerMovement : MonoBehaviour // podremos ejecutar awake, update y fixed update
{
    // Start is called once before the first execution of Update after the MonoBehaviour i
    // SerializeField nos permite exponer variables privadas en el inspector de Unity para poder modificarlas desde allí sin necesidad de hacerlas públicas
    [SerializeField] private float moveSpeed = 5f; // velocidad de movimiento del jugador
    [SerializeField] private float jumpForce = 6f; // fuerza de salto del jugador
    [SerializeField] private float turnSpeed = 10f; // velocidad de giro del jugador
    [SerializeField] private float groundCheckDistance = 1.1f; // distancia para verificar si el jugador está en el suelo

    private Rigidbody rb; // referencia al componente Rigidbody del jugador para aplicar movimiento fisico
    private Vector2 moveInput; // entrada de movimiento del jugador, Vector2 guarda 2 numeros X y Y
    private bool jumpRequested; // bandera para indicar si se ha solicitado un salto
    private void Awake() //se ejecuta una vez al inicio del juego, antes de cualquier otra función
    {
        rb = GetComponent<Rigidbody>(); // buscamos el comp Rigidbody del personaje para usarlo despues
    }

    // Update is called once per frame
    private void Update() //update se ejecuta cada frame (para leer el teclado)
    {
        Keyboard keyboard = Keyboard.current; // obtenemos el teclado actual

        if (keyboard == null)
        {
            return;
        }
        float horizontal = 0f; //variable para guardar el movimiento horizontal
        float vertical = 0f; //variable para guardar el movimiento vertical

        if (keyboard.aKey.isPressed)
        {
            horizontal -= 1f;
        }
        if (keyboard.dKey.isPressed)
        {
            horizontal += 1f;
        }
        if (keyboard.sKey.isPressed)
        {
            vertical -= 1f;
        }
        if (keyboard.wKey.isPressed)
        {
            vertical += 1f;
        }

        moveInput = new Vector2(horizontal, vertical).normalized; // normalizamos el vector de entrada para que no supere 1

        if (keyboard.spaceKey.wasPressedThisFrame)
        {
            jumpRequested = true; // si se presiona la barra espaciadora solicitamos un salto
        }
    }

    private void FixedUpdate() //se ejecuta en intervalos regulares (lo mejor para fisicas)/ (mover el RigidBody)
    {
        Vector3 moveDirection = new Vector3(moveInput.x, 0f, moveInput.y); // convertimos el vector de entrada en un vector 3D para mover al jugador

        Vector3 currentVelocity = rb.linearVelocity; // obtenemos la velocidad actual del jugador

        rb.linearVelocity = new Vector3(moveDirection.x * moveSpeed, currentVelocity.y, moveDirection.z * moveSpeed); // aplicamos la velocidad al RigidBody del jugador
        
        if (moveDirection.sqrMagnitude > 0.01f) // si el jugador se esta moviendo
        {
            Quaternion targetRotation = Quaternion.LookRotation(moveDirection); // calculamos la rotacion deseada del jugador
            Quaternion smoothRotation = Quaternion.Slerp(rb.rotation, targetRotation, turnSpeed * Time.fixedDeltaTime); // aplicamos la rotacion al RigidBody del jugador
            rb.MoveRotation(smoothRotation); // aplicamos la rotacion al RigidBody del jugador
        }

        bool isGrounded = Physics.Raycast(transform.position, Vector3.down, groundCheckDistance); // verificamos si el jugador esta en el suelo
        if (isGrounded && jumpRequested)
        {
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse); // aplicamos una fuerza hacia arriba para hacer saltar al jugador
        }
        jumpRequested = false; // reiniciamos la bandera de salto

    }

    
}
