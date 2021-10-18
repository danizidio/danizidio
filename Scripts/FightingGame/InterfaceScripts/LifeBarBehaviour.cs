using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//This script manages the UI for a life bar
//combo counter and if the life bar is active for a player

public class LifeBarBehaviour : MonoBehaviour
{
    Animator anim;
    [SerializeField] Image img, img2;

    [SerializeField] GameObject pressStart;

    [SerializeField] Image _charPortait;

    [SerializeField] GameObject enemyLifeBar;
    public Image CharPortrait { get { return _charPortait; } set { _charPortait = value; } }

    public ComboCounter combo;

//Adjust the fill amount properties for the images
//And setting a specific color representing an untouched health point for a player
    private void Start()
    {
        anim = this.GetComponent<Animator>();

        img.fillAmount = 1;
        img2.fillAmount = 1;

        img.color = new Color32(255, 255, 0, 255);

        if (enemyLifeBar != null) enemyLifeBar.SetActive(false);
    }

//This update is called when a player takes a hit
//Setting the animation
//the calcs for the damage subtracts the value for current life
//The color for the image representing the life bar changes if not in 100% life and if drops below 30%
    public void UpdateLifeBar(float currentLife, float maxLife)
    {
        anim.SetTrigger("HIT");

        float value = currentLife / maxLife;

        img.fillAmount = value;

        if(currentLife < 0)
        {
            img.fillAmount = 0;
        }

        StartCoroutine(RedBarUpdate(value, currentLife, maxLife));

        if (value <= .3f)
        {
            img.color = new Color32(255, 0, 0, 255);
        }
        else
        {
            img.color = new Color32(50, 255, 0, 255);
        }
    }

//The red bar is the effect for the primary life bar
//First the primary bar is reduced, after some time the secondary drops too
    IEnumerator RedBarUpdate(float v,float currentLife, float maxLife)
    {
        yield return new WaitForSeconds(.5f);

        float value2 = currentLife / maxLife;

        img2.fillAmount = value2;

        if(value2 < 0)
        {
            value2 = 0;
        }
    }

//A little health bar appears when a player gives a hit in an enemy
//Stays for an amount of time then desappears if dont have any update
    public void EnemyBarUpdate(float currentLife, float maxLife, Sprite portrait)
    {
        enemyLifeBar.SetActive(true);

        enemyLifeBar.GetComponent<EnemyLifeBar>().FrontLifeBar.fillAmount = currentLife / maxLife;

        enemyLifeBar.GetComponent<EnemyLifeBar>().EnemyPortrait.sprite = portrait;

        enemyLifeBar.GetComponent<EnemyLifeBar>().StayActive();
    }

//If the bar is active the bar appears with all pertinent UI
//If not, the text for "press start" shows
    public void SettingLifeBars(bool act)
    {
        CharPortrait.gameObject.SetActive(act);
        pressStart.SetActive(!act);
    }


//Show the values stored em the combo script
    public void ComboIncrement()
    {
        combo.GetComponent<ComboCounter>().ComboIncrement();
    }
}
