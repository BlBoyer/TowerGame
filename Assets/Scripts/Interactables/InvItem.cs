using UnityEngine;
public class InvItem : InteractableObject
{
    //override method
    protected override void onInteract()
    {
        //stop rendering object
        this.gameObject.SetActive(false);
        //add to inventory
    }
}
