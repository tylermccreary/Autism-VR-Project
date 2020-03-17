using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class BotSpawner : MonoBehaviour
{
    [SerializeField] private float spawnRate;
    [SerializeField] private int maxSpawnedAtOnce;
    [SerializeField] private Collider aCollider;
    [SerializeField] Transform[] spawnLocationsOutside;
    [SerializeField] Transform[] spawnLocationsInside;
    [SerializeField] GameObject botPrefab;

    private int nextPriority = 10;
    private int totalShoppersShopping = 0;

    private float timeSinceLastSpawn = 0;

    private void Start() {
        SpawnCurrentShoppers();
    }

    private void Update() {
        timeSinceLastSpawn += Time.deltaTime;
        if (timeSinceLastSpawn > spawnRate) {
            timeSinceLastSpawn = 0f;
            if (totalShoppersShopping < maxSpawnedAtOnce) {
                int spawnIndex = Random.Range(0, spawnLocationsOutside.Length);
                SpawnShopper(spawnLocationsOutside[spawnIndex]);
            }
        }
    }

    private void OnTriggerEnter(Collider other) {
        BotShopperController botShopperController = other.gameObject.GetComponent<BotShopperController>();
        if (botShopperController != null && botShopperController.finishedShopping) {
            Destroy(other.gameObject);
            totalShoppersShopping -= 1;
        }
    }

    private void SpawnCurrentShoppers() {
        int amountToSpawn = Mathf.Min((int)(maxSpawnedAtOnce * 0.8f), spawnLocationsInside.Length);
        for (int i = 0; i < amountToSpawn; i++) {
            GameObject spawnedBot = SpawnShopper(spawnLocationsInside[i]);
            BotShopperController botShopperController = spawnedBot.GetComponent<BotShopperController>();
            botShopperController.ShowCart();
        }
    }

    private GameObject SpawnShopper(Transform locationTransform) {
        GameObject spawnedBot = Instantiate(botPrefab, locationTransform);
        spawnedBot.transform.parent = null;
        NavMeshAgent agent = spawnedBot.GetComponent<NavMeshAgent>();
        agent.avoidancePriority = nextPriority;
        totalShoppersShopping += 1;
        nextPriority += 1;

        return spawnedBot;
    }
}
