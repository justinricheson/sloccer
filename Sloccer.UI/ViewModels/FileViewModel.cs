namespace Sloccer.UI.ViewModels
{
    public class FileViewModel : ViewModelBase
    {
        private string _filename;
        public string Filename
        {
            get { return _filename; }
            set
            {
                if (_filename != value)
                {
                    _filename = value;
                    NotifyChange("Filename");
                }
            }
        }

        private StatsViewModel _stats;
        public StatsViewModel Stats
        {
            get { return _stats; }
            set
            {
                if (_stats != value)
                {
                    _stats = value;
                    NotifyChange("Stats");
                }
            }
        }

        public FileViewModel()
        {
            Stats = new StatsViewModel();
        }
    }
}
