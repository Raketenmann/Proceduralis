using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.Runtime.InteropServices;

public interface IImageFormat
{
    void LoadFrom(byte[] data, int offset = 0);
    void SaveTo(byte[] data, int offset = 0);

    int Size
    {
        get;
    }
}

public interface IFormat24bppRgb : IImageFormat
{
    byte R
    {
        get;
        set;
    }

    byte G
    {
        get;
        set;
    }

    byte B
    {
        get;
        set;
    }
}

[StructLayout(LayoutKind.Sequential)]
public struct Format24BppRgb : IFormat24bppRgb
{
    public Format24BppRgb(byte value)
        : this()
    {
        _r = value;
        _g = value;
        _b = value;
    }

    public Format24BppRgb(byte r, byte g, byte b)
        : this()
    {
        _r = r;
        _g = g;
        _b = b;
    }

    #region IFormat24bppRgb Members

    private byte _r;
    public byte R
    {
        get
        {
            return _r;
        }
        set
        {
            _r = value;
        }
    }

    private byte _g;
    public byte G
    {
        get
        {
            return _g;
        }
        set
        {
            _g = value;
        }
    }

    private byte _b;
    public byte B
    {
        get
        {
            return _b;
        }
        set
        {
            _b = value;
        }
    }

    #endregion

    #region IImageFormat Members

    public void LoadFrom(byte[] data, int offset = 0)
    {
        R = data[offset + 0];
        G = data[offset + 1];
        B = data[offset + 2];
    }

    public void SaveTo(byte[] data, int offset = 0)
    {
        data[offset + 0] = R;
        data[offset + 1] = G;
        data[offset + 2] = B;
    }

    public int Size
    {
        get
        {
            return 3;
        }
    }

    #endregion
}

public interface IFormat32bppArgb : IImageFormat
{
    byte A
    {
        get;
        set;
    }

    byte R
    {
        get;
        set;
    }

    byte G
    {
        get;
        set;
    }

    byte B
    {
        get;
        set;
    }
}

[StructLayout(LayoutKind.Sequential)]
public struct Format32BppArgb : IFormat32bppArgb
{
    public Format32BppArgb(byte value)
        : this()
    {
        _a = value;
        _r = value;
        _g = value;
        _b = value;
    }

    public Format32BppArgb(byte a, byte r, byte g, byte b)
        : this()
    {
        _a = a;
        _r = r;
        _g = g;
        _b = b;
    }

    #region IFormat32bppArgb Members

    private byte _a;
    public byte A
    {
        get
        {
            return _a;
        }
        set
        {
            _a = value;
        }
    }

    private byte _r;
    public byte R
    {
        get
        {
            return _r;
        }
        set
        {
            _r = value;
        }
    }

    private byte _g;
    public byte G
    {
        get
        {
            return _g;
        }
        set
        {
            _g = value;
        }
    }

    private byte _b;
    public byte B
    {
        get
        {
            return _b;
        }
        set
        {
            _b = value;
        }
    }

    #endregion

    #region IImageFormat Members

    public void LoadFrom(byte[] data, int offset = 0)
    {
        A = data[offset + 0];
        R = data[offset + 1];
        G = data[offset + 2];
        B = data[offset + 3];
    }

    public void SaveTo(byte[] data, int offset = 0)
    {
        data[offset + 0] = A;
        data[offset + 1] = R;
        data[offset + 2] = G;
        data[offset + 3] = B;
    }

    public int Size
    {
        get
        {
            return 4;
        }
    }

    #endregion
}

public interface IFormat32bppRgba : IImageFormat
{
    byte R
    {
        get;
        set;
    }

    byte G
    {
        get;
        set;
    }

    byte B
    {
        get;
        set;
    }

    byte A
    {
        get;
        set;
    }
}

[StructLayout(LayoutKind.Sequential)]
public struct Format32BppRgba : IFormat32bppRgba
{
    public Format32BppRgba(byte value)
        : this()
    {
        _r = value;
        _g = value;
        _b = value;
        _a = value;
    }

    public Format32BppRgba(byte r, byte g, byte b, byte a)
        : this()
    {
        _r = r;
        _g = g;
        _b = b;
        _a = a;
    }

    #region IFormat32bppArgb Members

    private byte _r;
    public byte R
    {
        get
        {
            return _r;
        }
        set
        {
            _r = value;
        }
    }

