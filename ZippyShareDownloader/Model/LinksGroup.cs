using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZippyShareDownloader.Html;
using ZippyShareDownloader.util;

namespace ZippyShareDownloader.Model
{
    class LinksGroup:BindableBase
    {
        private bool _isEnded = false;
        private bool _isDecompressedAfter;
        private string _name;
        private string _downloadPath;
        ObservableCollection<LinksItem> _links;

        public bool IsDecompressedAfter { get => _isDecompressedAfter; set => SetProperty(ref _isDecompressedAfter, value); }
        public string Name { get => _name; set => SetProperty(ref _name, value); }
        public ObservableCollection<LinksItem> Links{ get => _links; set => SetProperty(ref _links, value); }
        public string DownloadPath { get => _downloadPath; set => SetProperty(ref _downloadPath, value); }

        public LinksGroup(string name, bool isDecompressedAfter)
        {
            Name = name;
            IsDecompressedAfter = isDecompressedAfter;
        }
        public LinksGroup(List<string> LinksUrl, string name, bool isDecompressedAfter):this(name,isDecompressedAfter)
        {
            Links = ParseUrlToLinks(LinksUrl);
        }
        private ObservableCollection<LinksItem> ParseUrlToLinks(List<string> LinksUrl)
        {
            var temp = new ObservableCollection<LinksItem>();
            foreach (var s in LinksUrl)
            {
                if (s.Equals(string.Empty)) continue;
                var link = s;
                if (!link.StartsWith(HtmlFactory.Http) && !link.StartsWith(HtmlFactory.Https))
                {
                    link = HtmlFactory.Http + s;
                }               
                var dream = new LinksItem(link);
                temp.Add(dream);
            }
            return temp;
        }
        public void Refresh()
        {
            if (_isEnded) return;
            var everyone = Links.Aggregate(true,
                (current, downloadEntity) => current && downloadEntity.Status == DownloadStatus.Completed);

            if (!everyone) return;
            Decompress();
            _isEnded = true;
        }
        private void Decompress()
        {
            var file = DownloadPath + Links.OrderBy(e => e.FileName).First().FileName;
            ArchiveUtil.UnpackArchive(file, Directory.GetParent(file).FullName);
        }
    }
}
