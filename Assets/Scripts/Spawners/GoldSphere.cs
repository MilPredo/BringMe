
using UnityEngine;
using System.Collections;

public class GoldSphere : MonoBehaviour
{
    int lifespan = 5;

    void Start() {
        Debug.Log("LifeSpan" + lifespan.ToString());
        StartCoroutine(LifeWait());
    }

    IEnumerator LifeWait() {
        yield return new WaitForSeconds(lifespan);
        Debug.Log("Dead");
        ItemPool.Instance.AddToPool(gameObject);
    }
}