    private byte _g;
    public byte G
    {
        get
        {
            return _g;
        }
        set
        {
            _g = value;
        }
    }

    private byte _b;
    public byte B
    {
        get
        {
            return _b;
        }
        set
        {
            _b = value;
        }
    }

    private byte _a;
    public byte A
    {
        get
        {
            return _a;
        }
        set
        {
            _a = value;
        }
    }

    #endregion

    #region IImageFormat Members

    public void LoadFrom(byte[] data, int offset = 0)
    {
        R = data[offset + 0];
        G = data[offset + 1];
        B = data[offset + 2];
        A = data[offset + 3];
    }

    public void SaveTo(byte[] data, int offset = 0)
    {
        data[offset + 0] = R;
        data[offset + 1] = G;
        data[offset + 2] = B;
        data[offset + 3] = A;
    }

    public int Size
    {
        get
        {
            return 4;
        }
    }

    #endregion
}


public enum AddressMode
{
    Clamp,
    Fixed
}

public interface I3x3ImageRegion<TFormat>
    where TFormat : struct, IImageFormat
{
    TFormat NE
    {
        get;
    }
    TFormat N
    {
        get;
    }
    TFormat NW
    {
        get;
    }
    TFormat E
    {
        get;
    }
    TFormat C
    {
        get;
    }
    TFormat W
    {
        get;
    }
    TFormat SE
    {
        get;
    }
    TFormat S
    {
        get;
    }
    TFormat SW
    {
        get;
    }
}

public static class LinqImageProcessingExtensions
{
    private abstract class EnumerableImageDataBase<TFormat>
        where TFormat : struct, IImageFormat
    {
        public IEnumerator<TFormat[]> GetEnumerator()
        {
            var width = _sections.First().Width;
            var height = _sections.First().Height;
            var pixelSize = _format.Size;

            var rowData = new byte[_sections.Length][];
            for (int i = 0; i < _sections.Length; i++)
                rowData[i] = new byte[width * _format.Size];
            var rowDataSize = rowData.Length;
            var bitmapData = (from section in _sections select section.Bitmap.LockBits(new Rectangle(0, 0, width, height), ImageLockMode.ReadOnly, section.Bitmap.PixelFormat)).ToArray();
            var bitmapPtrs = (from idx in Enumerable.Range(0, _sections.Length)
                            let startSkip = _sections[idx].Left
                            select new IntPtr(bitmapData[idx].Scan0.ToInt64() + (startSkip * _format.Size))).ToArray();
            var rowStride = (from idx in Enumerable.Range(0, _sections.Length)
                            let defStride = bitmapData[idx].Stride
                            let section = _sections[idx]
                            let startSkip = section.Left
                            let endSkip = section.Bitmap.Width - (section.Left + section.Width)
                            select defStride + ((startSkip + endSkip) * _format.Size)).ToArray();

            try
            {
                var result = new TFormat[_sections.Length];
                
                for (int r = 0; r < height; r++)
                {
                    for (int b = 0; b < bitmapData.Length; b++)
                    {
                        Marshal.Copy(bitmapPtrs[b], rowData[b], 0, rowDataSize);
                        for (int c = 0; c < rowDataSize; c += pixelSize)
                            result[b].LoadFrom(rowData[b], c);
                        bitmapPtrs[b] = new IntPtr(bitmapPtrs[b].ToInt64() + rowStride[b]);
                    }
                    yield return result;
                }
            }
            finally
            {
                for (int i = 0; i < bitmapData.Length; i++)
                    _sections[i].Bitmap.UnlockBits(bitmapData[i]);
            }
        }

        private readonly BitmapSection[] _sections;

        protected EnumerableImageDataBase(params BitmapSection[] sections)
        {
            _sections = sections;
            if (!(((from section in _sections select section.Width).Skip(1).All(w => w == _sections.First().Width)) &&
                ((from section in _sections select section.Height).Skip(1).All(w => w == _sections.First().Height))))
                throw new ArgumentException("Cannot process multiple bitmaps with differently sized sections!");
        }

        private static TFormat _format;
    }

