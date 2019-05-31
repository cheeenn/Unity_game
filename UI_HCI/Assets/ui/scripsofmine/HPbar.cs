using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class HPbar : MonoBehaviour
{

    [SerializeField]
    private float fillamount;

    [SerializeField]
    private Image content;
    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        Handlerbar(fillamount);
    }
    private void Handlerbar(float fillamount)
    {
        content.fillAmount = fillamount;
    }
   private float Map(float value,float inMin, float inMax,float outmin,float outmax)
    {
        return (value - inMin) * (outmax - outmin) / (inMax - inMin) + outmin;
    }
}
