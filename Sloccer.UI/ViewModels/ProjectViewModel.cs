namespace Sloccer.UI.ViewModels
{
    public class ProjectViewModel : ViewModelBase
    {
        private string _rootDirectory;
        private StatsViewModel _stats;

        public string RootDirectory
        {
            get { return _rootDirectory; }
            set
            {
                if (_rootDirectory != value)
                {
                    _rootDirectory = value;
                    NotifyChange("RootDirectory");
                }
            }
        }

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

        public ProjectViewModel()
        {
            Stats = new StatsViewModel();
        }
    }
}
