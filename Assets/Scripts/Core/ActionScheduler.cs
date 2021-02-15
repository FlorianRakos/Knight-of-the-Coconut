using UnityEngine;
using RPG.Core;

namespace RPG.Core
{
    
    public class ActionScheduler : MonoBehaviour 
    {

        IAction currentActiion;


        public void StartAction(IAction action)
        {

            if (currentActiion == action) return;
            if (currentActiion != null)
            {
                currentActiion.Cancel();           
            }
            currentActiion = action;  
            

        }


    }
}