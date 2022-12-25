namespace Xorog.UniversalExtensions;

public static class ConvertExtensions
{
    public static int ToBase64CharArray(this byte[] inArray, int offsetIn, int length, char[] outArray, int offsetOut)
        => Convert.ToBase64CharArray(inArray, offsetIn, length, outArray, offsetOut);

    public static int ToBase64CharArray(this byte[] inArray, int offsetIn, int length, char[] outArray, int offsetOut, Base64FormattingOptions options)
        => Convert.ToBase64CharArray(inArray, offsetIn, length, outArray, offsetOut, options);

    public static string ToBase64String(this ReadOnlySpan<byte> bytes, Base64FormattingOptions options = Base64FormattingOptions.None)
        => Convert.ToBase64String(bytes, options);

    public static string ToBase64String(this byte[] inArray, Base64FormattingOptions options)
        => Convert.ToBase64String(inArray, options);

    public static string ToBase64String(this byte[] inArray, int offset, int length)
        => Convert.ToBase64String(inArray, offset, length);

    public static string ToBase64String(this byte[] inArray)
        => Convert.ToBase64String(inArray);

    public static string ToBase64String(this byte[] inArray, int offset, int length, Base64FormattingOptions options)
        => Convert.ToBase64String(inArray, offset, length, options);

    public static bool ToBoolean(this sbyte value)
        => Convert.ToBoolean(value);

    public static bool ToBoolean(this ulong value)
        => Convert.ToBoolean(value);

    public static bool ToBoolean(this uint value)
        => Convert.ToBoolean(value);

    public static bool ToBoolean(this ushort value)
        => Convert.ToBoolean(value);

    public static bool ToBoolean(this string? value, IFormatProvider? provider)
        => Convert.ToBoolean(value, provider);

    public static bool ToBoolean(this string? value)
        => Convert.ToBoolean(value);

    public static bool ToBoolean(this float value)
        => Convert.ToBoolean(value);

    public static bool ToBoolean(this object? value, IFormatProvider? provider)
        => Convert.ToBoolean(value, provider);

    public static bool ToBoolean(this double value)
        => Convert.ToBoolean(value);

    public static bool ToBoolean(this long value)
        => Convert.ToBoolean(value);

    public static bool ToBoolean(this object? value)
        => Convert.ToBoolean(value);

    public static bool ToBoolean(this byte value)
        => Convert.ToBoolean(value);

    public static bool ToBoolean(this char value)
        => Convert.ToBoolean(value);

    public static bool ToBoolean(this DateTime value)
        => Convert.ToBoolean(value);

    public static bool ToBoolean(this bool value)
        => Convert.ToBoolean(value);

    public static bool ToBoolean(this short value)
        => Convert.ToBoolean(value);

    public static bool ToBoolean(this int value)
        => Convert.ToBoolean(value);

    public static bool ToBoolean(this decimal value)
        => Convert.ToBoolean(value);

    public static byte ToByte(this float value)
        => Convert.ToByte(value);

    public static byte ToByte(this ulong value)
        => Convert.ToByte(value);

    public static byte ToByte(this uint value)
        => Convert.ToByte(value);

    public static byte ToByte(this ushort value)
        => Convert.ToByte(value);

    public static byte ToByte(this string? value, int fromBase)
        => Convert.ToByte(value, fromBase);

    public static byte ToByte(this string? value, IFormatProvider? provider)
        => Convert.ToByte(value, provider);

    public static byte ToByte(this string? value)
        => Convert.ToByte(value);

    public static byte ToByte(this sbyte value)
        => Convert.ToByte(value);

    public static byte ToByte(this short value)
        => Convert.ToByte(value);

    public static byte ToByte(this object? value)
        => Convert.ToByte(value);

    public static byte ToByte(this char value)
        => Convert.ToByte(value);

    public static byte ToByte(this DateTime value)
        => Convert.ToByte(value);

