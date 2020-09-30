using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Dialogue.Editor;
using NUnit.Framework;
using Sirenix.Utilities;
using UnityEditor;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class GraphSaveUtility
{
    private DialogueGraphView _targetGraphView;
    private DialogueContainer _containerCache;

    private List<Edge> Edges => _targetGraphView.edges.ToList();
    private List<DialogueNode> DialogueNodes => _targetGraphView.nodes.ToList().Cast<DialogueNode>().ToList();

    public static GraphSaveUtility GetInstance(DialogueGraphView targetGraphView)
    {
        return new GraphSaveUtility
        {
            _targetGraphView = targetGraphView
        };
    }

    public void SaveGraph(string filename)
    {
        if (!Edges.Any())return;
        var dialogueContainer = ScriptableObject.CreateInstance<DialogueContainer>();
        var connectedPorts = Edges.Where(edge => edge.input.node != null).ToArray();

        connectedPorts.ForEach(port =>
        {
            var input = port.input.node as DialogueNode;
            var output = port.output.node as DialogueNode;
            
            dialogueContainer.NodeLinks.Add(new NodeLinkData
            {
                BaseNodeGUID = output.GUID,
                PortName = port.output.portName,
                TargetNodeGUID = input.GUID
            });
        });

        foreach (var dialogueNode in DialogueNodes.Where(node => !node.EntryPoint))
        {
            dialogueContainer.DialogueNodes.Add(new DialogueNodeData()
            {
                NodeGUID = dialogueNode.GUID,
                DialogueText = dialogueNode.DialogueText,
                NodePos = dialogueNode.GetPosition().position
            });
        }

        if (AssetDatabase.IsValidFolder("Assets/Resources"))
        {
            AssetDatabase.CreateFolder("Assets", "Resources");
        }
        AssetDatabase.CreateAsset(dialogueContainer, $"Assets/Resources/{filename}.asset");
        AssetDatabase.SaveAssets();

    }

    public void LoadGraph(string filename)
    {
        _containerCache = Resources.Load<DialogueContainer>(filename);

        if (_containerCache == null)
        {
            EditorUtility.DisplayDialog("File Not Found", "The file does not exist", "OK");
            return;
        }

        ClearGraph();
        CreateNodes();
        //ConnectNodes();
    }

    private void CreateNodes()
    {
        foreach (var nodeData in _containerCache.DialogueNodes)
        {
            var tempNode = _targetGraphView.CreateDialogueNode(nodeData.DialogueText);
            tempNode.GUID = nodeData.NodeGUID;
            _targetGraphView.AddElement(tempNode);

            var nodePorts = _containerCache.NodeLinks.Where(
                x => x.BaseNodeGUID == nodeData.NodeGUID).ToList();
            nodePorts.ForEach(x => _targetGraphView.AddChoicePort(tempNode, x.PortName));
        }
    }

    private void ClearGraph()
    {
        DialogueNodes.Find(x => x.EntryPoint).GUID = _containerCache.NodeLinks[0].BaseNodeGUID;
        foreach (var node in DialogueNodes.Where(node => !node.EntryPoint))
        {
            Edges.Where(x => x.input.node == node).ToList().ForEach(edge => _targetGraphView.RemoveElement(edge));
            _targetGraphView.RemoveElement(node);
        }
    }
}
