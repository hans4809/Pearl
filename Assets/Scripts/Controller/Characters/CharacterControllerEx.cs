using System.Collections;
using System.Collections.Generic;
using System.Net.Http.Headers;
using UnityEditorInternal;
using UnityEngine;
using UnityEngine.TextCore.Text;
using UnityEngine.UI;

public class CharacterControllerEx : PlayableController
{
    // Start is called before the first frame update
    void Start()
    {
        Init();
    }
    public override void Init()
    {
        WorldObjectType = Define.WorldObject.Player;
        if(BombEffect == null)
            BombEffect = GetComponentInChildren<BombEffect>().gameObject;

        BombEffect.gameObject.SetActive(false);
    }
    protected override void UpdateIdle()
    {
        base.UpdateIdle();
    }
    protected override void UpdateRun()
    {
        base.UpdateRun();
    }
    protected override void UpdateWalk()
    {
        base.UpdateWalk();
    }
    protected override void Damaged()
    {
        base.Damaged();
    }
    protected override void UpdateFalling()
    {
        base.UpdateFalling();
    }

    protected override void UpdateAirborne()
    {
        base.UpdateAirborne();
    }

    protected override void Died()
    {
        base.Died();
    }
    // Update is called once per frame
    void Update()
    {
        //switch (State)
        //{
        //    case Define.State.Idle:
        //        UpdateIdle();
        //        break;
        //    case Define.State.Walk:
        //        UpdateWalk();
        //        break;
        //    case Define.State.Damaged:
        //        Damaged();
        //        break;
        //    case Define.State.Airborne:
        //        UpdateAirborne();
        //        break;
        //}
    }
    private void FixedUpdate()
    {
    }


    private void OnTriggerEnter2D(Collider2D other)
    {
        //var WorldObjectType = Managers.Game.GetWorldObjectType(other.gameObject);

        //switch (WorldObjectType)
        //{
        //    case Define.WorldObject.Item:
        //        other.gameObject.GetComponent<BaseItem>().Used();
        //        break;
        //    case Define.WorldObject.Enemy:
        //        State = Define.State.Damaged;
        //        break;
        //}
    }
}
