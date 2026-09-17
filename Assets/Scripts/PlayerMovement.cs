using UnityEngine;
using UnityEngine.InputSystem; // uso del teclado

public class PlayerMovement : MonoBehaviour // podremos ejecutar awake, update y fixed update
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    [SerializeField] private float speed = 5f; // velocidad de movimiento del jugador
    [SerializeField] private float jumpForce = 5f; // fuerza de salto del jugador
    [SerializeField] private float turnSpeed = 5f; // velocidad de giro del jugador
    [SerializeField] private float groundCheckDistance = 0.1f; // distancia para verificar si el jugador está en el suelo

    private Rigidbody rb; // referencia al componente Rigidbody del jugador
    private Vector2 moveInput; // entrada de movimiento del jugador, Vector2 guarda 2 numeros X y Y
    private bool jumpRequested; // bandera para indicar si se ha solicitado un salto
    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    private void Update()
    {
        Keyboard keyboard = Keyboard.current; // obtenemos el teclado actual

        if (keyboard == null)
        {
            return;
        }
        float horizontal = 0f;
        float vertical = 0f;    

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
}
