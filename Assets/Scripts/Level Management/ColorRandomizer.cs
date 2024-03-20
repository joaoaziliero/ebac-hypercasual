using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColorRandomizer : MonoBehaviour
{
    [SerializeField] private List<Color> _colorPallete;
    [SerializeField] private List<Material> _materialsToColor;

    private void Start()
    {
        RandomizeMaterials
            (
            rand: new System.Random(),
            materials: _materialsToColor,
            colors: _colorPallete
            )
            .ForEach(tuple => { tuple.material.color = tuple.color; });
    }

    private List<(Material material, Color color)> RandomizeMaterials(System.Random rand, List<Material> materials, List<Color> colors)
    {
        return materials
            .OrderBy(_ => rand.Next())
            .Select((mat, i) => (mat, colors[i % colors.Count]))
            .ToList();
    }
}
