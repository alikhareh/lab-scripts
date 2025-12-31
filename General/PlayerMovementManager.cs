using Unity.VisualScripting;
using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class PlayerMovementManager : MonoBehaviour
{
    
    [Header("PickUp")]
    public float rayDistance = 10f;
    public Camera cam;
    public float radius = 0.5f;
    public LayerMask layerMask;
    [Header("Movement")]
    public float walkSpeed = 5f;
    public float sprintSpeed = 8f;
    public float gravity = -9.81f;

    [HideInInspector]public GameObject inHandObject = null;

    [Header("Mouse Look")]
    public float mouseSensitivity = 120f;
    public Transform cameraPivot; // دوربین یا آبجکت محوری که Pitch روی آن اعمال می‌شود
    public float minPitch = -85f;
    public float maxPitch = 85f;

    private CharacterController controller;
    private Vector3 velocity;
    private float pitch = 0f;
    public Transform hand;
    public SpectroDeviceManager device;
    
    private LabObjectManager currentObject = null;


    void Start()
    {
        controller = GetComponent<CharacterController>();
        //Cursor.lockState = CursorLockMode.Locked;
        //Cursor.visible = false;
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        if (cameraPivot == null)
        {
            // تلاش برای پیدا کردن دوربین فرزند به صورت خودکار
            cam = GetComponentInChildren<Camera>();
            if (cam != null) cameraPivot = cam.transform;
        }

        
       
    }

    void Update()
    {
        HandleMouseLook();
        HandleMovement();
        ApplyGravity();

        Vector3 origin = cam.transform.position;
        Vector3 direction = cam.transform.forward;

        Vector3 point1 = origin;
        Vector3 point2 = origin + direction * rayDistance;

        RaycastHit[] hits = Physics.CapsuleCastAll(
            point1,
            point2,
            radius,
            direction,
            0f,
            layerMask
        );

        LabObjectManager hitObject = null;
        float closestDistance = Mathf.Infinity;

// فقط نزدیک‌ترین آبجکت مهمه
        foreach (RaycastHit hit in hits)
        {
            LabObjectManager labObj = hit.collider.GetComponent<LabObjectManager>();
            if (labObj != null)
            {
                if (hit.distance < closestDistance)
                {
                    closestDistance = hit.distance;
                    hitObject = labObj;
                }
            }
        }

// ===== Enter =====
        if (hitObject != null && currentObject != hitObject)
        {
            if (currentObject != null)
                currentObject.HidePopUp();

            currentObject = hitObject;
            currentObject.ShowPopUp();
        }

// ===== Exit =====
        if (hitObject == null && currentObject != null)
        {
            currentObject.HidePopUp();
            currentObject = null;
        }

// ===== Input =====
        if (currentObject != null && Input.GetKeyDown(KeyCode.Z))
        {
            if (inHandObject != null)
            {
                inHandObject.transform.SetParent(null);
                inHandObject.GetComponent<Rigidbody>().isKinematic = false;
                inHandObject  = null;
            }
            
            currentObject.transform.SetParent(hand);
            currentObject.transform.localPosition = Vector3.zero;
            inHandObject = currentObject.gameObject;
            inHandObject.GetComponent<Rigidbody>().isKinematic = true;
            if (inHandObject == device.sample)
            {
                device.sample = null;
            }
            else if(device.blank == inHandObject)
            {
                device.blank = null;
            }
            
        }

        if (Input.GetKeyDown(KeyCode.X) && inHandObject != null)
        {
            inHandObject.transform.SetParent(null);
            inHandObject.GetComponent<Rigidbody>().isKinematic = false;
            inHandObject  = null;
        }

// دیباگ
        Debug.DrawLine(point1, point2, Color.green);


    void HandleMouseLook()
    {
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;

        // چرخش افقی (Yaw) روی بدنه پلیر
        transform.Rotate(Vector3.up * mouseX);

        // چرخش عمودی (Pitch) فقط روی دوربین
        if (cameraPivot != null)
        {
            pitch -= mouseY;
            pitch = Mathf.Clamp(pitch, minPitch, maxPitch);
            cameraPivot.localRotation = Quaternion.Euler(pitch, 0f, 0f);
        }
    }

    void HandleMovement()
    {
        float inputX = Input.GetAxisRaw("Horizontal");   // A/D یا چپ/راست
        float inputZ = Input.GetAxisRaw("Vertical");     // W/S یا جلو/عقب

        Vector3 moveDir = (transform.right * inputX + transform.forward * inputZ).normalized;

        bool isSprinting = Input.GetKey(KeyCode.LeftShift);
        float speed = isSprinting ? sprintSpeed : walkSpeed;

        controller.Move(moveDir * speed * Time.deltaTime);
    }

    void ApplyGravity()
    {
        if (controller.isGrounded && velocity.y < 0f)
        {
            // مقدار کوچک منفی تا کاراکتر روی زمین بچسبد
            velocity.y = -2f;
        }

        velocity.y += gravity * Time.deltaTime;
        controller.Move(new Vector3(0f, velocity.y, 0f) * Time.deltaTime);
    }
} 
}
