using System;
using System.Diagnostics;
using System.Windows.Forms;
using EasyTabs;

namespace WindowsFormsApp1
{
    public partial class AppCointainer : TitleBarTabs
    {
        public AppCointainer()
        {
            InitializeComponent();


            AeroPeekEnabled = true;
            TabRenderer = new ChromeTabRenderer(this);
            
        }

        public override TitleBarTab CreateTab()
        {
            return new TitleBarTab(this)
            {
                Content = new Form6
                {
                    Text = "New Tab"
                }
            };
        }



        private void AppCointainer_Load(object sender, EventArgs e)
        {
            
        }

        

        
        private void AppCointainer_FormClosed(object sender, FormClosedEventArgs e)
        {

            Application.ExitThread();
        }
    }
}
