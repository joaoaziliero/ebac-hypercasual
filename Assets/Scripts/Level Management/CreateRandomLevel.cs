using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class CreateRandomLevel : MonoBehaviour
{
    [SerializeField] private List<GameObject> _levelChunks;
    [SerializeField] private int _totalChunks;
    [SerializeField] private float _constantChunkSize;

    private void Start()
    {
        OrderLevel
            (
            rand: new System.Random(),
            chunks: _levelChunks,
            total: _totalChunks,
            size: _constantChunkSize
            )
            .ForEach(tuple => Instantiate(tuple.chunk)
            .transform.DOMoveZ(tuple.deltaZ, 1).SetRelative(true));
    }

    private List<(GameObject chunk, float deltaZ)> OrderLevel(System.Random rand, List<GameObject> chunks, int total, float size)
    {
        return Enumerable
            .Range(0, total)
            .OrderBy(_ => rand.Next())
            .Select((x, i) => (chunks[x % chunks.Count], size * i))
            .ToList();
    }
}
