// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using System;
using Xunit;
public class AllocBug
{
    public int ret = 0;
    public AllocBug()
    {
    }

    [Fact]
    public static int TestEntryPoint()
    {
        AllocBug ab = new AllocBug();

        ab.RunTest(41938869, 41943020);
        Console.WriteLine(100 == ab.ret ? "Test Passed" : "Test Failed");
        return ab.ret;
    }
    private void RunTest(int start, int stop)
    {
        for (int i = start; i <= stop; i++)
        {
            Allocate(i);
            if (0 != ret)
                break;
        }

        if (0 == ret)
            ret = 100;
    }

    private void Allocate(int bytesToAlloc)
    {
        try
        {
            byte[] buffer = new byte[bytesToAlloc];
        }
        catch (Exception)
        {
            Console.WriteLine("Unexpected Exception when allocating "+bytesToAlloc+" bytes.");
            ret = -1;
        }
    }
}
