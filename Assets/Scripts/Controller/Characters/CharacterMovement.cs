using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterMovement : MonoBehaviour
{
    [SerializeField] private float _moveSpeed;
    public float MoveSpeed { get => _moveSpeed; private set => _moveSpeed = value; }

    [SerializeField] private float _rotationSpeed;
    public float RotationSpeed { get => _rotationSpeed; private set => _rotationSpeed = value; }

    [SerializeField] private Animator _anim;
    public Animator Anim { get => _anim; private set => _anim = value; }

    [SerializeField] private Vector2 _moveInput;
    public Vector2 MoveInput { get => _moveInput; private set => _moveInput = value; }

    [SerializeField] private SpriteRenderer _sR;
    public SpriteRenderer SR { get => _sR; private set => _sR = value; }

    [SerializeField] private int _playerIndex = 1;
    public int PlayerIndex { 
        get => _playerIndex;
        set 
        { 
            if (value <= 1) _playerIndex = 1;
            else if (value >= 2) _playerIndex = 2;
        }  
    }

#region 3D
    [SerializeField] private Rigidbody _rb;
    public Rigidbody Rb { get => _rb; private set => _rb = value; }

    [SerializeField] private Vector3 _dirVec3;
    public Vector3 DirVec3 { get => _dirVec3; private set => _dirVec3 = value; }

    [SerializeField] private Vector3 _lookVec3;
    public Vector3 LookVec3 { get => _lookVec3; private set => _lookVec3 = value; }

    [SerializeField] private Camera _followCamera;
    public Camera FollowCamera { get => _followCamera; private set => _followCamera = value; }
#endregion

#region 2D
    [SerializeField] private Rigidbody2D _rb2D;
    public Rigidbody2D Rb2D { get => _rb2D; private set => _rb2D = value; }

    [SerializeField] private Vector2 _dirVec2;
    public Vector2 DirVec2 { get => _dirVec2; private set => _dirVec2 = value; }
    #endregion
    // Start is called before the first frame update
    void Start()
    {
        if (Rb == null)
            Rb = GetComponent<Rigidbody>();

        if (Rb2D == null)
            Rb2D = GetComponent<Rigidbody2D>();

        if (SR == null)
            SR = GetComponent<SpriteRenderer>();

        //if (FollowCamera == null)
        //    FollowCamera = Camera.main;
    }

    private void FixedUpdate()
    {
        MoveAndRotate();
    }

    // Update is called once per frame
    void Update()
    {
        float horizontalInput = Input.GetAxis($"Horizontal{PlayerIndex}");
        float verticalInput = Input.GetAxis($"Vertical{PlayerIndex}");
        MoveInput = new Vector2(horizontalInput, verticalInput);

        DirVec3 = new Vector3(-verticalInput, 0, horizontalInput).normalized;
    }

    void MoveAndRotate()
    {
        QuarterViewMoveAndRotate();
    }

    void QuarterViewMoveAndRotate()
    {
        if (DirVec3.magnitude > 0)
        {
            gameObject.GetComponent<CharacterControllerEx>().State = Define.State.Walk;

            Rb.velocity = DirVec3 * MoveSpeed;
            Debug.Log($"{DirVec3.z}");

            if (DirVec3.z >= 0) SR.flipX = false;
            else SR.flipX = true;
        }
        else
        {
            gameObject.GetComponent<CharacterControllerEx>().State = Define.State.Idle;
            Rb.velocity = new Vector3(0, Rb.velocity.y, 0);
        }

    }

    void ShoulderViewMoveAndRotate()
    {
        var targetSpeed = MoveSpeed * MoveInput.magnitude;
        var moveDirection = Vector3.Normalize(transform.forward * MoveInput.y + transform.right * MoveInput.x);

        Rb.velocity = new Vector3(moveDirection.x * targetSpeed, Rb.velocity.y, moveDirection.z * targetSpeed);

        var targetRotation = FollowCamera.transform.eulerAngles.y;

        transform.eulerAngles = Vector3.up * targetRotation;
    }
}
