using Anubis.System;
using Anubis.System.UI;

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ExCore.OffsetsStore
{
    public partial class Welcome : Form, UIElement
    {
        public Welcome()
        {
            InitializeComponent();
        }

        public bool m_IsActive { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public bool m_IsNeedUpdate { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

        public Form GetParent()
            => this;

        public void OnDestroy()
        {
            throw new NotImplementedException();
        }

        public void OnHide()
        {
            throw new NotImplementedException();
        }

        public void OnLoading()
        {
            throw new NotImplementedException();
        }

        public void OnVisible()
        {
            throw new NotImplementedException();
        }

        public void SetActive( bool active )
        {
            throw new NotImplementedException();
        }

        public void SetNeedUpdate( bool needUpdate )
        {
            throw new NotImplementedException();
        }
    }
}
