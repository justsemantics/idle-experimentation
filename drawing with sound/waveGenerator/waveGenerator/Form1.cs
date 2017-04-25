using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace waveGenerator
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            Vector v1 = new Vector(0, -1);
            Vector v2 = new Vector(-.95f, -.35f);
            Vector v3 = new Vector(-.59f, .81f);
            Vector v4 = new Vector(.59f, .81f);
            Vector v5 = new Vector(.95f, -.35f);

            WaveGenerator wg = new WaveGenerator();
            wg.SetWaveShape(new Vector[] { v1, v3, v5, v2, v4});
            wg.Generate();
            wg.Save("C:\\Users\\theen\\Desktop\\wawawaw\\test.wav");
        }
    }
}
