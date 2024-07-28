using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimTest : MonoBehaviour
{
    [SerializeField] CharacterControllerEx characterControllerEx;
    public CharacterControllerEx Character { get { return characterControllerEx; } }
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
