using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Atlassian.Jira;
using GitCommands;
using GitUIPluginInterfaces;
using JetBrains.Annotations;

namespace GitUI.CommandsDialogs
{
    public partial class FormCreateReleaseNote : GitExtensionsForm
    {
        private readonly string _message;

        public string JiraTicket { get; set; }

        [Obsolete("For VS designer and translation test only. Do not remove.")]
        private FormCreateReleaseNote()
        {
            InitializeComponent();
        }

        public FormCreateReleaseNote(string message)
        {
            InitializeComponent();
            InitializeComplete();

            _message = message;
        }

        protected override void OnLoad(EventArgs e)
        {
            textReleaseNote.Text = _message;
            textReleaseNote.Select();
        }

        private void Ok_Click(object sender, EventArgs e)
        {
            var newNote = textReleaseNote.Text;
            var testNote = textNotes.Text;

            if (string.IsNullOrWhiteSpace(newNote))
            {
                MessageBox.Show("Please enter a release note.");
                return;
            }

            try
            {
                var jira = Jira.CreateRestClient(AppSettings.AlastriJiraURL, AppSettings.AlastriJiraUsername, AppSettings.AlastriJiraPassword);

                var newIssue = jira.CreateIssue("RN");
                newIssue.Type = "Task";
                newIssue.Summary = newNote;
                newIssue.Description = testNote;

                newIssue.SaveChanges();

                JiraTicket = newIssue.Key.Value;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), "Creating ticket failed");
            }
        }
    }
}
