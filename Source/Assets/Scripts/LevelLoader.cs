using UnityEngine;

public class LevelLoader : MonoBehaviour
{
    [SerializeField] private Texture2D map;
    [SerializeField] private PrefabColor[] prefabColor;

    void Awake()
    {
        Generate();
    }

    void Generate()
    {
        for (int y = 0; y < map.width; y++)
        {
            for (int x = 0; x < map.height; x++)
            {
                Tile(x, y);
            }
        }
    }

    void Tile(int x, int y)
    {
        var pixel = map.GetPixel(x, y);
        if (pixel.a == 0) return;

        foreach (var pc in prefabColor)
        {
            if (pc.color.Equals(pixel))
            {
                Instantiate(pc.prefab, new Vector2(x, y), Quaternion.identity, transform);
            }
        }
    }
}

[System.Serializable]
public class PrefabColor
{
    public Color color;
    public GameObject prefab;
}