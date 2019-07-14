using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PickUp : MonoBehaviour
{
    Vector2 dotPos = new Vector2(625, 320);

    bool isChecking;

    int checkAmount;




    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.F))
        {
            if (isChecking)
            {
                
                // open slider
                // generate pos
                //start moving
                // Vector2 pos = slider dot pos;
                // if(pos in the area){
            }
            else
            {
                RaycastHit hit;
                Ray ray = Camera.main.ScreenPointToRay(dotPos);
                if (Physics.Raycast(ray, out hit, 100.0f))
                {
                    if (hit.collider.tag == "Gift")
                    {
                        Debug.Log("You are collecting this gift");
                        isChecking = true;
                        
                        // if player press F and he is now in skill check
                        //check position of Point in this moment
                        //if this point in the area open next skill check
                        // if not close slider
                        //make sound
                        //if it was last skill check and player did it right
                        // destroy gift and add it to amount 
                    }
                }

                checkAmount = Random.Range(2, 5);
            }
            
        }


    }
}
