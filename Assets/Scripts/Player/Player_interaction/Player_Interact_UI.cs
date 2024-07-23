
using UnityEngine;

public class Player_Interact_UI : MonoBehaviour
{
  public GameObject interact_ui;

  private void Update() 
  {
    if(PlayerInteraction.instance.Get_Interactable_object() != null)
    {
      Show_UI();
    }else
    {
      Hide_UI();
    }
    
  }

  // to show and hide "E" Ui image //
  public void Show_UI()
  {
    interact_ui.SetActive(true);
  }

  public void Hide_UI()
  {
    interact_ui.SetActive(false);
  }
}
