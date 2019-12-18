﻿using Unity.Entities;
using Unity.Transforms;
using UnityEngine;
using UnityEngine.Serialization;

public class RandomGenerator : MonoBehaviour
{
    private Entity entity;

    [FormerlySerializedAs("HowMany")] [SerializeField]
    private int howMany = 100;

    // ReSharper disable once RedundantDefaultMemberInitializer
    [SerializeField] private GameObject prefab = null;

    [SerializeField] private Vector2 x = Vector2.one, y = Vector2.one, z = Vector2.one, mass = Vector2.one;

    // Start is called before the first frame update
    private void Start()
    {
        var ePrefab = GameObjectConversionUtility.ConvertGameObjectHierarchy(prefab, World.Active);
        var entityManager = World.Active.GetOrCreateSystem<BeginSimulationEntityCommandBufferSystem>()
            .CreateCommandBuffer();
        for (var i = 0; i < howMany; i++)
        {
            entity = entityManager.Instantiate(ePrefab);
            entityManager.SetComponent(entity, new Translation
            {
                Value =
                {
                    x = Random.Range(x.x, x.y),
                    y = Random.Range(y.x, y.y),
                    z = Random.Range(z.x, z.y)
                }
            });
            entityManager.AddComponent(entity, new PlanetData {Mass = Random.Range(mass.x, mass.y)});
        }
    }
}