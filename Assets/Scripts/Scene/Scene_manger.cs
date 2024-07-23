
using UnityEngine;
using UnityEngine.SceneManagement;

public class Scene_manger : MonoBehaviour
{
    public GameObject Pause_Pannel;
    public GameObject Pause_image;

    
    private void Update() 
    { 
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            Pause();
        }
    }
        
    public void start_()
    {
        SceneManager.LoadScene(1);

    }

    public void exit()
    {
        Application.Quit();
    }

    public void menu()
    {
        SceneManager.LoadScene(0);
    }

    public void Pause()
    {
       PlayerInteraction.instance.inPause = true;
       PlayerInteraction.instance.movement_CameraLook(false);
       Pause_image.SetActive(false);
       Pause_Pannel.SetActive(true);
       Time.timeScale = 0f;
    }

    public void Resume()
    {
       Pause_image.SetActive(true);
       Pause_Pannel.SetActive(false);
       Time.timeScale = 1f;
       PlayerInteraction.instance.inPause = false;
       
       
    }
    
}
