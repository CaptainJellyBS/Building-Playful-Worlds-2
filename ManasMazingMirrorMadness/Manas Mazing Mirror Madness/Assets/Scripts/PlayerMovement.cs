using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{

    Rigidbody rb;
    Camera camera;
    public Vector3 camOffset;
    public bool onGround;
    public bool canMove = true;

    #region Serialized
    [SerializeField]
    float speed;
    [SerializeField]
    float jumpSpeed = 1.0f;
    [SerializeField]
    float sensitivityX = 15f;
    [SerializeField]
    float sensitivityY = 15f;
    [SerializeField]
    float minimumX = -360F;
    [SerializeField]
    float maximumX = 360F;
    [SerializeField]
    float minimumY = -60F;
    [SerializeField]
    float maximumY = 60F;
    #endregion

    float rotX, rotY;
    Quaternion originalRotation;

    private void Awake()
    {
        camera = Camera.main;
        rb = GetComponentInParent<Rigidbody>();
    }

    // Start is called before the first frame update
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        //Cursor.visible = true;
        originalRotation = transform.localRotation;
    }

    // Update is called once per frame
    void Update()
    {
        HandleInput();
        MouseRot();
    }


    //Mouse rotation code taken from: https://answers.unity.com/questions/29741/mouse-look-script.html and adapted to suit my game
    void MouseRot()
    {
        rotX += Input.GetAxis("Mouse X") * sensitivityX;
        rotY += Input.GetAxis("Mouse Y") * sensitivityY;
        rotX = ClampAngle(rotX, minimumX, maximumX);
        rotY = ClampAngle(rotY, minimumY, maximumY);
        Quaternion xQuaternion = Quaternion.AngleAxis(rotX, Vector3.up);
        Quaternion yQuaternion = Quaternion.AngleAxis(rotY, -Vector3.right);
        transform.localRotation = originalRotation * xQuaternion * Quaternion.identity ;
        camera.transform.localRotation = originalRotation * Quaternion.identity * yQuaternion;
    }

    void HandleInput()
    {
        transform.Translate(new Vector3(Input.GetAxis("Horizontal"), 0, 0) * Time.deltaTime * speed, Space.Self);
        float xsp = Input.GetAxis("Vertical") * Time.deltaTime * speed;
        transform.Translate(new Vector3(transform.forward.x * xsp, 0, transform.forward.z * xsp), Space.World);

        if (Input.GetKeyDown(KeyCode.Space) && onGround)
        {
            rb.AddForce(Vector3.up * jumpSpeed, ForceMode.Impulse);
        }
    }


    //Mouse rotation code taken from: https://answers.unity.com/questions/29741/mouse-look-script.html
    public float ClampAngle(float angle, float min, float max)
    {
        if (angle < -360F)
            angle += 360F;
        if (angle > 360F)
            angle -= 360F;
        return Mathf.Clamp(angle, min, max);
    }

    #region COllisoin

    public void OnCollisionStay(Collision col)
    {

        if (col.gameObject.CompareTag("Floor"))
        {
            onGround = true;
        }
    }
    public void OnCollisionExit(Collision col)
    {
        if (col.gameObject.CompareTag("Floor"))
        {
            onGround = false;
        }
    }

    public void OnTriggerEnter(Collider col)
    {
        if(col.gameObject.CompareTag("Door"))
        {
            StartCoroutine(OpenDoor(col));
        }

        if(col.gameObject.CompareTag("Death"))
        {
            col.gameObject.GetComponent<DeathAndRespawn>().Kill(gameObject);
        }
    }

    public void OnTriggerExit(Collider col)
    {
        if (col.gameObject.CompareTag("Door"))
        {
            foreach (Door d in col.GetComponentsInChildren<Door>())
            {
                d.CloseDoor();
            }
        }
    }

    /// <summary>
    /// Ugly coroutine to fix an issue where player could see the room change through door
    /// </summary>
    /// <param name="col"></param>
    /// <returns></returns>
    IEnumerator OpenDoor(Collider col)
    {
        foreach(Door d in col.GetComponentsInChildren<Door>())
        {
            d.gameObject.GetComponentInChildren<BoxCollider>().enabled = false;
        }
        yield return new WaitForFixedUpdate();
        yield return new WaitForFixedUpdate();

        foreach (Door d in col.GetComponentsInChildren<Door>())
        {
            d.OpenDoor();
            d.gameObject.GetComponentInChildren<BoxCollider>().enabled = true;
        }
    }

    #endregion
}
