using System;
using System.Windows.Forms;

namespace SmartDnsServer
{
	public partial class FormMain : Form
	{
		public FormMain()
		{
			InitializeComponent();
		}

		private void showToolStripMenuItem_Click(object sender, EventArgs e)
		{
			Show();
		}

		private void exitToolStripMenuItem_Click(object sender, EventArgs e)
		{
			CloseForm();
		}

		bool _isClose;
		private void FormMain_FormClosing(object sender, FormClosingEventArgs e)
		{
			if (!_isClose)
			{
				e.Cancel = true;
				Hide();
			}
		}

		private void btnStart_Click(object sender, EventArgs e)
		{
			StartDnsServer();
			btnStart.Enabled = false;
		}

		private static void StartDnsServer()
		{
			var manager = new DnsServerManager();
			manager.Start();
		}

		private void FormMain_Load(object sender, EventArgs e)
		{
			notifyIcon1.ContextMenuStrip = contextMenuStripNotifyIcon;

			StartDnsServer();
			btnStart.Enabled = false;
		}

		private void notifyIcon1_MouseDoubleClick(object sender, MouseEventArgs e)
		{
			Show();
		}

		private void exitToolStripMenuItem1_Click(object sender, EventArgs e)
		{
			CloseForm();
		}

		private void CloseForm()
		{
			_isClose = true;
			Close();
		}

		private void FormMain_Shown(object sender, EventArgs e)
		{
			Hide();
		}

		private void notifyIcon1_MouseClick(object sender, MouseEventArgs e)
		{
		}

	}
}
