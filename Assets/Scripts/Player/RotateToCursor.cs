using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class RotateToCursor : MonoBehaviour
{
    Camera camera;
    Rigidbody2D playerRB;
    
    void Start()
    {
        camera = Camera.main;
        playerRB = this.GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        Rotate();
    }

    private void Rotate()
    {
        Vector3 mousePosition = camera.ScreenToWorldPoint(GetMousePosition());
        playerRB.transform.eulerAngles = new Vector3(0, 0, TanAngleDeg(mousePosition));
    }

    private Vector3 GetMousePosition()
    {
        return new Vector3(Input.mousePosition.x, Input.mousePosition.y, Input.mousePosition.z - camera.transform.position.z);
    }

     /// <summary>
     /// Return the angle in degrees whose Tan is y/x of a Vector3
     /// </summary>
     /// <returns> Tangent in dregrees </returns>
     ///<param name="position">Vector3 of which we want to make the tangent</param>
    private float TanAngleDeg(Vector3 position)
    {
        return Mathf.Atan2((position.y - transform.position.y), (position.x - transform.position.x)) * Mathf.Rad2Deg;
    }
}
