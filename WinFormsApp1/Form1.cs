using System.Xml.Linq;

namespace WinFormsApp1
{
    public partial class Form1 : Form
    {
        private Dictionary<string, float> _rates = new Dictionary<string, float>();
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            XDocument api = XDocument.Load("http://api.nbp.pl/api/exchangerates/tables/a/?format=xml");

            var rates = api.Descendants("Rate");
            _rates.Clear();
            foreach (var rate in rates)
            {
                string code = rate.Element("Code").Value;
                float mid = float.Parse(rate.Element("Mid").Value, System.Globalization.CultureInfo.InvariantCulture);

                _rates.Add(code, mid);
            }

            textBox1.Text = _rates["USD"].ToString();
            textBox2.Text = _rates["EUR"].ToString();
            textBox3.Text = _rates["CHF"].ToString();
        }

        private void Exchange(object sender, EventArgs e)
        {
            RadioButton radio = sender as RadioButton;
            string code = radio.Text;
            float pln = float.Parse(textBox4.Text);
            float exchanged = pln / _rates[code];
            textBox5.Text = exchanged.ToString();
        }
    }
}