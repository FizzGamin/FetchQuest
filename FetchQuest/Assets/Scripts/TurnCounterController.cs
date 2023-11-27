using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TurnCounterController : MonoBehaviour
{
    public GameObject turnCharacterPrefab;

    public List<GameObject> curTurnsListed = new List<GameObject>();
    public List<EntityController> curEntityTurn = new List<EntityController>();


    public bool IsPlayerTurn()
    {
        PlayerController player = curEntityTurn[0] as PlayerController;
        return player != null;
    }

    public void RemoveCurrentTurn()
    {
        GameObject curTurnImage = curTurnsListed[0];
        curTurnsListed.RemoveAt(0);
        curEntityTurn.RemoveAt(0);
        Destroy(curTurnImage);
    }

    public void AddTurn(Sprite image, EntityController curEntity)
    {
        GameObject newTurn = Instantiate(turnCharacterPrefab);
        newTurn.transform.SetParent(transform);
        newTurn.transform.localScale = Vector3.one;
        newTurn.transform.GetChild(0).GetChild(0).GetComponent<Image>().sprite = image;
        curTurnsListed.Add(newTurn);
        curEntityTurn.Add(curEntity);
    }

    public void ClearTurns()
    {
        curTurnsListed.Clear();
        curEntityTurn.Clear();
        for (int i = transform.childCount; i > 0; i--)
        {
            Destroy(transform.GetChild(0).gameObject);
        }
    }
}
