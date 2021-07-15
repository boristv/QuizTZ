using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class LevelSpawner : MonoBehaviour
{
    [SerializeField] private Transform gameField;
    [SerializeField] private GameObject cellPrefab;

    private List<Cell> _cells = new List<Cell>();
    private List<Entity> _entitiesOnLevel;
    private int _cellCount;

    public UnityAction<Entity> LevelSpawnFinish;

    public void Spawn(int cellCount, List<Entity> entities, Entity rightEntity, bool spawnEffects = false)
    {
        ClearLevel(cellCount);
        for (int i = _cellCount; i < cellCount; i++)
        {
            var cellGO = Instantiate(cellPrefab, gameField.position, Quaternion.identity);
            cellGO.transform.parent = gameField;
            cellGO.transform.localScale = Vector3.one;
            _cells.Add(cellGO.GetComponent<Cell>());
        }
        _cellCount = cellCount;
        FillingCells(entities, rightEntity);
        if (spawnEffects)
        {
            foreach (var cell in _cells)
            {
                cell.Bounce(0.5f);
            }
        }
        LevelSpawnFinish?.Invoke(rightEntity);
    }
    
    private void ClearLevel(int remainingCellCount)
    {
        for (var i = _cellCount - 1; i >= remainingCellCount; i--)
        {
            var cell = _cells[i];
            Destroy(cell.gameObject);
            _cells.RemoveAt(i);
        }
    }
    
    private void FillingCells(List<Entity> entities, Entity rightEntity)
    {
        var randomEntities = RandomEntities(entities, rightEntity, _cellCount);
        for (var i = 0; i < _cells.Count; i++)
        {
            _cells[i].Entity = randomEntities[i];
        }
    }
    
    //с учетом того, что сущностей всегда больше или равно количеству ячеек
    private List<Entity> RandomEntities(List<Entity> entities, Entity rightEntity, int count)
    {
        var randomEntities = new List<Entity> {rightEntity};
        for ( ; randomEntities.Count < count; ) 
        {
            var random = Random.Range(0, entities.Count);
            if (!randomEntities.Contains(entities[random])) 
            {
                randomEntities.Add(entities[random]);                    
            }
        }

        int randomPosition = Random.Range(0, randomEntities.Count);
        randomEntities[0] = randomEntities[randomPosition];
        randomEntities[randomPosition] = rightEntity;
        
        return randomEntities;
    }
}
