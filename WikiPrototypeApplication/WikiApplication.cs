using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WikiPrototypeApplication
{
    public partial class WikiApplication : Form
    {
        public WikiApplication()
        {
            InitializeComponent();
        }

        static int row = 12;
        static int column = 4;
        int ptr = 0;
        string[,] wikiEntry = new string[row, column];

        private void buttonAdd_Click(object sender, EventArgs e)
        {
            Add();
            DisplayList();
        }

        private void Add()
        {
           if (ptr < row)
            {
                try
                {

                    wikiEntry[ptr, 0] = textName.Text;
                    wikiEntry[ptr, 1] = textCategory.Text;
                    wikiEntry[ptr, 2] = textStructure.Text;
                    wikiEntry[ptr, 3] = textDefinition.Text;
                    ptr++;
                }
                catch
                {
                    MessageBox.Show("Entry is invalid");
                }

            }
            else
            {
                statusStrip.Show();
            }

        }
       
        private void DisplayList()
        {
            listView.Items.Clear();
            for (int x = 0; x < row; x++)
            {
               ListViewItem wikiData = new ListViewItem(wikiEntry[x, 0]);
                wikiData.SubItems.Add(wikiEntry[x,1]);
                wikiData.SubItems.Add(wikiEntry[x, 2]);
                wikiData.SubItems.Add(wikiEntry[x, 3]);

                listView.Items.Add(wikiData);   

            }
        }


    }
}
