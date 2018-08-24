using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Data;
using System.Data.Odbc;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Security.RightsManagement;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Forms;
using System.Windows.Input;
using System.Windows.Media;
using Microsoft.Win32;
using Supp_xml.Annotations;
using Supp_xml.Models;
using OpenFileDialog = System.Windows.Forms.OpenFileDialog;
using SaveFileDialog = System.Windows.Forms.SaveFileDialog;
using Supp_xml.ViewModels;
using System.Xml;
using System.Xml.Linq;
using NetBike.Xml.Formatting;
using MessageBox = System.Windows.MessageBox;


namespace Supp_xml.ViewModels
{



    public class MainViewModel : BasicViewModel
    {
        #region Members


        private string _fileName;
        private DateTime _endDate;
        private string _box;
        private DateTime _startDate;
        private string _serverName;
        private string fileContents;


        #endregion


        #region Methods


        public string ServerName
        {
            get { return _serverName; }
            set
            {
                _serverName = value;
                RaisePropertyChanged("ServerName");
            }
        }

        public string TextBoxPath
        {
            get { return _box; }
            set
            {
                _box = value;
                RaisePropertyChanged("TextBoxPath");
            }

        }

        public string FileName

        {
            get { return _fileName; }
            set
            {
                _fileName = value;
                RaisePropertyChanged("FileName");
            }
        }

        public DateTime EndDate
        {
            get { return _endDate; }
            set
            {

                _endDate = value;
                RaisePropertyChanged("EndDate");

            }
        }


        public DateTime StartDate
        {
            get { return _startDate; }
            set
            {
                _startDate = value;
                RaisePropertyChanged("StartDate");

            }
        }

        public RelayCommands ClickCommand { get; set; }

        #endregion

        public MainViewModel()
        {
            ClickCommand = new RelayCommands(onClick);
            _startDate = DateTime.Now;
            _endDate = DateTime.Now;
        }



        public void onClick()
        {

            // 1. SQL Abfrage

            var daten = SQL.SqlConnection(_startDate, _endDate, _serverName);

            FolderBrowserDialog sfd = new FolderBrowserDialog();

            if (sfd.ShowDialog() == DialogResult.OK)
            {

                TextBoxPath = sfd.SelectedPath;
                var fs = File.Create(@sfd.SelectedPath + "\\" + _fileName + ".xml");
                fs.Close();

                StreamWriter tw = new StreamWriter(sfd.SelectedPath + "\\" + _fileName + ".xml", true);
                foreach (var a in daten)
                {
                    tw.Write(a.Text);
                }
                tw.Close();
            }

        }

        
       

    }
    }




