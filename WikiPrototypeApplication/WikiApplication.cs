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
            ststripWiki.Text = "";
        }

        static int row = 12;
        static int column = 4;
        int ptr = 0;
        string[,] wikiEntry = new string[row, column];

        private void buttonAdd_Click(object sender, EventArgs e)
        {
            Add();
            Clear_Textboxes();
            BubbleSort(); 
            DisplayList();
        }

        private void stStripArrayFull()
        {
            ststripWiki.Text = "The array is full";
            ststripWiki.BackColor = Color.Red;
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
                stStripArrayFull();
            }

        }
        private void Clear_Textboxes()
        {
            textName.Clear();
            textCategory.Clear();
            textStructure.Clear();
            textDefinition.Clear();
        }

        private void BubbleSort()
        {
            for (int i = 0; i < row; i++)
            {
                for (int j = 0; j < row - 1; j++)
                {
                    if (!(string.IsNullOrEmpty(wikiEntry[j + 1, 0])))
                    {
                        if (string.Compare(wikiEntry[j, 0], wikiEntry[j + 1, 0]) == 1)
                        {
                            Swap(j);

                        }
                    }
                }
            }
        }

        private void Swap(int indx)
        {
            string temp;
            for (int i = 0; i < column; i++)
            {
                temp = wikiEntry[indx, i ];
                wikiEntry[indx, i] = wikiEntry[indx + 1, i];
                wikiEntry[indx + 1, i] = temp;
            }
        }
       
        private void DisplayList()
        {
            dataListView.Items.Clear();
            for (int x = 0; x < row; x++)
            {
               ListViewItem wikiData = new ListViewItem(wikiEntry[x, 0]);
                wikiData.SubItems.Add(wikiEntry[x,1]);
                wikiData.SubItems.Add(wikiEntry[x, 2]);
                wikiData.SubItems.Add(wikiEntry[x, 3]);

                dataListView.Items.Add(wikiData);   

            }
        }

        private void textSearch_Click(object sender, EventArgs e)
        {
            textSearch.Clear();
            int startIndex = -1;
            int finalIndex = ptr;
            bool flag = false;
            int foundIndex = -1;

            while (!flag && !((finalIndex - startIndex) <= 1))
            {
                int newIndex = (finalIndex - startIndex) / 2;
                if (string.Compare(wikiEntry[newIndex, 0], textName.Text) == 0)
                {
                    foundIndex = newIndex;
                    flag = true;
                    break;
                }
                else
                {
                    if (string.Compare(wikiEntry[newIndex, 0], textName.Text) == 1)
                        finalIndex = newIndex;
                    else
                        startIndex = newIndex;
                }
            }
            if (flag)
            {

            }
        }

        private void buttonEdit_Click(object sender, EventArgs e)
        {

        }

        private void buttonSort_Click(object sender, EventArgs e)
        {
            BubbleSort();
        }
    }
}