    public static byte ToByte(this decimal value)
        => Convert.ToByte(value);

    public static byte ToByte(this byte value)
        => Convert.ToByte(value);

    public static byte ToByte(this bool value)
        => Convert.ToByte(value);

    public static byte ToByte(this int value)
        => Convert.ToByte(value);

    public static byte ToByte(this long value)
        => Convert.ToByte(value);

    public static byte ToByte(this double value)
        => Convert.ToByte(value);

    public static byte ToByte(this object? value, IFormatProvider? provider)
        => Convert.ToByte(value, provider);

    public static char ToChar(this object? value, IFormatProvider? provider)
        => Convert.ToChar(value, provider);

    public static char ToChar(this ulong value)
        => Convert.ToChar(value);

    public static char ToChar(this uint value)
        => Convert.ToChar(value);

    public static char ToChar(this ushort value)
        => Convert.ToChar(value);

    public static char ToChar(this string value, IFormatProvider? provider)
        => Convert.ToChar(value, provider);

    public static char ToChar(this string value)
        => Convert.ToChar(value);

    public static char ToChar(this float value)
        => Convert.ToChar(value);

    public static char ToChar(this sbyte value)
        => Convert.ToChar(value);

    public static char ToChar(this object? value)
        => Convert.ToChar(value);

    public static char ToChar(this int value)
        => Convert.ToChar(value);

    public static char ToChar(this short value)
        => Convert.ToChar(value);

    public static char ToChar(this double value)
        => Convert.ToChar(value);

    public static char ToChar(this decimal value)
        => Convert.ToChar(value);

    public static char ToChar(this DateTime value)
        => Convert.ToChar(value);

    public static char ToChar(this char value)
        => Convert.ToChar(value);

    public static char ToChar(this byte value)
        => Convert.ToChar(value);

    public static char ToChar(this bool value)
        => Convert.ToChar(value);

    public static char ToChar(this long value)
        => Convert.ToChar(value);

    public static DateTime ToDateTime(this float value)
        => Convert.ToDateTime(value);

    public static DateTime ToDateTime(this string? value)
        => Convert.ToDateTime(value);

    public static DateTime ToDateTime(this string? value, IFormatProvider? provider)
        => Convert.ToDateTime(value, provider);

    public static DateTime ToDateTime(this sbyte value)
        => Convert.ToDateTime(value);

    public static DateTime ToDateTime(this uint value)
        => Convert.ToDateTime(value);

    public static DateTime ToDateTime(this ulong value)
        => Convert.ToDateTime(value);

    public static DateTime ToDateTime(this ushort value)
        => Convert.ToDateTime(value);

    public static DateTime ToDateTime(this object? value, IFormatProvider? provider)
        => Convert.ToDateTime(value, provider);

    public static DateTime ToDateTime(this int value)
        => Convert.ToDateTime(value);

    public static DateTime ToDateTime(this long value)
        => Convert.ToDateTime(value);

    public static DateTime ToDateTime(this object? value)
        => Convert.ToDateTime(value);

    public static DateTime ToDateTime(this char value)
        => Convert.ToDateTime(value);

    public static DateTime ToDateTime(this DateTime value)
        => Convert.ToDateTime(value);

    public static DateTime ToDateTime(this byte value)
        => Convert.ToDateTime(value);

    public static DateTime ToDateTime(this double value)
        => Convert.ToDateTime(value);

    public static DateTime ToDateTime(this short value)
        => Convert.ToDateTime(value);

    public static DateTime ToDateTime(this bool value)
        => Convert.ToDateTime(value);

    public static DateTime ToDateTime(this decimal value)
        => Convert.ToDateTime(value);

    public static decimal ToDecimal(this sbyte value)
        => Convert.ToDecimal(value);

    public static decimal ToDecimal(this ulong value)
        => Convert.ToDecimal(value);

    public static decimal ToDecimal(this uint value)
        => Convert.ToDecimal(value);

