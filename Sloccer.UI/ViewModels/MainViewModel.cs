using Sloccer.Core;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using System;

namespace Sloccer.UI.ViewModels
{
    public class MainViewModel : ViewModelBase
    {
        private string _directory;
        private FileViewModel _selectedFile;
        private ProjectViewModel _project;

        public string Directory
        {
            get { return _directory; }
            set
            {
                if (_directory != value)
                {
                    _directory = value;
                    NotifyChange("Directory");
                }
            }
        }
        public ObservableCollection<FileViewModel> Files { get; set; }
        public FileViewModel SelectedFile
        {
            get { return _selectedFile; }
            set
            {
                if (_selectedFile != value)
                {
                    _selectedFile = value;
                    NotifyChange("SelectedFile");
                }
            }
        }
        public ProjectViewModel Project
        {
            get { return _project; }
            set
            {
                if (_project != value)
                {
                    _project = value;
                    NotifyChange("Project");
                }
            }
        }

        public DelegateCommand OpenCommand { get; set; }

        public MainViewModel()
        {
            Files = new ObservableCollection<FileViewModel>();
            OpenCommand = new DelegateCommand(Open);
        }

        private void Open()
        {
            var fbd = new FolderBrowserDialog();
            if (fbd.ShowDialog() == DialogResult.OK)
            {
                Directory = fbd.SelectedPath;
                Files.Clear();

                var files = GetFiles(Directory);
                var results = new List<SlocResult>();
                foreach (var file in files)
                {
                    var code = new FileStringRetriever(file)
                        .GetString();
                    var result = new CSharpSlocAnalyser()
                        .GetSlocFor(code);
                    var fileVm = new FileViewModel
                    {
                        Filename = file.Name
                    };
                    fileVm.Stats.Fill(result);
                    Files.Add(fileVm);
                    results.Add(result);
                }

                Project = new ProjectViewModel
                {
                    RootDirectory = Directory
                };
                Project.Stats.Fill(results);
            }
        }

        private List<FileInfo> GetFiles(string directory)
        {
            var files = new List<FileInfo>();

            files.AddRange(System.IO.Directory
                .GetFiles(directory)
                .Where(f => Path.GetExtension(f) == ".cs")
                .Select(f => new FileInfo(f)));

            foreach (var child in System.IO.Directory.GetDirectories(directory))
            {
                files.AddRange(GetFiles(child));
            }

            return files;
        }
    }
}
