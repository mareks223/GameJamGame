using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spoilers : MonoBehaviour
{
   public GameObject spoilersONE;
   public GameObject spoilersTwo;
   void Start()
   {
        spoilersONE.SetActive(false);
        spoilersTwo.SetActive(false);
   }

   void OnMouseOver()
    {
        spoilersONE.SetActive(true);
        spoilersTwo.SetActive(true);
    }

    void OnMouseExit()
    {
        spoilersONE.SetActive(false);
        spoilersTwo.SetActive(false);
    }
}
