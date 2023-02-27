using System.Collections.Generic;
using UnityEngine;


public class LevelGenerator : MonoBehaviour
{
    [SerializeField] private Transform axis;
    [SerializeField] private Floor floorPrefab;
    [SerializeField] private FloorDestroy floorDestroyPrefab;

    private List<Floor> floors = new List<Floor>();
    private List<FloorDestroy> destroyFloors = new List<FloorDestroy>();
    public List<Floor> Floors => floors;
    public List<FloorDestroy> DestroyFloors => destroyFloors;


    [Header("Setting")]
    [SerializeField] private int defaultFloorAmount;
    [SerializeField] private float floorHeight;
    [SerializeField] private int emptySegmentAmount;
    [SerializeField] private int minTrapSegment;
    [SerializeField] private int maxTrapSegment;

    private float floorAmount = 0;
    public float FloorAmount => floorAmount;

    private float lastFoorY = 0;
    public float LastFloorY => lastFoorY;

    public void Generate(int level)
    {
        DestroyChild();

        floorAmount = defaultFloorAmount + level;

        axis.transform.localScale = new Vector3(1, floorAmount * floorHeight + floorHeight, 1);

        for (int i = 0; i < floorAmount; i++)
        {
            Floor floor = Instantiate(floorPrefab, transform);
            floor.transform.Translate(0, i * floorHeight, 0);
            floor.name = "Floor" + i;
            floors.Add(floor);

            FloorDestroy floorDestroy = Instantiate(floorDestroyPrefab, transform);
            floorDestroy.transform.Translate(0, i * floorHeight, 0);
            floorDestroy.name = "FloorDestroy " + i;
            floorDestroy.gameObject.SetActive(false);
            destroyFloors.Add(floorDestroy);

            if (i == 0)
            {
                floor.SetFinishAllSegment();
            }

            if (i > 0 && i < floorAmount - 1)
            {
                floor.SetRandomRotation();
                floor.AddEmptySegment(emptySegmentAmount);
                floor.AddRandomTrapSegment(Random.Range(minTrapSegment, maxTrapSegment + 1));
            }

            if (i == floorAmount - 1)

            {
                floor.AddEmptySegment(2);
                lastFoorY = floor.transform.position.y;
            }
        }
    }

    private void DestroyChild()
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            if (transform.GetChild(i) == axis) continue;

            Destroy(transform.GetChild(i).gameObject);
        }
    }
}
