using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Input;

namespace OpenFileDialogWPF.ViewModel
{
    public class MainViewModel:BaseViewModel
    {
        //This source code named "BaseViewModel" is reference from https://www.howkteam.vn
        public ICommand BrowseCommand { get; set; }

        private string _path;
        public string path { get => _path; set { _path = value;OnPropertyChanged(); } }
        public MainViewModel()
        {

            BrowseCommand = new RelayCommand<object>(
                (p) =>
                {
                    return true;
                },
                (p) =>
                {
                    Microsoft.Win32.OpenFileDialog dlg = new Microsoft.Win32.OpenFileDialog();

                    // Set filter for file extension and default file extension 
                    dlg.Filter = "Image files (*.png;*.jpeg)|*.png;*.jpeg|All files (*.*)|*.*";


                    // Display OpenFileDialog by calling ShowDialog method 
                    Nullable<bool> result = dlg.ShowDialog();


                    // Get the selected file name and display in a TextBox 
                    if (result == true)
                    {
                        //-----------------get the url path of project
                        // This will get the current WORKING directory (i.e. \bin\Debug)
                        string workingDirectory = Environment.CurrentDirectory;
                        // or: Directory.GetCurrentDirectory() gives the same result

                        // This will get the current PROJECT directory
                        string projectDirectory = Directory.GetParent(workingDirectory).Parent.FullName;
                        
                        //---------------- Open document -----------------------
                        string source = dlg.FileName;
                        // file name to save to database if you want
                        string FileName = Path.GetFileName(source);
                        // path to save file in project
                        string dest = projectDirectory+ @"\Images\" + FileName;
                        // copy file to our project [note: still note check same file]
                        File.Copy(source, dest);
                        path = "You've saved file in your project from " + source;
                    }

                }
                );
        }
    }
}
