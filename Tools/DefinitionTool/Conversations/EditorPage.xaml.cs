using Game.Definitions.Conversations;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;

namespace DefinitionTool.Conversations
{
    /// <summary>
    /// Interaction logic for EditorPage.xaml
    /// </summary>
    public partial class EditorPage : Window
    {
        private readonly ConversationDefinition Definition;
        public List<ConversationPage> Pages => Definition.ConversationPages;
        private ConversationPage CurrentlySelectedPage => this.lstReplyOptions.SelectedIndex == -1 ? null : this.Pages[this.lstReplyOptions.SelectedIndex];

        public EditorPage(ConversationDefinition definition) {
            InitializeComponent();

            this.Definition = definition;

            this.txtName.Text = Definition.Name;

            RefreshList();
        }

        private void RefreshList() {
            this.lstReplyOptions.Items.Clear();

            for (int i = 0; i < this.Pages.Count; i++) {
                this.lstReplyOptions.Items.Add(new PageListboxItem(this.Pages[i], i));
            }

            if (this.CurrentlySelectedPage != null) {
                this.grdEditor.IsEnabled = true;
            } else {
                this.grdEditor.IsEnabled = false;
            }
        }

        private void txtName_TextChanged(object sender, TextChangedEventArgs e) {
            this.Definition.Name = txtName.Text;
        }

        private void lstReplyOptions_SelectionChanged(object sender, SelectionChangedEventArgs e) {
            var page = this.CurrentlySelectedPage;
            if (page == null) {
                return;
            }

            this.txtDisplayBoxTexture.Text = page.DisplayBoxTexture;
            this.txtDisplayText.Text = page.DisplayText;

            if (this.CurrentlySelectedPage != null) {
                this.grdEditor.IsEnabled = true;
            } else {
                this.grdEditor.IsEnabled = false;
            }

            this.grdReplyOptions.ItemsSource = this.CurrentlySelectedPage.ReplyOptions.ToArray();
        }
    
        private void cmdAdd_Click(object sender, RoutedEventArgs e) {
            this.Pages.Add(new ConversationPage());
            RefreshList();
        }

        private void cmdRemove_Click(object sender, RoutedEventArgs e) {
            if (this.CurrentlySelectedPage == null) {
                return;
            }

            if (MessageBox.Show("Are you sure you want to delete this page?", "Delete Page", MessageBoxButton.YesNo, MessageBoxImage.Warning) != MessageBoxResult.Yes) {
                return;
            }

            this.Pages.RemoveAt(this.lstReplyOptions.SelectedIndex);
            this.RefreshList();
        }

        private void AddPage(object sender, RoutedEventArgs e) {
            this.CurrentlySelectedPage.ReplyOptions.Add(new ReplyOption());
            this.grdReplyOptions.ItemsSource = null;
            this.grdReplyOptions.ItemsSource = this.CurrentlySelectedPage.ReplyOptions.ToArray();
        }

        private void RemovePage(object sender, RoutedEventArgs e) {
            foreach (var item in this.grdReplyOptions.SelectedItems) {
                this.CurrentlySelectedPage.ReplyOptions.Remove(item as ReplyOption);
            }
            this.grdReplyOptions.ItemsSource = null;
            this.grdReplyOptions.ItemsSource = this.CurrentlySelectedPage.ReplyOptions.ToArray();
        }

        private void txtDisplayText_TextChanged(object sender, TextChangedEventArgs e) {
            this.CurrentlySelectedPage.DisplayText = txtDisplayText.Text;
            (this.lstReplyOptions.SelectedItem as PageListboxItem).RefreshLabel(this.lstReplyOptions.SelectedIndex);
        }

        private void txtDisplayBoxTexture_TextChanged(object sender, TextChangedEventArgs e) {
            this.CurrentlySelectedPage.DisplayBoxTexture = txtDisplayBoxTexture.Text;
        }
    }

    public class PageListboxItem : Label
    {
        public ConversationPage Page;

        public PageListboxItem(ConversationPage page, int index) {
            this.Page = page;
            this.RefreshLabel(index);
        }

        public void RefreshLabel(int index) {
            this.Content = $"{index}: {Page.DisplayText}";
        }
    }
}