    public static decimal ToDecimal(this ushort value)
        => Convert.ToDecimal(value);

    public static decimal ToDecimal(this string? value, IFormatProvider? provider)
        => Convert.ToDecimal(value, provider);

    public static decimal ToDecimal(this string? value)
        => Convert.ToDecimal(value);

    public static decimal ToDecimal(this float value)
        => Convert.ToDecimal(value);

    public static decimal ToDecimal(this object? value, IFormatProvider? provider)
        => Convert.ToDecimal(value, provider);

    public static decimal ToDecimal(this object? value)
        => Convert.ToDecimal(value);

    public static decimal ToDecimal(this long value)
        => Convert.ToDecimal(value);

    public static decimal ToDecimal(this int value)
        => Convert.ToDecimal(value);

    public static decimal ToDecimal(this short value)
        => Convert.ToDecimal(value);

    public static decimal ToDecimal(this double value)
        => Convert.ToDecimal(value);

    public static decimal ToDecimal(this decimal value)
        => Convert.ToDecimal(value);

    public static decimal ToDecimal(this DateTime value)
        => Convert.ToDecimal(value);

    public static decimal ToDecimal(this char value)
        => Convert.ToDecimal(value);

    public static decimal ToDecimal(this byte value)
        => Convert.ToDecimal(value);

    public static decimal ToDecimal(this bool value)
        => Convert.ToDecimal(value);

    public static double ToDouble(this ulong value)
        => Convert.ToDouble(value);

    public static double ToDouble(this uint value)
        => Convert.ToDouble(value);

    public static double ToDouble(this ushort value)
        => Convert.ToDouble(value);

    public static double ToDouble(this string? value, IFormatProvider? provider)
        => Convert.ToDouble(value, provider);

    public static double ToDouble(this string? value)
        => Convert.ToDouble(value);

    public static double ToDouble(this sbyte value)
        => Convert.ToDouble(value);

    public static double ToDouble(this object? value, IFormatProvider? provider)
        => Convert.ToDouble(value, provider);

    public static double ToDouble(this float value)
        => Convert.ToDouble(value);

    public static double ToDouble(this long value)
        => Convert.ToDouble(value);

    public static double ToDouble(this int value)
        => Convert.ToDouble(value);

    public static double ToDouble(this short value)
        => Convert.ToDouble(value);

    public static double ToDouble(this double value)
        => Convert.ToDouble(value);

    public static double ToDouble(this decimal value)
        => Convert.ToDouble(value);

    public static double ToDouble(this DateTime value)
        => Convert.ToDouble(value);

    public static double ToDouble(this char value)
        => Convert.ToDouble(value);

    public static double ToDouble(this byte value)
        => Convert.ToDouble(value);

    public static double ToDouble(this bool value)
        => Convert.ToDouble(value);

    public static double ToDouble(this object? value)
        => Convert.ToDouble(value);

    public static string ToHexString(this ReadOnlySpan<byte> bytes)
        => Convert.ToHexString(bytes);

    public static string ToHexString(this byte[] inArray)
        => Convert.ToHexString(inArray);

    public static string ToHexString(this byte[] inArray, int offset, int length)
        => Convert.ToHexString(inArray, offset, length);

    public static short ToInt16(this ulong value)
        => Convert.ToInt16(value);

    public static short ToInt16(this uint value)
        => Convert.ToInt16(value);

    public static short ToInt16(this ushort value)
        => Convert.ToInt16(value);

    public static short ToInt16(this string? value, int fromBase)
        => Convert.ToInt16(value, fromBase);

    public static short ToInt16(this string? value, IFormatProvider? provider)
        => Convert.ToInt16(value, provider);

    public static short ToInt16(this string? value)
        => Convert.ToInt16(value);

    public static short ToInt16(this float value)
        => Convert.ToInt16(value);

    public static short ToInt16(this sbyte value)
        => Convert.ToInt16(value);

    public static short ToInt16(this object? value, IFormatProvider? provider)
        => Convert.ToInt16(value, provider);

