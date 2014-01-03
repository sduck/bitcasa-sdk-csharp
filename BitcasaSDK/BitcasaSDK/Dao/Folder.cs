using System;

namespace BitcasaSdk.Dao
{
    public class Folder : Item
    {
        public override string ToString()
        {
            return String.Format("[Folder] {0} - {1}", Name, Path);
        }
    }
}
