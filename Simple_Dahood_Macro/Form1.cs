using System.Runtime.InteropServices;

namespace Simple_Dahood_Macro
{
    public partial class Form1 : Form
    {
        [DllImport("winmm.dll")]
        private static extern uint timeBeginPeriod(uint uPeriod);

        [DllImport("user32.dll")]
        private static extern short GetAsyncKeyState(int vKey);

        private const int Q_Key = 0x51;

        private const byte I_Key = 0x0017;
        private const byte O_Key = 0x0018;

        private static int Delay = 8;

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            LoadDelay();
            textBox1.Text = $"{Delay}";
            Task.Run(Macro);
        }

        private void Macro()
        {
            SendInputs sendInp = new SendInputs(I_Key, O_Key);

            timeBeginPeriod(1);

            while (true)
            {
                while (GetAsyncKeyState(Q_Key) < 0)
                {
                    sendInp.SendKeys();
                    Thread.Sleep(Delay);
                }
                Thread.Sleep(50);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            bool result = int.TryParse(textBox1.Text, out Delay);

            if (!result)
            {
                MessageBox.Show("Please only enter integers, ex: 1, 2, 3, 4....", "Dahood macro", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                LoadDelay();
            }
            textBox1.Text = $"{Delay}";

            SaveDelay();
        }

        private void LoadDelay()
        {
            Delay = Properties.Settings.Default.Delay;
        }

        private void SaveDelay()
        {
            Properties.Settings.Default.Delay = Delay;
            Properties.Settings.Default.Save();
        }
    }
}
