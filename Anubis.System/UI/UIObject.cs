using Anubis.System.Attributes;
using Anubis.System.UI.Behaviours;

using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Anubis.System.UI
{
    [RequiredBehaviour(typeof(UIBehaviourInvoker))]
    public class UIObject : ExObject
    {
        private ConcurrentDictionary<Guid, UIElementWrapper> m_CachedElements;

        public UIObject()
            : base()
        {
            InternalInitiaize();
        }

        public UIObject(string tag)
            : base(tag)
        {
            InternalInitiaize();
        }

        public override void Destroy()
        {
            Task.WaitAll( m_CachedElements.ForEachAsync( ( pair ) => pair.Value.Destroy() ) );

            m_CachedElements.Clear();
        }

        private void InternalInitiaize()
        {
            m_CachedElements = new ConcurrentDictionary<Guid, UIElementWrapper>();
        }

        public void Execute<T>(Action<T> act, out Exception catchedException) where T : UIElement
        {
            var item = Get<T>();
            if(item != null)
            {
                var parent = item.GetParent();
                if(parent != null)
                {
                    try
                    {
                        parent.BeginInvoke( ( MethodInvoker )( () =>
                        {
                            act( item );
                        }));

                        catchedException = null;
                    }
                    catch(Exception ex)
                    {
                        catchedException = ex;
                    }
                }
            }

            catchedException = new ArgumentNullException($"UIElement '{typeof(T).FullName}' not found for executing code inside!");
        }
        public void Execute<T>(string name, Action<T> act, out Exception catchedException ) where T : UIElement
        {
            var item = Unbox<T>(name);
            if ( item != null )
            {
                var parent = item.GetParent();
                if ( parent != null )
                {
                    try
                    {
                        parent.BeginInvoke( ( MethodInvoker )( () =>
                        {
                            act( item );
                        } ) );

                        catchedException = null;
                    }
                    catch ( Exception ex )
                    {
                        catchedException = ex;
                    }
                }
            }

            catchedException = new ArgumentNullException( $"UIElement '{typeof( T ).FullName}' not found for executing code inside!" );
        }
        public void Execute<T>( Guid guid, Action<T> act, out Exception catchedException ) where T : UIElement
        {
            var item = Unbox<T>( guid );
            if ( item != null )
            {
                var parent = item.GetParent();
                if ( parent != null )
                {
                    try
                    {
                        parent.BeginInvoke( ( MethodInvoker )( () =>
                        {
                            act( item );
                        } ) );

                        catchedException = null;
                    }
                    catch ( Exception ex )
                    {
                        catchedException = ex;
                    }
                }
            }

            catchedException = new ArgumentNullException( $"UIElement '{typeof( T ).FullName}' not found for executing code inside!" );
        }

        public T Get<T>() where T : UIElement
        {
            var item = m_CachedElements.FirstOrDefault( ( x ) => x.Value.Unbox<T>() != null );
            if ( item.IsNull() )
            {
                return default;
            }

            return item.Value.Unbox<T>();
        }
        public UIElementWrapper Wrapper<T>() where T : UIElement
        {
            var item = Get<T>();
            if ( item != null )
            {
                return m_CachedElements.FirstOrDefault( ( x ) => x.Value.GetBoxed() == ( UIElement )item ).Value;
            }

            return null;
        }
        
        public T Unbox<T>(string name) where T : UIElement
        {
            var item = Element( name );
            if(item != null)
            {
                return item.Unbox<T>();
            }

            return default;
        }
        public T Unbox<T>(Guid guid) where T : UIElement
        {
            var item = Element( guid );
            if ( item != null )
            {
                return item.Unbox<T>();
            }

            return default;
        }
        
        public UIElementWrapper Element( string name )
        {
            var item = GetElementInternal( ( x ) => x.Value.GetName().ToLower() == name.ToLower() );
            if(item.IsNull())
            {
                return null;
            }

            return item.Value;
        }
        public UIElementWrapper Element(Guid guid)
        {
            var item = GetElementInternal( ( x ) => x.Value.GetGuid() == guid );
            if ( item.IsNull() )
            {
                return null;
            }

            return item.Value;
        }

        public bool Cache(UIElement element, string name)
        {
            var item = new UIElementWrapper( name, element );
            if(item != null)
            {
                return m_CachedElements.TryAdd( item.GetGuid(), item );
            }

            return false;
        }
        public bool Cache( UIElement element, string name, UIElementState state )
        {
            var item = new UIElementWrapper( name, element, state );
            if ( item != null )
            {
                return m_CachedElements.TryAdd( item.GetGuid(), item );
            }

            return false;
        }
        public bool Cache( UIElement element, string name, UIElementState state, Guid guid )
        {
            var item = new UIElementWrapper( name, element, state, guid );
            if ( item != null )
            {
                return m_CachedElements.TryAdd( guid, item );
            }

            return false;
        }

        public T CacheOrGet<T>(T obj, string name) where T : UIElement
        {
            var item = Element( name );
            if(item == null)
            {
                if(Cache(obj, name))
                {
                    return CacheOrGet( obj, name );
                }

                return default;
            }

            return item.Unbox<T>();
        }
        public T CacheOrGet<T>( T obj, string name, UIElementState state ) where T : UIElement
        {
            var item = Element( name );
            if ( item == null )
            {
                if ( Cache( obj, name, state ) )
                {
                    return CacheOrGet( obj, name, state );
                }

                return default;
            }

            return item.Unbox<T>();
        }
        public T CacheOrGet<T>( T obj, string name, UIElementState state, Guid guid, bool getByGuid = false ) where T : UIElement
        {
            UIElementWrapper item;

            if(getByGuid)
            {
                item = Element( guid );
            }
            else
            {
                item = Element( name );
            }

            if ( item == null )
            {
                if ( Cache( obj, name, state, guid ) )
                {
                    return CacheOrGet( obj, name, state, guid );
                }

                return default;
            }

            return item.Unbox<T>();
        }

        public bool Remove(UIElementWrapper wrapper)
        {
            return m_CachedElements.TryRemove( wrapper.GetGuid(), out _ );
        }
        public bool Remove(string name)
        {
            var item = Element( name );
            if(item != null)
            {
                return Remove( item );
            }

            return false;
        }
        public bool Remove(Guid guid)
        {
            var item = Element( guid );
            if(item != null)
            {
                return Remove( item );
            }

            return false;
        }

        public void SetStateAsync(UIElementState state)
        {
            Task.WaitAll( new Task[]
            {
                m_CachedElements.ForEachAsync( ( x ) =>
                {
                    x.Value.SetState( state );
                    x.Value.SetNeedUpdate( true );
                })
            });
        }
        public void SetState( UIElementState state )
        {
            m_CachedElements.ForEach( ( x ) =>
            {
                x.Value.SetState( state );
                x.Value.SetNeedUpdate( true );
            });
        }
        public void SetState<T>(UIElementState state) where T : UIElement
        {
            var item = Wrapper<T>();
            if(item != null)
            {
                item.SetState( state );
                item.SetNeedUpdate( true );
            }
        }
        public void SetState( string name, UIElementState state )
        {
            var item = Element( name );
            if(item != null)
            {
                item.SetState( state );
                item.SetNeedUpdate( true );
            }
        }
        public void SetState( Guid guid, UIElementState state )
        {
            var item = Element( guid );
            if ( item != null )
            {
                item.SetState( state );
                item.SetNeedUpdate( true );
            }
        }

        public Dictionary<Guid, UIElementWrapper> Elements()
            => m_CachedElements.ToDictionary( ( x ) => x.Key, ( x ) => x.Value );

        private KeyValuePair<Guid, UIElementWrapper> GetElementInternal( Func<KeyValuePair<Guid, UIElementWrapper>, bool> predicate )
        {
            var item = m_CachedElements.FirstOrDefault( predicate );
            if(item.IsNull())
            {
                return default;
            }

            return item;
        }
    }
}
