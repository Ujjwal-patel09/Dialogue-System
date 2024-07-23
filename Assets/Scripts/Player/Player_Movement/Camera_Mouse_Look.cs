using UnityEngine;

public class Camera_Mouse_Look : MonoBehaviour
{
    public float MouseSensitivity = 100f;
    public bool CursorIsLock = false;
    
    private Transform PlayerTransform;
    private float Xrotation;

    void Start()
    {
        PlayerTransform = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
            
    }

    void Update()
    {
        float mouseX = Input.GetAxisRaw("Mouse X") * MouseSensitivity * Time.deltaTime;
        float mouseY = Input.GetAxisRaw("Mouse Y") * MouseSensitivity * Time.deltaTime;

        Xrotation -= mouseY;
        Xrotation = Mathf.Clamp(Xrotation,-90,90);

        transform.localRotation = Quaternion.Euler(Xrotation,0,0);//for rotating camera only in x axis when dragging mouse in y axis
        PlayerTransform.Rotate(Vector3.up * mouseX);// for rotating player body when dragging mouse in x axis
        
    }
}
