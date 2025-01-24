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
        // Detectar qu� flecha fue presionada y cambiar a la c�mara correspondiente
        if (context.control.displayName == "Flecha Arriba")
        {
            SwitchToCamera(cameraUp, "CameraUp"); // Transici�n a c�mara arriba
        }
        else if (context.control.displayName == "Flecha Abajo")
        {
            SwitchToCamera(cameraDown, "CameraDown"); // Transici�n a c�mara abajo
        }
        else if (context.control.displayName == "Flecha Izquierda")
        {
            SwitchToCamera(cameraLeft, "CameraLeft"); // Transici�n a c�mara izquierda
        }
        else if (context.control.displayName == "Flecha Derecha")
        {
            SwitchToCamera(cameraRight, "CameraRight"); // Transici�n a c�mara derecha
        }
        else if (context.control.displayName == "M")    
        {
            SwitchToCamera(cameraRight, "CameraMaqueta"); // Transici�n a c�mara maqueta
        }
    }

    private void SwitchToCamera(CinemachineVirtualCamera targetCamera, string cameraState)
    {
        // Desactivar todas las c�maras primero
        cameraUp.gameObject.SetActive(false);
        cameraDown.gameObject.SetActive(false);
        cameraLeft.gameObject.SetActive(false);
        cameraRight.gameObject.SetActive(false);

        // Activar la c�mara correspondiente
        targetCamera.gameObject.SetActive(true);

        // Cambiar al estado correspondiente en el Animator para la transici�n de la c�mara
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
