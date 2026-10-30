using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Management;
using System.Net;
using System.Net.NetworkInformation;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ChatPlus
{
    public partial class Goto : Form
    {
        NetworkInterface[] interfaces = NetworkInterface.GetAllNetworkInterfaces();

        // The nickname the user chose (available after the login panel closes)
        public string Nickname = "";

        // ----- Light minimal colors -----
        private readonly Color avatarBg = Color.FromArgb(231, 229, 228);
        private readonly Color avatarIcon = Color.FromArgb(68, 68, 65);
        private readonly Color borderNormal = Color.FromArgb(180, 178, 169);
        private readonly Color borderFocus = Color.FromArgb(44, 44, 42);
        private readonly Color textColor = Color.FromArgb(44, 44, 42);
        private readonly Color hintColor = Color.FromArgb(136, 135, 128);
        private readonly Color errorColor = Color.FromArgb(226, 75, 74);

        private const string PlaceholderText = "Your nickname";
        private const string DefaultHint = "2 to 20 characters";

        private bool showingPlaceholder = true;
        private bool hasError = false;

        public Goto()
        {
            InitializeComponent();

            // rounded corners (the controls themselves are in Goto.Designer.cs)
            inputBorder.Region = new Region(RoundPath(new Rectangle(0, 0, 324, 44), 8));
            inputInner.Region = new Region(RoundPath(new Rectangle(0, 0, 322, 42), 7));
            submitButton.Region = new Region(RoundPath(new Rectangle(0, 0, 324, 44), 8));

            loginPanel.BringToFront();   // login screen on top of everything else
            CenterCard();
        }

        // =====================================================
        //  Called when the user has chosen a valid nickname
        // =====================================================
        private void OnNicknameChosen()
        {
            loginPanel.Visible = false;
            this.AcceptButton = null;
            this.Text = "ChatPlus - " + Nickname;

            // TODO: your chat starts here, e.g. connect to the server using Nickname
        }

        // ----- Layout -----

        private void loginPanel_Resize(object sender, EventArgs e)
        {
            CenterCard();
        }

        private void CenterCard()
        {
            card.Location = new Point(
                Math.Max(0, (loginPanel.ClientSize.Width - card.Width) / 2),
                Math.Max(0, (loginPanel.ClientSize.Height - card.Height) / 2));
        }

        private static GraphicsPath RoundPath(Rectangle r, int radius)
        {
            int d = radius * 2;
            GraphicsPath path = new GraphicsPath();
            path.AddArc(r.X, r.Y, d, d, 180, 90);
            path.AddArc(r.Right - d, r.Y, d, d, 270, 90);
            path.AddArc(r.Right - d, r.Bottom - d, d, d, 0, 90);
            path.AddArc(r.X, r.Bottom - d, d, d, 90, 90);
            path.CloseFigure();
            return path;
        }

        // ----- Avatar circle with person icon -----

        private void avatar_Paint(object sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;
            g.SmoothingMode = SmoothingMode.AntiAlias;

            using (SolidBrush brush = new SolidBrush(avatarBg))
            {
                g.FillEllipse(brush, 0, 0, 63, 63);
            }

            using (Pen pen = new Pen(avatarIcon, 3f))
            {
                pen.StartCap = LineCap.Round;
                pen.EndCap = LineCap.Round;
                g.DrawEllipse(pen, 24, 15, 14, 14);      // head
                g.DrawArc(pen, 19, 35, 24, 22, 180, 180); // shoulders
            }
        }

        // ----- Input box -----

        private void inputInner_Click(object sender, EventArgs e)
        {
            nicknameBox.Focus();
        }

        private void nicknameBox_Enter(object sender, EventArgs e)
        {
            if (showingPlaceholder)
            {
                nicknameBox.Text = "";
                nicknameBox.ForeColor = textColor;
                showingPlaceholder = false;
            }
            if (!hasError) inputBorder.BackColor = borderFocus;
        }

        private void nicknameBox_Leave(object sender, EventArgs e)
        {
            if (nicknameBox.Text.Length == 0)
            {
                showingPlaceholder = true;
                nicknameBox.Text = PlaceholderText;
                nicknameBox.ForeColor = hintColor;
            }
            if (!hasError) inputBorder.BackColor = borderNormal;
        }

        private void nicknameBox_TextChanged(object sender, EventArgs e)
        {
            hasError = false;
            hintLabel.Text = DefaultHint;
            hintLabel.ForeColor = hintColor;
            inputBorder.BackColor = nicknameBox.Focused ? borderFocus : borderNormal;
        }

        // ----- Join button -----

        private void ShowError(string message)
        {
            hasError = true;
            hintLabel.Text = message;
            hintLabel.ForeColor = errorColor;
            inputBorder.BackColor = errorColor;
            nicknameBox.Focus();
        }

        private void submitButton_Click(object sender, EventArgs e)
        {
            string nick = showingPlaceholder ? "" : nicknameBox.Text.Trim();

            if (nick.Length < 2)
            {
                ShowError("Nickname must be at least 2 characters");
                return;
            }

            if (!Regex.IsMatch(nick, "^[A-Za-z0-9_-]+$"))
            {
                ShowError("Use only letters, numbers, _ and -");
                return;
            }

            Nickname = nick;
            OnNicknameChosen();
        }
    }
}
