﻿using WorkingWithStrings.Abstract;

namespace WorkingWithStrings.Commands;

internal abstract class ResultCommandBase : ICommand
{
    private readonly IOutputProvider _outputProvider;

    public ResultCommandBase(IOutputProvider outputProvider)
    {
        _outputProvider = outputProvider;
    }

    public abstract string Description { get; }

    public abstract string GetResult();

    public void Execute()
    {
        try
        {
            _outputProvider.WriteResult(GetResult());
        }
        catch (Exception ex)
        {
            Console.WriteLine("Cannot write output");
            Console.WriteLine(ex);
        }
    }
}
