using System;
using System.Drawing;
using System.IO;
using System.Reflection;
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

        #region Behaviour
        private void dataListView_MouseClick(object sender, MouseEventArgs e)
        {
            if (dataListView.SelectedIndices.Count > -1)
            {
                selectedlistviewcount = dataListView.SelectedIndices[0];
                textName.Text = wikiEntry[selectedlistviewcount, 0];
                textCategory.Text = wikiEntry[selectedlistviewcount, 1];
                textStructure.Text = wikiEntry[selectedlistviewcount, 2];
                textDefinition.Text = wikiEntry[selectedlistviewcount, 3];
            }
            else
            {
                MessageBox.Show("Error when selecting data from List View");
            }
        }

        private void WikiApplication_MouseClick(object sender, MouseEventArgs e)
        {
            stStripArrayReset();
        }
        private void textSearch_Click(object sender, EventArgs e)
        {
            textSearch.Clear();
            textSearch.ForeColor = Color.Black;

        }
        #endregion

        #region Status Strips
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

        private void stStripArrayEdit()
        {
            ststripWiki.Text = "Entry was successfully edited";
            ststripWiki.BackColor = Color.YellowGreen;
        }

        private void stStripArrayDelete()
        {
            ststripWiki.Text = "Entry was successfully deleted";
            ststripWiki.BackColor = Color.Tomato;
        }

        private void stStripArrayReset()
        {
            ststripWiki.Text = string.Empty;
            ststripWiki.BackColor = Color.Empty;

        }
        #endregion

        #region Methods
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

        private void Delete()
        {
            for (int i = 0; i < dataListView.Columns.Count; i++)
            {
                wikiEntry[selectedlistviewcount, i] = string.Empty;
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
                    //if (String.CompareOrdinal(wikiEntry[j, 0], wikiEntry[j + 1, 0]) > 0)
                    //{
                    //    Swap(j);
                    //}
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

        private void ButtonsRoutine()
        {
            Clear_Textboxes();
            BubbleSort();
            DisplayList();
        }

        private void Search()
        {
            int startIndex = -1;
            int finalIndex = ptr;
            bool flag = false;
            int foundIndex = -1;

            while (!flag && !((finalIndex - startIndex) <= 1))
            {
                int newIndex = (finalIndex + startIndex) / 2;
                if (string.Compare(wikiEntry[newIndex, 0], textSearch.Text) == 0)
                {
                    foundIndex = newIndex;
                    flag = true;
                    break;
                }
                else
                {
                    if (string.Compare(wikiEntry[newIndex, 0], textSearch.Text) == 1)
                        finalIndex = newIndex;
                    else
                        startIndex = newIndex;
                }
            }
            if (flag)
            {
                MessageBox.Show("Found");
                textName.Text = wikiEntry[foundIndex, 0];
                textCategory.Text = wikiEntry[foundIndex, 1];
                textStructure.Text = wikiEntry[foundIndex, 2];
                textDefinition.Text = wikiEntry[foundIndex, 3];
            }
            else
            {
                MessageBox.Show("Not found");
            }
        }
        #endregion

        #region Buttons

        private void buttonAdd_Click(object sender, EventArgs e)
        {
            Add();
            ButtonsRoutine();
            textName.Focus();
            stStripArrayReset();
        }

        private void buttonSearch_Click(object sender, EventArgs e)
        {
            Search();
            textSearch.Clear();
            stStripArrayReset();
        }

        private void buttonEdit_Click(object sender, EventArgs e)
        {
            if (selectedlistviewcount > -1)
            {
                Edit();
                ButtonsRoutine();
                selectedlistviewcount = -1;
                stStripArrayEdit();

            }
            else
            {
                stStripArrayEmpty();
            }
        }
        private void buttonDelete_Click(object sender, EventArgs e)
        {
            if (selectedlistviewcount > -1)
            {
                DialogResult result = MessageBox.Show("Do you want to delete the selected entry?", "Delete", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                if (result == DialogResult.Yes)
                {
                    Delete();
                    ButtonsRoutine();
                    ptr--;
                    selectedlistviewcount = -1;
                    stStripArrayDelete();
                }
                else
                {
                    ButtonsRoutine();
                    selectedlistviewcount = -1;
                }

            }
        }

        private void buttonSort_Click(object sender, EventArgs e)
        {
            BubbleSort();
            stStripArrayReset();
        }
        

        private void buttonLoad_Click(object sender, EventArgs e)
        {
            string defaultDirectory = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
            opFileDialog.InitialDirectory = defaultDirectory;
            opFileDialog.FileName = "definitions.dat";
            if (opFileDialog.ShowDialog() == DialogResult.OK)
            {
                BinaryReader br;
                int x = 0;
                dataListView.Items.Clear();
                try
                {
                    br = new BinaryReader(new FileStream(opFileDialog.FileName, FileMode.Open));
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message + "\nCannot open file for reading");
                    return;
                }
                while (br.BaseStream.Position != br.BaseStream.Length)
                {
                    try
                    {
                        wikiEntry[x, 0] = br.ReadString();
                        wikiEntry[x, 1] = br.ReadString();
                        wikiEntry[x, 2] = br.ReadString();
                        wikiEntry[x, 3] = br.ReadString();
                        x++;
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Cannot read data from file or EOF" + ex);
                        break;
                    }
                }
                br.Close();
                ptr = x;
                DisplayList();
            }

            stStripArrayReset();
        }

        private void buttonSave_Click(object sender, EventArgs e)
        {
            string defaultDirectory = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
            svFileDialog.InitialDirectory = defaultDirectory;
            svFileDialog.FileName = "definitions.dat";
            if (svFileDialog.ShowDialog() == DialogResult.OK)
            {
                BinaryWriter bw;
                try
                {
                    bw = new BinaryWriter(new FileStream("definitions.dat", FileMode.Create));
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message + "\n Cannot append to file.");
                    return;
                }
                try
                {
                    for (int i = 0; i < ptr; i++)
                    {
                        bw.Write(wikiEntry[i, 0]);
                        bw.Write(wikiEntry[i, 1]);
                        bw.Write(wikiEntry[i, 2]);
                        bw.Write(wikiEntry[i, 3]);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message + "\n Cannot write data to file.");
                    return;
                }
                bw.Close();
            }
            stStripArrayReset();
                       
        }
        #endregion
    }
}
