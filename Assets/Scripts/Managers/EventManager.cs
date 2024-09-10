using UnityEngine.Events;
public static class EventManager
{
    public static event UnityAction<int> OnUpdateWoodAmount;
    public static void UpdateWoodAmount(int resource_amount) => OnUpdateWoodAmount?.Invoke(resource_amount);

    public static event UnityAction<int> OnUpdateFoodAmount;
    public static void UpdateFoodAmount(int resource_amount) => OnUpdateFoodAmount?.Invoke(resource_amount);

    public static event UnityAction<int> OnUpdateCoinAmount;
    public static void UpdateCoinAmount(int resource_amount) => OnUpdateCoinAmount?.Invoke(resource_amount);

    public static event UnityAction<int> OnUpdateIronAmount;
    public static void UpdateIronAmount(int resource_amount) => OnUpdateIronAmount?.Invoke(resource_amount);

    public static event UnityAction<int> OnUpdateMoraleAmount;
    public static void UpdateMoraleAmount(int resource_amount) => OnUpdateMoraleAmount?.Invoke(resource_amount);
}
