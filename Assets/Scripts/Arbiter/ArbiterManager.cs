using System.Collections.Generic;
using UnityEngine;

public class ArbiterManager : MonoBehaviour {

    [SerializeField] List<Item> targetSelections;
    [SerializeField] private Item targetItem;

    private void Start() {
        ChangeTargetItem();
    }

    public void ChangeTargetItem() {
        targetItem = targetSelections[Random.Range(0, targetSelections.Count)];
        Debug.Log($"Changing Target Item: { targetItem.prefab.name }");
    }

    public void PickUpItem(Item item) {
        Debug.Log($"Pickup: { item.itemName }");
    }

    public string TargetItemName {
        get { return $"{this.targetItem.prefab.name}(Clone)"; }
        private set {}
    }
}
