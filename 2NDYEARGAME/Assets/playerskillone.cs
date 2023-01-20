using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerskillone : MonoBehaviour
{
    public Collider col;
    // Start is called before the first frame update
    void Start()
    {
        Destroy(gameObject, 2f);
        col.enabled = true;
        StartCoroutine(diseableSkill());
    }

   IEnumerator diseableSkill()
   {
    yield return new WaitForSeconds(1);
    col.enabled = false;
   }

}