    public static short ToInt16(this int value)
        => Convert.ToInt16(value);

    public static short ToInt16(this long value)
        => Convert.ToInt16(value);

    public static short ToInt16(this short value)
        => Convert.ToInt16(value);

    public static short ToInt16(this double value)
        => Convert.ToInt16(value);

    public static short ToInt16(this decimal value)
        => Convert.ToInt16(value);

    public static short ToInt16(this DateTime value)
        => Convert.ToInt16(value);

    public static short ToInt16(this char value)
        => Convert.ToInt16(value);

    public static short ToInt16(this byte value)
        => Convert.ToInt16(value);

    public static short ToInt16(this bool value)
        => Convert.ToInt16(value);

    public static short ToInt16(this object? value)
        => Convert.ToInt16(value);

    public static int ToInt32(this float value)
        => Convert.ToInt32(value);

    public static int ToInt32(this string? value)
        => Convert.ToInt32(value);

    public static int ToInt32(this string? value, IFormatProvider? provider)
        => Convert.ToInt32(value, provider);

    public static int ToInt32(this sbyte value)
        => Convert.ToInt32(value);

    public static int ToInt32(this ushort value)
        => Convert.ToInt32(value);

    public static int ToInt32(this uint value)
        => Convert.ToInt32(value);

    public static int ToInt32(this ulong value)
        => Convert.ToInt32(value);

    public static int ToInt32(this string? value, int fromBase)
        => Convert.ToInt32(value, fromBase);

    public static int ToInt32(this object? value)
        => Convert.ToInt32(value);

    public static int ToInt32(this object? value, IFormatProvider? provider)
        => Convert.ToInt32(value, provider);

    public static int ToInt32(this long value)
        => Convert.ToInt32(value);

    public static int ToInt32(this int value)
        => Convert.ToInt32(value);

    public static int ToInt32(this short value)
        => Convert.ToInt32(value);

    public static int ToInt32(this double value)
        => Convert.ToInt32(value);

    public static int ToInt32(this decimal value)
        => Convert.ToInt32(value);

    public static int ToInt32(this DateTime value)
        => Convert.ToInt32(value);

    public static int ToInt32(this char value)
        => Convert.ToInt32(value);

    public static int ToInt32(this byte value)
        => Convert.ToInt32(value);

    public static int ToInt32(this bool value)
        => Convert.ToInt32(value);

    public static long ToInt64(this float value)
        => Convert.ToInt64(value);

    public static long ToInt64(this string? value)
        => Convert.ToInt64(value);

    public static long ToInt64(this string? value, IFormatProvider? provider)
        => Convert.ToInt64(value, provider);

    public static long ToInt64(this string? value, int fromBase)
        => Convert.ToInt64(value, fromBase);

    public static long ToInt64(this object? value)
        => Convert.ToInt64(value);

    public static long ToInt64(this uint value)
        => Convert.ToInt64(value);

    public static long ToInt64(this ulong value)
        => Convert.ToInt64(value);

    public static long ToInt64(this sbyte value)
        => Convert.ToInt64(value);

    public static long ToInt64(this ushort value)
        => Convert.ToInt64(value);

    public static long ToInt64(this object? value, IFormatProvider? provider)
        => Convert.ToInt64(value, provider);

    public static long ToInt64(this short value)
        => Convert.ToInt64(value);

    public static long ToInt64(this long value)
        => Convert.ToInt64(value);

    public static long ToInt64(this int value)
        => Convert.ToInt64(value);

    public static long ToInt64(this double value)
        => Convert.ToInt64(value);

    public static long ToInt64(this decimal value)
        => Convert.ToInt64(value);

    public static long ToInt64(this DateTime value)
        => Convert.ToInt64(value);

    public static long ToInt64(this char value)
        => Convert.ToInt64(value);

    public static long ToInt64(this byte value)
        => Convert.ToInt64(value);

