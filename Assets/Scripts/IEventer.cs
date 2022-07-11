using System;

public interface IEventer<T>
{
    public Action<T> CallbackEvent { get; set; }

    public void Subscribe(Action<T> callback);
}
