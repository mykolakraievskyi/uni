using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using CabFlow.Core;
using CabFlow.ViewModel.Drivers;
using CabFlow.ViewModel.Orders;
using CabFlow.ViewModel.Vehicles;

namespace CabFlow.Services
{
    public class TabService : ObservableObject
    {
        public TabItem SelectedTab
        {
            get => _selectedTab;
            set { _selectedTab = value; OnPropertyChanged(); }
        }
        public ObservableCollection<TabItem> Tabs => _tabs;

        private TabItem _selectedTab;
        private readonly ObservableCollection<TabItem> _tabs = new();

        public void AddTab(string header, Core.ViewModel content)
        {
            _tabs.Add(new TabItem()
            {
               Header = header,
               Content = new Frame()
               {
                   Content = content
               }
            });
        }

        public void OpenTab(string header)
        {
            var tab = _tabs.FirstOrDefault(t => t.Header.ToString() == header);
            if (tab != null)
            {
                SelectedTab = tab;
            }
        }

        public void AddOrOpenTab(string header, Core.ViewModel content)
        {
            var tab = _tabs.FirstOrDefault(t => t.Header.ToString() == header);
            if (tab == null)
            {
                AddTab(header, content);
                tab = _tabs.FirstOrDefault(t => t.Header.ToString() == header);
            }
            SelectedTab = tab;
        }

        public void RemoveTab(string header) {
            var tab = _tabs.FirstOrDefault(t => t.Header.ToString() == header);
            if (tab != null)
            {
                _tabs.Remove(tab);
            }
        }

    }
}
