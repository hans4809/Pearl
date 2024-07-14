using System.Collections;
using System.Collections.Generic;
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

    public void OnKeyEscape()
    {
        if(Time.timeScale != 0)
        {
            Time.timeScale = 0;
            Managers.UI.ShowPopUpUI<UI_Setting>();
        }
        else
        {
            Time.timeScale = 1;
            Managers.UI.ClosePopUpUI();
        }

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
        if (Time.timeScale == 0)
            return;

        if (Input.GetKeyUp(KeyCode.Escape))
        {
            Time.timeScale = 0;
            //Managers.Game.GameState = EGameState.Pause;
            Managers.UI.ShowPopUpUI<UI_Setting>();
        }
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
