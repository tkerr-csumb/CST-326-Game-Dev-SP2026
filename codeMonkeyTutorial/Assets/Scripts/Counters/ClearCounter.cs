using UnityEngine;

public class ClearCounter : BaseCounter {
    [SerializeField] private KitchenObjectSO kitchenObjectSO;

    public override void Interact(Player player){
        if (!HasKitchenObject()){
            // no object
            if (player.HasKitchenObject()){
                // is carrying object
                player.GetKitchenObject().SetKitchenObjectParent(this);
            } else {
                // not carrying object
            }
        } else {
            // has object
            if (player.HasKitchenObject()){
                // is carrying object
                if (player.GetKitchenObject().TryGetPlate(out PlateKitchenObject plateKitchenObject)){
                    //player is holding plate
                    if (plateKitchenObject.TryAddIngredient(GetKitchenObject().GetKitchenObjectSO())){
                        GetKitchenObject().DestroySelf();
                    }
                } else {
                    if (GetKitchenObject().TryGetPlate(out plateKitchenObject)){
                        if (plateKitchenObject.TryAddIngredient(player.GetKitchenObject().GetKitchenObjectSO())) {
                            player.GetKitchenObject().DestroySelf();
                        }
                    }
                }
            } else {
                // not carrying object
                GetKitchenObject().SetKitchenObjectParent(player);
            }
        }
    }
}
