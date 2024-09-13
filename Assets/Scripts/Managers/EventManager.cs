using UnityEngine;
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

    public static event UnityAction<int> OnMoraleChanged;
    public static void MoraleChanged(int resource_amount) => OnMoraleChanged?.Invoke(resource_amount);


    public static event UnityAction<string, GameObject> OnTeamChangeAutomaticAnimations;
    public static void TeamChangeAutomaticAnimations(string team_color, GameObject building_obj) => OnTeamChangeAutomaticAnimations?.Invoke(team_color, building_obj);

    public static event UnityAction OnSceneReload;
    public static void SceneReload() => OnSceneReload?.Invoke();

    public static event UnityAction<string> OnTransitionStart;
    public static void TransitionStart(string scene_name) => OnTransitionStart?.Invoke(scene_name);
    #region Test Events

    public static event UnityAction<string> OnMouseClickToCapture;
    public static void MouseClickToCapture(string color) => OnMouseClickToCapture?.Invoke(color);
    #endregion
}
