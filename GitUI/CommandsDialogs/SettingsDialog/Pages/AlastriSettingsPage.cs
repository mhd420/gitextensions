using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using Atlassian.Jira;
using GitCommands;
using GitCommands.Utils;
using Microsoft.Win32;

namespace GitUI.CommandsDialogs.SettingsDialog.Pages
{
    public partial class AlastriSettingsPage : SettingsPageWithHeader
    {
        public AlastriSettingsPage()
        {
            InitializeComponent();
            Text = "Alastri";
            InitializeComplete();
        }

        public static SettingsPageReference GetPageReference()
        {
            return new SettingsPageReferenceByType(typeof(AlastriSettingsPage));
        }

        protected override void SettingsToPage()
        {
            textJiraServer.Text = AppSettings.AlastriJiraURL;
            textUsername.Text = AppSettings.AlastriJiraUsername;
            textPassword.Text = AppSettings.AlastriJiraPassword;
        }

        protected override void PageToSettings()
        {
            AppSettings.AlastriJiraURL = textJiraServer.Text;
            AppSettings.AlastriJiraUsername = textUsername.Text;
            AppSettings.AlastriJiraPassword = textPassword.Text;
        }

        private async void buttonTest_Click(object sender, EventArgs e)
        {
            try
            {
                var jira = Jira.CreateRestClient(textJiraServer.Text, textUsername.Text, textPassword.Text);
                var result = await jira.Projects.GetProjectAsync("RN");

                MessageBox.Show("Success!");
            }
            catch
            {
                MessageBox.Show("Failed!");
            }
        }
    }
}
