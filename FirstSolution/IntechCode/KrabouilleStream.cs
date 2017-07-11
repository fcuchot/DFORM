using System;
using System.Collections.Generic;
using System.IO;
using System.Security.Cryptography;
using System.Text;

namespace IntechCode
{

    public enum KrabouilleMode
    {
        Krabouille,
        UnKrabouille
    }

    public class KrabouilleStream : Stream
    {
        readonly Stream _inner;
        readonly KrabouilleMode _mode;
        int _pos;
        byte[] _s;

        public KrabouilleStream( Stream inner, string password, KrabouilleMode mode )
        {
            if (inner == null) throw new ArgumentNullException(nameof(inner));
            if (string.IsNullOrWhiteSpace(password)) throw new ArgumentNullException(nameof(password));
            if (!inner.CanRead && mode == KrabouilleMode.UnKrabouille) throw new ArgumentException("Must be readable.", nameof(inner));
            if (!inner.CanWrite && mode == KrabouilleMode.Krabouille) throw new ArgumentException("Must be writable.", nameof(inner));
            _mode = mode;
            _inner = inner;
            var k = new Rfc2898DeriveBytes(password, new byte[] { 98, 123, 87, 2, 87, 2, 87, 2, 23, 1, 32 });
            _s = k.GetBytes(20);
        }

        public override bool CanRead => _inner.CanRead;

        public override bool CanSeek => _inner.CanSeek;

        public override bool CanWrite => _inner.CanWrite;

        public override long Length => _inner.Length;

        public override long Position
        {
            get => _inner.Position;
            set => _inner.Position = value;
        }

        public override void Flush() => _inner.Flush();

        public override int Read(byte[] buffer, int offset, int count)
        {
            if (_mode == KrabouilleMode.Krabouille) throw new InvalidOperationException();
            int lenRead = _inner.Read(buffer, offset, count);
            if (count > lenRead) count = lenRead;
            for (int i = offset; i < offset+count; ++i )
            {
                buffer[i] ^= _s[_pos % _s.Length];
                ++_pos;
            }
            return lenRead;
        }
        public override void Write(byte[] buffer, int offset, int count)
        {
            if (_mode == KrabouilleMode.UnKrabouille) throw new InvalidOperationException();
            for (int i = offset; i < offset + count; ++i)
            {
                buffer[i] ^= _s[_pos % _s.Length];
                ++_pos;
            }
            _inner.Write(buffer, offset, count);
        }

        /// <summary>
        /// Basic, standard implementation that relies on <see cref="Position"/> do actually
        /// seek in this sream. Of course <see cref="CanSeek"/> must be true otherwise an exception 
        /// is thrown.
        /// </summary>
        /// <param name="offset">Offset (in bytes) from <paramref name="origin"/>.</param>
        /// <param name="origin">One of the <see cref="SeekOrigin"/> value.</param>
        /// <returns>The new <see cref="Position"/>.</returns>
        public override long Seek(long offset, SeekOrigin origin)
        {
            if (!CanSeek) throw new InvalidOperationException();
            switch(origin)
            {
                case SeekOrigin.Begin: Position = offset; break;
                case SeekOrigin.End: Position = Length - offset; break;
                case SeekOrigin.Current: Position += offset; break;
            }
            return Position;
        }

        public override void SetLength(long value) => _inner.SetLength(value);

    }
}
