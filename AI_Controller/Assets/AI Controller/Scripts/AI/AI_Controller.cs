using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class AI_Controller : MonoBehaviour
{
    private AI_PresetsSave _aiPresetsSave;
    private NavMeshAgent _agent;

    [HideInInspector] public int _presetIndex;
    [HideInInspector] public int _agentState;//0=inspect, 1=patrol, 2=chase, 3=attack

    //patrol variable
    public AI_Points _pointsList;
    private int patrolType;// 0=Cirlcular, 1=Reverse, 2=Random
    private bool patrolReverse = false;
    [HideInInspector] public int pointIndex;
    private float idleTime;
    private float idleCounter;


    private void Start()
    {
        _aiPresetsSave = Resources.Load<AI_PresetsSave>("ScriptObjects/AI_PresetsSave");
        _agent = GetComponent<NavMeshAgent>();

        //Check if agent can patrol
        if (_aiPresetsSave.agentPatrol[_presetIndex] == true)
        {
            patrolType = _aiPresetsSave.agentPatrolType[_presetIndex];//get patrol type
            pointIndex = _pointsList.GetNearestPointIndex(transform.position);//get nearest point index
        }
    }

    private void Update()
    {
        //Agent Patrol
        if (_aiPresetsSave.agentPatrol[_presetIndex])
            Patrol();
    }


    private void Patrol() 
    {
        if (_agentState == 0)
        {
            idleCounter += Time.deltaTime;

            if (idleCounter >= idleTime)
            {
                idleCounter = 0;//reset idle counter
                _agentState = 1;//set agent to patrol state

                PatrolNextPoint();
            }
        }
        else if (_agentState == 1)
        {
            if (!_agent.pathPending && _agent.remainingDistance < _agent.stoppingDistance)
            {
                _agentState = 0;//set agent to idle state
                idleTime = Random.Range(_aiPresetsSave.agentMinDelay[_presetIndex], _aiPresetsSave.agentMaxDelay[_presetIndex]);// get random patrol delay time
            }
        }

    }

    private void PatrolNextPoint()
    {
        // Returns if no points have been set up
        if (_pointsList._points.Count == 0)
            return;

        //patrol type
        if (patrolType == 0)//circular
        {
            //set agent target destination
            _agent.destination = _pointsList._points[pointIndex].position;
            pointIndex++;

            //cicle patrol
            if (pointIndex > _pointsList._points.Count - 1)
                pointIndex = 0;
        }
        else if (patrolType == 1)//reverse
        {
            if (!patrolReverse)
            {
                //set agent target destination
                _agent.destination = _pointsList._points[pointIndex].position;
                pointIndex++;

                //reverse patrol
                if (pointIndex > _pointsList._points.Count - 2)
                    patrolReverse = true;
            }
            else
            {
                //set agent target destination
                _agent.destination = _pointsList._points[pointIndex].position;
                pointIndex--;

                //reverse patrol
                if (pointIndex < 1)
                    patrolReverse = false;
            }
        }
        else if (patrolType == 2)//random
        {
            //random index
            int rand = Random.Range(0, _pointsList._points.Count);

            while (rand == pointIndex)
                rand = Random.Range(0, _pointsList._points.Count);

            pointIndex = rand;

            //set agent target destination
            _agent.destination = _pointsList._points[pointIndex].position;
        }
    }
}
