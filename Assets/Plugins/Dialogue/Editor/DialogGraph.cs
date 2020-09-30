using System;
using UnityEditor;
using UnityEditor.UIElements;
using UnityEngine;
using UnityEngine.UIElements;

namespace Dialogue.Editor
{
    public class DialogueGraph : EditorWindow
    {
        private DialogueGraphView _graphView;
        private string _filename = "New Narrative";

        [MenuItem("Dialogue Graph/Open Graph Editor")]
        public static void OpenDialogueEditor()
        {
            var window = GetWindow<DialogueGraph>();
            window.titleContent = new GUIContent("Dialogue Graph");
        }

        private void OnEnable()
        {
            ConstructGraphView();
            GenerateToolbar();
        }

        private void ConstructGraphView()
        {
            _graphView = new DialogueGraphView {name = "Dialogue Graph"};
            _graphView.StretchToParentSize();
            rootVisualElement.Add(_graphView);
        }

        private void GenerateToolbar()
        {
            var toolbar = new Toolbar();
            
            var fileNameTextField = new TextField("File Name: ");
            fileNameTextField.SetValueWithoutNotify(_filename);
            fileNameTextField.MarkDirtyRepaint();
            fileNameTextField.RegisterValueChangedCallback(evt => _filename = evt.newValue);
            toolbar.Add(fileNameTextField);
            
            toolbar.Add(new Button(SaveData){text = "Save Data"});
            toolbar.Add(new Button(LoadData){text = "Load Data"});

            var nodeCreateButton = new Button(() => 
                _graphView.CreateNode("Dialogue Node")) {text = "Create Node"};
            toolbar.Add(nodeCreateButton);
            
            rootVisualElement.Add(toolbar);
        }

        private void LoadData()
        {
            if (string.IsNullOrEmpty(_filename))
            {
                EditorUtility.DisplayDialog("Invalid filename!", "Please enter a valid filename.", "OK");
                return;
            }

            var saveUtility = GraphSaveUtility.GetInstance(_graphView);
            saveUtility.LoadGraph(_filename);
        }

        private void SaveData()
        {
            
        }

        private void OnDisable()
        {
            rootVisualElement.Remove(_graphView);
        }


    }
}
