using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Serialization;

public class Wave : MonoBehaviour
{
    public SpawnInstruction[] spawnInstructions;
    private int m_CurrentIndex;
    private NodePath m_Path;
    public event Action completed;

    public float progress => (float)m_CurrentIndex / spawnInstructions.Length;

    public void StartWave(NodePath path)
    {
        m_Path = path;
        m_CurrentIndex = 0;
        StartCoroutine(SpawnWave());
    }
    
    IEnumerator SpawnWave()
    {
        while (m_CurrentIndex < spawnInstructions.Length)
        {
            var instruction = spawnInstructions[m_CurrentIndex];
            SpawnAgent(instruction.spawnPrefab, m_Path.GetNode(instruction.startNode));
            m_CurrentIndex++;
            yield return new WaitForSeconds(instruction.delay);
        }
    }
    
    protected virtual void SpawnAgent(GameObject agent, Node node)
    {
        Vector3 spawnPosition = node.transform.position;
        var agentInstance = GameObject.Instantiate(agent);
        agentInstance.transform.position = spawnPosition;
        agentInstance.GetComponent<Enemy>().Path = m_Path;
    }
}