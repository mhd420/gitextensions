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
        private static string[] Products = { "HI", "RR", "TS", "SC" };

        private readonly string _message;
        private readonly List<CheckBox> _productCheck;

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
            _productCheck = new List<CheckBox>();

            foreach (var product in Products)
            {
                var cbox = new CheckBox()
                {
                    Name = $"check{product}",
                    Text = product,
                    AutoSize = true,
                };

                flowProducts.Controls.Add(cbox);
                _productCheck.Add(cbox);
            }
        }

        protected override void OnLoad(EventArgs e)
        {
            textReleaseNote.Text = _message;
            textReleaseNote.Select();
        }

        private void Ok_Click(object sender, EventArgs e)
        {
            var releaseNote = textReleaseNote.Text;
            var testNote = textNotes.Text;
            var selectedProducts = _productCheck.Where(p => p.Checked).Select(p => p.Text).ToArray();

            if (string.IsNullOrWhiteSpace(releaseNote))
            {
                MessageBox.Show("Please enter a release note.");
                return;
            }

            if (selectedProducts.Length == 0)
            {
                MessageBox.Show("Please select at least one product for this release note.");
                return;
            }

            try
            {
                var jira = Jira.CreateRestClient(AppSettings.AlastriJiraURL, AppSettings.AlastriJiraUsername, AppSettings.AlastriJiraPassword);

                var summary = releaseNote.SplitLines().FirstOrDefault();
                var description = $"Release Notes:\r\n{releaseNote}\r\n\r\nTesting Notes:\r\n{testNote}";

                var newIssue = jira.CreateIssue("RN");
                newIssue.Type = "Task";
                newIssue.Summary = summary;
                newIssue.Description = description;
                newIssue["Release Notes"] = releaseNote;

                newIssue.CustomFields.AddArray("Affects Projects", selectedProducts);

                newIssue.SaveChanges();

                JiraTicket = newIssue.Key.Value;
                DialogResult = DialogResult.OK;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), "Creating ticket failed");
            }
        }
    }
}
