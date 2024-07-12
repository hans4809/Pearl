using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasePlatform : MonoBehaviour
{
    //[SerializeField] Collider _collider;
    //public Collider Collider { get => _collider; private set => _collider = value; }
    [SerializeField] Rigidbody _rb;
    public Rigidbody RB { get => _rb; private set => _rb = value; }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    public virtual void Init()
    {
        if (RB == null)
        {
            RB = GetComponent<Rigidbody>();
            RB.useGravity = false;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
