using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZippyShareDownloader.Model
{
    class LinksItem:BindableBase
    {
        private string _serviceLink;
        private string _downloadGroup;
        private DownloadStatus _status;
        private string _fileName;

        public string ServiceLink
        {
            get => _serviceLink;
            set => SetProperty(ref _serviceLink , value);
        }
        public string DownloadGroup
        {
            get => _downloadGroup;
            set => SetProperty(ref _downloadGroup, value);
        }
        public DownloadStatus Status { get => _status; set => SetProperty(ref _status , value); }
        public string FileName { get => _fileName; set => SetProperty(ref _fileName, value); }


        public LinksItem(string serviceLink)
        {
            ServiceLink = serviceLink;
        }


    }
}
