using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Net;
using System.Text.RegularExpressions;

namespace CompanyInfo
{
    public partial class Form1 : Form
    {
        string uri = "https://wwwinfo.mfcr.cz/cgi-bin/ares/darv_res.cgi";

        public Form1()
        {
            InitializeComponent();
        }

        private void search_Click(object sender, EventArgs e)
        {
            string DataOutput = null;
            int i = 0;

            string urlData = String.Empty;
            WebClient wc = new WebClient();
            
            wc.Encoding = Encoding.UTF8;
            wc.Encoding = UTF8Encoding.UTF8;

            wc.Headers.Add("User-Agent", "Mozilla/5.0 (Windows; U; Windows NT 6.1; en-GB; rv:1.9.2.12) Gecko/20101026 Firefox/3.6.12");
            wc.Headers.Add("Accept", "*/*");
            wc.Headers.Add("Accept-Language", "en-gb,en;q=0.5");
            wc.Headers.Add("Accept-Charset", "ISO-8859-1,utf-8;q=0.7,*;q=0.7");

            urlData = wc.DownloadString(uri+"?ico=" + textBox.Text + "&jazyk=cz&xml=1");

            string[] tag = { "ICO", "OF", "N", "NU", "CD", "PSC", "NPF", "Nazev_NACE", "KPP", "Zuj_kod_orig", "NZUJ" };
            string[] label = { "IČ:", "Firma:", "Město:", "Ulice:", "ČP:", "PSČ:", "Právní forma:", "Ekonomické činnosti:\n", "velikostní kat. dle počtu zam.:", "ZÚJ kód:", "ZÚJ:" };

            foreach (string data in tag)
            {
                Regex rx = new Regex(@"<D:"+ data + ">(.*)<", RegexOptions.Compiled | RegexOptions.IgnoreCase);
                
                DataOutput += label[i]+" ";

                foreach (Match match in rx.Matches(urlData))
                    DataOutput += match.Groups[1].Value + "\n";

                DataOutput += "\n";
                i++;
            }

            dataOut.Text = DataOutput;

        }
    }
}
