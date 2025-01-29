using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using Cinemachine;
using UnityEditor.Rendering;

public class CameraGestor : MonoBehaviour
{
    [SerializeField] private Animator cameraAnimator; // El Animator con los clips de las transiciones
    [SerializeField] private CinemachineVirtualCamera VirtualCameraIdle;
    [SerializeField] private CinemachineVirtualCamera VirtualCameraAtras;
    [SerializeField] private CinemachineVirtualCamera VirtualCameraIzquierda;
    [SerializeField] private CinemachineVirtualCamera VirtualCameraDerecha;
    [SerializeField] private CinemachineVirtualCamera VirtualCameraInterior;
    [SerializeField] private CinemachineVirtualCamera VirtualCameraExterior;
    [SerializeField] private CinemachineVirtualCamera VirtualCameraPared;

    [SerializeField] private InputAction cameraSwitchAction;

    // Lista de cámaras en el orden de rotación
    public CinemachineVirtualCamera[] mainCameras;
    private int currentCameraIndex = 0; // Índice de la cámara actual

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
        switch (context.control.displayName)
        {
            case "W":
                if (IsMainCameraActive())
                {
                    SwitchToCamera(VirtualCameraPared, "VirtualCameraPared");
                }
                else
                {
                    RotateMainCamera(1); // Avanzar en el ciclo
                }
                break;
            case "S":
                RotateMainCamera(-1); // Retroceder en el ciclo
                break;

            case "D":
                RotateMainCamera(1); // Retroceder en el ciclo
                break;
            case "A":
                RotateMainCamera(-1); // Retroceder en el ciclo
                break;
            case "Q":
                if (IsMainCameraActive()) SwitchToCamera(VirtualCameraExterior, "VirtualCameraExterior");
                break;
            case "E":
                if (IsMainCameraActive()) SwitchToCamera(VirtualCameraInterior, "VirtualCameraInterior");
                break;
        }

        //switch (context.control.displayName)
        //{
        //    case "W":
        //        if (IsMainCameraActive())
        //        {
        //            // If not on the main camera, switch to Camarapareed
        //            SwitchToCamera(cameraPared, "CameraPared");
        //        }
        //        else
        //        {
        //            RotateCamera(2); // Puedes ajustar esto según tu lógica

        //            //SwitchToCamera(cameraUp, "CameraUp"); // Transición a cámara principal
        //        }
        //        break;
        //    //case "S":
        //    //    SwitchToCamera(cameraDown, "CameraDown");
        //    //    break;
        //    //case "A":
        //    //    SwitchToCamera(cameraLeft, "CameraLeft");
        //    //    break;
        //    //case "D":
        //    //    SwitchToCamera(cameraRight, "CameraRight");
        //    //    break;
        //    case "Q":
        //        if (IsMainCameraActive())
        //        {
        //            SwitchToCamera(cameraExterior, "CameraExterior");
        //        }
        //        break;
        //    case "E":
        //        if (IsMainCameraActive())
        //        {
        //            SwitchToCamera(cameraInterior, "CameraInterior");
        //        }
        //        break;
        //    case "D": // Rotar a la derecha
        //        RotateCamera(1); // 1 para avanzar en la lista de cámaras
        //        break;
        //    case "A": // Rotar a la izquierda
        //        RotateCamera(-1); // -1 para retroceder en la lista de cámaras
        //        break;
        //    case "S": // Rotar hacia abajo (opcional)
        //        RotateCamera(-2); // Puedes ajustar esto según tu lógica
        //        break;
        //}
    }


    private void RotateMainCamera(int direction)
    {
        // Desactivar la cámara actual
        mainCameras[currentCameraIndex].gameObject.SetActive(false);

        // Calcular el nuevo índice de forma cíclica
        currentCameraIndex = (currentCameraIndex + direction + mainCameras.Length) % mainCameras.Length;

        // Activar la nueva cámara
        //mainCameras[currentCameraIndex].gameObject.SetActive(true);
        SwitchToCamera(mainCameras[currentCameraIndex], mainCameras[currentCameraIndex].name);
        //cameraAnimator.Play(mainCameras[currentCameraIndex].name);
    }

    private void SwitchToCamera(CinemachineVirtualCamera targetCamera, string cameraState)
    {
        foreach (var cam in mainCameras)
        {
            cam.gameObject.SetActive(false);
        }

        VirtualCameraPared.gameObject.SetActive(false);
        VirtualCameraExterior.gameObject.SetActive(false);
        VirtualCameraInterior.gameObject.SetActive(false);

        targetCamera.gameObject.SetActive(true);
        cameraAnimator.Play(cameraState);
    }

    //private void SwitchToCamera(CinemachineVirtualCamera targetCamera, string cameraState)
    //{
    //    // Desactivar todas las cámaras primero
    //    cameraUp.gameObject.SetActive(false);
    //    cameraDown.gameObject.SetActive(false);
    //    cameraLeft.gameObject.SetActive(false);
    //    cameraRight.gameObject.SetActive(false);
    //    cameraExterior.gameObject.SetActive(false);
    //    cameraInterior.gameObject.SetActive(false);
    //    cameraPared.gameObject.SetActive(false);

    //    // Activar la cámara correspondiente
    //    targetCamera.gameObject.SetActive(true);

    //    // Cambiar al estado correspondiente en el Animator para la transición de la cámara
    //    cameraAnimator.Play(cameraState);
    //}
    private bool IsMainCameraActive()
    {
        // Check if the main camera is active (you can define what the main camera is)
        return VirtualCameraIdle.gameObject.activeSelf; // Adjust this condition based on your setup
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
