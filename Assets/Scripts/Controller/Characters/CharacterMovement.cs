using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum EGroundType
{
    Cliff,
    Field,
    Grass,
    Gravel,
}
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

    [SerializeField] 
    public int PlayerIndex { 
        get => _playerIndex;
        set 
        { 
            if (value <= 1) _playerIndex = 1;
            else if (value >= 2) _playerIndex = 2;
        }  
    }


    [SerializeField] private Rigidbody2D _rb2D;
    public Rigidbody2D Rb2D { get => _rb2D; private set => _rb2D = value; }

    [SerializeField] private Vector2 _dirVec2;
    public Vector2 DirVec2 { get => _dirVec2; private set => _dirVec2 = value; }

    [Header("ParabolicVariable")]
    [SerializeField] private Vector2 _initialVelocity;
    public Vector2 InitialVelocity { get => _initialVelocity; private set => _initialVelocity = value; }
    [SerializeField] private Vector2 _parabolicStartPosition;
    public Vector2 ParabolicStartPosition { get => _parabolicStartPosition; private set => _parabolicStartPosition = value; }
    // Start is called before the first frame update
    void Start()
    {
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
        if(gameObject.GetComponent<CharacterControllerEx>().State == Define.State.Idle 
            || gameObject.GetComponent<CharacterControllerEx>().State == Define.State.Walk)
        {
            float horizontalInput = Input.GetAxis($"Horizontal{PlayerIndex}");
            float verticalInput = Input.GetAxis($"Vertical{PlayerIndex}");
            MoveInput = new Vector2(horizontalInput, verticalInput);

            DirVec2 = new Vector2(horizontalInput, verticalInput).normalized;

            if (DirVec2.magnitude <= 0)
            {
                ReturnToIdle();
            }
        }
    }

    void MoveAndRotate()
    {
        if (gameObject.GetComponent<CharacterControllerEx>().State == Define.State.Idle
            || gameObject.GetComponent<CharacterControllerEx>().State == Define.State.Walk)
        {
            if (DirVec2.magnitude > 0)
            {
                gameObject.GetComponent<CharacterControllerEx>().State = Define.State.Walk;

                Rb2D.velocity = DirVec2 * MoveSpeed;
                //Rb2D.MovePosition(Rb2D.position + DirVec2 * MoveSpeed * Time.fixedDeltaTime);

                if (DirVec2.x > 0)
                {
                    SR.flipX = true;
                    //transform.rotation = Quaternion.Euler(new Vector3(0, 180, 0));
                }
                else
                {
                    SR.flipX = false;
                    //transform.rotation = Quaternion.Euler(new Vector3(0, 0, 0));
                }
            }
            else
            {
                ReturnToIdle();
            }
        }
    }

    public void PlayWalkSound()
    {
        Managers.Sound.Play("SFX/Grass_Walk");
    }

    public void ReturnToIdle()
    {
        gameObject.GetComponent<CharacterControllerEx>().State = Define.State.Idle;
        Rb2D.velocity = Vector2.zero;
    }
}