    private class EnumerableImageDataReaderSingle<TFormat> : EnumerableImageDataBase<TFormat>, IEnumerable<TFormat>
        where TFormat: struct, IImageFormat
    {
        public EnumerableImageDataReaderSingle(params BitmapSection[] sections)
            : base(sections)
        {
        }
    
        public new IEnumerator<TFormat>  GetEnumerator()
        {
            var enumerator = base.GetEnumerator();
            while (enumerator.MoveNext())
                yield return enumerator.Current[0];
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
 	        return GetEnumerator();
        }
    }

    private class EnumerableImageDataReaderMultiple<TFormat> : EnumerableImageDataBase<TFormat>, IEnumerable<TFormat[]>
        where TFormat: struct, IImageFormat
    {
        public EnumerableImageDataReaderMultiple(params BitmapSection[] sections)
            : base(sections)
        {
        }
    
        public new IEnumerator<TFormat[]>  GetEnumerator()
        {
            return base.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
 	        return GetEnumerator();
        }
    }

    private sealed class Enumerable3x3ImageRegion<TFormat> : IEnumerable<I3x3ImageRegion<TFormat>>
        where TFormat : struct, IImageFormat
    {
        private struct _3x3ImageRegion : I3x3ImageRegion<TFormat>
        {
            private readonly TFormat[] _data;

            public _3x3ImageRegion(TFormat[] data)
            {
                if (data == null)
                    throw new ArgumentNullException("data");

                if (data.Length != 9)
                    throw new ArgumentException("Need exactly 9 pieces of data for 3x3 region");

                _data = data;
            }

            #region I3x3ImageRegion<TFormat> Members

            public TFormat NE
            {
                get
                {
                    return _data[0];
                }
            }
            public TFormat N
            {
                get
                {
                    return _data[1];
                }
            }
            public TFormat NW
            {
                get
                {
                    return _data[2];
                }
            }
            public TFormat E
            {
                get
                {
                    return _data[3];
                }
            }
            public TFormat C
            {
                get
                {
                    return _data[4];
                }
            }
            public TFormat W
            {
                get
                {
                    return _data[5];
                }
            }
            public TFormat SE
            {
                get
                {
                    return _data[6];
                }
            }
            public TFormat S
            {
                get
                {
                    return _data[7];
                }
            }
            public TFormat SW
            {
                get
                {
                    return _data[8];
                }
            }

            #endregion
        }

        private IntPtr GetPrevRowStart(AddressMode addressV, int currRowIndex, IntPtr data, int rowStride, int height)
        {
            var currRow = new IntPtr(data.ToInt64() + currRowIndex * rowStride);
            switch (addressV)
            {
                case AddressMode.Clamp:
                    return (currRowIndex - 1) < 0 ? currRow : new IntPtr(currRow.ToInt64() - rowStride);

                case AddressMode.Fixed:
                    return (currRowIndex - 1) < 0 ? _fixedValueData : new IntPtr(currRow.ToInt64() - rowStride);

                default:
                    throw new NotSupportedException(string.Format("The address mode {0} is not supported yet", addressV));
            }
        }

        private IntPtr GetNextRowStart(AddressMode addressV, int currRowIndex, IntPtr data, int rowStride, int height)
        {
            var currRow = new IntPtr(data.ToInt64() + currRowIndex * rowStride);
            switch (addressV)
            {
                case AddressMode.Clamp:
                    return (currRowIndex + 1) > (height - 1) ? currRow : new IntPtr(currRow.ToInt64() + rowStride);

                case AddressMode.Fixed:
                    return (currRowIndex + 1) > (height - 1) ? _fixedValueData : new IntPtr(currRow.ToInt64() + rowStride);

                default:
                    throw new NotSupportedException(string.Format("The address mode {0} is not supported yet", addressV));
            }
        }

        private int GetPrevColumnOffset(AddressMode addressU, int currOffset, int rowSize)
        {
            switch (addressU)
            {
                case AddressMode.Clamp:
                    return (currOffset - _format.Size) < 0 ? currOffset : currOffset - _format.Size;

                case AddressMode.Fixed:
                    return (currOffset - _format.Size) < 0 ? -1 : currOffset - _format.Size;

                default:
                    throw new NotSupportedException(string.Format("The address mode {0} is not supported yet", addressU));
            }
        }

        private int GetNextColumnOffset(AddressMode addressU, int currOffset, int rowSize)
        {
            switch (addressU)
            {
                case AddressMode.Clamp:
                    return (currOffset + _format.Size) > (rowSize - 1) ? currOffset : currOffset + _format.Size;

                case AddressMode.Fixed:
                    return (currOffset + _format.Size) > (rowSize - 1) ? -1 : currOffset + _format.Size;

                default:
                    throw new NotSupportedException(string.Format("The address mode {0} is not supported yet", addressU));
            }
        }

        #region IEnumerable<I3x3ImageRegion<TFormat>> Members

        public IEnumerator<I3x3ImageRegion<TFormat>> GetEnumerator()
        {
            var width = _bitmap.Width;
            var height = _bitmap.Height;
            var pixelSize = _format.Size;

            var rowData = new byte[width * pixelSize];
            var prevRowData = new byte[width * pixelSize];
            var nextRowData = new byte[width * pixelSize];

            var bitmapData = _bitmap.LockBits(new Rectangle(0, 0, width, height), ImageLockMode.ReadOnly, _bitmap.PixelFormat);
            var rowStride = bitmapData.Stride;
            var rowDataSize = rowData.Length;

            if (_addressV == AddressMode.Fixed)
                _fixedValueData = Marshal.AllocHGlobal(rowStride);

            var bitmapStart = bitmapData.Scan0;
            var bitmapPtr = bitmapStart;
            try
            {
                if (_addressV == AddressMode.Fixed)
                {
                    var fixedData = new byte[rowDataSize];
                    for (int i = 0; i < rowDataSize; i += pixelSize)
                        _fixedValue.SaveTo(fixedData, i);
                    Marshal.Copy(fixedData, 0, _fixedValueData, rowDataSize);
                }

                for (int r = 0; r < height; r++)
                {
                    Marshal.Copy(GetPrevRowStart(_addressV, r, bitmapStart, rowStride, height), prevRowData, 0, rowDataSize);
                    Marshal.Copy(bitmapPtr, rowData, 0, rowDataSize);
                    Marshal.Copy(GetNextRowStart(_addressV, r, bitmapStart, rowStride, height), nextRowData, 0, rowDataSize);

                    for (int c = 0; c < rowDataSize; c += pixelSize)
                    {
                        var pixel = new TFormat();

                        var prevColumnOffset = GetPrevColumnOffset(_addressU, c, rowDataSize);
                        var nextColumnOffset = GetNextColumnOffset(_addressU, c, rowDataSize);

                        if (prevColumnOffset == -1)
                        {
                            _3x3Contents[0] = _fixedValue;
                            _3x3Contents[3] = _fixedValue;
                            _3x3Contents[6] = _fixedValue;
                        }
                        else
                        {
                            _3x3Contents[0].LoadFrom(prevRowData, prevColumnOffset);
                            _3x3Contents[3].LoadFrom(rowData, prevColumnOffset);
                            _3x3Contents[6].LoadFrom(nextRowData, prevColumnOffset);
                        }

                        if (nextColumnOffset == -1)
                        {
                            _3x3Contents[2] = _fixedValue;
                            _3x3Contents[5] = _fixedValue;
                            _3x3Contents[8] = _fixedValue;
                        }
                        else
                        {
                            _3x3Contents[2].LoadFrom(prevRowData, nextColumnOffset);
                            _3x3Contents[5].LoadFrom(rowData, nextColumnOffset);
                            _3x3Contents[8].LoadFrom(nextRowData, nextColumnOffset);
                        }

                        _3x3Contents[1].LoadFrom(prevRowData, c);
                        _3x3Contents[4].LoadFrom(rowData, c);
                        _3x3Contents[7].LoadFrom(nextRowData, c);

                        yield return new _3x3ImageRegion(_3x3Contents);
                    }
                    bitmapPtr = new IntPtr(bitmapPtr.ToInt64() + rowStride);
                }
            }
            finally
            {
                if (_addressV == AddressMode.Fixed)
                    Marshal.FreeHGlobal(_fixedValueData);

                _bitmap.UnlockBits(bitmapData);
            }
        }

        #endregion

        #region IEnumerable Members

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        #endregion

        private static TFormat _format;

        private readonly Bitmap _bitmap;
        private readonly AddressMode _addressU;
        private readonly AddressMode _addressV;
        private readonly TFormat[] _3x3Contents = new TFormat[9];
        private readonly TFormat _fixedValue;

        private IntPtr _fixedValueData;

        public Enumerable3x3ImageRegion(Bitmap bitmap, AddressMode addressU, AddressMode addressV, TFormat fixedValue = default(TFormat))
        {
            if (bitmap == null)
                throw new ArgumentNullException("bitmap");

            _bitmap = bitmap;
            _addressU = addressU;
            _addressV = addressV;
            _fixedValue = fixedValue;
        }
    }

