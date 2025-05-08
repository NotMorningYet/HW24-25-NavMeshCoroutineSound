using UnityEngine;

public class MineFactory : MonoBehaviour
{
    [SerializeField] private Mine _minePrefab;
    [SerializeField] private ExplosionFactory _explosionFactory;

    public Mine Create(Transform transform)
    {        
        int rotationY = Random.Range(0, 360);     

        Mine mine = Instantiate(_minePrefab, transform.position, Quaternion.Euler(new Vector3(0, rotationY, 0)));
        mine.Initialize(_explosionFactory);

        return mine;
    }
}
