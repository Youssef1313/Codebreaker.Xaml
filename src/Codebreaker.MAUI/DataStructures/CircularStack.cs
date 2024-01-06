using System.Diagnostics.CodeAnalysis;

namespace Codebreaker.MAUI.DataStructures;

internal class CircularStack<T>(int size)
    where T : notnull
{
    private T?[] _data = new T?[size];

    private int _top = -1;

    private int _bottom = 0;

    public bool IsFull => (_top + 1) % Capacity == _bottom;

    public bool IsEmpty => _top == -1;

    public int Capacity => _data.Length;

    public void Push(T item)
    {
        if (IsFull)
            _bottom = (_bottom + 1) % Capacity;

        _top = (_top + 1) % Capacity;
        _data[_top] = item;
    }

    public bool TryPop([MaybeNullWhen(false)] out T item)
    {
        if (IsEmpty)
        {
            item = default;
            return false;
        }

        item = _data[_top] ?? throw new InvalidOperationException("The item to be returned is null, which should never be case.");
        _data[_top] = default;
        _top = (_top - 1) % Capacity;
        return true;
    }

    public T Pop()
    {
        if (TryPop(out var item))
            return item;

        throw new EmptyException("Stack is empty");
    }

    public bool TryPeek([MaybeNullWhen(false)] out T item)
    {
        if (IsEmpty)
        {
            item = default;
            return false;
        }

        item = _data[_top] ?? throw new InvalidOperationException("The item to be returned is null, which should never be case.");
        return true;
    }

    public T Peek()
    {
        if (TryPeek(out var item))
            return item;

        throw new EmptyException("Stack is empty");
    }

    public void Clear()
    {
        _data = new T?[size];
    }
}

internal class EmptyException : Exception
{
    public EmptyException()
    {
    }

    public EmptyException(string? message) : base(message)
    {
    }

    public EmptyException(string? message, Exception? innerException) : base(message, innerException)
    {
    }
}