    public static IEnumerable<TFormat> GetData<TFormat>(this Bitmap bitmap)
        where TFormat : struct, IImageFormat
    {
        return new EnumerableImageDataReaderSingle<TFormat>(bitmap);
    }

    public static IEnumerable<TFormat[]> GetData<TFormat>(params Bitmap[] bitmaps)
        where TFormat : struct, IImageFormat
    {
        return new EnumerableImageDataReaderMultiple<TFormat>(bitmaps.Cast<BitmapSection>().ToArray());
    }

    public static IEnumerable<TFormat[]> GetData<TFormat>(IEnumerable<Bitmap> bitmaps)
        where TFormat : struct, IImageFormat
    {
        return new EnumerableImageDataReaderMultiple<TFormat>(bitmaps.Cast<BitmapSection>().ToArray());
    }

    public static IEnumerable<TFormat[]> GetData<TFormat>(params BitmapSection[] sections)
        where TFormat : struct, IImageFormat
    {
        return new EnumerableImageDataReaderMultiple<TFormat>(sections.ToArray());
    }

    public static IEnumerable<TFormat[]> GetData<TFormat>(IEnumerable<BitmapSection> sections)
        where TFormat : struct, IImageFormat
    {
        return new EnumerableImageDataReaderMultiple<TFormat>(sections.ToArray());
    }

