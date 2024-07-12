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
                    anim.SetBool("isFalling", false);
                    anim.SetBool("isDamaged", false);
                    anim.SetBool("isAirborne", false);
                    break;
                case Define.State.Walk:
                    anim.SetBool("isFalling", false);
                    anim.SetBool("isDamaged", false);
                    anim.SetBool("isAirborne", false);
                    anim.SetBool("isWalk", true);
                    break;
                case Define.State.Falling:
                    anim.SetBool("isWalk", false);
                    anim.SetBool("isDamaged", false);
                    anim.SetBool("isAirborne", false);
                    anim.SetBool("isFalling", true);
                    break;
                case Define.State.Damaged:
                    anim.SetBool("isWalk", false);
                    anim.SetBool("isFalling", false);
                    anim.SetBool("isAirborne", false);
                    anim.SetBool("isDamaged", true);
                    break;
                case Define.State.Airborne:
                    anim.SetBool("isWalk", false);
                    anim.SetBool("isFalling", false);
                    anim.SetBool("isDamaged", false);
                    anim.SetBool("isAirborne", true);
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
