namespace Demo.Common.Calculation
{
    public interface IScalarCalculator <TInput, TResult>
    {
        TResult Calculate(TInput value);
    }
}
