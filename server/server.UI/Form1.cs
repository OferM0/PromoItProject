using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using server.Entities;

namespace server.UI
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            MainManager.Instance.Init();
        }
    }
}
