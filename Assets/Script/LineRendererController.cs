using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewBehaviourScript : MonoBehaviour
{
    [SerializeField] List<LineRenderer> lineRenderers = new List<LineRenderer>();



    public void Setposition(Transform startpos, Transform endpos)
    {
        if (lineRenderers.Count > 0)
        {
            for (int i = 0; i < lineRenderers.Count; i++)
            {
                if (lineRenderers[i].positionCount >= 2)
                {
                    lineRenderers[i].SetPosition(0, startpos.position);
                    lineRenderers.[i].SetPosition(1, endpos.position);  
                }
            }
        }
    }
}
