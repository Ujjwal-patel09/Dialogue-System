
using System.Collections;
using UnityEngine;

public class Car_Enter_Exit : MonoBehaviour, I_Interactable
{
    
    private CarController carController;
    private Camera_Controller camera_Controller;
    private GameObject player;
    private Camera_Mouse_Look camera_Mouse_Look;

    void Start()
    {
        carController = GameObject.FindGameObjectWithTag("Car").GetComponent<CarController>();
        camera_Controller = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera_Controller>();
        player = GameObject.FindGameObjectWithTag("Player");
        camera_Mouse_Look = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera_Mouse_Look>();
    }

    private void Update() 
    {
      if(Input.GetKeyDown(KeyCode.F))
      {
        exit_car();
      }        
    }

    public void Interact()
    {
        Enter_in_car();
    }

    private void Enter_in_car()
    {
        camera_Controller.is_Enter_car = true;
        camera_Mouse_Look.enabled = false;
        carController.enabled = true;
        StartCoroutine(HidePlayer());
    }

    IEnumerator HidePlayer()
    {
        yield return new WaitForSeconds(0.3f);
        player.SetActive(false);
    }

    private void exit_car()
    {
        Vector3 Player_to_Car_offset = new Vector3(2,0,0);
        player.transform.position = transform.position + Player_to_Car_offset;
        camera_Controller.is_Enter_car = false;
        camera_Mouse_Look.enabled = true;
        player.SetActive(true);
        PlayerInteraction.instance.movement_CameraLook(true);
        PlayerInteraction.instance.isInteract = false;
        carController.enabled = false;
    }
}
