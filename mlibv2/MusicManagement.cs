﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Configuration;
using System.Data.SqlClient;

///
using System.IO;
using MusicProjectLibrary_1;
using MaterialSkin;
using Dapper;
using System.Threading.Tasks;
/// <summary>
/// 
/// </summary>
namespace MusicProjectLibrary_1
{
    public partial class MusicLibraryWindow : Form //MaterialSkin.Controls.MaterialForm
    {
        List<SQLAlbumTable> AlbumList = new List<SQLAlbumTable>(); //sqlprzemy - table : Albums
        List<SQLTrackTable> TrackList = new List<SQLTrackTable>(); //sqlprzemy - table : Tracks
        public int AlbumRowIndex = 0;
        public MusicLibraryWindow()
        {

            InitializeComponent();     
        }


        private void Form1_Load(object sender, EventArgs e)
        {
            Functions.getPurgatoryPath(tbxPickedPath,true);
            Functions.getMainDirectoryPath(tbxMusicPath, true);
            

            int countRecordTracks = RefreshSpecificTable(1);
            BoxListConsole.Items.Add("Tracks count: " + countRecordTracks.ToString());
            SQLDataValidate.bw_ReadDataGridForAll(sender, AlbumsDataGridView, BoxListConsole.Items);

            int countRecordAlbums = RefreshSpecificTable(2);
            BoxListConsole.Items.Add("Album count: " + countRecordAlbums.ToString());

            int countRecordArtist = RefreshSpecificTable(3);
            BoxListConsole.Items.Add("Artist table updated: " + countRecordArtist.ToString());

            BoxListConsole.SelectedIndex = BoxListConsole.Items.Count - 1;
            GlobalVariables globalProcCatalog = new GlobalVariables();
        }
        public void checkFilesInDirectory_Click(object sender, EventArgs e)
        {
            Functions.pickPath(1, tbxPickedPath);
            //Functions.showFolderBrowserDialog(ListBoxItems.Items);
        }
        private void btnChangeGeneralCatalogPath_Click(object sender, EventArgs e)
        {
            Functions.pickPath(2, tbxMusicPath);
        }
        private void idAlbumLabel_Click(object sender, EventArgs e)
        {

        }
        private void release_DateDateTimePicker_ValueChanged(object sender, EventArgs e)
        {

        }
        private void nameTextBox_TextChanged(object sender, EventArgs e)
        {

        }
        private void idAlbumTextBox_TextChanged(object sender, EventArgs e)
        {

        }
        private void favourite_Tracks_NoTextBox_TextChanged(object sender, EventArgs e)
        {

        }
        private void nameLabel_Click(object sender, EventArgs e)
        {

        }
        private void favourite_Tracks_NoLabel_Click(object sender, EventArgs e)
        {

        }
        private void release_DateLabel_Click(object sender, EventArgs e)
        {

        }
        private void albumListBindingNavigatorSaveItem_Click_1(object sender, EventArgs e)
        {
            
        }  

        private void moveFileButt_Click(object sender, EventArgs e)
        {
            
        }  
        private void ButtonReadTag_Click(object sender, EventArgs e)
        {
            var watch = System.Diagnostics.Stopwatch.StartNew();
            GlobalChecker.TestSqlAlbumIdQuery = 0;
            MusicFileMgt.readFiles(BoxListConsole.Items, progBar, tbxPickedPath.Text, lblProgress);

            int countRecordAlbum = DBFunctions.AutoSearchDatabaseAlbums("", AlbumsDataGridView);
            int countRecordArtist = DBFunctions.AutoSearchDatabaseArtists("", dgvArtists);
            BoxListConsole.Items.Add("Album table updated: " + countRecordAlbum.ToString());
            BoxListConsole.Items.Add("Artist table updated: " + countRecordArtist.ToString());
            BoxListConsole.SelectedIndex = BoxListConsole.Items.Count - 1;

            BoxListConsole.Items.Add("Total SQL query [album id] = " + GlobalChecker.TestSqlAlbumIdQuery.ToString());
            watch.Stop();
            double elapsedMs = watch.ElapsedMilliseconds;
            BoxListConsole.Items.Add("..........processing done in time: " + Math.Round(elapsedMs / 1000, 2) + "s.");
            BoxListConsole.SelectedIndex = BoxListConsole.Items.Count - 1;
        }        
        private void ButtonReadDir_Click(object sender, EventArgs e)
        {
            
        }

        private void CheckBoxProcessCatalogs_CheckedChanged(object sender, EventArgs e)
        {
            if (CheckBoxProcessCatalogs.Checked == true)
                GlobalVariables.globalProcessCatalog = true;            
            else            
                GlobalVariables.globalProcessCatalog = false;
            
        }
        private void CheckBoxModifyFIles_CheckedChanged(object sender, EventArgs e)
        {
            if (CheckBoxProcessCatalogs.Checked == true)
                GlobalVariables.globalModifyFIles = true;
            else
                GlobalVariables.globalModifyFIles = false;
        }
        private void button1_Click(object sender, EventArgs e)
        {          
        }        

