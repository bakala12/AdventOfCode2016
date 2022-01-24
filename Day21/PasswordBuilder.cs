using System;
using System.Linq;

namespace Day21
{
    public class PasswordBuilder
    {
        public readonly char[] _storage;

        public PasswordBuilder(string str)
        {
            _storage = str.ToCharArray();
        }

        public PasswordBuilder SwapPosition(int x, int y)
        {
            char tmp = _storage[x];
            _storage[x] = _storage[y];
            _storage[y] = tmp;
            return this;
        }

        public PasswordBuilder SwapLetters(char x, char y)
        {
            for(int i = 0; i < _storage.Length; i++)
            {
                if(_storage[i] == x)
                    _storage[i] = y;
                else if(_storage[i] == y)
                    _storage[i] = x;
            }
            return this;
        }

        public PasswordBuilder RotateLeft(int shift)
        {
            shift = shift % _storage.Length;
            char[] buffer = new char[shift];
            Array.Copy(_storage, buffer, shift);
            Array.Copy(_storage, shift, _storage, 0, _storage.Length - shift);
            Array.Copy(buffer, 0, _storage, _storage.Length - shift, shift);
            return this;
        }

        public PasswordBuilder RotateRight(int shift)
        {
            shift = shift % _storage.Length;
            char[] buffer = new char[shift];
            Array.Copy(_storage, _storage.Length - shift, buffer, 0, shift);
            Array.Copy(_storage, 0, _storage, shift, _storage.Length - shift);
            Array.Copy(buffer, 0, _storage, 0, shift);
            return this;
        }

        public PasswordBuilder RotateOnLetter(char l)
        {
            var shift = IndexOf(l);
            return RotateRight(shift + 1 + (shift >= 4 ? 1 : 0));
        }

        public PasswordBuilder UnrotateOnLetter(char l)
        {
            var newInd = IndexOf(l);
            var oldInd = Enumerable.Range(0, _storage.Length).First(s => (2*s+1+(s>=4 ? 1 : 0)) % _storage.Length == newInd);
            return RotateRight((oldInd-newInd+_storage.Length) % _storage.Length);
        }

        public PasswordBuilder Reverse(int start, int end)
        {
            for(int s = start, e = end; s < e; s++, e--)
            {
                char t = _storage[s];
                _storage[s] = _storage[e];
                _storage[e] = t;
            }
            return this;
        }

        public PasswordBuilder MovePositionTo(int x, int y)
        {
            char t = _storage[x];
            Array.Copy(_storage, x+1, _storage, x, _storage.Length-x-1);
            Array.Copy(_storage, y, _storage, y+1, _storage.Length-y-1);
            _storage[y] = t;
            return this;
        }

        public override string ToString()
        {
            return string.Join("", _storage);
        }

        private int IndexOf(char l)
        {
            for(int i = 0; i < _storage.Length; i++)
                if(_storage[i] == l)
                    return i;
            return -1;
        }
    }
}
