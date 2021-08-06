using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerMovement : MonoBehaviour
{

    public CharacterController controller;
    public Transform PlayerCamera;
    
    

    //player settings
    public Animator animate;
    public float speed = 0;
    float rotateSpeed = 90.0f;

    public GameObject tower;
    public GameObject canon;

    public float towerRotateSpeedSlerp;
    public float maxSpeed;
    public float minSpeed;
    //movement variables
    float vertical;
    float horizontal;

    Vector3 velocity = new Vector3(0,-1f,0);
    public float gravity = 9.81f;
    //character's rotation
    RaycastHit hitGround;
    Vector3 forward;
    Vector3 cameraForward;
    Vector3 lastPosition;
    float velocityOfPlayer;

    //audio
    public AudioSource TankSourceEngine;
    //public AudioClip horn;

    // Update is called once per frame
    void Update()
    {
        //set up aiming raycast
        cameraForward = PlayerCamera.position + PlayerCamera.forward.normalized * 100;
        

        //tower's angle
        Quaternion towerRotation = Quaternion.LookRotation((transform.position - PlayerCamera.transform.position).normalized);
        //canon's y angle
        Quaternion canonRotation = Quaternion.LookRotation((cameraForward - transform.position).normalized);
        tower.transform.rotation = Quaternion.Slerp(tower.transform.rotation, towerRotation, Time.deltaTime*towerRotateSpeedSlerp);
        tower.transform.localEulerAngles = new Vector3(0f, tower.transform.localEulerAngles.y, 0f);
        canon.transform.rotation = Quaternion.Slerp(canon.transform.rotation, canonRotation, Time.deltaTime * towerRotateSpeedSlerp);
        canon.transform.localEulerAngles = new Vector3(canon.transform.localEulerAngles.x, 0f, canon.transform.localEulerAngles.z);
        
        //Move keyboard input
        horizontal = Input.GetAxisRaw("Horizontal");
        vertical = Input.GetAxisRaw("Vertical");
        //speed controller
        if (Input.GetKey(KeyCode.W) && speed < maxSpeed)
        {
            speed += 3f * Time.deltaTime;
        }
        else if (Input.GetKey(KeyCode.S) && speed > -minSpeed)
        {
            speed -= 3f * Time.deltaTime;
        }
        else if (Input.GetKey(KeyCode.Space))
        {
            if (speed > 0.5)
            {
                speed -= 5f * Time.deltaTime;
            }
            else if (speed < -0.5)
            {
                speed += 5f * Time.deltaTime;
            }
            else
            {
                speed = 0;
            }
        }
        else
        {
            velocityOfPlayer = (transform.position - lastPosition).magnitude;
            if (speed > 0.5)
            {
                speed -= 1f * Time.deltaTime;
            }
            else if(speed < -0.5)
            {
                speed += 1f * Time.deltaTime;
            }
            else
            {
                speed = 0;
            }
            if (velocityOfPlayer <= 0.01)
            {

                speed = 0;
            }

        }
        lastPosition = transform.position;
        //send value to animator
        animate.SetFloat("speed", speed);
        if (speed < 0.5f && speed > -0.5f)
            animate.SetBool("stop", true);
        else
            animate.SetBool("stop", false);

        //modify charactor's angle on slope
        Physics.Raycast(transform.position, -Vector3.up, out hitGround);
        forward = Vector3.Cross(hitGround.normal, -transform.right);

        transform.rotation = Quaternion.Slerp(transform.rotation,  Quaternion.LookRotation(forward), Time.deltaTime * 10);
        //player movement
        controller.Move(transform.forward.normalized * speed * Time.deltaTime);

        //gravity effect
        controller.Move(velocity *gravity* Time.deltaTime);
        transform.Rotate(Vector3.up * rotateSpeed * horizontal * Time.deltaTime);

        //set wheel rotate speed
        animate.SetFloat("WheelSpeed", speed / 5f);

        //engine sound control
        
        TankSourceEngine.pitch = Mathf.Abs(speed / maxSpeed);
        if(TankSourceEngine.pitch < 0.2f)
        {
            TankSourceEngine.pitch = 0.2f;
        }
    }
}
