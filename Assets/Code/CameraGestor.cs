using Cinemachine;
using CowtasticGameStudio.MuuliciousHarvest;
using System.Collections;
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

    // Lista de cámaras en el orden de rotación
    public CinemachineVirtualCamera[] mainCameras;
    private int currentCameraIndex = 0; // Índice de la cámara actual

    [SerializeField] private HUDManager HUDManager;

    private void Awake()
    {
        // Inicializar el Input System        
        cameraAnimator = GetComponent<Animator>();
        cameraSwitchAction.performed += OnCameraSwitch;
        // playerInput.Enable();

        SwitchToCamera(VirtualCameraIdle, nameof(VirtualCameraIdle));
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

                break;
            case "S":
                if (IsParedCameraActive() || IsExteriorCameraActive() || IsInteriorCameraActive())
                {
                    SwitchToCamera(VirtualCameraIdle, "VirtualCameraIdle");
                }
                else
                {
                    RotateMainCamera(-2); // Retroceder en el ciclo

                }
                break;
            case "D":
                if (!IsParedCameraActive() && !IsExteriorCameraActive() && !IsInteriorCameraActive())
                {
                    RotateMainCamera(1);
                }
                break;
            case "A":
                if (!IsParedCameraActive() && !IsExteriorCameraActive() && !IsInteriorCameraActive())
                {
                    RotateMainCamera(-1);
                }
                break;
            case "Q":
                if (IsMainCameraActive())
                {
                    SwitchToCamera(VirtualCameraInterior, "VirtualCameraExterior");
                }
                else if (IsInteriorCameraActive())
                {
                    SwitchToCamera(VirtualCameraExterior, "VirtualCameraExterior");
                }
                break;
            case "E":
                if (IsMainCameraActive())
                {
                    SwitchToCamera(VirtualCameraInterior, "VirtualCameraInterior");
                }
                else if (IsExteriorCameraActive())
                {
                    SwitchToCamera(VirtualCameraInterior, "VirtualCameraInterior");
                }
                break;
        }
    }
    private void RotateMainCamera(int direction)
    {
        currentCameraIndex = (currentCameraIndex + direction + mainCameras.Length) % mainCameras.Length;
        SwitchToCamera(mainCameras[currentCameraIndex], mainCameras[currentCameraIndex].name);
    }

    private void SwitchToCamera(CinemachineVirtualCamera targetCamera, string cameraState)
    {
        //Debug.Log("Cambiando a cámara: " + cameraState);
        cameraState = cameraState.Replace(" ", "");

        // Desactivar todas las cámaras
        VirtualCameraAtras.gameObject.SetActive(false);
        VirtualCameraIzquierda.gameObject.SetActive(false);
        VirtualCameraDerecha.gameObject.SetActive(false);
        VirtualCameraIdle.gameObject.SetActive(false);
        VirtualCameraPared.gameObject.SetActive(false);
        VirtualCameraExterior.gameObject.SetActive(false);
        VirtualCameraInterior.gameObject.SetActive(false);

        // Activar la nueva cámara
        targetCamera.gameObject.SetActive(true);
        cameraAnimator.Play(cameraState);
       
        // Actualizar el hud con un delay para que no aparezca el hud en mitad de la transición de camaras
        StartCoroutine(DelayedHUDUpdate(targetCamera, 1f));

        //Debug.Log("Cámara actual: " + targetCamera.name);
    }

    // Corrutina para retrasar la actualización del HUD
    private IEnumerator DelayedHUDUpdate(CinemachineVirtualCamera targetCamera, float delay)
    {
        yield return new WaitForSeconds(delay);
        HUDManager.UpdateHUDForCamera(targetCamera);
    }

    private bool IsMainCameraActive()
    {      
        return VirtualCameraIdle.gameObject.activeSelf;
    }

    private bool IsInteriorCameraActive()
    {
        return VirtualCameraInterior.gameObject.activeSelf;
    }

    private bool IsExteriorCameraActive()
    {

        return VirtualCameraExterior.gameObject.activeSelf;
    }

    private bool IsParedCameraActive()
    {

        return VirtualCameraPared.gameObject.activeSelf;
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
