using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MockupProject_K30_DotNet.ViewModel
{
    public class TabItem : ViewModelBase
    {
        public string Header { get; set; }
        public string Content { get; set; }
        public string closeButtonVisibility { get; set; }
        public string CloseButtonVisibility
        {
            get
            {
                return closeButtonVisibility;
            }
            set
            {
                closeButtonVisibility = value;
                NotifyPropertyChanged("CloseButtonVisibility");
            }
        }
    }
}
