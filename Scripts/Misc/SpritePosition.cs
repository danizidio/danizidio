using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpritePosition : MonoBehaviour
{
    SpriteRenderer[] s;
    public SpritePosition[] p;
    Transform t;
    public Vector3 v;
    public int count = 0;

    public Vector3[] others;

    public float[] difference;
    public float overallPos;

    void Start()
    {
        t = this.gameObject.transform;

        p = GameObject.FindObjectsOfType<SpritePosition>();

        s = GetComponentsInChildren<SpriteRenderer>();

        others = new Vector3[p.Length - 1];
        difference = new float[p.Length - 1];
    }

    void LateUpdate()
    {
        plrPos();
        VerifyOthersPos();
    }

    //changing the sprite layers
    public void ChangeLayer(string n)
    {
        foreach (var item in s)
        {
            item.sortingLayerName = n;
        } 
    }

    //collecting others positions
    public void VerifyOthersPos()
    {
        foreach (var item in p)
        {
            if (item.gameObject != this.gameObject)
            {
                others[count] = new Vector3(item.plrPos().x, item.plrPos().y, item.plrPos().z);

                difference[count] = others[count].z - v.z;

                count++;
            }
        }
        count = 0;
    }

    //verify sprite positions
    public Vector3 plrPos()
    {
        return v = new Vector3(t.transform.localPosition.x, t.transform.localPosition.y, t.transform.localPosition.z);
    }

    public float OverAllPos()
    {
        switch(PlayerContainer.instance.NPlayers)
        {
            case 2:
                {
                    overallPos = difference[0];

                    break;
                }
            case 3:
                {
                    overallPos = difference[0] + difference[1];

                    break;
                }
            case 4:
                {
                    overallPos = difference[0] + difference[1] + difference[2];

                    break;
                }
            default:
                {

                    break;
                }
        }
        return overallPos;
    }
}
