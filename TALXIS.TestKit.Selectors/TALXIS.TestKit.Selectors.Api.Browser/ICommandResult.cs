﻿// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT license.

using System;

namespace TALXIS.TestKit.Selectors.Browser
{
    public interface ICommandResult
    {
        string CommandName { get; }

        int ExecutionAttempts { get; }

        DateTime? StartTime { get; }

        Exception Exception { get; }

        bool? Success { get; }

        DateTime? StopTime { get; }

        double ExecutionTime { get; }

        double TransitionTime { get; set; }

        double ThinkTime { get; set; }

        int Depth { get; set; }
    }
}