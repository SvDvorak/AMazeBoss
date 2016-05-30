﻿using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Assets.LevelEditorUnity
{
    [ExecuteInEditMode]
    public class PuzzleLayoutView : MonoBehaviour
    {
        public GameObject Connector;
        public GameObject Node;

        public Dictionary<TilePos, GameObject> NodeViews = new Dictionary<TilePos, GameObject>();
        public Dictionary<NodeConnection, GameObject> NodeConnectionViews = new Dictionary<NodeConnection, GameObject>();

        private PuzzleLayout PuzzleLayout { get { return PuzzleLayout.Instance; } }

        private void LoadLevelStateFromScene()
        {
            var connectors = gameObject.GetChildren("Connector", true).Select(x => x.transform);

            foreach (var connector in connectors)
            {
                var start = connector.localPosition;
                var end = start + connector.rotation*Vector3.forward*TilePos.TileLength;
                var nodeConnection = new NodeConnection(new TilePos(start), new TilePos(end));
                PuzzleLayout.Instance.AddNodeConnection(nodeConnection);
            }
        }

        public void OnEnable()
        {
            LoadLevelStateFromScene();
            PuzzleLayout.NodeAdded += AddNode;
            PuzzleLayout.NodeRemoved += RemoveNode;
            PuzzleLayout.ConnectionAdded += AddNodeConnection;
            PuzzleLayout.ConnectionRemoved += RemoveConnection;
        }

        public void OnDisable()
        {
            PuzzleLayout.NodeAdded -= AddNode;
            PuzzleLayout.NodeRemoved -= RemoveNode;
            PuzzleLayout.ConnectionAdded -= AddNodeConnection;
            PuzzleLayout.ConnectionRemoved -= RemoveConnection;
        }

        private void AddNode(Node node)
        {
            if (!NodeViews.ContainsKey(node.Position))
            {
                var nodeView = (GameObject)Instantiate(
                    Node,
                    node.Position.ToV3(),
                    Quaternion.identity);
                nodeView.transform.SetParent(transform);
                NodeViews.Add(node.Position, nodeView);
            }
        }

        private void RemoveNode(Node node)
        {
            if (NodeViews.ContainsKey(node.Position))
            {
                DestroyImmediate(NodeViews[node.Position]);
                NodeViews.Remove(node.Position);
            }
        }

        public void AddNodeConnection(NodeConnection connection)
        {
            if (!NodeConnectionViews.ContainsKey(connection))
            {
                var connectionView = (GameObject)Instantiate(
                    Connector,
                    connection.Start.ToV3(),
                    Quaternion.FromToRotation(Vector3.forward, (connection.End - connection.Start).ToV3()));
                connectionView.transform.SetParent(transform);
                NodeConnectionViews.Add(connection, connectionView);
            }
        }

        public void RemoveConnection(NodeConnection connection)
        {
            if (NodeConnectionViews.ContainsKey(connection))
            {
                DestroyImmediate(NodeConnectionViews[connection]);
                NodeConnectionViews.Remove(connection);
            }
        }
    }
}