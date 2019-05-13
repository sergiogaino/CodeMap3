using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using CodeMap.Domain.Entities;
using CodeMap.Domain.Services;
namespace CodeMap.UI
{
    public partial class CodeMap : Form
    {
        public CodeMap()
        {
            InitializeComponent();
        }

        private void CodeMap_Load(object sender, EventArgs e)
        {
            Domain.Services.CodeFile svccf = new Domain.Services.CodeFile(@"C:\testeapp");

            Domain.Entities.CodeFile cf = new Domain.Entities.CodeFile(@"C:\testeapp\lala.asp", true, svccf, 0);

            MessageBox.Show(cf.content[0]);
        }
    }
}
