using System;
using System.Collections.Generic;
using System.Text;

namespace IntechCode.IntechCollection
{
    public interface IMyDictionary<TKey,TValue> : IMyReadOnlyCollection<KeyValuePair<TKey,TValue>>
    {
        /// <summary>
        /// Adds a key-value pair. The key must not already exist.
        /// </summary>
        /// <param name="key">The key to add.</param>
        /// <param name="value">The value to add.</param>
        void Add(TKey key, TValue value);

        /// <summary>
        /// Removes a key.
        /// </summary>
        /// <param name="key">The key to remove.</param>
        /// <returns>True on success, false if the key did not exist.</returns>
        bool Remove(TKey key);

        /// <summary>
        /// Tests whether a key exists in the dictionary.
        /// </summary>
        /// <param name="key">The key to challenge.</param>
        /// <returns>True if the key exists, false otherwise.</returns>
        bool ContainsKey(TKey key);

        /// <summary>
        /// Gets or sets the value associated to a key.
        /// When getting, if the key DOES NOT EXIST: <see cref="KeyNotFoundException"/>.
        /// When setting, the value is added or updated.
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        TValue this[TKey key] { get; set; }


        bool TryGetValue(TKey key, out TValue value);

    }
}
