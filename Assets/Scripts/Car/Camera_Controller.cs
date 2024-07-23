
using UnityEngine;

public class Camera_Controller : MonoBehaviour
{
  public bool is_Enter_car = false;
  public Transform Player_Camera_holder;
  public Transform car; 
  public Transform car_cameraPoint;
  public GameObject exit_ui;
  Vector3 velocity = Vector3.zero;

  private void Start() 
  {
    is_Enter_car = false;
  }

  void FixedUpdate() 
  {
    if(is_Enter_car == true)
    {
      transform.SetParent(car_cameraPoint);
      exit_ui.SetActive(true);
      transform.LookAt(car.position);
      transform.position = Vector3.SmoothDamp(transform.position,car_cameraPoint.position,ref velocity,0.2f);
    }
    else
    {
      transform.SetParent(Player_Camera_holder);
      exit_ui.SetActive(false);
      transform.position = Vector3.SmoothDamp(transform.position,Player_Camera_holder.position,ref velocity,0.5f);
    }
  }
}
