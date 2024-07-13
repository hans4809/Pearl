using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimTest : MonoBehaviour
{
    [SerializeField] CharacterControllerEx characterControllerEx;
    public CharacterControllerEx Character { get { return characterControllerEx; } }
    [SerializeField] float _power;
    public float Power { get { return _power; } }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Z))
        {
            Character.State = Define.State.Damaged;
        }
        if (Input.GetKeyDown(KeyCode.X))
        {
            Character.State = Define.State.Airborne;
        }
    }
}
