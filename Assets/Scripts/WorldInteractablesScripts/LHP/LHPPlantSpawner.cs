using UnityEngine;
using System.Collections.Generic;
using System.Collections;
public class LHPPlantSpawner : MonoBehaviour
{
    [SerializeField] NunComponent nun;
    [SerializeField] List<LHPPlantComponent> possiblePlants = new List<LHPPlantComponent>();
    [SerializeField] int plantCount;

    List<LHPPlantComponent> plantPool = new List<LHPPlantComponent>();
    List<LHPPlantComponent> disabledPlants = new List<LHPPlantComponent>();

    [SerializeField] BoxCollider spawnArea;

    [SerializeField] Queue<LHPPlantComponent> plantsToCollect = new Queue<LHPPlantComponent>();

    [SerializeField] int maxPlantsUp;
    [SerializeField] int plantsUp;

    private void Start()
    {
        foreach (var p in possiblePlants)
        {
            for (int i = 0; i < plantCount; i++)
            {
                var instancedP = Instantiate(p, transform);
                instancedP.InitializePlant(this);
                instancedP.gameObject.SetActive(false);

                plantPool.Add(instancedP);
            }
        }

        disabledPlants = new List<LHPPlantComponent>(plantPool);

        StartCoroutine(PlantCollectionRoutine());
    }

    private IEnumerator PlantCollectionRoutine()
    {
        while (true)
        {
            while (plantsUp <= maxPlantsUp)
            {
                plantsToCollect.Enqueue(SpawnRandomPlant());
                plantsUp++;
            }

            var targetPlant = plantsToCollect.Dequeue();

            if(targetPlant != null && nun != null)
            {
                yield return StartCoroutine(nun.WalkToAndPickUp(targetPlant.transform, () => targetPlant.PickupPlant(), () => targetPlant.CompletePickupAndDisable()));
            }

            yield return null;
        }
    }

    public LHPPlantComponent SpawnRandomPlant()
    {
        int plantID = Random.Range(0, disabledPlants.Count);
        LHPPlantComponent plant = disabledPlants[plantID];

        disabledPlants.RemoveAt(plantID);

        plant.transform.localPosition = spawnArea.center - spawnArea.size / 2 + new Vector3(Random.Range(0, spawnArea.size.x), 0, Random.Range(0, spawnArea.size.z));
        plant.StartPlant();

        return plant;
    }

    public void RegisterDisabled(LHPPlantComponent plant)
    {
        disabledPlants.Add(plant);

        plantsUp--;
    }
}
