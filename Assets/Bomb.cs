using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bomb : MonoBehaviour
{
    [SerializeField] Animator _anim;
    public Animator Anim { get => _anim; private set => _anim = value; }
    // Start is called before the first frame update
    void Start()
    {
        if(Anim != null)
            Anim = GetComponent<Animator>();
    }

    private void OnEnable()
    {
        Anim.SetBool("isBomb", true);
    }

    private void OnDisable()
    {
        Anim.SetBool("isBomb", false);
    }
    // Update is called once per frame
    void Update()
    {
        
    }

    public void EndOfAnimation()
    {
        Managers.Resource.Destroy(gameObject);
    }
}
