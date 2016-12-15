using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FormsDemo
{
    public partial class Form1 : Form
    {
        private List<Person> _personen;

        public string Vorname
        {
            get
            {
                return textBox1.Text;
            }
        }

        public string Nachname
        {
            get { return textBox1.Name; }
        }

        public Form1()
        {
            _personen = new List<Person>();
            _personen.Add(new Person { Id = 1, Name = "David", Age = 32 });

            InitializeComponent();

            listBox1.DisplayMember = "Name";
            listBox1.DataSource = _personen;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            _personen.Add(new Person { Name = Vorname + " " + Nachname });
            listBox1.DataSource = null;
            listBox1.Items.Clear();
            listBox1.DataSource = _personen;
        }
    }

    public class Person
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Age { get; set; }
    }
}
