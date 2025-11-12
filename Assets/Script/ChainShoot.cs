using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChainShoot : MonoBehaviour
{
    [SerializeField] float refreshRate = 0.01f;
    [SerializeField][Range(1, 10)] int maximunEnemieslnChain = 3;
    [SerializeField] float delayBetweenEachChain = 0.3f;
    [SerializeField] Transform playerFirePoint;
    [SerializeField] EnemyDetector playerEnemyDetector;
    [SerializeField] GameObject lineRendererPrefab;

    bool shooting;
    bool shot;
    float counter = 1;
    GameObject currentClosestEnemy;
    List<GameObject> spawnedLineRenderers = new List<GameObject>();
    List<GameObject> enemieslnChain = new List<GameObject>();
    List<GameObject> activeEffect = new List<GameObject>(); 

    void StopShooting()
    {
        shooting = false;
        shot = false;
        counter = 1;

        for (int i = 0; i < spawnedLineRenderers.Count; i++)
        {
            Destroy(spawnedLineRenderers[i]);
        }

        spawnedLineRenderers.Clear();
        enemieslnChain.Clear();

        for(int i = 0; i < activeEffect.Count; i++)
        {
            Destroy(activeEffect[i]);
        }

        activeEffect.Clear();
    }
    IEnumerator UpdateLineRenderer(GameObject lineR, Transform startpos, Transform endpos, bool getClosestEnemyToplayer = false)
    {
        if(shooting && shot && lineR != null)
        {
            lineR.GetComponent<LineRenderer>().SetPosition(startpos, endpos);

            yield return new WaitForSeconds(refreshRate);

            if(getClosestEnemyToplayer)
            {
                StartCoroutine(UpdateLineRenderer(lineR, startpos, playerEnemyDetector.GetClosestEnemy().transform, true));

                if(currentClosestEnemy != playerEnemyDetector.GetClosestEnemy())
                {
                    StopShooting();
                    //Star tShooting();
                }

            }
            else
            {
                StartCoroutine(UpdateLineRenderer(lineR, startpos, endpos));
            }
        }
    }
    
    void NewLineRenderer(Transform startpos, Transform endpos, bool getClosestEnemyToPlayer = false)
    {
        GameObject lineR = Instantiate(lineRendererPrefab);
        spawnedLineRenderers.Add(lineR);
        StartCoroutine(UpdateLineRenderer(lineR, startpos, endpos, getClosestEnemyToPlayer));
    }

    IEnumerator ChainReaction(GameObject closestEnemey)
    {
        yield return new WaitForSeconds(delayBetWeenEachChain)
           
        if(counter == maximunEnemieslnChain)
        {
            yield return null
        }
        else
        {
            if(shooting)
            {
                counter++;
                enemieslnChain.Add(closestEnemey);

                if(!enemieslnChain.Contains(closestEnemey.GetComponent<EnemyDetector>().GetClosestEnemy()))
                {
                    NewLineRenderer(closestEnemey.transform, closestEnemey.GetComponent<EnemyDetector>().GetClosestEnemy().transform);
                    StartCoroutine(ChainReaction(closestEnemey.GetComponent<EnemyDetector>().GetClosestEnemy()));

                }
            }
        }
    }

    //스타트 슈팅 합수 작성 하기 업데이트 키 설정도 하기




}
