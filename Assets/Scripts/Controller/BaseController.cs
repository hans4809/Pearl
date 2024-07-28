using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public abstract class BaseController : MonoBehaviour
{
    [SerializeField]
    protected GameObject _lockTarget;
    [SerializeField]
    protected Define.State _state = Define.State.Idle;
    public Define.WorldObject WorldObjectType { get; protected set; } = Define.WorldObject.Unknown;
    [SerializeField] GameObject _bombEffect;
    public GameObject BombEffect { get => _bombEffect; protected set => _bombEffect = value; }
    // Start is called before the first frame update
    private void Start()
    {
    }
    public abstract void Init();
    public virtual Define.State State
    {
        get { return _state; }
        set
        {
            _state = value;

            Animator anim = GetComponent<Animator>();
            switch (_state)
            {
                case Define.State.Idle:
                    anim.SetBool("isWalk", false);
                    anim.SetBool("isDamaged", false);
                    anim.SetBool("isAirborne", false);
                    anim.SetBool("isBad", false);
                    break;
                case Define.State.Walk:
                    anim.SetBool("isDamaged", false);
                    anim.SetBool("isAirborne", false);
                    anim.SetBool("isWalk", true);
                    anim.SetBool("isBad", false);
                    break;
                case Define.State.Damaged:
                    anim.SetBool("isWalk", false);
                    anim.SetBool("isAirborne", false);
                    anim.SetBool("isDamaged", true);
                    anim.SetBool("isBad", false);
                    gameObject.GetComponent<CharacterMovement>().OnDamaged();
                    break;
                case Define.State.Airborne:
                    anim.SetBool("isWalk", false);
                    anim.SetBool("isDamaged", false);
                    anim.SetBool("isAirborne", true);
                    anim.SetBool("isBad", false);
                    if (BombEffect != null)
                        BombEffect.SetActive(true);
                    //Managers.Resource.Instantiate("Effects/BombEffect", new Vector3(-0.3f, -3.40f, 0f), Quaternion.identity, this.transform);
                    gameObject.GetComponent<CharacterMovement>().DrawProjectileMotionLine();
                    gameObject.GetComponent<CharacterMovement>().JumpForce();
                    //gameObject.GetComponent<CharacterMovement>().ParabolicAirborne();
                    break;
                case Define.State.Bad:
                    anim.SetBool("isWalk", false);
                    anim.SetBool("isDamaged", false);
                    anim.SetBool("isAirborne", false);
                    anim.SetBool("isBad", true);
                    gameObject.GetComponent<CharacterMovement>().OnBad();
                    break;

            }
        }
    }
    // Update is called once per frame
    private void Update()
    {
    }
    private void FixedUpdate()
    {
        
    }
    protected virtual void UpdateIdle()
    {
    }

    protected virtual void UpdateWalk()
    {
    }

    protected virtual void UpdateRun()
    {
    }

    protected virtual void Damaged()
    {

    }

    protected virtual void Died()
    {

    }
}
