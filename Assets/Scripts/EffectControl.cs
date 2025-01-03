using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EffectControl : MonoBehaviour
{

    public static EffectControl EffectInstance;
    // Start is called before the first frame update
    void Start()
    {
        if(EffectInstance == null)
        {
            EffectInstance = this;
        }
        else
        {
            if(EffectInstance != this)
            {
                Destroy(gameObject);
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        CheckShaking();
    }

    
    public GameObject curTalker;
    public bool shake=false;
    public void CheckShaking()
    {
        if(shake)
        {
            ShakeSprite shakeSprite = curTalker.GetComponent<ShakeSprite>();
            shakeSprite.Shake();
            shake = false;
        }
    }
}
