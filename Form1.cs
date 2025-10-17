using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.VisualStyles;
using SortApp.Modl;

namespace SortApp
{
    public partial class Form1 : Form
    {
        ModelEF database = new ModelEF();
        List<Pavilion> pavilions = new List<Pavilion>();
        List<Pavilion> PavilionsChange = new List<Pavilion>();
        List<string> pavilionsProp = new List<string>();
        public Form1()
        {
            
            InitializeComponent();

        }

        private void Form1_Load(object sender, EventArgs e)
        {
            PavilionsChange = pavilions = database.Pavilion.ToList();

            LoadStartData();
            LoadDataCombo();

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            PavilionsChange = pavilions.Where(x => x.Status.Contains(textBox1.Text)).ToList();
            LoadOrder();

        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            LoadOrder();

        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            LoadOrder();
        }
        private void LoadStartData()
        {
            pavilionBindingSource.DataSource = PavilionsChange;
        }

        private void LoadDataCombo()
        {
            pavilionsProp = typeof(Pavilion).GetProperties().Select(x => x.Name).ToList();
            pavilionsProp.RemoveRange(pavilionsProp.Count - 2, 2);
            comboBox1.DataSource = pavilionsProp;
            comboBox1.SelectedIndex = 0;
        }
        private void LoadOrder()
        {
            PavilionsChange = checkBox1.Checked ?
            //присваивается это значение списка -
            PavilionsChange.OrderByDescending(p => p.GetType().GetProperties().
            First(x => x.Name == comboBox1.SelectedItem.ToString()).GetValue(p)).ToList()
            //иначе это значение списка
            : PavilionsChange.OrderBy(p => p.GetType().GetProperties().
            First(x => x.Name == comboBox1.SelectedItem.ToString()).GetValue(p)).ToList();
            LoadStartData();// <-- выполенние метода загрузки данных
        }
    }
}
