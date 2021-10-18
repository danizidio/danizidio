using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LifeBarBehaviour : MonoBehaviour
{
    Animator anim;
    [SerializeField] Image img, img2;

    [SerializeField] GameObject pressStart;

    [SerializeField] Image _charPortait;

    [SerializeField] GameObject enemyLifeBar;
    public Image CharPortrait { get { return _charPortait; } set { _charPortait = value; } }

    public ComboCounter combo;

    private void Start()
    {
        anim = this.GetComponent<Animator>();

        img.fillAmount = 1;
        img2.fillAmount = 1;

        img.color = new Color32(255, 255, 0, 255);

        if (enemyLifeBar != null) enemyLifeBar.SetActive(false);
    }

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

    public void EnemyBarUpdate(float currentLife, float maxLife, Sprite portrait)
    {
        enemyLifeBar.SetActive(true);

        enemyLifeBar.GetComponent<EnemyLifeBar>().FrontLifeBar.fillAmount = currentLife / maxLife;

        enemyLifeBar.GetComponent<EnemyLifeBar>().EnemyPortrait.sprite = portrait;

        enemyLifeBar.GetComponent<EnemyLifeBar>().StayActive();
    }

    public void SettingLifeBars(bool act)
    {
        CharPortrait.gameObject.SetActive(act);
        pressStart.SetActive(!act);
    }



    public void ComboIncrement()
    {
        combo.GetComponent<ComboCounter>().ComboIncrement();
    }
}
