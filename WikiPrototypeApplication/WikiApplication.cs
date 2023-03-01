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
        int selectedlistviewcount = -1;
        string[,] wikiEntry = new string[row, column];


        #region Functions


        private void stStripArrayFull()
        {
            ststripWiki.Text = "The array is full";
            ststripWiki.BackColor = Color.Yellow;
        }

        private void stStripArrayEmpty()
        {
            ststripWiki.Text = "The entry is invalid";
            ststripWiki.BackColor = Color.Red;
        }

        private void stStripArrayReset()
        {
            ststripWiki.Text = string.Empty;
            ststripWiki.BackColor = Color.Empty;

        }
        
        private void Add()
        {
            if (!(string.IsNullOrEmpty(textName.Text)))
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
            else
            {
               stStripArrayEmpty();
            }        
        }
        private void Edit()
        {
            wikiEntry[selectedlistviewcount, 0] = textName.Text;
            wikiEntry[selectedlistviewcount, 1] = textCategory.Text;
            wikiEntry[selectedlistviewcount, 2] = textStructure.Text;
            wikiEntry[selectedlistviewcount, 3] = textDefinition.Text;
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
                temp = wikiEntry[indx, i];
                wikiEntry[indx, i] = wikiEntry[indx + 1, i];
                wikiEntry[indx + 1, i] = temp;
            }
        }
       
        private void DisplayList()
        {
            dataListView.Items.Clear();
            for (int i = 0; i < row; i++)
            {
                if (wikiEntry[i, 0] != "~" && wikiEntry[i, 0] != null)
                    {
                        ListViewItem wikiData = new ListViewItem(wikiEntry[i, 0]);
                        wikiData.SubItems.Add(wikiEntry[i, 1]);
                        wikiData.SubItems.Add(wikiEntry[i, 2]);
                        wikiData.SubItems.Add(wikiEntry[i, 3]);

                        dataListView.Items.Add(wikiData);
                    }
            }
        }

        #endregion

        private void dataListView_MouseClick(object sender, MouseEventArgs e)
        {
            selectedlistviewcount = dataListView.SelectedIndices[0];
            textName.Text = wikiEntry[selectedlistviewcount, 0];
            textCategory.Text = wikiEntry[selectedlistviewcount, 1];
            textStructure.Text = wikiEntry[selectedlistviewcount, 2];
            textDefinition.Text = wikiEntry[selectedlistviewcount, 3];            
        }

        #region Buttons

        private void buttonAdd_Click(object sender, EventArgs e)
        {
            Add();
            Clear_Textboxes();
            BubbleSort();
            DisplayList();
            textName.Focus();
        }

        // not finished yet
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
            if (selectedlistviewcount > -1)
            {
                Edit();
                Clear_Textboxes();
                BubbleSort();
                DisplayList();
                selectedlistviewcount = -1;
            }
            else
            {
                stStripArrayEmpty();
            }
        }

        private void buttonSort_Click(object sender, EventArgs e)
        {
            BubbleSort();
        }


        #endregion

        private void buttonDelete_Click(object sender, EventArgs e)
        {

        }

        private void WikiApplication_MouseClick(object sender, MouseEventArgs e)
        {
            stStripArrayReset();
        }
    }
}