    public static long ToInt64(this bool value)
        => Convert.ToInt64(value);

    public static sbyte ToSByte(this string value, IFormatProvider? provider)
        => Convert.ToSByte(value, provider);

    public static sbyte ToSByte(this float value)
        => Convert.ToSByte(value);

    public static sbyte ToSByte(this string? value, int fromBase)
        => Convert.ToSByte(value, fromBase);

    public static sbyte ToSByte(this string? value)
        => Convert.ToSByte(value);

    public static sbyte ToSByte(this uint value)
        => Convert.ToSByte(value);

    public static sbyte ToSByte(this ulong value)
        => Convert.ToSByte(value);

    public static sbyte ToSByte(this sbyte value)
        => Convert.ToSByte(value);

    public static sbyte ToSByte(this ushort value)
        => Convert.ToSByte(value);

    public static sbyte ToSByte(this object? value, IFormatProvider? provider)
        => Convert.ToSByte(value, provider);

    public static sbyte ToSByte(this int value)
        => Convert.ToSByte(value);

    public static sbyte ToSByte(this long value)
        => Convert.ToSByte(value);

    public static sbyte ToSByte(this object? value)
        => Convert.ToSByte(value);

    public static sbyte ToSByte(this byte value)
        => Convert.ToSByte(value);

    public static sbyte ToSByte(this char value)
        => Convert.ToSByte(value);

    public static sbyte ToSByte(this DateTime value)
        => Convert.ToSByte(value);

    public static sbyte ToSByte(this bool value)
        => Convert.ToSByte(value);

    public static sbyte ToSByte(this double value)
        => Convert.ToSByte(value);

    public static sbyte ToSByte(this short value)
        => Convert.ToSByte(value);

    public static sbyte ToSByte(this decimal value)
        => Convert.ToSByte(value);

    public static float ToSingle(this ulong value)
        => Convert.ToSingle(value);

    public static float ToSingle(this uint value)
        => Convert.ToSingle(value);

    public static float ToSingle(this ushort value)
        => Convert.ToSingle(value);

    public static float ToSingle(this string? value, IFormatProvider? provider)
        => Convert.ToSingle(value, provider);

    public static float ToSingle(this string? value)
        => Convert.ToSingle(value);

    public static float ToSingle(this float value)
        => Convert.ToSingle(value);

    public static float ToSingle(this sbyte value)
        => Convert.ToSingle(value);

    public static float ToSingle(this object? value, IFormatProvider? provider)
        => Convert.ToSingle(value, provider);

    public static float ToSingle(this object? value)
        => Convert.ToSingle(value);

    public static float ToSingle(this int value)
        => Convert.ToSingle(value);

    public static float ToSingle(this short value)
        => Convert.ToSingle(value);

    public static float ToSingle(this double value)
        => Convert.ToSingle(value);

    public static float ToSingle(this decimal value)
        => Convert.ToSingle(value);

    public static float ToSingle(this DateTime value)
        => Convert.ToSingle(value);

    public static float ToSingle(this char value)
        => Convert.ToSingle(value);

    public static float ToSingle(this byte value)
        => Convert.ToSingle(value);

    public static float ToSingle(this bool value)
        => Convert.ToSingle(value);

    public static float ToSingle(this long value)
        => Convert.ToSingle(value);

    public static string ToString(this float value)
        => Convert.ToString(value);

    public static string ToString(this long value, int toBase)
        => Convert.ToString(value, toBase);

    public static string? ToString(this object? value)
        => Convert.ToString(value);

    public static string? ToString(this object? value, IFormatProvider? provider)
        => Convert.ToString(value, provider);

    public static string ToString(this sbyte value)
        => Convert.ToString(value);

    public static string ToString(this sbyte value, IFormatProvider? provider)
        => Convert.ToString(value, provider);

    public static string ToString(this float value, IFormatProvider? provider)
        => Convert.ToString(value, provider);

