using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallingPlatform : BasePlatform
{
    [SerializeField] float _fallingTime = 1f;
    public float FallingTime { get => _fallingTime; private set => _fallingTime = value; }

    [SerializeField] float _fallingPower = 5f;
    public float FallingPower { get => _fallingPower; private set => _fallingPower = value; }
    // Start is called before the first frame update
    void Start()
    {
        Init();
    }

    public override void Init()
    {
        base.Init();
    }


    void Falling()
    {
        //RB.useGravity = true;
        //RB.velocity = Vector3.down * FallingPower;
        RB.isKinematic = false;
        RB.AddForce(Vector3.down * FallingPower, ForceMode.Impulse);
        Debug.Log("Falling");
    }


    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            Invoke("Falling", FallingTime);
            Debug.Log("Collsion");
        }
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
