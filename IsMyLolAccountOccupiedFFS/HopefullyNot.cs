using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Net.Http;
using System.Xml.Serialization;
using System.Xml;

namespace IsMyLolAccountOccupiedFFS
{
    class HopefullyNot : Form 
    {

        private NotifyIcon trayIcon;
        private ContextMenu trayMenu;
        private Icon positiveIcon;
        private Icon negativeIcon;

        private Thread thread;
        private bool running = false;

        private HttpClient client;
        private String host;
        private String url;

        public HopefullyNot()
        {
            Shown += FormLoaded;
            
            trayMenu = new ContextMenu();
            trayMenu.MenuItems.Add("Lol Nexus", OnNexus);
            trayMenu.MenuItems.Add("Exit", OnExit);

            trayIcon = new NotifyIcon();
            trayIcon.Text = "League of Legends Occupied";
            trayIcon.Icon = new Icon(SystemIcons.Application , 40, 40);

            positiveIcon = Properties.Resources.positive1;
            negativeIcon = Properties.Resources.negative1;

            trayIcon.ContextMenu = trayMenu;
            trayIcon.Visible = true;

            client = new HttpClient();
            host = @"http://tdegroot.nl";
            url = @"/api/LolOccupied/";

            running = true;
            thread = new Thread(new ThreadStart(run));
            thread.Start();
            
        }

        private void FormLoaded(Object sender, EventArgs e)
        {
            Hide();
        }

        public void run()
        {
            while (running)
            {
                Console.WriteLine("Woo");
                LolNexusResult result = pingChampion();
                if (result == null) continue;
                if (!result.successful)
                {
                    trayIcon.Icon = positiveIcon;
                }
                else
                {
                    trayIcon.Icon = negativeIcon;
                }
                
                Thread.Sleep(5000);
            }
            trayIcon.Dispose();
            Application.Exit();
        }

        private LolNexusResult pingChampion()
        {
            LolNexusResult result = null;
            try {
                XmlSerializer serializer = new XmlSerializer(typeof(LolNexusResult));
                XmlReader xmlReader = XmlReader.Create(host + url);
                result = (LolNexusResult)serializer.Deserialize(xmlReader);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.StackTrace);
            }

            return result;
        }

        private void OnNexus(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start("http://www.lolnexus.com/EUW/search?name=krindle&region=euw");
        }

        private void OnExit(object sender, EventArgs e)
        {
             running = false;           
        }



    }

}
