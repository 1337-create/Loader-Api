using System.Windows.Forms;

namespace Anubis.System.UI
{
    public interface UIElement
    {
        bool m_IsActive { get; set; }
        bool m_IsNeedUpdate { get; set; }

        void SetActive( bool active );
        void SetNeedUpdate( bool needUpdate );

        void OnHide();
        void OnVisible();
        void OnLoading();
        void OnDestroy();

        Form GetParent();
    }
}