    public static string ToString(this short value, int toBase)
        => Convert.ToString(value, toBase);

    public static string? ToString(this string? value, IFormatProvider? provider)
        => Convert.ToString(value, provider);

    public static string ToString(this ushort value)
        => Convert.ToString(value);

    public static string ToString(this ushort value, IFormatProvider? provider)
        => Convert.ToString(value, provider);

    public static string ToString(this uint value)
        => Convert.ToString(value);

    public static string ToString(this uint value, IFormatProvider? provider)
        => Convert.ToString(value, provider);

    public static string ToString(this ulong value)
        => Convert.ToString(value);

    public static string ToString(this ulong value, IFormatProvider? provider)
        => Convert.ToString(value, provider);

    public static string ToString(this long value, IFormatProvider? provider)
        => Convert.ToString(value, provider);

    public static string? ToString(this string? value)
        => Convert.ToString(value);

    public static string ToString(this long value)
        => Convert.ToString(value);

    public static string ToString(this short value, IFormatProvider? provider)
        => Convert.ToString(value, provider);

    public static string ToString(this int value, IFormatProvider? provider)
        => Convert.ToString(value, provider);

    public static string ToString(this bool value)
        => Convert.ToString(value);

    public static string ToString(this bool value, IFormatProvider? provider)
        => Convert.ToString(value, provider);

    public static string ToString(this byte value)
        => Convert.ToString(value);

    public static string ToString(this byte value, IFormatProvider? provider)
        => Convert.ToString(value, provider);

    public static string ToString(this char value)
        => Convert.ToString(value);

    public static string ToString(this char value, IFormatProvider? provider)
        => Convert.ToString(value, provider);

    public static string ToString(this DateTime value)
        => Convert.ToString(value);

    public static string ToString(this byte value, int toBase)
        => Convert.ToString(value, toBase);

    public static string ToString(this decimal value)
        => Convert.ToString(value);

    public static string ToString(this decimal value, IFormatProvider? provider)
        => Convert.ToString(value, provider);

    public static string ToString(this double value)
        => Convert.ToString(value);

    public static string ToString(this double value, IFormatProvider? provider)
        => Convert.ToString(value, provider);

    public static string ToString(this short value)
        => Convert.ToString(value);

    public static string ToString(this int value, int toBase)
        => Convert.ToString(value, toBase);

    public static string ToString(this int value)
        => Convert.ToString(value);

    public static string ToString(this DateTime value, IFormatProvider? provider)
        => Convert.ToString(value, provider);

    public static ushort ToUInt16(this float value)
        => Convert.ToUInt16(value);

    public static ushort ToUInt16(this string? value)
        => Convert.ToUInt16(value);

    public static ushort ToUInt16(this string? value, IFormatProvider? provider)
        => Convert.ToUInt16(value, provider);

    public static ushort ToUInt16(this ulong value)
        => Convert.ToUInt16(value);

    public static ushort ToUInt16(this ushort value)
        => Convert.ToUInt16(value);

    public static ushort ToUInt16(this uint value)
        => Convert.ToUInt16(value);

    public static ushort ToUInt16(this sbyte value)
        => Convert.ToUInt16(value);

    public static ushort ToUInt16(this string? value, int fromBase)
        => Convert.ToUInt16(value, fromBase);

    public static ushort ToUInt16(this object? value, IFormatProvider? provider)
        => Convert.ToUInt16(value, provider);

    public static ushort ToUInt16(this double value)
        => Convert.ToUInt16(value);

    public static ushort ToUInt16(this long value)
        => Convert.ToUInt16(value);

    public static ushort ToUInt16(this object? value)
        => Convert.ToUInt16(value);

    public static ushort ToUInt16(this byte value)
        => Convert.ToUInt16(value);

    public static ushort ToUInt16(this char value)
        => Convert.ToUInt16(value);

    public static ushort ToUInt16(this DateTime value)
        => Convert.ToUInt16(value);

