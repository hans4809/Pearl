using System.Collections;
using System.Collections.Generic;
using UnityEditorInternal.Profiling.Memory.Experimental;
using UnityEngine;

public class Item : MonoBehaviour, IFieldObject
{
    public Vector3 itemLocation;
    public ItemEnum itemType;

    public int pearlAmount = 1;
    public int kingPearlAmount = 10;
    public int bugAmount = -1;
    public int netAmount = 1;
    public int timerPlusAmount = 1;
    public int timerMinusAmount = -1;



    public void UseItemPlayer1()
    {
        itemType = gameObject.GetComponent<Item>().itemType;

        switch(itemType)
        {
            case ItemEnum.PEARL:
                Managers.Score.player1Score += pearlAmount;
                break;
            case ItemEnum.KING_PEARL:
                Managers.Score.player1Score += kingPearlAmount;
                break;
            case ItemEnum.BUG:
                Managers.Score.player1Score += bugAmount;
                break;
            case ItemEnum.NET:
                Managers.Score.player1Score += netAmount;
                Managers.Score.player2Score -= netAmount;
                break;
            case ItemEnum.TIME_PLUS:
                Managers.Time.counter += timerPlusAmount;
                break;
            case ItemEnum.TIME_MINUS:
                Managers.Time.counter += timerMinusAmount;
                break;



        }
    }

    public void UseItemPlayer2() // 분명 더 좋은 방법이 있는데......포인터 못 써서 이렇게 따로 만든다...
    {

    }


    private void OnTriggerEnter(Collider other) // 3D collider
    {
        if (other.CompareTag("Player1"))
        {
            UseItemPlayer1();
        }
        else if (other.CompareTag("Player2"))
        {
            UseItemPlayer2();
        }
    }

    private void Awake()
    {
        itemLocation = gameObject.transform.position;
    }

}
