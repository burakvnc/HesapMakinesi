using System;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace HesapMakinesi
{
    public partial class Form1 : Form
    {
        [DllImport("user32.dll", SetLastError = true)]
        static extern IntPtr GetWindowDC(IntPtr hWnd);

        [DllImport("user32.dll", SetLastError = true)]
        static extern int ReleaseDC(IntPtr hWnd, IntPtr hDC);

        const int WM_NCPAINT = 0x85;
        const int WM_NCACTIVATE = 0x86;

        protected override void WndProc(ref Message m)
        {
            base.WndProc(ref m);
            if (m.Msg == WM_NCPAINT || m.Msg == WM_NCACTIVATE)
            {
                IntPtr hdc = GetWindowDC(m.HWnd);
                if ((int)hdc != 0)
                {
                    using (Graphics g = Graphics.FromHdc(hdc))
                    {
                        g.FillRectangle(Brushes.Green, new Rectangle(0, 0, this.Width, 30));
                    }
                    ReleaseDC(m.HWnd, hdc);
                }
            }
        }


        private System.Windows.Forms.Button[] numberButtons;
        private System.Windows.Forms.Button addButton;
        private System.Windows.Forms.Button subtractButton;
        private System.Windows.Forms.Button multiplyButton;
        private System.Windows.Forms.Button divideButton;
        private System.Windows.Forms.Button equalsButton;
        private System.Windows.Forms.Button clearButton;
        private System.Windows.Forms.Button decimalButton;
        private System.Windows.Forms.Button percentButton;
        private System.Windows.Forms.Button sqrtButton;
        private System.Windows.Forms.Button squareButton;
        private System.Windows.Forms.Button reciprocalButton;
        private System.Windows.Forms.Button dotButton;

        private double ilkSayi = 0;
        private string islem = "";

        public Form1()
        {
            InitializeComponent();
            InitializeCalculator();
        }


        private void InitializeCalculator()
        {

            this.numberButtons = new System.Windows.Forms.Button[10];
            for (int i = 0; i < 10; i++)
            {
                this.numberButtons[i] = new System.Windows.Forms.Button
                {
                    Size = new System.Drawing.Size(100, 70),
                    TabIndex = i + 1,
                    Text = i.ToString(),
                    FlatStyle = FlatStyle.Flat,
                    ForeColor = Color.White,
                    BackColor = Color.FromArgb(59, 59, 59)
                };
                this.numberButtons[i].FlatAppearance.BorderSize = 0;
                this.numberButtons[i].Click += new System.EventHandler(this.sayi_Click);
                this.Controls.Add(this.numberButtons[i]);
            }

            // Number Buttons Layout
            int buttonIndex = 1;
            for (int y = 4; y >= 2; y--)
            {
                for (int x = 0; x < 3; x++)
                {
                    this.numberButtons[buttonIndex].Location = new System.Drawing.Point(5 + x * 100,150 + y * 70);
                    buttonIndex++;
                }
            }
            this.numberButtons[0].Location = new System.Drawing.Point(105, 500);

            // Operation Buttons
            this.addButton = new System.Windows.Forms.Button
            {
                Location = new System.Drawing.Point(305, 430),
                Name = "addButton",
                Size = new System.Drawing.Size(100, 70),
                TabIndex = 11,
                Text = "+",
                FlatStyle = FlatStyle.Flat,
                ForeColor = Color.White,
                BackColor = Color.FromArgb(50, 50, 50)
            };
            this.addButton.FlatAppearance.BorderSize = 0;
            this.addButton.Click += new System.EventHandler(this.islem_Click);
            this.Controls.Add(this.addButton);

            this.subtractButton = new System.Windows.Forms.Button
            {
                Location = new System.Drawing.Point(305, 360),
                Name = "subtractButton",
                Size = new System.Drawing.Size(100, 70),
                TabIndex = 12,
                Text = "-",
                FlatStyle = FlatStyle.Flat,
                ForeColor = Color.White,
                BackColor = Color.FromArgb(50, 50, 50)
            };
            this.subtractButton.FlatAppearance.BorderSize = 0;
            this.subtractButton.Click += new System.EventHandler(this.islem_Click);
            this.Controls.Add(this.subtractButton);

            this.multiplyButton = new System.Windows.Forms.Button
            {
                Location = new System.Drawing.Point(305, 290),
                Name = "multiplyButton",
                Size = new System.Drawing.Size(100, 70),
                TabIndex = 13,
                Text = "*",
                FlatStyle = FlatStyle.Flat,
                ForeColor = Color.White,
                BackColor = Color.FromArgb(50, 50, 50)
            };
            this.multiplyButton.FlatAppearance.BorderSize = 0;
            this.multiplyButton.Click += new System.EventHandler(this.islem_Click);
            this.Controls.Add(this.multiplyButton);

            this.divideButton = new System.Windows.Forms.Button
            {
                Location = new System.Drawing.Point(305, 220),
                Name = "divideButton",
                Size = new System.Drawing.Size(100, 70),
                TabIndex = 14,
                Text = "/",
                FlatStyle = FlatStyle.Flat,
                ForeColor = Color.White,
                BackColor = Color.FromArgb(50, 50, 50)
            };
            this.divideButton.FlatAppearance.BorderSize = 0;
            this.divideButton.Click += new System.EventHandler(this.islem_Click);
            this.Controls.Add(this.divideButton);

            this.equalsButton = new System.Windows.Forms.Button
            {
                Location = new System.Drawing.Point(305, 500),
                Name = "equalsButton",
                Size = new System.Drawing.Size(100, 70),
                TabIndex = 15,
                Text = "=",
                FlatStyle = FlatStyle.Flat,
                ForeColor = Color.White,
                BackColor = Color.FromArgb(243, 128, 100)

            };
            this.equalsButton.FlatAppearance.BorderSize = 0;
            this.equalsButton.Click += new System.EventHandler(this.esittir_Click);
            this.Controls.Add(this.equalsButton);

            this.clearButton = new System.Windows.Forms.Button
            {
                Location = new System.Drawing.Point(105, 150),
                Name = "clearButton",
                Size = new System.Drawing.Size(100, 70),
                TabIndex = 16,
                Text = "CE",
                FlatStyle = FlatStyle.Flat,
                ForeColor = Color.White,
                BackColor = Color.FromArgb(50, 50, 50)
            };
            this.clearButton.FlatAppearance.BorderSize = 0;
            this.clearButton.Click += new System.EventHandler(this.temizle_Click);
            this.Controls.Add(this.clearButton);
            this.clearButton = new System.Windows.Forms.Button
            {
                Location = new System.Drawing.Point(205, 150),
                Name = "clearButton",
                Size = new System.Drawing.Size(100, 70),
                TabIndex = 16,
                Text = "C",
                FlatStyle = FlatStyle.Flat,
                ForeColor = Color.White,
                BackColor = Color.FromArgb(50, 50, 50)
            };
            this.clearButton.FlatAppearance.BorderSize = 0;
            this.clearButton.Click += new System.EventHandler(this.temizle_Click);
            this.Controls.Add(this.clearButton);

            this.clearButton = new System.Windows.Forms.Button
            {
                Location = new System.Drawing.Point(305, 150),
                Name = "clearButton",
                Size = new System.Drawing.Size(100, 70),
                TabIndex = 16,
                Text = "⌫",
                FlatStyle = FlatStyle.Flat,
                ForeColor = Color.White,
                BackColor = Color.FromArgb(50, 50, 50)
            };
            this.clearButton.FlatAppearance.BorderSize = 0;
            this.clearButton.Click += new System.EventHandler(this.temizle_Click);
            this.Controls.Add(this.clearButton);

            this.clearButton = new System.Windows.Forms.Button
            {
                Location = new System.Drawing.Point(5, 150),
                Name = "clearButton",
                Size = new System.Drawing.Size(100, 70),
                TabIndex = 16,
                Text = "ƒ",
                FlatStyle = FlatStyle.Flat,
                ForeColor = Color.White,
                BackColor = Color.FromArgb(50, 50, 50)
            };
            this.clearButton.FlatAppearance.BorderSize = 0;
            this.clearButton.Click += new System.EventHandler(this.temizle_Click);
            this.Controls.Add(this.clearButton);

            this.decimalButton = new System.Windows.Forms.Button
            {
                Location = new System.Drawing.Point(205, 500),
                Name = "decimalButton",
                Size = new System.Drawing.Size(100, 70),
                TabIndex = 17,
                Text = ",",
                FlatStyle = FlatStyle.Flat,
                ForeColor = Color.White,
                BackColor = Color.FromArgb(59, 59, 59)
            };
            this.decimalButton.FlatAppearance.BorderSize = 0;
            this.decimalButton.Click += new System.EventHandler(this.sayi_Click);
            this.Controls.Add(this.decimalButton);

            this.percentButton = new System.Windows.Forms.Button
            {
                Location = new System.Drawing.Point(5, 500),
                Name = "percentButton",
                Size = new System.Drawing.Size(100, 70),
                TabIndex = 18,
                Text = "%",
                FlatStyle = FlatStyle.Flat,
                ForeColor = Color.White,
                BackColor = Color.FromArgb(59, 59, 59)
            };
            this.percentButton.FlatAppearance.BorderSize = 0;
            this.percentButton.Click += new System.EventHandler(this.islem_Click);
            this.Controls.Add(this.percentButton);

            this.sqrtButton = new System.Windows.Forms.Button
            {
                Location = new System.Drawing.Point(205, 220),
                Name = "sqrtButton",
                Size = new System.Drawing.Size(100, 70),
                TabIndex = 19,
                Text = "√",
                FlatStyle = FlatStyle.Flat,
                ForeColor = Color.White,
                BackColor = Color.FromArgb(50, 50, 50)
            };
            this.sqrtButton.FlatAppearance.BorderSize = 0;
            this.sqrtButton.Click += new System.EventHandler(this.islem_Click);
            this.Controls.Add(this.sqrtButton);

            this.squareButton = new System.Windows.Forms.Button
            {
                Location = new System.Drawing.Point(105, 220),
                Name = "squareButton",
                Size = new System.Drawing.Size(100, 70),
                TabIndex = 20,
                Text = "x²",
                FlatStyle = FlatStyle.Flat,
                ForeColor = Color.White,
                BackColor = Color.FromArgb(50, 50, 50)
            };
            this.squareButton.FlatAppearance.BorderSize = 0;
            this.squareButton.Click += new System.EventHandler(this.islem_Click);
            this.Controls.Add(this.squareButton);

            this.reciprocalButton = new System.Windows.Forms.Button
            {
                Location = new System.Drawing.Point(5, 220),
                Name = "reciprocalButton",
                Size = new System.Drawing.Size(100, 70),
                TabIndex = 21,
                Text = "1/x",
                FlatStyle = FlatStyle.Flat,
                ForeColor = Color.White,
                BackColor = Color.FromArgb(50, 50, 50)
            };
            this.reciprocalButton.FlatAppearance.BorderSize = 0;
            this.reciprocalButton.Click += new System.EventHandler(this.islem_Click);
            this.Controls.Add(this.reciprocalButton);
        }

        private void sayi_Click(object sender, EventArgs e)
        {
            Button button = (Button)sender;
            textBox1.Text += button.Text;
        }

        private void islem_Click(object sender, EventArgs e)
        {
            Button button = (Button)sender;
            ilkSayi = Convert.ToDouble(textBox1.Text);
            islem = button.Text;
            textBox1.Clear();
        }

        private void esittir_Click(object sender, EventArgs e)
        {
            double ikinciSayi = Convert.ToDouble(textBox1.Text);
            double sonuc = 0;

            switch (islem)
            {
                case "+":
                    sonuc = ilkSayi + ikinciSayi;
                    break;
                case "-":
                    sonuc = ilkSayi - ikinciSayi;
                    break;
                case "*":
                    sonuc = ilkSayi * ikinciSayi;
                    break;
                case "/":
                    sonuc = ilkSayi / ikinciSayi;
                    break;
                case "%":
                    sonuc = ilkSayi % ikinciSayi;
                    break;
                case "√":
                    sonuc = Math.Sqrt(ilkSayi);
                    break;
                case "x²":
                    sonuc = ilkSayi * ilkSayi;
                    break;
                case "1/x":
                    sonuc = 1 / ilkSayi;
                    break;
            }

            textBox1.Text = sonuc.ToString();
        }

        private void temizle_Click(object sender, EventArgs e)
        {
            textBox1.Clear();
            ilkSayi = 0;
            islem = "";
        }
    }
}
