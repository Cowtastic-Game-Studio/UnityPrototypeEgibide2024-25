using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using Cinemachine;

public class CameraGestor : MonoBehaviour
{
    [SerializeField] private Animator cameraAnimator; // El Animator con los clips de las transiciones
    [SerializeField] private CinemachineVirtualCamera cameraUp;
    [SerializeField] private CinemachineVirtualCamera cameraDown;
    [SerializeField] private CinemachineVirtualCamera cameraLeft;
    [SerializeField] private CinemachineVirtualCamera cameraRight; 
    [SerializeField] private CinemachineVirtualCamera cameraMaqueta;

    [SerializeField] private InputAction cameraSwitchAction;

    private void Awake()
    {
        // Inicializar el Input System
        //var playerInput = new InputActionMap("CameraControls");
        cameraAnimator = GetComponent<Animator>();
        // Asignar acciones a cada flecha
        //cameraSwitchAction = playerInput.AddAction("SwitchCamera", binding: "<Keyboard>/upArrow,<Keyboard>/downArrow,<Keyboard>/leftArrow,<Keyboard>/rightArrow");

        cameraSwitchAction.performed += OnCameraSwitch;
       // playerInput.Enable();
    }

    private void OnCameraSwitch(InputAction.CallbackContext context)
    {
        Debug.Log("pulsado + " + context.control.displayName);
        // Detectar qué flecha fue presionada y cambiar a la cámara correspondiente
        if (context.control.displayName == "Flecha Arriba")
        {
            SwitchToCamera(cameraUp, "CameraUp"); // Transición a cámara arriba
        }
        else if (context.control.displayName == "Flecha Abajo")
        {
            SwitchToCamera(cameraDown, "CameraDown"); // Transición a cámara abajo
        }
        else if (context.control.displayName == "Flecha Izquierda")
        {
            SwitchToCamera(cameraLeft, "CameraLeft"); // Transición a cámara izquierda
        }
        else if (context.control.displayName == "Flecha Derecha")
        {
            SwitchToCamera(cameraRight, "CameraRight"); // Transición a cámara derecha
        }
        else if (context.control.displayName == "M")    
        {
            SwitchToCamera(cameraRight, "CameraMaqueta"); // Transición a cámara maqueta
        }
    }

    private void SwitchToCamera(CinemachineVirtualCamera targetCamera, string cameraState)
    {
        // Desactivar todas las cámaras primero
        cameraUp.gameObject.SetActive(false);
        cameraDown.gameObject.SetActive(false);
        cameraLeft.gameObject.SetActive(false);
        cameraRight.gameObject.SetActive(false);

        // Activar la cámara correspondiente
        targetCamera.gameObject.SetActive(true);

        // Cambiar al estado correspondiente en el Animator para la transición de la cámara
        cameraAnimator.Play(cameraState);
    }

    private void OnEnable()
    {
        cameraSwitchAction.Enable();
    }

    private void OnDisable()
    {
        cameraSwitchAction.Disable();
    }
}
