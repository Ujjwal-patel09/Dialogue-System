
using UnityEngine;

public class PlayerInteraction : MonoBehaviour
{
   #region Singleton
   public static PlayerInteraction instance;
   private void Awake() 
   { 
     if(instance == null && instance != this)
     {
        instance = this;
     }
   }
   #endregion
   
    // for player raycast variables for identifies npc//
    [SerializeField]public Transform interactor_Source_Cam;
    [SerializeField]public float interact_range;
    
    [HideInInspector]public bool isInteract;
    [HideInInspector]public bool inPause;

    private FPS_Player_Movement fPS_Player_Movement;
    private Camera_Mouse_Look camera_Mouse_Look;

    private void Start() 
    {
      fPS_Player_Movement = GetComponent<FPS_Player_Movement>();
      camera_Mouse_Look =  Camera.main.GetComponent<Camera_Mouse_Look>();
         
    }

    void Update()   
    {
        if(isInteract == false && inPause == false)
        { 
            movement_CameraLook(true);
            if(Input.GetKeyDown(KeyCode.E))
            {
                Ray ray = new Ray(interactor_Source_Cam.position,interactor_Source_Cam.forward);
                if(Physics.Raycast(ray,out RaycastHit Hit_Info,interact_range))// cast a ray from player camera in forward and geeting hit info //
                {
                    if(Hit_Info.collider.gameObject.TryGetComponent(out I_Interactable i_Interactable))// hit info collides with I_Interactable, so out I_Interactable//
                    {
                       i_Interactable.Interact();// This i_Interactable interface is attached with all the object or npc whom player will interact //
                       isInteract = true;
                       movement_CameraLook(false);
                    }
                }
            }
        }
    }

    public void movement_CameraLook(bool state)// for Enable and Disable Player Movement when in conversation
    {
        if(state ==  false)
        {
            fPS_Player_Movement.enabled = false;
            camera_Mouse_Look.enabled = false;
            Cursor.lockState = CursorLockMode.None;
        }
        else
        {
            fPS_Player_Movement.enabled = true;
            camera_Mouse_Look.enabled = true;
            Cursor.lockState = CursorLockMode.Locked;
        }
    }


    public I_Interactable Get_Interactable_object()// used in Playeri_intereact_ui script for display "E" button ui in scene//
    {
        if(isInteract == false)
        {
            Ray r = new Ray(interactor_Source_Cam.position,interactor_Source_Cam.forward);
            if(Physics.Raycast(r,out RaycastHit Hit_Info,interact_range))
            { 
                if(Hit_Info.collider.gameObject.TryGetComponent(out I_Interactable i_Interactable))
                {
                 return i_Interactable;
                }
            }
        }
        return null;
    }
}
