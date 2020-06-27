using Annex;
using Annex.Logging;
using DefinitionTool.Conversations;
using DefinitionTool.Questlines;
using System.Windows;

namespace DefinitionTool
{
    public partial class MainWindow : Window
    {
        public MainWindow() {
            InitializeComponent();

            ServiceProvider.Provide(new Log());
        }

        private void Questlines_MouseDoubleClick(object sender, System.Windows.Input.MouseButtonEventArgs e) {
            Globals.MainWindow.Hide();

            Globals.QuestlineEditor = new MainDefinitionEditor(new QuestlineMiddleMan());
            Globals.QuestlineEditor.ShowDialog();

            Globals.MainWindow.ShowDialog();
        }

        private void Conversations_MouseDoubleClick(object sender, System.Windows.Input.MouseButtonEventArgs e) {
            Globals.MainWindow.Hide();

            Globals.ConversationEditor = new MainDefinitionEditor(new ConversationMiddleMan());
            Globals.ConversationEditor.ShowDialog();

            Globals.MainWindow.ShowDialog();
        }
    }
}
