using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float MoveSpeed = 5f; //Speed for movement speed
    public float rotationSpeed = 5f; //Spped for facing the Cursor
    private Rigidbody Rigidbody;
    Vector3 mousePos;
    Vector3 movement;
    private Quaternion lookRotation;


    void Start()
    {
        Rigidbody = GetComponent<Rigidbody>();

        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    // Update is called once per frame
    void Update()
    {
        //inputs
        float horizontalInput = Input.GetAxisRaw("Horizontal");
        float verticalInput = Input.GetAxisRaw("Vertical"); // Y axis is for deepness
        movement = new Vector3(horizontalInput, 0f, verticalInput).normalized;
    }

    void FixedUpdate()
    {
        if (movement.x != 0 || movement.z != 0)
        {
            Rigidbody.velocity = movement * MoveSpeed;
            // transform.Translate(movement * (MoveSpeed * Time.deltaTime), Space.World);  //Copy the vector3 movement to object
            this.gameObject.GetComponentInChildren<Animator>().SetBool("Walking", true);
        }
        else
        {
            Rigidbody.velocity = Vector3.zero;

            this.gameObject.GetComponentInChildren<Animator>().SetBool("Walking", false);
        }

        FaceCursor();
    }

    //Calculate MousePosition and makes the object always lookup to that position
    void FaceCursor()
    {
        Vector3 cursorScreenPosition = Input.mousePosition;
        float distanceFromCamera = 15.0f;
        Vector3 cursorWorldPosition =
            Camera.main.ScreenToWorldPoint(new Vector3(cursorScreenPosition.x, cursorScreenPosition.y,
                distanceFromCamera));
        Vector3 directionToCursor = cursorWorldPosition - transform.position;

        // Ensure the object doesn't roll by setting its up direction to the world up
        Quaternion rotation = Quaternion.LookRotation(directionToCursor, Vector3.up);

        // Smoothly rotate the object toward the cursor
        transform.rotation = Quaternion.Slerp(transform.rotation, rotation, rotationSpeed * Time.deltaTime);
    }
}