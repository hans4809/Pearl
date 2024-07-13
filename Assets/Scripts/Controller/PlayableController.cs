using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayableController : BaseController
{
    // Start is called before the first frame update
    void Start()
    {
        
    }
    public override void Init()
    {
        WorldObjectType = Define.WorldObject.Player;
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
    protected virtual void UpdateFalling()
    {

    }
    protected virtual void UpdateAirborne()
    {

    }
    protected override void Died()
    {
        base.Died();
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
