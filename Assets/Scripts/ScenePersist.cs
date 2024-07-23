using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScenePersist : MonoBehaviour
{
   void Awake()
   {
       
       int scenePersistLength = FindObjectsOfType<ScenePersist>().Length;
        
        if(scenePersistLength > 1)
        {
            ResetScenePersist();
        }
        else
        {
            DontDestroyOnLoad(gameObject);
        }
   }

   public void ResetScenePersist()
   {
       Destroy(gameObject);
   }
}
