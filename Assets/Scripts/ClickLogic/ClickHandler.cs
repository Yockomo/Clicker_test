using StarterAssets;
using UnityEngine;

public class ClickHandler : MonoBehaviour
{
    [SerializeField] private EnemyPool enemyPool;
    [SerializeField] private StarterAssetsInputs inputs;


    private void OnEnable()
    {
        inputs.OnStartTouch += HandleClick;
    }

    private void OnDisable()
    {
        inputs .OnStartTouch -= HandleClick;
    }

    public void HandleClick(Vector2 coordinate)
    {
        var screenCoordinates = new Vector3(coordinate.x, coordinate.y, Camera.main.nearClipPlane);
        var worldCoordinates = Camera.main.ScreenToWorldPoint(screenCoordinates);
        var direction = new Vector3(0, -1, 0);

        Physics.Raycast(worldCoordinates,direction, out RaycastHit rayHit,999f);
        var result = Physics.OverlapSphere(rayHit.point, 0.6f);
        foreach (var res in result)
        {
            if ( res.TryGetComponent<IClickable>(out IClickable click))
            {
                click.Click();
                return;
            }
        }
    }
}
