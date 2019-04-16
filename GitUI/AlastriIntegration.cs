using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;
using Atlassian.Jira;
using GitCommands;
using GitCommands.Git.Tag;
using GitUI.CommandsDialogs;
using GitUI.Script;
using GitUI.UserControls.RevisionGrid;
using GitUIPluginInterfaces;

namespace GitUI
{
    public class AlastriIntegration
    {
        private static Regex NoteMatch = new Regex(@"note\-(\d+)\-[a-f0-9]+", RegexOptions.IgnoreCase);

        internal static void CreateReleaseNoteSubMenu(RevisionGridControl grid, GitUICommands commands, ContextMenuStrip menuStrip,
            GitRevision revision, GitRefListsForRevision refList)
        {
            var releaseMenu = menuStrip.Items.OfType<ToolStripMenuItem>().FirstOrDefault(n => n.Name == "ReleaseNotes");
            if (releaseMenu == null)
            {
                releaseMenu = new ToolStripMenuItem("Release Notes", Properties.Images.Book)
                {
                    Name = "ReleaseNotes",
                };
                menuStrip.Items.Add(releaseMenu);
            }

            if (revision.IsArtificial)
            {
                releaseMenu.Enabled = false;
                releaseMenu.Visible = false;
                return;
            }

            releaseMenu.Enabled = true;
            releaseMenu.Visible = true;

            var subItems = new ContextMenuStrip();

            var noteTag = refList.AllTags.FirstOrDefault(f => NoteMatch.IsMatch(f.Name));
            var hasClipNote = Clipboard.ContainsText() && NoteMatch.IsMatch(Clipboard.GetText());

            if (noteTag == null)
            {
                var createNote = new ToolStripMenuItem("Create Release Note");
                createNote.Click += (sender, args) => CreateNewNoteTag(grid, commands, revision, null);
                subItems.Items.Add(createNote);

                if (hasClipNote)
                {
                    var pasteMatch = NoteMatch.Match(Clipboard.GetText());
                    var ticketId = pasteMatch.Groups[1].Value;

                    var pasteNote = new ToolStripMenuItem("Paste Release Note");
                    pasteNote.Click += (sender, args) => CreateNewNoteTag(grid, commands, revision, ticketId);
                    subItems.Items.Add(pasteNote);
                }
            }
            else
            {
                var noteMatch = NoteMatch.Match(noteTag.Name);
                var ticketId = noteMatch.Groups[1].Value;

                var viewNote = new ToolStripMenuItem("View Release Note");
                viewNote.Click += (sender, args) => ViewReleaseNote(ticketId);
                subItems.Items.Add(viewNote);

                var copyNote = new ToolStripMenuItem("Copy Release Note");
                copyNote.Click += (sender, args) => CopyReleaseNote(noteTag.Name);
                subItems.Items.Add(copyNote);
            }

            releaseMenu.DropDown = subItems;
        }

        private static void CreateNewNoteTag(RevisionGridControl grid, GitUICommands commands, GitRevision revision, string ticketId)
        {
            ticketId = ticketId ?? CreateJiraTicket(grid, revision);
            if (string.IsNullOrWhiteSpace(ticketId))
            {
                return;
            }

            var tagName = $"note-{ticketId}-{revision.ObjectId.ToShortString(8)}";
            var newTag = new GitCreateTagArgs(tagName, revision.ObjectId);

            commands.DoActionOnRepo(() =>
            {
                var gitTagController = new GitTagController(commands);
                var success = gitTagController.CreateTag(newTag, grid);

                if (success)
                {
                    PushTag(grid, commands.Module, tagName);
                }

                return success;
            });
        }

        private static void PushTag(RevisionGridControl grid, GitModule module, string tagName)
        {
            var remote = module.GetCurrentRemote();
            if (string.IsNullOrEmpty(remote))
            {
                remote = "origin";
            }

            var pushCmd = GitCommandHelpers.PushTagCmd(remote, tagName, false);
            using (var form = new FormRemoteProcess(module, pushCmd)
            {
                Remote = remote,
                Text = $"Pushing tag to {remote}",
            })
            {
                form.ShowDialog(grid);
            }
        }

        private static void ViewReleaseNote(string ticketId)
        {
            var url = new Uri(new Uri(AppSettings.AlastriJiraURL), $"/browse/RN-{ticketId}");

            Process.Start(url.ToString());
        }

        private static void CopyReleaseNote(string noteTagName)
        {
            Clipboard.SetText(noteTagName);
        }

        private static string CreateJiraTicket(IWin32Window grid, GitRevision revision)
        {
            using (var newTicketForm = new FormCreateReleaseNote(revision.Subject))
            {
                if (newTicketForm.ShowDialog(grid) == DialogResult.OK)
                {
                    var numberMatch = Regex.Match(newTicketForm.JiraTicket, @"[A-Z]{1,10}-(\d+)");
                    var ticketId = numberMatch.Groups[1].Value;

                    ViewReleaseNote(ticketId);

                    return ticketId;
                }
            }

            return null;
        }
    }
}
