using Cinemachine;
using UnityEngine;
using UnityEngine.InputSystem;

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

    // Lista de c�maras en el orden de rotaci�n
    public CinemachineVirtualCamera[] mainCameras;
    private int currentCameraIndex = 0; // �ndice de la c�mara actual

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
        //Debug.Log("pulsado + " + context.control.displayName);
        switch (context.control.displayName)
        {
            case "W":
                //RotateMainCamera(1); // Avanzar en el ciclo

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
        //        //RotateMainCamera(1); // Avanzar en el ciclo

        //        if (IsMainCameraActive())
        //        {
        //            SwitchToCamera(VirtualCameraPared, "VirtualCameraPared");
        //        }
        //        else
        //        {
        //            SwitchToCamera(VirtualCameraIdle, "VirtualCameraIdle");
        //            // Avanzar en el ciclo
        //        }
        //        break;
        //    case "S":
        //        SwitchToCamera(VirtualCameraAtras, "VirtualCameraAtras");
        //        // Retroceder en el ciclo
        //        break;

        //    case "D":
        //        SwitchToCamera(VirtualCameraDerecha, "VirtualCameraDerecha");
        //        ; // Retroceder en el ciclo
        //        break;
        //    case "A":
        //        SwitchToCamera(VirtualCameraIzquierda, "VirtualCameraIzquierda");
        //        break;
        //    case "Q":
        //        if (IsMainCameraActive()) SwitchToCamera(VirtualCameraExterior, "VirtualCameraExterior");
        //        break;
        //    case "E":
        //        if (IsMainCameraActive()) SwitchToCamera(VirtualCameraInterior, "VirtualCameraInterior");
        //        break;
        //}


    }


    private void RotateMainCamera(int direction)
    {
        // Desactivar la c�mara actual
        //mainCameras[currentCameraIndex].gameObject.SetActive(false);

        // Calcular el nuevo �ndice de forma c�clica
        currentCameraIndex = (currentCameraIndex + direction + mainCameras.Length) % mainCameras.Length;

        // Activar la nueva c�mara
        //mainCameras[currentCameraIndex].gameObject.SetActive(true);
        SwitchToCamera(mainCameras[currentCameraIndex], mainCameras[currentCameraIndex].name);
        //cameraAnimator.Play(mainCameras[currentCameraIndex].name);
    }

    private void SwitchToCamera(CinemachineVirtualCamera targetCamera, string cameraState)
    {
        Debug.Log("Cambiando a c�mara: " + cameraState);
        cameraState = cameraState.Replace(" ", "");

        // Desactivar todas las c�maras
        VirtualCameraAtras.gameObject.SetActive(false);
        VirtualCameraIzquierda.gameObject.SetActive(false);
        VirtualCameraDerecha.gameObject.SetActive(false);
        VirtualCameraIdle.gameObject.SetActive(false);
        VirtualCameraPared.gameObject.SetActive(false);
        VirtualCameraExterior.gameObject.SetActive(false);
        VirtualCameraInterior.gameObject.SetActive(false);



        // Activar la nueva c�mara
        targetCamera.gameObject.SetActive(true);
        cameraAnimator.Play(cameraState);

        Debug.Log("C�mara actual: " + targetCamera.name);
    }
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
