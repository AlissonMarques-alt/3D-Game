using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class Moviment : MonoBehaviour
{
    [Header("Movement Settings:")]
    public float speedRun;
    public float speedSprint;
    public float rotateSpeed;

    [Header("Animation Settings:")]
    public Animator anim;

    float speed;
    Rigidbody rb;

    float xRaw;
    float zRaw;

    float x;
    float z;

    
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        speed = speedRun;
    }

    private void Update()
    {
        Movimentation();
        Animation();
        Rotation();
    
        if(Input.GetMouseButton(1))
        {
            Camera.main.transform.gameObject.GetComponent<Camera>().fieldOfView = Mathf.Lerp(Camera.main.transform.gameObject.GetComponent<Camera>().fieldOfView, 40f, .1f);
        }
        else
        {
            Camera.main.transform.gameObject.GetComponent<Camera>().fieldOfView = Mathf.Lerp(Camera.main.transform.gameObject.GetComponent<Camera>().fieldOfView, 50f, .1f);
        }
    }

    private void FixedUpdate()
    {
        xRaw = Input.GetAxisRaw("Horizontal");
        zRaw = Input.GetAxisRaw("Vertical");

        x = Input.GetAxis("Horizontal");
        z = Input.GetAxis("Vertical");
    }

    void Movimentation()
    {
        if (xRaw != 0 || zRaw!= 0)
        {
            rb.MovePosition(rb.position + speed * Time.fixedDeltaTime * transform.forward);

            if (Input.GetKey(KeyCode.LeftShift))
                speed = speedSprint;
            else
                speed = speedRun;
        }
    }

    void Rotation()
    {
        float camY = Camera.main.transform.rotation.eulerAngles.y;

        if (zRaw == 1)
        {
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.Euler(0, camY, 0), Time.deltaTime * 5);
        }
        if (zRaw == -1)
        {
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.Euler(0, camY - 180, 0), Time.deltaTime * 5);
        }
        if (xRaw == 1)
        {
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.Euler(0, camY + 90, 0), Time.deltaTime * 5);
        }
        if (xRaw == -1)
        {
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.Euler(0, camY - 90, 0), Time.deltaTime * 5);
        }
    }

    void Animation()
    {
        if (!xRaw.Equals(0) || !zRaw.Equals(0))
        {
            if (Input.GetKey(KeyCode.LeftShift))
            {
                anim.SetBool("sprint", true);
            }
            else
            {
                anim.SetBool("sprint", false);
                anim.SetBool("running", true);
            }
        }
        else
        {
            anim.SetBool("running", false);
            anim.SetBool("sprint", false);
        }
    }
}
