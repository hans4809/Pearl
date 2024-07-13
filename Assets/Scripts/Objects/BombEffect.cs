using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombEffect : MonoBehaviour
{
    [SerializeField] Animator _anim;
    public Animator Anim { get => _anim; private set => _anim = value; }
    // Start is called before the first frame update
    void Start()
    {
        if(Anim != null)
            Anim = GetComponent<Animator>();
    }

    public void OnFinishedAnimation()
    {
        gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
