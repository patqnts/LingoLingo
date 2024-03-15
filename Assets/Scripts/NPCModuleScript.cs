using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCModuleScript : MonoBehaviour
{
    // Start is called before the first frame update
   public void OpenChallenge()
    {
        ModulesHandler.Instance.StartGame();
    }
}
