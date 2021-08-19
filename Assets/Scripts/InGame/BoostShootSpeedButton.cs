using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BoostShootSpeedButton : MonoBehaviour
{

    Button thisButton;
    public Image thisImage;
   
    float fillamount;

    private void Start()
    {
        thisButton = GetComponent<Button>();
        //thisImage = GetComponent<Image>();
        fillamount = 100;
    }
    void Update()
    {
        thisImage.fillAmount = fillamount;
    }


    public void Pressed()
    {
        thisButton.interactable = false;
        StartCoroutine(RechargeTime());

    }

    private IEnumerator RechargeTime() 
    {
        fillamount = 0;
        for(int i = 0; i < 40; i++) 
        {
            fillamount += (0.025f);
            
            yield return new WaitForSeconds(1);

        }
        fillamount = 1;
        thisButton.interactable = true;

    }

    
}
