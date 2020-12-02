public interface IReadOnlyAEBAction
{
    bool Active { get; }
    double Breakpedal { get; }
    string Message { get; }
}