using Foundation;
using System;
using UIKit;
using Hazzat.Service;
using System.Collections.Generic;

namespace Hazzat.iOS
{
    public partial class SectionViewController : UITableViewController
    {
        private DataSource _dataSource;
        private HazzatController _controller;
        public int id { get; set; }

        public SectionViewController(IntPtr handle) : base (handle)
        {
            Title = NSBundle.MainBundle.LocalizedString("Section", "Section Page");
        }

        void AddNewItem(object sender, EventArgs args)
        {
            using (var indexPath = NSIndexPath.FromRowSection(0, 0))
                TableView.InsertRows(new NSIndexPath[] { indexPath }, UITableViewRowAnimation.Automatic);
        }
        public void AddNewItems(int length)
        {
            for (var i = 0; i <= length; i++)
            {
                using (var indexPath = NSIndexPath.FromRowSection(i, 0))
                    TableView.InsertRows(new NSIndexPath[] { indexPath }, UITableViewRowAnimation.Automatic);
            }
        }

        public override void DidReceiveMemoryWarning()
        {
            // Releases the view if it doesn't have a superview.
            base.DidReceiveMemoryWarning();

            // Release any cached data, images, etc that aren't in use.
        }

        public override void ViewDidLoad()
        {
            base.ViewDidLoad();

            _controller = new HazzatController();

            NavigationItem.RightBarButtonItem = new UIBarButtonItem(UIImage.FromFile("settings.png")
                , UIBarButtonItemStyle.Plain
                , (ooo, oo) => {

                });

            List<KeyValuePair<int, string>> lst = new List<KeyValuePair<int, string>>();
            _controller.GetSeasonServices(id, (src, data) =>
            {
                foreach (var item in data.Result)
                {
                    
                    lst.Add(new KeyValuePair<int, string>(item.Service_ID,item.Service_Name));
                }
                InvokeOnMainThread(() =>
                {
                    TableView.Source = _dataSource = new DataSource(lst, this);
                });
            });
        }
    }
}
