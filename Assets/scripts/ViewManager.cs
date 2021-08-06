using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ViewManager : MonoBehaviour
{
    public Camera ThirdPersonCamera;
    public Camera AimCamera;
    public Transform AimPoint;
    public Transform minimapCamera;
    // Update is called once per frame
    private void Start()
    {
        AimCamera.gameObject.SetActive(false);
        
    }
    void Update()
    {
        minimapCamera.position = new Vector3(gameObject.transform.position.x, gameObject.transform.position.y + 50,
            gameObject.transform.position.z);
        AimCamera.transform.rotation = ThirdPersonCamera.transform.rotation;
        if (Input.GetMouseButton(1))
        {
            AimCamera.gameObject.SetActive(true);
            AimCamera.transform.position = Vector3.Slerp(AimCamera.transform.position,
            AimPoint.position, Time.deltaTime*10f);
        }
        else
        {
            AimCamera.gameObject.SetActive(false); 
            ThirdPersonCamera.gameObject.SetActive(true);
            AimCamera.transform.position = ThirdPersonCamera.transform.position;
            
        }
    }
}
