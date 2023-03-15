using System;
using System.Drawing;
using System.IO;
using System.Reflection;
using System.Windows.Forms;

// RAIHAN KHALIL ABDILLAH 30065696
// WIKI APPLICATION | February 1st 2023
// Small wiki for different types of data structures

namespace WikiPrototypeApplication
{
    public partial class WikiApplication : Form
    {
        public WikiApplication()
        {
            InitializeComponent();
            ststripWiki.Text = "";
        }

        // Using static for the size of the array
        static int row = 12;
        static int column = 4;
        // pointer is global, and set to 0
        int ptr = 0;
        // value for the selected listview is global and -1 because 0 is the first entry of the listview
        int selectedlistviewcount = -1;
        string[,] wikiEntry = new string[row, column];

        #region Behaviour - Everything besides buttons are clicked
        private void dataListView_MouseClick(object sender, MouseEventArgs e)
        {
            stStripArrayReset();
            // the selected listview value is changed to the index number of the listview's entry
            selectedlistviewcount = dataListView.SelectedIndices[0];
            if (selectedlistviewcount > -1)
            {
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
            // clicking on the form reset the status strip, listview count, and clear selection from list view
            stStripArrayReset();
            selectedlistviewcount = -1;
            dataListView.SelectedItems.Clear();
        }
        private void textSearch_Click(object sender, EventArgs e)
        {
            // remove gray text from the search textbox
            textSearch.Clear();
            textSearch.ForeColor = Color.Black;

        }

        #endregion

        #region Status Strips - in lieu of Message box for not too important events
        private void stStripArrayFull()
        {
            ststripWiki.Text = "Array is full";
            ststripWiki.BackColor = Color.Yellow;
        }

        private void stStripArrayEmpty()
        {
            ststripWiki.Text = "Entry is invalid";
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

        private void stStripArrayFound()
        {
            ststripWiki.Text = "Entry is found";
            ststripWiki.BackColor = Color.LightGreen;
        }

        private void stStripArrayNotFound()
        {
            ststripWiki.Text = "Entry is not found";
            ststripWiki.BackColor = Color.Yellow;
        }
        #endregion

        #region Methods
        private void Add()
        {
            // if the name textbox is filled, go through
            if (!(string.IsNullOrEmpty(textName.Text)))
            {
                // if the array is not full, go through
                if (ptr < row)
                {
                    // array is filled with all textboxes
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
            // selected listview value is passed to the array for editing
            wikiEntry[selectedlistviewcount, 0] = textName.Text;
            wikiEntry[selectedlistviewcount, 1] = textCategory.Text;
            wikiEntry[selectedlistviewcount, 2] = textStructure.Text;
            wikiEntry[selectedlistviewcount, 3] = textDefinition.Text;
        }

        private void Delete()
        {
            // turns all entry of the selected list value to "zzz"
            for (int i = 0; i < dataListView.Columns.Count; i++)
            {
                wikiEntry[selectedlistviewcount, i] = "zzz";
            }
        }

        private void Clear_Textboxes()
        {
            // clear all textboxes
            textName.Clear();
            textCategory.Clear();
            textStructure.Clear();
            textDefinition.Clear();
        }

        private void BubbleSort()
        {
            // bubble sort with compare ordinal
            for (int i = 0; i < row; i++)
            {
                for (int j = 0; j < row - 1; j++)
                {
                    if (String.CompareOrdinal(wikiEntry[j, 0], wikiEntry[j + 1, 0]) > 0)
                    {
                        Swap(j);
                    }
                }
            }
        }

        private void Swap(int indx)
        {
            // swapping method
            string temp;
            for (int i = 0; i < column; i++)
            {
                temp = wikiEntry[indx + 1, i];
                wikiEntry[indx + 1, i] = wikiEntry[indx, i];
                wikiEntry[indx, i] = temp;

            }
        }

        private void DisplayList()
        {
            // unselect the entry on the list view
            dataListView.Items.Clear();
            // adding the entries already in the array to the listview using ListViewItem object
            for (int i = 0; i < row; i++)
            {
                if (wikiEntry[i, 0] != "zzz" && wikiEntry[i, 0] != null)
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
            // clear textboxes and show array
            Clear_Textboxes();
            DisplayList();
        }

        private void Search()
        {
            // binary search
            int startIndex = -1;
            int finalIndex = ptr;
            bool flag = false;
            int foundIndex = -1;
            // all words written in search textbox becomes a title, First letter of words are capitalised
            string temp = textSearch.Text.ToLower();
            string searchtext = new System.Globalization.CultureInfo("en-US").TextInfo.ToTitleCase(temp);
            
            // while flag is FALSE AND the set index is less or equal to 1, keep looping until finish
            while (!flag && !((finalIndex - startIndex) <= 1))
            {
                // do calculation for new index value to do the compare 
                int newIndex = (finalIndex + startIndex) / 2;
                // comparing the entry of an array at new index with text on the search textbox
                if (string.Compare(wikiEntry[newIndex, 0], searchtext) == 0)
                {
                    // if match, the new index value assigned to found index, turns the flag TRUE, and then break the while loop
                    foundIndex = newIndex;
                    flag = true;
                    break;
                }
                else
                {
                    // if not match, the new index replace the pointer value of final or start index then start over with the loop
                    if (string.Compare(wikiEntry[newIndex, 0], searchtext) == 1)
                        finalIndex = newIndex;
                    else
                        startIndex = newIndex;
                }
            }
            if (flag)
            {
                // when flag is TRUE, show wiki at found index to the textboxes, highlight the the correct entry on the list view, and show on status strip
                textName.Text = wikiEntry[foundIndex, 0];
                textCategory.Text = wikiEntry[foundIndex, 1];
                textStructure.Text = wikiEntry[foundIndex, 2];
                textDefinition.Text = wikiEntry[foundIndex, 3];

                dataListView.SelectedItems.Clear();
                dataListView.Items[foundIndex].Selected = true;
                dataListView.Select();
                stStripArrayFound();
            }
            else
            {
                // if after looping, flag remains FALSE, show search not found on the status strip
                stStripArrayNotFound();
            }
        }
        #endregion

        #region Buttons

        private void buttonAdd_Click(object sender, EventArgs e)
        {
            // do add method, clear textboxes, display array, and focus back to the name textbox
            Add();
            ButtonsRoutine();
            textName.Focus();
        }

        private void buttonSearch_Click(object sender, EventArgs e)
        {
            // sort the data before the method, do search method, clear the search textbox
            BubbleSort();
            Search();
            textSearch.Clear();

        }

        private void buttonEdit_Click(object sender, EventArgs e)
        {
            // when listview's entry is selected
            if (selectedlistviewcount > -1)
            {
                // do edit method, clear textboxes, display array, reset the listview index counter, show status strip
                Edit();
                ButtonsRoutine();
                selectedlistviewcount = -1;
                stStripArrayEdit();

            }
            else
            {
                // show status strip
                stStripArrayEmpty();
            }
        }
        private void buttonDelete_Click(object sender, EventArgs e)
        {
            // when listview's entry is selected
            if (selectedlistviewcount > -1)
            {
                // do message box to warn user
                DialogResult result = MessageBox.Show("Do you want to delete the selected entry?", "Delete", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                if (result == DialogResult.Yes)
                {
                    // do delete method, clear textboxes, display array, reduce pointer value, reset listview index, show status strip
                    Delete();
                    ButtonsRoutine();
                    ptr--;
                    selectedlistviewcount = -1;
                    stStripArrayDelete();
                }
                else
                {
                    // clear textboxes, display array, reset listview index
                    ButtonsRoutine();
                    selectedlistviewcount = -1;
                }

            }
        }

        private void buttonSort_Click(object sender, EventArgs e)
        {
            // do sort method, display array, show status strip
            BubbleSort();
            DisplayList();
            stStripArrayReset();
        }


        private void buttonLoad_Click(object sender, EventArgs e)
        {
            // create open file dialog, load binary file into the wiki entry, then display array
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
            // create save file dialog, save wiki entry to a binary file with default name "definitions.dat"
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