    public static IEnumerable<I3x3ImageRegion<TFormat>> Get3x3Sampler<TFormat>(this Bitmap bitmap, AddressMode addressU = AddressMode.Clamp, AddressMode addressV = AddressMode.Clamp, TFormat fixedValue = default(TFormat))
        where TFormat : struct, IImageFormat
    {
        return new Enumerable3x3ImageRegion<TFormat>(bitmap, addressU, addressV);
    }

    public static void SetData<TFormat>(this Bitmap bitmap, IEnumerable<TFormat> data)
        where TFormat : struct, IImageFormat
    {
        var format = new TFormat();
        var width = bitmap.Width;
        var height = bitmap.Height;
        var pixelSize = format.Size;

        var rowData = new byte[width * pixelSize];
        var rowDataSize = rowData.Length;
        var bitmapData = bitmap.LockBits(new Rectangle(0, 0, width, height), ImageLockMode.WriteOnly, bitmap.PixelFormat);
        var rowStride = bitmapData.Stride;
        var bitmapPtr = bitmapData.Scan0;
        try
        {
            int offset = 0;
            foreach (var pixel in data)
            {
                if (offset < rowDataSize)
                {
                    pixel.SaveTo(rowData, offset);
                    offset += pixelSize;
                }
                else
                {
                    offset = 0;
                    Marshal.Copy(rowData, 0, bitmapPtr, rowDataSize);
                    bitmapPtr = new IntPtr(bitmapPtr.ToInt64() + rowStride);

                    pixel.SaveTo(rowData, offset);
                    offset += pixelSize;
                }
            }
            Marshal.Copy(rowData, 0, bitmapPtr, rowDataSize); // Last row
        }
        finally
        {
            bitmap.UnlockBits(bitmapData);
        }
    }

    public static BitmapSection Section(this Bitmap bitmap, int left, int top, int width, int height)
    {
        return new BitmapSection(bitmap, left, top, width, height);
    }
}

public struct BitmapSection
{
    public readonly Bitmap Bitmap;
    public readonly int Left;
    public readonly int Top;
    public readonly int Width;
    public readonly int Height;
    
    public BitmapSection(Bitmap bitmap, int left, int top, int width, int height)
    {
        Bitmap = bitmap;
        Left = left;
        Top = top;
        Width = width;
        Height = height;
    }

    public static implicit operator BitmapSection(Bitmap bitmap)
    {
        return new BitmapSection(bitmap, 0, 0, bitmap.Width, bitmap.Height);
    }
    public static implicit operator Bitmap(BitmapSection section)
    {
        return section.Bitmap;
    }
}