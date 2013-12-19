using System;

namespace BitcasaSDK.Dao
{
    public class Folder : Item
    {
        public override string ToString()
        {
            return String.Format("{0} - {1}", Name, Path);
        }
    }
}
