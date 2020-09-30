using System.Collections;
using ItemRelated;
using UnityEngine;

public class UIBinding : MonoBehaviour
{
    //private void Awake()
    //{
    //    StartCoroutine(Init());
    //}

    IEnumerator Start()
    {
        var player = FindObjectOfType<Player>();
        while (player == null)
        {
            yield return null;
            player = FindObjectOfType<Player>();
        }
        GetComponent<UIInventoryPanel>().BindToInventory(player.GetComponent<Inventory>());
    }
    
}
