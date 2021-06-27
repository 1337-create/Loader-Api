using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Anubis.System
{
    public static class CollectionEx
    {
        public static void ForEach<T>(this IEnumerable<T> en, Action<T> act)
        {
            foreach(var item in en)
            {
                act( item );
            }
        }
        public static Task ForEachAsync<T>( this IEnumerable<T> en, Action<T> act )
        {
            return Task.Run( () =>
            {
                foreach ( var item in en )
                {
                    act( item );
                }
            } );
        }

        public static void ForEach<TKey, TValue>(this IDictionary<TKey, TValue> dict, Action<KeyValuePair<TKey, TValue>> act)
        {
            foreach(var item in dict)
            {
                act( item );
            }
        }
        public static Task ForEachAsync<TKey, TValue>( this IDictionary<TKey, TValue> dict, Action<KeyValuePair<TKey, TValue>> act )
        {
            return Task.Run( () => 
            {
                foreach ( var item in dict )
                {
                    act( item );
                }
            } );
        }

        public static bool IsNull<TKey, TValue>( this KeyValuePair<TKey, TValue> pair )
            => pair.Equals( default( KeyValuePair<TKey, TValue> ) );
    }
}
