using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem; // uso del teclado

public class PlayerMovement : MonoBehaviour // podremos ejecutar awake, update y fixed update
{
    // Start is called once before the first execution of Update after the MonoBehaviour i
    // SerializeField nos permite exponer variables privadas en el inspector de Unity para poder modificarlas desde allí sin necesidad de hacerlas públicas
    [SerializeField] private float moveSpeed = 5f; // velocidad de movimiento del jugador
    [SerializeField] private float jumpForce = 6f; // fuerza de salto del jugador
    [SerializeField] private float mouseSensitivity = 0.15f;
    [SerializeField] private float turnSmoothTime = 0.08f; // tiempo de giro suave del jugador
    [SerializeField] private float groundCheckDistance = 1.1f; // distancia para verificar si el jugador está en el suelo

    private float targetYaw;
    private float turnVelocity;

    private Rigidbody rb; // referencia al componente Rigidbody del jugador para aplicar movimiento fisico
    private Vector2 moveInput; // entrada de movimiento del jugador, Vector2 guarda 2 numeros X y Y
    private bool jumpRequested; // bandera para indicar si se ha solicitado un salto
    private void Awake() //se ejecuta una vez al inicio del juego, antes de cualquier otra función
    {
        rb = GetComponent<Rigidbody>(); // buscamos el comp Rigidbody del personaje para usarlo despues
        rb.freezeRotation = true; // no quiero que el jugador gire al chocar (arreglamos la camara)
        targetYaw = rb.rotation.eulerAngles.y;
        rb.interpolation = RigidbodyInterpolation.Interpolate;
    }

    // Update is called once per frame
    private void Update() //update se ejecuta cada frame (para leer el teclado)
    {
        Mouse mouse = Mouse.current;

        if (mouse != null && mouse.rightButton.isPressed)
        {
            targetYaw += mouse.delta.ReadValue().x * mouseSensitivity;
        }

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
        // Suavizamos el giro hacia la orientación elegida con el mouse.
        float smoothYaw = Mathf.SmoothDampAngle(
            rb.rotation.eulerAngles.y,
            targetYaw,
            ref turnVelocity,
            turnSmoothTime,
            Mathf.Infinity,
            Time.fixedDeltaTime
        );

        Quaternion playerRotation = Quaternion.Euler(0f, smoothYaw, 0f);
        rb.MoveRotation(playerRotation);

        // Convertimos WASD en movimiento según la orientación del personaje
        Vector3 localMovement = new Vector3(moveInput.x, 0f, moveInput.y);
        Vector3 moveDirection = playerRotation * localMovement;

        // Conservamos la velocidad vertical para la gravedad y el salto
        Vector3 currentVelocity = rb.linearVelocity;

        rb.linearVelocity = new Vector3(
            moveDirection.x * moveSpeed,
            currentVelocity.y,
            moveDirection.z * moveSpeed
        );

        bool isGrounded = Physics.Raycast(
            transform.position,
            Vector3.down,
            groundCheckDistance
        );

        if (isGrounded && jumpRequested)
        {
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
        }

        jumpRequested = false;
    }

    
}
