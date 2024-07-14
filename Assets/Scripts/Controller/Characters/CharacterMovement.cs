using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.TextCore.Text;

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
    [SerializeField] private Vector2 _parabolicInitialVelocity;
    public Vector2 ParabolicInitialVelocity { get => _parabolicInitialVelocity; private set => _parabolicInitialVelocity = value; }
    [SerializeField] private Vector2 _parabolicStartPosition;
    public Vector2 ParabolicStartPosition { get => _parabolicStartPosition; private set => _parabolicStartPosition = value; }
    [SerializeField] private float _parabolicElapsedTime;
    public float ParabolicElapsedTime { get => _parabolicElapsedTime; private set => _parabolicElapsedTime = value; }
    [SerializeField] private float _parabolicGravity;
    public float ParabolicGravity { get => _parabolicGravity; private set => _parabolicGravity = value; }
    [SerializeField] Vector2 _maxHeightDisplacement;
    public Vector2 MaxHeightDisplacement { get => _maxHeightDisplacement; private set => _maxHeightDisplacement = value; }
    [Header("StunVariable")]
    [SerializeField] float _stunTimer;
    public float StunTimer { get => _stunTimer; private set => _stunTimer = value; }

    [SerializeField] Coroutine _returnToIdleCoroutine;
    public Coroutine ReturnToIdleCoroutine { get => _returnToIdleCoroutine; private set => _returnToIdleCoroutine = value; }

    [SerializeField] CharacterControllerEx _character;
    public CharacterControllerEx Character { get => _character; private set => _character = value; }
    // Start is called before the first frame update
    void Start()
    {
        if (Rb2D == null)
            Rb2D = GetComponent<Rigidbody2D>();

        if (SR == null)
            SR = GetComponent<SpriteRenderer>();

        if (MaxHeightDisplacement == Vector2.zero)
            MaxHeightDisplacement = new Vector2(0.5f, 0.5f);

        Character = gameObject.GetComponent<CharacterControllerEx>();

        //if (FollowCamera == null)
        //    FollowCamera = Camera.main;
    }

    private void FixedUpdate()
    {
        if (Managers.Game.GameState != EGameState.Playing)
            return;

        if (Character.State== Define.State.Idle || Character.State == Define.State.Walk)
        {
            MoveAndRotate();
            DirVec2 = Vector2.zero;
        }
    }

    // Update is called once per frame
    void Update()
    {
        DirVec2 = Vector2.zero;
        if (Managers.Game.GameState != EGameState.Playing)
            return;
         
        if (Character.State == Define.State.Idle || Character.State == Define.State.Walk)
        {
            float horizontalInput = Input.GetAxis($"Horizontal{PlayerIndex}");
            float verticalInput = Input.GetAxis($"Vertical{PlayerIndex}");

            DirVec2 = new Vector2(horizontalInput, verticalInput).normalized;

            if (DirVec2.magnitude <= 0.1f)
            {
                ReturnToIdle();
            }
        }
    }

    void MoveAndRotate()
    {
        if (DirVec2.magnitude > 0.1f)
        {
            Character.State = Define.State.Walk;

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

    public void PlayWalkSound()
    {
        Managers.Sound.Play("SFX/Grass_Walk");
    }

    public void ReturnToIdle()
    {
        Character.State = Define.State.Idle;
        Rb2D.velocity = Vector2.zero;
        ParabolicStartPosition = Rb2D.position;
        ParabolicElapsedTime = 0f;
        Rb2D.gravityScale = 0f;
    }

    public void JumpForce()
    {
        if(ReturnToIdleCoroutine != null)
        {
            StopCoroutine(ReturnToIdleCoroutine);
            ReturnToIdleCoroutine = null;
        }

        Rb2D.gravityScale = 1.0f;
        // m*k*g*h = m*v^2/2 (단, k == gravityScale) <= 역학적 에너지 보존 법칙 적용
        float v_y = Mathf.Sqrt(2 * Rb2D.gravityScale * -Physics2D.gravity.y * MaxHeightDisplacement.y);
        // 포물선 운동 법칙 적용
        float v_x = MaxHeightDisplacement.x * v_y / (2 * MaxHeightDisplacement.y)/*Anim.GetCurrentAnimatorClipInfo(0)[0].clip.length*/;

        Vector2 force = Rb2D.mass * (new Vector2(v_x, v_y) - Rb2D.velocity);
        Rb2D.AddForce(force, ForceMode2D.Impulse);

        float timer = (4 * MaxHeightDisplacement.y) / v_y;
        ReturnToIdleCoroutine = StartCoroutine(RetrunToIdleCor(timer));
    }

    IEnumerator RetrunToIdleCor(float timer)
    {
        yield return new WaitForSeconds(timer);
        ReturnToIdle();
    }

    public void OnDamaged()
    {
        if (ReturnToIdleCoroutine != null)
        {
            StopCoroutine(ReturnToIdleCoroutine);
            ReturnToIdleCoroutine = null;
        }
        Rb2D.velocity = Vector3.zero;
        ReturnToIdleCoroutine = StartCoroutine(RetrunToIdleCor(StunTimer));
    }

    public void OnBad()
    {
        if (ReturnToIdleCoroutine != null)
        {
            StopCoroutine(ReturnToIdleCoroutine);
            ReturnToIdleCoroutine = null;
        }
        Rb2D.velocity = Vector3.zero;
        ReturnToIdleCoroutine = StartCoroutine(RetrunToIdleCor(0.1f));
    }
}
