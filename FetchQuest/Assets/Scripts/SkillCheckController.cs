using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SkillCheckController : MonoBehaviour
{
    public Image skillCheckBar;
    public Image playerMark;
    private float skillCheckWidth = 180f;

    public float curPlayerMarkPos = 0; // 0 - 1
    public float duration = 1; 
    public bool active = true;
    public bool movingRight = true;
    private float time = 0;

    public static SkillCheckController instance;

    void Awake()
    {
        instance = this;
    }

    public void EnableSkillCheck(bool isPaused)
    {
        active = isPaused;
    }

    public bool IsSkillCheckEnabled()
    {
        return active;
    }

    public void ResetSkillCheck()
    {
        //curPlayerMarkPos = Random.Range(0f, 100f) / 100f;
        time = 0;
        movingRight = Random.Range(0, 2) == 0;
        EnableSkillCheck(true);
    }

    // Update is called once per frame
    void Update()
    {
        if (!active)
            return;

        //Check if we need to move the other direction
        if (curPlayerMarkPos >= 1 && movingRight || curPlayerMarkPos <= 0 && !movingRight)
        {
            movingRight = !movingRight;
            time = 0;
        }

        //Calculate new position
        float t = time / duration;
        if (movingRight)
        {
            if (time > duration)
                curPlayerMarkPos = 1;
            else
                curPlayerMarkPos = Mathf.SmoothStep(0, 1, t);
        }
        else
        {
            if (time > duration)
                curPlayerMarkPos = 0;
            else
                curPlayerMarkPos = Mathf.SmoothStep(1, 0, t);
        }
        time += Time.deltaTime;
        float pos = Mathf.Lerp(-skillCheckWidth, skillCheckWidth, curPlayerMarkPos);
        playerMark.transform.localPosition = new Vector3(pos, 0, 0);
    }

    public SkillCheck GetSkillCheck()
    {
        //from 0 - 1 total
        //5% chance
        if (IsBetween(.475f, .525f, curPlayerMarkPos))
            return SkillCheck.Perfect;
        //24% chance
        else if(IsBetween(.38f, .62f, curPlayerMarkPos))
            return SkillCheck.Good;
        //60% chance
        else if (IsBetween(.2f, .8f, curPlayerMarkPos))
            return SkillCheck.Average;
        //remaining
        else
            return SkillCheck.Poor;
    }

    public bool IsBetween(float min, float max, float number)
    {
        return number > min && number < max;
    }

    public enum SkillCheck
    {
        Perfect,
        Good,
        Average,
        Poor
    }
}
