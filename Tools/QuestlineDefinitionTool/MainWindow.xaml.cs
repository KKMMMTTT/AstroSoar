using Game.Questlines;
using Microsoft.WindowsAPICodePack.Dialogs;
using System.IO;
using System.Windows;
using System.Windows.Controls;

namespace QuestlineDefinitionTool
{
    public partial class MainWindow : Window
    {
        public MainWindow() {
            InitializeComponent();

            this.lstQuestlineDefinitions.MouseDoubleClick += this.LstQuestlineDefinitions_MouseDoubleClick;
        }

        private void LstQuestlineDefinitions_MouseDoubleClick(object sender, System.Windows.Input.MouseButtonEventArgs e) {
            (this.lstQuestlineDefinitions.SelectedItem as QuestlineDefinitionListboxItem)?.OnClick();
        }

        private void RefreshQuestlineDefinitionList(int? newSelectedIndex = null) {
            this.lstQuestlineDefinitions.Items.Clear();

            foreach (var item in Globals.QuestlineDefinitions) {
                this.lstQuestlineDefinitions.Items.Add(new QuestlineDefinitionListboxItem(item));
            }

            if (newSelectedIndex != null) {
                this.lstQuestlineDefinitions.SelectedIndex = (int)newSelectedIndex;
            }
        }

        private void mnuSetDefinitionsPath_Click(object sender, RoutedEventArgs e) {
            var filedialog = new CommonOpenFileDialog();
            filedialog.Title = "Select the Questline Definition folder";
            filedialog.Multiselect = false;
            filedialog.IsFolderPicker = true;

            if (filedialog.ShowDialog() != CommonFileDialogResult.Ok) {
                return;
            }
            if (!Directory.Exists(filedialog.FileName)) {
                MessageBox.Show($"The directory {filedialog.FileName} does not exist!", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            Globals.DefinitionPath = filedialog.FileName;
        }

        private void RemoveClick(object sender, RoutedEventArgs e) {
            if (MessageBox.Show("Are you sure you want to remove this questline definition?", "Confirm Remove", MessageBoxButton.YesNo, MessageBoxImage.Warning) == MessageBoxResult.No) {
                return;
            }
            int index = this.lstQuestlineDefinitions.SelectedIndex;

            if (index >= 0 && index < this.lstQuestlineDefinitions.Items.Count) {
                Globals.QuestlineDefinitions.RemoveAt(index);
                RefreshQuestlineDefinitionList(-1);
            }
        }

        private void NewDefinition(object sender, RoutedEventArgs e) {
            var newDefinition = new QuestlineDefinition() { Name = "New Questline" };
            Globals.QuestlineDefinitions.Add(newDefinition);
            RefreshQuestlineDefinitionList(Globals.QuestlineDefinitions.Count - 1);
        }

        private void SaveAll(object sender, RoutedEventArgs e) {

        }

        public class QuestlineDefinitionListboxItem : Label
        {
            private QuestlineDefinition Definition;

            public QuestlineDefinitionListboxItem(QuestlineDefinition definition) {
                this.Definition = definition;
                this.Content = definition.Name;
            }

            public void OnClick() {
                Globals.MainWindow.Hide();
                new EditorPage(this.Definition).ShowDialog();
                Globals.MainWindow.RefreshQuestlineDefinitionList();
                Globals.MainWindow.ShowDialog();
            }
        }
    }
}
