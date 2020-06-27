using Game.Definitions.Questlines;
using System;
using System.Windows;
using System.Windows.Controls;

namespace DefinitionTool.Questlines
{
    /// <summary>
    /// Interaction logic for EditorPage.xaml
    /// </summary>
    public partial class EditorPage : Window
    {
        private StepDefinition CurrentlySelectedStep => this.lstSteps.SelectedIndex == -1 ? null : this.Steps[this.lstSteps.SelectedIndex];
        private QuestlineDefinition QuestlineDefinition;
        private StepDefinition[] Steps {
            get => this.QuestlineDefinition.Steps;
            set => this.QuestlineDefinition.Steps = value;
        }

        public EditorPage(QuestlineDefinition questlineDefinition) {
            InitializeComponent();
            this.QuestlineDefinition = questlineDefinition;

            if (this.QuestlineDefinition.Steps == null) {
                this.QuestlineDefinition.Steps = new StepDefinition[0];
            }

            this.txtName.Text = questlineDefinition.Name;
            this.txtDescription.Text = questlineDefinition.Description;

            RefreshStepList();
        }

        private void LstSteps_SelectionChanged(object sender, SelectionChangedEventArgs e) {
            var step = this.CurrentlySelectedStep;
            if (step == null) {
                return;
            }
            this.txtStepDescription.Text = step.Description;

            if (this.CurrentlySelectedStep != null) {
                this.grdStepEditor.IsEnabled = true;
            } else {
                this.grdStepEditor.IsEnabled = false;
            }

            this.grdTasks.ItemsSource = this.CurrentlySelectedStep.Task;
        }

        private void RefreshStepList() {
            this.lstSteps.Items.Clear();

            for (int i = 0; i < this.Steps.Length; i++) {
                this.lstSteps.Items.Add(new StepListboxItem(this.Steps[i], i));
            }

            if (this.CurrentlySelectedStep != null) {
                this.grdStepEditor.IsEnabled = true;
            } else {
                this.grdStepEditor.IsEnabled = false;
            }
        }

        private void txtName_TextChanged(object sender, TextChangedEventArgs e) {
            QuestlineDefinition.Name = txtName.Text;
        }

        private void txtDescription_TextChanged(object sender, TextChangedEventArgs e) {
            QuestlineDefinition.Description = txtDescription.Text;
        }

        private void txtStepDescription_TextChanged(object sender, TextChangedEventArgs e) {
            var step = this.CurrentlySelectedStep;
            if (step == null) {
                return;
            }
            step.Description = this.txtStepDescription.Text;
            (this.lstSteps.SelectedItem as StepListboxItem).RefreshLabel(this.lstSteps.SelectedIndex);
        }

        private void cmdAdd_Click(object sender, RoutedEventArgs e) {
            var newSteps = new StepDefinition[this.Steps.Length + 1];
            for (int i = 0; i < this.Steps.Length; i++) {
                newSteps[i] = this.Steps[i];
            }
            newSteps[^1] = new StepDefinition() { Task = new TaskDefinition[0] };
            this.QuestlineDefinition.Steps = newSteps;
            RefreshStepList();
        }

        private void cmdRemove_Click(object sender, RoutedEventArgs e) {
            if (this.CurrentlySelectedStep == null) {
                return;
            }

            if (MessageBox.Show("Are you sure you want to delete this step?", "Delete Step", MessageBoxButton.YesNo, MessageBoxImage.Warning) != MessageBoxResult.Yes) {
                return;
            }

            var newSteps = new StepDefinition[Math.Max(this.Steps.Length - 1, 0)];
            int bad = this.lstSteps.SelectedIndex;
            int insertIndex = 0;

            for (int i = 0; i < this.Steps.Length; i++) {
                if (i == bad) {
                    continue;
                }
                newSteps[insertIndex++] = this.Steps[i];
            }
            this.QuestlineDefinition.Steps = newSteps;
            this.RefreshStepList();
        }

        private void cmdShiftUp_Click(object sender, RoutedEventArgs e) {
            if (this.CurrentlySelectedStep == null) {
                return;
            }

            int source = this.lstSteps.SelectedIndex;
            int destination = Math.Max(this.lstSteps.SelectedIndex - 1, 0);

            var temp = this.Steps[destination];
            this.Steps[destination] = this.Steps[source];
            this.Steps[source] = temp;

            RefreshStepList();
            this.lstSteps.SelectedIndex = destination;
        }

        private void cmdShiftDown_Click(object sender, RoutedEventArgs e) {
            if (this.CurrentlySelectedStep == null) {
                return;
            }

            int source = this.lstSteps.SelectedIndex;
            int destination = Math.Min(this.lstSteps.SelectedIndex + 1, this.lstSteps.Items.Count - 1);

            var temp = this.Steps[destination];
            this.Steps[destination] = this.Steps[source];
            this.Steps[source] = temp;

            RefreshStepList();
            this.lstSteps.SelectedIndex = destination;
        }

        private void AddTask(object sender, RoutedEventArgs e) {
            var newTasks = new TaskDefinition[this.CurrentlySelectedStep.Task.Length + 1];
            for (int i = 0; i < this.CurrentlySelectedStep.Task.Length; i++) {
                newTasks[i] = this.CurrentlySelectedStep.Task[i];
            }
            newTasks[^1] = new TaskDefinition();

            this.CurrentlySelectedStep.Task = newTasks;
            this.grdTasks.ItemsSource = newTasks;
        }

        private void RemoveTask(object sender, RoutedEventArgs e) {

            int insertIndex = 0;
            var newTasks = new TaskDefinition[this.CurrentlySelectedStep.Task.Length - this.grdTasks.SelectedItems.Count];

            foreach (var task in this.CurrentlySelectedStep.Task) {
                if (this.grdTasks.SelectedItems.Contains(task)) {
                    continue;
                }
                newTasks[insertIndex++] = task;
            }

            this.CurrentlySelectedStep.Task = newTasks;
            this.grdTasks.ItemsSource = newTasks;
        }

        public class StepListboxItem : Label
        {
            public StepDefinition StepDefinition;

            public StepListboxItem(StepDefinition stepDefinition, int index) {
                this.StepDefinition = stepDefinition;
                this.RefreshLabel(index);
            }

            public void RefreshLabel(int index) {
                this.Content = $"{index}: {StepDefinition.Description}";
            }
        }
    }
}
