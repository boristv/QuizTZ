using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;

public class GameSession : MonoBehaviour
{
    private readonly int[] _cellCountAtLevel = new[] {3, 6, 9};
    private int _difficultyLevel = 0;
    
    [SerializeField] private List<EntitiesSet> entitiesSets;
    [SerializeField] private LevelSpawner levelSpawner;

    private List<Entity> _previousRightAnswers = new List<Entity>();
    private Entity _rightAnswer;
    
    public UnityAction OnSessionOver;

    [SerializeField] private GameObject stars;
    private ParticleSystem starsParticleSystem;
    
    private void Start()
    {
        ClickListener.OnClick += Answer;
        starsParticleSystem = stars.GetComponent<ParticleSystem>();
        StartSession();
    }

    public void StartSession()
    {
        StartLevel(0, true);
    }

    private void StartLevel(int difficultyLevel, bool spawnEffects = false)
    {
        _difficultyLevel = difficultyLevel;
        var entities = entitiesSets[Random.Range(0, entitiesSets.Count)].Entities;
        
        //если все сущности уже были показаны
        if (!entities.Except(_previousRightAnswers).Any())
        {
            foreach (var entity in entities)
            {
                _previousRightAnswers.Remove(entity); 
            }
        }
        
        do
        {
            _rightAnswer = entities[Random.Range(0, entities.Count)];
        } while (_previousRightAnswers.Contains(_rightAnswer));

        _previousRightAnswers.Add(_rightAnswer);
        
        levelSpawner.Spawn(_cellCountAtLevel[difficultyLevel], entities, _rightAnswer, spawnEffects);
    }
    

    private void NextLevel()
    {
        if (_difficultyLevel == _cellCountAtLevel.Length - 1)
        {
            GameSessionOver();
        }
        else
        {
            _difficultyLevel++;
            StartLevel(_difficultyLevel);
        }
    }
    
    private void SuccessAnswer(EntityObject entityObject)
    {
        stars.transform.position = new Vector3(entityObject.transform.position.x, entityObject.transform.position.y, 0);
        starsParticleSystem.Play();
        const float time = 0.5f;
        entityObject.Bounce(time);
        Invoke(nameof(NextLevel), time);
    }

    private void WrongAnswer(EntityObject entityObject)
    {
        entityObject.EaseInBounce(0.5f);
    }

    private void Answer(GameObject go, Entity entity)
    {
        var entityObject = go.GetComponent<EntityObject>();
        if (entity.Equals(_rightAnswer))
        {
            SuccessAnswer(entityObject);
        }
        else
        {
            WrongAnswer(entityObject);
        }
    }

    private void GameSessionOver()
    {
        OnSessionOver?.Invoke();
    }
}
