using System.IO;
using System.Runtime.InteropServices;

namespace ArxLibertatisServer
{
    public static class StreamExtensions
    {
        public static void WriteStruct<T>(this Stream stream, T obj)
        {
            var objectLength = Marshal.SizeOf(typeof(T));
            var objectBuffer = Marshal.AllocHGlobal(objectLength);
            var objectBytes = new byte[objectLength];
            Marshal.Copy(objectBytes, 0, objectBuffer, objectLength);//manually zeroing buffer because the runtime might only partially write char arrays and memory isnt zeroed
            Marshal.StructureToPtr(obj, objectBuffer, true);
            Marshal.Copy(objectBuffer, objectBytes, 0, objectBytes.Length);
            Marshal.FreeHGlobal(objectBuffer);
            stream.Write(objectBytes);
        }

        public static T ReadStruct<T>(this Stream stream)
        {
            var objectLength = Marshal.SizeOf(typeof(T));
            var bytesLeft = objectLength;
            //var bytes = stream.ReadBytes(objectLength);
            var bytes = new byte[objectLength];
            while (bytesLeft > 0)
            {
                int bytesRead = stream.Read(bytes, objectLength - bytesLeft, bytesLeft);
                bytesLeft -= bytesRead;
            }
            var pinnedBytes = GCHandle.Alloc(bytes, GCHandleType.Pinned);
            var readObject = (T)Marshal.PtrToStructure(pinnedBytes.AddrOfPinnedObject(), typeof(T));
            pinnedBytes.Free();
            return readObject;
        }
    }
}
