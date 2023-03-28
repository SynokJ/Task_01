public class InputSystem
{
    private const int _MAX_HIT_DISTANCE = 20;

    public delegate void OnDestiantionSet(UnityEngine.Vector3 destPos);
    private OnDestiantionSet onDestiantionSet = default;

    private UnityEngine.LayerMask _hitLayerMask = default;
    private UnityEngine.Touch _firstTouch = default;
    private UnityEngine.Camera _camera = null;
    private UnityEngine.Ray ray = default;

    public InputSystem(UnityEngine.LayerMask hitLayer, OnDestiantionSet onDestiantionSet)
    {
        _hitLayerMask = hitLayer;
        _camera = UnityEngine.Camera.main;
        this.onDestiantionSet = onDestiantionSet;
    }

    public void OnScreenTouched()
    {
        if (UnityEngine.Input.touchCount == 0)
            return;

        if (UnityEngine.EventSystems.EventSystem.current.IsPointerOverGameObject(_firstTouch.fingerId))
            return;

            _firstTouch = UnityEngine.Input.GetTouch(0);
        if (_firstTouch.phase == UnityEngine.TouchPhase.Began)
        {
            ray = _camera.ScreenPointToRay(_firstTouch.position);
            if (UnityEngine.Physics.Raycast(ray, out UnityEngine.RaycastHit hit, _MAX_HIT_DISTANCE, _hitLayerMask))
                onDestiantionSet?.Invoke(hit.point);
        }
    }
}