        private void btnNew_Click(object sender, EventArgs e)
        {
            try
            {
                //musicLibraryDataSet.Track.AddTrackRow(musicLibraryDataSet.Track.NewTrackRow());
                musicLibraryDataSetBindingSource.MoveLast();
                
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message, "Message", MessageBoxButtons.OK, MessageBoxIcon.Error);
                //musicLibraryDataSet.RejectChanges();
            }
        }
        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                
                Validate();
                musicLibraryDataSetBindingSource.EndEdit();

                MessageBox.Show("updated");


            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Message", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }        
        private void btnDelete_Click(object sender, EventArgs e)
        {

        }

        private void dataGridView_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)13)
            {

            }
        }
        private void btnFindAlbums_Click(object sender, EventArgs e)
        {
            int countRecord = RefreshSpecificTable(1);
            BoxListConsole.Items.Add("Album count: " + countRecord.ToString());
            BoxListConsole.SelectedIndex = BoxListConsole.Items.Count - 1;
            
        }         
        private void btnDeleteArtistFromDB_Click(object sender, EventArgs e)
        {
            DBFunctions db = new DBFunctions();
            db.DeleteAlbumTableContent_Parameter(tbxDeleteArtistFromDB.Text);
     
        }       
        private void btnFindTracks_Click(object sender, EventArgs e)
        {
            int countRecord = RefreshSpecificTable(2);
            BoxListConsole.Items.Add("Tracks count: " + countRecord.ToString());
            BoxListConsole.SelectedIndex = BoxListConsole.Items.Count - 1;            
        }     
        private void CheckBoxWriteIndexes_CheckedChanged(object sender, EventArgs e)
        {
            if (CheckBoxWriteIndexes.Checked == true)
                GlobalVariables.globalwriteIndexes = true;
            else
                GlobalVariables.globalwriteIndexes = false;
        }                   
        
        ////////////////////////////////////////////////////////DGV[1]////////////////////////////////////////
        private void AlbumsDataGridView_DataBindingComplete_1(object sender, DataGridViewBindingCompleteEventArgs e)
        {
            //AlbumsDataGridView.Sort(this.AlbumsDataGridView.Columns["AlbumDirectory"], ListSortDirection.Ascending); //[przemy knowledge - sorting datagridview]
            foreach (DataGridViewRow r in AlbumsDataGridView.Rows)
            {
                r.Cells["AlbumDirectory"] = new DataGridViewLinkCell();
            }
            foreach (DataGridViewColumn c in AlbumsDataGridView.Columns)
            {
                SQLDataValidate.dataGridColumns DGC = new SQLDataValidate.dataGridColumns();
                if (c.Index == DGC.colAlbumDirectory || c.Index == DGC.colDirectoryGenre || c.Index == DGC.colAlbumGeneralGenre
                    || c.Index == DGC.colArtistCheck || c.Index == DGC.colAlbumCheck || c.Index == DGC.colGenreCheck || c.Index == DGC.colRatingCheck || c.Index == DGC.colIndexCheck)
                    c.AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;

            }
        }        
        private void AlbumsDataGridView_DoubleClick(object sender, EventArgs e)
        {
            AlbumsDataGridView.Rows[AlbumsDataGridView.SelectedCells[0].RowIndex].Selected = true;
            AlbumRowIndex = AlbumsDataGridView.SelectedCells[0].RowIndex;
        }
        private void AlbumsDataGridView_CellContentDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            PickGenre pickGenreForm = new PickGenre();
            DirectoryManagement.DoubleClickOnGridCallBack(AlbumsDataGridView, pickGenreForm, BoxListConsole, AlbumRowIndex);
            SQLDataValidate.bw_ReadDataGridForAll(sender, AlbumsDataGridView, BoxListConsole.Items);
        }
        private void AlbumsDataGridView_SelectionChanged_1(object sender, EventArgs e)
        {
            if (AlbumsDataGridView.SelectedCells.Count > 0)
            {
                AlbumRowIndex = AlbumsDataGridView.SelectedCells[0].RowIndex; //[knowledge get row index from data grid view]
                GlobalVariables.globalSelectedGridAlbumID = (int)AlbumsDataGridView[0, AlbumRowIndex].Value; //[knowledge get value from specific column in datagrid view]                
            }
        }
        private void AlbumsDataGridView_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            PickGenre pickGenreForm = new PickGenre();
            int masterRow = AlbumRowIndex;
            if (DirectoryManagement.DoubleClickOnGridCallBack(AlbumsDataGridView, pickGenreForm, BoxListConsole, AlbumRowIndex))
            {
                RefreshSpecificTable(1);
                SQLDataValidate.bw_ReadDataGridForAll(sender, AlbumsDataGridView, BoxListConsole.Items);
                AlbumsDataGridView.ClearSelection();                               //[przemy knowledge - zaznaczanie data grid view]
                AlbumsDataGridView.CurrentCell = AlbumsDataGridView.Rows[masterRow].Cells[0]; //[przemy knowledge - zaznaczanie data grid view]
                AlbumsDataGridView.Rows[masterRow].Selected = true;            //[przemy knowledge - zaznaczanie data grid view]
            }
        }
        private void AlbumsDataGridView_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
            ////////////////////////////////////////////////////////DGV[1][AboveButtons]////////////////////////////////////////
            private void btnCheckTrackDuplicates_Click(object sender, EventArgs e)
            {

                duplicates duplicatesForm = new duplicates();
                PassConsole lstbxConsole = new PassConsole();
                lstbxConsole.listboxConsole = BoxListConsole;
                duplicatesForm.ShowDialog();

            }
            private void btnDeleteAlbum_Click(object sender, EventArgs e)
            {
                DirectoryManagement.DeleteAlbum(AlbumsDataGridView, AlbumRowIndex);
            }
            private void btnDeclareGenre_Click_1(object sender, EventArgs e)
            {
                PickGenre pickGenreForm = new PickGenre();
                pickGenreForm.ShowDialog();

                int countRecord = RefreshSpecificTable(1);
                AlbumsDataGridView.Rows[AlbumRowIndex].Selected = true;
                //
            }
        private void btnProcessSelected_Click(object sender, EventArgs e)
        {

            //validuj SQL'a
            AlbumRowIndex = AlbumsDataGridView.SelectedCells[0].RowIndex;
            int hardIndex = AlbumRowIndex;
            SQLDataValidate.ReadDataGrid(AlbumsDataGridView, BoxListConsole.Items, tbxPickedPath, tbxMusicPath, AlbumRowIndex);

            int countRecord = DBFunctions.AutoSearchDatabaseAlbums("", AlbumsDataGridView);
            BoxListConsole.Items.Add("Album table updated: " + countRecord.ToString());
            BoxListConsole.SelectedIndex = BoxListConsole.Items.Count - 1;

            AlbumsDataGridView.ClearSelection();                                                //[przemy knowledge - zaznaczanie data grid view]
            AlbumsDataGridView.CurrentCell = AlbumsDataGridView.Rows[hardIndex].Cells[0]; //[przemy knowledge - zaznaczanie data grid view]
            AlbumsDataGridView.Rows[hardIndex].Selected = true;                         //[przemy knowledge - zaznaczanie data grid view]

            SQLDataValidate.bw_ReadDataGridForAll(sender, AlbumsDataGridView, BoxListConsole.Items);
            // aktualizuj tabele tracks - ze usunieto plik / aktualizuj, również że istnieje plik
            //rzuć info ile wyjebał MB z dysku

        }
        ////////////////////////////////////////////////////////DGV[2]////////////////////////////////////////
        private void TracksDataGridView_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
        ////////////////////////////////////////////////////////DGV[3]////////////////////////////////////////

        
        
        private void checkBoxCreateBackUp_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBoxCreateBackUp.Checked == true)
                GlobalVariables.globalCreateBackup = true;
            else
                GlobalVariables.globalCreateBackup = false;

        }
        private void btnDiscogs_Click(object sender, EventArgs e)
        {

            //string SearchArtist = "";
            //string SearchRelease = "";
            //DiscogsManagement.startDiscogs(BoxListConsole.Items, SearchArtist, SearchRelease, 0);
            //this.Show();
        }       
        private void btnSelectHealthy_Click(object sender, EventArgs e)
        {
            BoxListConsole.Items.Add("Start checking...");
            SQLDataValidate.ReadDataGridForAll(AlbumsDataGridView, BoxListConsole.Items);
        }
        ////////////////////////////////////////////////////////INTERNAL FUNCTIONS////////////////////////////////////////
        private int RefreshSpecificTable(int RefreshTableNo)
        {
            int counter = 0;
            switch (RefreshTableNo)
            {
                case 1:
                    counter = DBFunctions.AutoSearchDatabaseAlbums(tbxSearchAlbums.Text, AlbumsDataGridView);
                    return counter;
                case 2:
                    counter = DBFunctions.AutoSearchDatabaseTracks(tbxSearchTracks.Text, TracksDataGridView);
                    return counter;
                case 3:
                    counter = DBFunctions.AutoSearchDatabaseArtists("", dgvArtists);
                    return counter;
            }
            return counter;
        }

        
    }
}
