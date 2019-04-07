using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PunchButtonController : MonoBehaviour
{

    public Sprite ready_img_source;
    public Sprite not_ready_img_source;

    public Image button_image;
    public Image filled_image;
    

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void UpdateFillAmount(float amount)
    {
        filled_image.fillAmount = amount;
        if (filled_image.fillAmount == 1) ReadyToPunch();
        
    }

    public void ResetFillAmount()
    {
        filled_image.fillAmount = 0;
        NotReadyToPunch();
    }

    public void ReadyToPunch()
    {
        button_image.sprite = ready_img_source;
    }

    public void NotReadyToPunch()
    {
        button_image.sprite = not_ready_img_source;
    }

}
