namespace Anubis.System.UI.Behaviours
{
    public class UIBehaviourInvoker : ExBehaviour
    {
        private bool m_ExitRequested = false;

        public override void Update()
        {
            if ( m_ExitRequested )
            {
                ParentObject.Destroy();
                return;
            }

            var uiObject = Unbox<UIObject>( ParentObject );
            if(uiObject != null)
            {
                var elements = uiObject.Elements();
                if(elements != null)
                {
                    foreach(var element in elements)
                    {
                        var val = element.Value;
                        if ( val.IsNeedUpdate() )
                        {
                            switch ( val.GetState() )
                            {
                                case UIElementState.Hidden:
                                    {
                                        uiObject.Execute<UIElement>( val.GetGuid(), ( obj ) =>
                                        {
                                            obj.OnHide();
                                        }, 
                                        out _ );

                                        break;
                                    }

                                case UIElementState.Visible:
                                    {
                                        uiObject.Execute<UIElement>( val.GetGuid(), ( obj ) =>
                                        {
                                            obj.OnVisible();
                                        },
                                        out _ );

                                        break;
                                    }

                                case UIElementState.Loading:
                                    {
                                        uiObject.Execute<UIElement>( val.GetGuid(), ( obj ) =>
                                        {
                                            obj.OnLoading();
                                        },
                                        out _ );

                                        break;
                                    }
                            }
                        }
                    }
                }
            }
        }

        public void Exit( bool val )
            => m_ExitRequested = val;
    }
}
