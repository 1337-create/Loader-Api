using System;

namespace Anubis.System.UI
{
    public class UIElementWrapper
    {
        private Guid m_InternalGuid;
        private string m_Name;
        private UIElement m_BoxedElement;
        private UIElementState m_State;

        public UIElementWrapper(string name, UIElement element)
            : this(name, element, UIElementState.Loading)
        { }
        public UIElementWrapper(string name, UIElement element, UIElementState state)
            : this(name, element, state, Guid.NewGuid())
        { }
        public UIElementWrapper(string name, UIElement element, UIElementState state, Guid guid)
        {
            m_InternalGuid = guid;
            m_Name = name;
            m_BoxedElement = element;
            m_State = state;
        }

        public void Destroy()
            => m_BoxedElement.OnDestroy();

        public T Unbox<T>() where T : UIElement
            => ( T )m_BoxedElement;
        public UIElement GetBoxed()
            => m_BoxedElement;

        public UIElementState GetState()
            => m_State;
        public void SetState( UIElementState state )
            => m_State = state;

        public void SetName( string name )
            => m_Name = name;
        public string GetName()
            => m_Name;

        public void SetGuid( Guid guid )
            => m_InternalGuid = guid;
        public Guid GetGuid()
            => m_InternalGuid;

        public void SetActive( bool active )
            => m_BoxedElement.SetActive( active );
        public bool IsActive()
            => m_BoxedElement.m_IsActive;

        public void SetNeedUpdate( bool needUpdate )
            => m_BoxedElement.SetNeedUpdate( needUpdate );
        public bool IsNeedUpdate()
            => m_BoxedElement.m_IsNeedUpdate;
    }
}