    public static ushort ToUInt16(this bool value)
        => Convert.ToUInt16(value);

    public static ushort ToUInt16(this short value)
        => Convert.ToUInt16(value);

    public static ushort ToUInt16(this int value)
        => Convert.ToUInt16(value);

    public static ushort ToUInt16(this decimal value)
        => Convert.ToUInt16(value);

    public static uint ToUInt32(this sbyte value)
        => Convert.ToUInt32(value);

    public static uint ToUInt32(this uint value)
        => Convert.ToUInt32(value);

    public static uint ToUInt32(this ulong value)
        => Convert.ToUInt32(value);

    public static uint ToUInt32(this ushort value)
        => Convert.ToUInt32(value);

    public static uint ToUInt32(this string? value, int fromBase)
        => Convert.ToUInt32(value, fromBase);

    public static uint ToUInt32(this string? value, IFormatProvider? provider)
        => Convert.ToUInt32(value, provider);

    public static uint ToUInt32(this string? value)
        => Convert.ToUInt32(value);

    public static uint ToUInt32(this float value)
        => Convert.ToUInt32(value);

    public static uint ToUInt32(this object? value, IFormatProvider? provider)
        => Convert.ToUInt32(value, provider);

    public static uint ToUInt32(this byte value)
        => Convert.ToUInt32(value);

    public static uint ToUInt32(this long value)
        => Convert.ToUInt32(value);

    public static uint ToUInt32(this int value)
        => Convert.ToUInt32(value);

    public static uint ToUInt32(this short value)
        => Convert.ToUInt32(value);

    public static uint ToUInt32(this double value)
        => Convert.ToUInt32(value);

    public static uint ToUInt32(this decimal value)
        => Convert.ToUInt32(value);

    public static uint ToUInt32(this DateTime value)
        => Convert.ToUInt32(value);

    public static uint ToUInt32(this char value)
        => Convert.ToUInt32(value);

    public static uint ToUInt32(this bool value)
        => Convert.ToUInt32(value);

    public static uint ToUInt32(this object? value)
        => Convert.ToUInt32(value);

    public static ulong ToUInt64(this sbyte value)
        => Convert.ToUInt64(value);

    public static ulong ToUInt64(this float value)
        => Convert.ToUInt64(value);

    public static ulong ToUInt64(this string? value)
        => Convert.ToUInt64(value);

    public static ulong ToUInt64(this uint value)
        => Convert.ToUInt64(value);

    public static ulong ToUInt64(this string? value, int fromBase)
        => Convert.ToUInt64(value, fromBase);

    public static ulong ToUInt64(this ushort value)
        => Convert.ToUInt64(value);

    public static ulong ToUInt64(this object? value, IFormatProvider? provider)
        => Convert.ToUInt64(value, provider);

    public static ulong ToUInt64(this string? value, IFormatProvider? provider)
        => Convert.ToUInt64(value, provider);

    public static ulong ToUInt64(this object? value)
        => Convert.ToUInt64(value);

    public static ulong ToUInt64(this double value)
        => Convert.ToUInt64(value);

    public static ulong ToUInt64(this int value)
        => Convert.ToUInt64(value);

    public static ulong ToUInt64(this short value)
        => Convert.ToUInt64(value);

    public static ulong ToUInt64(this decimal value)
        => Convert.ToUInt64(value);

    public static ulong ToUInt64(this DateTime value)
        => Convert.ToUInt64(value);

    public static ulong ToUInt64(this char value)
        => Convert.ToUInt64(value);

    public static ulong ToUInt64(this byte value)
        => Convert.ToUInt64(value);

    public static ulong ToUInt64(this bool value)
        => Convert.ToUInt64(value);

    public static ulong ToUInt64(this ulong value)
        => Convert.ToUInt64(value);

    public static ulong ToUInt64(this long value)
        => Convert.ToUInt64(value);

    public static string Replace(this string input, string old, object @new)
        => input.Replace(old, @new.ToString());
}
