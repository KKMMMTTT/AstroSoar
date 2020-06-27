using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;

namespace DefinitionTool
{
    /// <summary>
    /// Interaction logic for MainDefinitionEditor.xaml
    /// </summary>
    public partial class MainDefinitionEditor : Window
    {
        private IDefinitionEditorMiddleMan _mm;

        public MainDefinitionEditor(IDefinitionEditorMiddleMan mm) {
            InitializeComponent();
            this._mm = mm;

            this.lstDefinitions.MouseDoubleClick += this.LstDefinitions_MouseDoubleClick;
            this.Closing += (sender, e) => mm.SaveAll();

            this._mm.LoadAll();
            this.RefreshDefinitionList();
        }

        public void RefreshDefinitionList(int? newSelectedIndex = null) {
            this.lstDefinitions.Items.Clear();

            foreach (var definition in this._mm.Definitions) {
                this.lstDefinitions.Items.Add(new DefinitionListboxItem(this._mm, definition));
            }

            if (newSelectedIndex != null) {
                this.lstDefinitions.SelectedIndex = (int)newSelectedIndex;
            }
        }

        private void NewDefinition(object sender, RoutedEventArgs e) {
            this._mm.AddNew();
            RefreshDefinitionList(this.lstDefinitions.Items.Count - 1);
        }

        private void LstDefinitions_MouseDoubleClick(object sender, System.Windows.Input.MouseButtonEventArgs e) {
            this.Hide();
            this._mm.BeginEdit((this.lstDefinitions.SelectedItem as DefinitionListboxItem)!.Definition);
            this.RefreshDefinitionList();
            this.ShowDialog();
        }

        private void ConversationEditor_Closing(object sender, System.ComponentModel.CancelEventArgs e) {
            throw new System.NotImplementedException();
        }
    }

    public class DefinitionListboxItem : Label
    {
        public object Definition;

        public DefinitionListboxItem(IDefinitionEditorMiddleMan mm, object definition) {
            this.Definition = definition;
            this.Content = mm.GetLabelFor(this.Definition);
        }
    }

    public interface IDefinitionEditorMiddleMan
    {
        IEnumerable<object> Definitions { get; }

        void LoadAll();
        void BeginEdit(object definition);
        object GetLabelFor(object definition);
        void SaveAll();
        void AddNew();
    }
}
