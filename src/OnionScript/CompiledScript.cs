using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace OrbHall.OnionScript
{
    /// <summary>
    /// A compiled script.
    /// </summary>
    public class CompiledScript
    {
        public static CompiledScript Load(BinaryReader reader)
        {
            return new CompiledScript();
        }

        public void Save(BinaryWriter writer)
        {

        }
    }
}
