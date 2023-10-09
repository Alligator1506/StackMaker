using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : Singleton<LevelManager>
{
    public List<Level> levels = new List<Level>();
    Level currentLevel;
    public Player player;

    int level = 1;

    public void Start()
    {      
        UIManager.Instance.OpenMainMenuUI();
        OnLoadLevel();
    }

    public void OnLoadLevel()
    {
        OnLoadLevel(level);
        OnInit();

    }

    public void OnLoadLevel(int indexLevel)
    {
        if (currentLevel != null)
        {
            Destroy(currentLevel.gameObject);
        }
        currentLevel = Instantiate(levels[indexLevel - 1]);
    }

    public void OnInit()
    {
        player.transform.position = currentLevel.startPoint.position;
        player.OnInit();
    }

    public void OnStart()
    {
        GameManager.Instance.ChangeState(GameState.Gameplay);
    }

    public void OnFinish()
    {
        UIManager.Instance.OpenFinishUI();
        GameManager.Instance.ChangeState(GameState.Finish);
    }

    public void NextLevel()
    {
        level++;
        OnLoadLevel();
    }
}
