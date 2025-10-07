using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BloodControl : MonoBehaviour
{
    public Image targetImage;
    public static BloodControl Instance;

    void Start()
    {
        Instance = this;
        targetImage.sprite = Resources.Load<Sprite>("images/life_3");
    }


    public void BloodChanged(int hp)
    {
        if (hp >= 0 && hp < 3)
        {
            targetImage.sprite = Resources.Load<Sprite>("images/life_"+hp.ToString());
        }
    }
}
