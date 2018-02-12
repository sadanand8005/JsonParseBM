using System;
using System.Collections.Generic;

namespace JsonParseBM
{
    public class SimpleObject
    {
        public string Name { get; set; }
        public int Count { get; set; }
    }

    public class SimpleObjectList : IComparable
    {
        public List<SimpleObject> List { get; set; }
        public int Total { get; set; }
        public string Name { get; set; }

        public SimpleObjectList()
        {

        }

        public SimpleObjectList(string Name, int Count)
        {
            this.Name = Name;
            this.Total = Count;
            this.List = new List<SimpleObject>();
        }

        public int CompareTo(object obj)
        {
            var _obj = obj as SimpleObjectList;
            if (_obj != null)
            {
                return _obj.Total - this.Total;
            }

            return 0;
        }
    }

    public class SimpleObjectKey : IEquatable<SimpleObjectKey>
    {
        public SimpleObjectKey()
        {

        }

        public SimpleObjectKey(string Name)
        {
            this.Name = Name;
        }

        public string Name
        {
            get
            {
                return _name;
            }
            set
            {
                _name = value;
                hashCode = _name.GetHashCode();
            }
        }

        private string _name;

        private int hashCode;
        public bool Equals(SimpleObjectKey other)
        {
            return this.Name.Equals(other.Name);
        }

        public override int GetHashCode()
        {
            return hashCode;
        }

        public override bool Equals(object obj)
        {
            SimpleObjectKey _obj = obj as SimpleObjectKey;
            if (_obj != null)
            {
                return Equals(_obj);
            }
            else
            {
                return false;
            }
        }
    }
}
