using UnityEngine;

public class CoinSpawner : MonoBehaviour
{
    public GameObject coinPrefab;
    public GameObject targetPrefab;

    public string yapi_adi = "";
    public string csvFilePath = "Assets/bilgiler/";   // CSV dosyası doğrudan burada verilecek

    public int numberOfCoins = 10;
    public float spawnRadius = 5f;

    void Start()
    {
        SpawnCoinsAroundTarget();
    }

    void SpawnCoinsAroundTarget()
    {
        if (coinPrefab == null || targetPrefab == null)
        {
            Debug.LogError("Coin prefab veya target prefab eksik!");
            return;
        }

        Vector3 targetPosition = targetPrefab.transform.position;

        for (int i = 0; i < numberOfCoins; i++)
        {
            float angle = Random.Range(0f, 360f);
            float distance = Random.Range(0f, spawnRadius);

            float x = targetPosition.x + Mathf.Cos(angle * Mathf.Deg2Rad) * distance;
            float z = targetPosition.z + Mathf.Sin(angle * Mathf.Deg2Rad) * distance;

            float y = Terrain.activeTerrain != null
                ? Terrain.activeTerrain.SampleHeight(new Vector3(x, 0, z))
                : targetPosition.y;

            Vector3 spawnPosition = new Vector3(x, y, z);

            GameObject newCoin = Instantiate(coinPrefab, spawnPosition, Quaternion.identity);

            var info = newCoin.AddComponent<CoinInfo>();
            info.yapiAdi = yapi_adi;
            info.csvFilePath = csvFilePath;
        }
    }
}
