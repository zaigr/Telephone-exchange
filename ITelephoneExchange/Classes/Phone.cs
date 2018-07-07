using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ATS.Interfaces
{
    public class Phone : IComparable<Phone>
    {
        private int _id;

        public Phone(int id)
        {
            this._id = id;
        }

        public int CompareTo(Phone other)
        {
            return _id.CompareTo(other._id);
        }

        public override bool Equals(object obj)
        {
            return _id.Equals((obj as Phone)?._id);
        }

        public override int GetHashCode()
        {
            return _id.GetHashCode();
        }

        public override string ToString()
        {
            return _id.ToString();
        }
    }
}
