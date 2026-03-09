using Unity.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class ClickManager : MonoBehaviour
{
    public string objectName = string.Empty;
    public float sensitivity = 0.02f;
    void Awake()
    {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }

    void Update()
    {
        Vector2 mouseDelta = Mouse.current.delta.ReadValue();
        float movex = mouseDelta.x * sensitivity*0.7f;
        float movey =  mouseDelta.y * sensitivity*1.25f;
        if(transform.position.x + movex > 20 || transform.position.x + movex < -20) movex = 0;
        if(transform.position.z + movey > 17.5 || transform.position.z + movey < -2.5) movey = 0;
        var move = new Vector3(movex, 0, movey);

        transform.position += move;
    }

    /*sin cursor.locked
    
    void Update()
    {
    
        Ray ray = cam.ScreenPointToRay(Mouse.current.position.ReadValue());

        Plane groundPlane = new Plane(Vector3.up, Vector3.zero);

        if (groundPlane.Raycast(ray, out float distance))
        {
            Vector3 worldPoint = ray.GetPoint(distance);
            transform.position = worldPoint;
        }
    }
     
    */

}