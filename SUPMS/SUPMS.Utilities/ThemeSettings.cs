using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SUPMS.Infrastructure.Utilities
{
    public class ThemeSettings
    {
        public ThemeSettings()
        {
            this.Color = "default";
            this.SideBarClosed = "1";
            this.layout = new LayOut();
        }
        public string Color { get; set; }
        public string SideBarClosed { get; set; }
        public LayOut layout { get; set; }
    }

    public class LayOut
    {
        public LayOut()
        {
            LayoutOption = "fluid";
            HeaderOption = "fixed";
            FooterOption = "fixed";
            SidebarPosOption = "left";
            SidebarStyleOption = "light";
            SidebarMenuOption = "accordion";
            HeaderTopDropdownStyle = "light";
        }
        public string LayoutOption{get;set;}
        public string HeaderOption{get;set;}
        public string FooterOption{get;set;}
        public string SidebarPosOption{get;set;}
        public string SidebarStyleOption{get;set;}
        public string SidebarMenuOption{get;set;}
        public string HeaderTopDropdownStyle{get;set;}
    }
}
