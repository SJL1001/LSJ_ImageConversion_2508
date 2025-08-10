    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Data;
    using System.Drawing;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using System.Windows.Forms;

    namespace ImageConversion
    {
        public partial class FlipProp : UserControl
        {
            public bool FlipX => flipXbutton.Checked;
            public bool FlipY => flipYbutton.Checked;
            public bool FlipXY => flipXYbutton.Checked;
            public FlipProp()
            {
                InitializeComponent();
            }
      
        }
    }
