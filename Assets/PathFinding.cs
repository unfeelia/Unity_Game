﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;
public struct Node
{
    public Transform node;
    public float fScore;
    public float gScore;
    public int index;
}
public class PathFinding : MonoBehaviour {
    public List<Transform> closedSet = new List<Transform>();
    public List<Transform> openSet = new List<Transform>();
    public List<Transform> result;
    public GameObject NodeMap;
    private Transform[] all;
    public List<Transform> neighbor;
    private List<Transform> added_neighbor;
    private bool already_added;
    public Transform goal;
    private Node current;
    private float min_fScore;
    private int min_num;
    private bool found = false;
    public Transform[] cameFrom = new Transform[30];
    void Start() {
        all = new Transform[30];
        neighbor = new List<Transform>();
        added_neighbor = new List<Transform>();
        for (int i = 0; i < 30; i++)
        {
            all[i] = NodeMap.gameObject.transform.GetChild(i);
            //Debug.Log(all[i].name);
        }
        already_added = false;
    }

    void Update() {
        if(found == false)
        {
            found = Astar();
        }
    }
    bool Astar()
    {
        found = true;
        int c = -1;
        float tentative_gScore;
        Node[] node_list = new Node[30];
        for(int i = 0; i < 30; i++)
        {
            node_list[i].node = all[i];
            node_list[i].index = i;
        }
        goal = node_list[26].node;
        List<Node> nan;
        nan = new List<Node>();
        openSet.Add(all[0]);
        
        //List<float> gScore = new List<float>();
        //gScore.Add(0);
        //List<float> fScore = new List<float>();
        //fScore.Add(Vector2.Distance(all[0].position, goal.position));
        node_list[0].fScore = Vector2.Distance(all[0].position, goal.position);
        node_list[0].gScore = 0;
        while(openSet.Count != 0)
        {
            //Debug.Log(openSet.Count);
            for (int j = 0; j < 30; j++)
            {
                if (node_list[j].node.name == openSet[0].name)
                {
                     min_fScore = node_list[j].fScore;
                }
            }
            for (int i = 0; i < openSet.Count; i++)
            {
                for(int j = 0; j < 30; j++)
                {
                    if(node_list[j].node == openSet[i])
                    {
                        if(node_list[j].fScore <= min_fScore && openSet.Contains(node_list[j].node))
                        {
                            min_fScore = node_list[j].fScore;
                            min_num = node_list[j].index;
                        }
                    }
                }
            }
            current.node = node_list[min_num].node;
            current.index = min_num;
            //Debug.Log(current);
            if (current.node == goal)
            {
                //cameFrom = smooth(cameFrom);
                Debug.Log("Route Found");
                result = reconstruct(cameFrom, current);
                return true;
            }
            openSet.Remove(current.node);
            closedSet.Add(current.node);
            neighbor.Clear();
            neighbors(current.node);
            //Debug.Log('!');
            nan.Clear();
            for (int i = 0; i < neighbor.Count; i++)
            {
                for(int j = 0; j < 30; j++)
                {
                    if(node_list[j].node == neighbor[i])
                    {
                        nan.Add(node_list[j]);
                    }
                }
            }
            c++;
            for (int i = 0; i < neighbor.Count; i++)
            {
                if(closedSet.Contains(nan[i].node))
                {
                    continue;
                }
                tentative_gScore = node_list[min_num].gScore + Vector2.Distance(current.node.position, goal.position);
                if(!openSet.Contains(nan[i].node))
                {
                    openSet.Add(nan[i].node);
                    Debug.Log('!');
                }
                else if(tentative_gScore >= nan[i].gScore)
                {
                    continue;
                }
                //Debug.Log('!');
                cameFrom[c] = current.node;
                node_list[nan[i].index].gScore = tentative_gScore;
                node_list[nan[i].index].fScore = node_list[nan[i].index].gScore + Vector2.Distance(nan[i].node.position, goal.position);
                //Debug.Log(node_list[nan[i].index].fScore);
            }
        }
        return false;
    }
    void neighbors(Transform node)
    {
        added_neighbor.Clear();
        neighbor.Clear();
        for (int i = 0; i < 30; i++)
        {
            if(node.position.x + 1 == all[i].position.x && node.position.y == all[i].position.y)
            {
                for(int j = 0; j < added_neighbor.Count; j++)
                {
                    if(all[i] == added_neighbor[j])
                    {
                        already_added = true;
                    }
                }
                if (already_added == false)
                {
                    neighbor.Add(all[i]);
                    added_neighbor.Add(all[i]);
                    //Debug.Log(all[i]);
                }
            }
            already_added = false;
            //Debug.Log(node.position.x + 1);
            //Debug.Log(node.position.y + 1);
            if (node.position.x + 1 == all[i].position.x && node.position.y + 1 == all[i].position.y)
            {
                for (int j = 0; j < added_neighbor.Count; j++)
                {
                    if (all[i] == added_neighbor[j])
                    {
                        already_added = true;
                    }
                }
                if (already_added == false)
                {
                    //Debug.Log(all[i]);
                    neighbor.Add(all[i]);
                    added_neighbor.Add(all[i]);
                }
            }
            already_added = false;
            if (node.position.x + 1 == all[i].position.x && node.position.y - 1 == all[i].position.y)
            {
                for (int j = 0; j < added_neighbor.Count; j++)
                {
                    if (all[i] == added_neighbor[j])
                    {
                        already_added = true;
                    }
                }
                if (already_added == false)
                {
                   // Debug.Log(all[i]);
                    neighbor.Add(all[i]);
                    added_neighbor.Add(all[i]);
                }
            }
            already_added = false;
            if (node.position.x  == all[i].position.x && node.position.y + 1 == all[i].position.y)
            {
                for (int j = 0; j < added_neighbor.Count; j++)
                {
                    if (all[i] == added_neighbor[j])
                    {
                        already_added = true;
                    }
                }
                if (already_added == false)
                {
                    //Debug.Log(all[i]);
                    neighbor.Add(all[i]);
                    added_neighbor.Add(all[i]);
                }
            }
            already_added = false;
            if (node.position.x == all[i].position.x && node.position.y - 1 == all[i].position.y)
            {
                for (int j = 0; j < added_neighbor.Count; j++)
                {
                    if (all[i] == added_neighbor[j])
                    {
                        already_added = true;
                    }
                }
                if (already_added == false)
                {
                   // Debug.Log(all[i]);
                    neighbor.Add(all[i]);
                    added_neighbor.Add(all[i]);
                }
            }
            already_added = false;
            if (node.position.x - 1 == all[i].position.x && node.position.y == all[i].position.y)
            {
                for (int j = 0; j < added_neighbor.Count; j++)
                {
                    if (all[i] == added_neighbor[j])
                    {
                        already_added = true;
                    }
                }
                if (already_added == false)
                {
                   // Debug.Log(all[i]);
                    neighbor.Add(all[i]);
                    added_neighbor.Add(all[i]);
                }
            }
            already_added = false;
            if (node.position.x - 1 == all[i].position.x && node.position.y + 1 == all[i].position.y)
            {
                for (int j = 0; j < added_neighbor.Count; j++)
                {
                    if (all[i] == added_neighbor[j])
                    {
                        already_added = true;
                    }
                }
                if (already_added == false)
                {
                    //Debug.Log(all[i]);
                    neighbor.Add(all[i]);
                    added_neighbor.Add(all[i]);
                }
            }
            already_added = false;
            if (node.position.x - 1 == all[i].position.x && node.position.y - 1 == all[i].position.y)
            {
                for (int j = 0; j < added_neighbor.Count; j++)
                {
                    if (all[i] == added_neighbor[j])
                    {
                        already_added = true;
                    }
                }
                if (already_added == false)
                {
                    //Debug.Log(all[i]);
                    neighbor.Add(all[i]);
                    added_neighbor.Add(all[i]);
                }
            }
            already_added = false;
        }
    }
    Transform[] smooth(Transform[] map)
    {
        neighbor.Clear();
        for(int i = 0; i < map.Length; i++)
        {
            if(i < map.Length - 1 )
            {
                neighbors(map[i]);
                if(neighbor.Contains(map[i+1]))
                {
                    map[i] = null;
                }
            }
        }
        return map;
    }
    List<Transform> reconstruct(Transform[] came, Node cur)
    {
        int c = 0;
        List<Transform> TotalPath = new List<Transform>();
        TotalPath.Add(cur.node);
        while (check(came,cur.node))
        {
            cur.node = cameFrom[cur.index];
            TotalPath.Add(cur.node);
        }
        return TotalPath;
    }
    bool check(Transform[] came, Transform cur)
    {
        for(int i = 0; i < came.Length; i++)
        {
            if(came[i] == cur)
            {
                return true;
            }
        }
        return false;
    }
    int check_num(Transform[] came, Transform cur)
    {
        for (int i = 0; i < came.Length; i++)
        {
            if (came[i] == cur)
            {
                return i;
            }
        }
        return 100;
    }
}
