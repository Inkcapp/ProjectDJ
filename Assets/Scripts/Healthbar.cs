using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Healthbar : MonoBehaviour
{
    [SerializeField]
    private Image foreground;
    
    public void Refresh(float maxHealth, float health) {
        foreground.fillAmount = (health / maxHealth);
    }
}
