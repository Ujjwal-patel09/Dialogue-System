
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
    public Transform interactor_Source_Cam;
    public float interact_range;
    public bool isInteract;
    public bool inPause;

    FPS_Player_Movement fPS_Player_Movement;
    Camera_Mouse_Look camera_Mouse_Look;

    private void Start() 
    {
      fPS_Player_Movement = GetComponent<FPS_Player_Movement>();
      camera_Mouse_Look =  GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera_Mouse_Look>();
         
    }

    void Update()   
    {
        if(isInteract == false && inPause == false)
        { 
            movement_CameraLook(true);
            if(Input.GetKeyDown(KeyCode.E))
            {
                Ray r = new Ray(interactor_Source_Cam.position,interactor_Source_Cam.forward);
                if(Physics.Raycast(r,out RaycastHit Hit_Info,interact_range))
                {
                    if(Hit_Info.collider.gameObject.TryGetComponent(out I_Interactable i_Interactable))
                    {
                       i_Interactable.Interact();
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


    public I_Interactable Get_Interactable_object()// used in Playeri_intereact_ui script for display "E" button in scene//
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
