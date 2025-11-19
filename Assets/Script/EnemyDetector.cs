using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDetector : MonoBehaviour
{
    [SerializeField] private float detectionRadius = 10.0f;
    [SerializeField] private LayerMask enemyLayer;

    public GameObject GetClosestEnemy()
    {
        Collider[] enemieslnRange = Physics.OverlapSphere(transform.position, detectionRadius, enemyLayer);
        if (enemieslnRange.Length > 0)
        {
            GameObject besstTarget = null;
            float closestDistanceSqr = Mathf.Infinity;
            Vector3 currentPosition = transform.position;

            foreach (Collider enemyCollider in enemieslnRange)
            {
                if (enemyCollider.gameObject == this.gameObject)
                    continue;

                Vector3 directionToTarget = enemyCollider.transform.position - currentPosition;
                float dSqrToTarget = directionToTarget.sqrMagnitude;

                if (dSqrToTarget < closestDistanceSqr)
                {
                    closestDistanceSqr = dSqrToTarget;
                    besstTarget = enemyCollider.gameObject;
                }
            }
            return besstTarget;
        }
        else
        {
            return null;
        }
    }

    public List<GameObject> GetEnemieslnRange()
    {
        List<GameObject> enemiesList = new List<GameObject>();
        Collider[] enemieslnRange = Physics.OverlapSphere(transform.position, detectionRadius, enemyLayer);

        foreach(Collider enemyCollider in enemieslnRange)
        {
            if(enemyCollider.gameObject == this.gameObject)
            {
                enemiesList.Add(enemyCollider.gameObject); 
            }
        }
        return enemiesList;
    }
    

}
