namespace Extensions
{
    public static class ReflectionExtensions
    {
        public static object GetPropValue(this object src, string propName)
        {
            return src.GetType().GetProperty(propName).GetValue(src, null);
        }

        public static void SetPropValue(this object src, string propName, string propValue)
        {
            src.GetType().GetProperty(propName).SetValue(src, propValue, null);
        }
    }
}
