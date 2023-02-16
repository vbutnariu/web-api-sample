namespace Demo.Common.Model.Generic
{
    public class GenericResultModel<T> where T : class
    {
        public T Value { get; set; }
    }
}
