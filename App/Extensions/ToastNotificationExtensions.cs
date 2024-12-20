﻿using System;
using Microsoft.Toolkit.Uwp.Notifications;

namespace Percentage.App.Extensions;

internal static class ToastNotificationExtensions
{
    private const string ActionArgumentKey = "action";
    private const string NotificationTypeArgumentKey = "notificationType";

    internal static bool TryGetActionArgument(this ToastArguments arguments, out Action action)
    {
        return arguments.TryGetValue(ActionArgumentKey, out action);
    }

    internal static bool TryGetNotificationTypeArgument(this ToastArguments arguments, out NotificationType notificationType)
    {
        return arguments.TryGetValue(NotificationTypeArgumentKey, out notificationType);
    }

    internal static void ShowToastNotification(string header, string body, NotificationType notificationType)
    {
        if (notificationType == NotificationType.None)
        {
            throw new NotSupportedException($"Notification type {notificationType} is not supported.");
        }
        
        new ToastContentBuilder()
            .AddText(header)
            .AddText(body)
            .AddButton(new ToastButton().SetContent("Details")
                .AddArgument(ActionArgumentKey, Action.ViewDetails))
            .AddButton(new ToastButton().SetContent("Disable")
                .AddArgument(ActionArgumentKey, Action.DisableBatteryNotification)
                .AddArgument(NotificationTypeArgumentKey, notificationType))
            .AddButton(new ToastButtonDismiss())
            .Show();
    }

    internal enum Action
    {
        ViewDetails = 0,
        DisableBatteryNotification
    }

    internal enum NotificationType
    {
        None = 0,
        Critical,
        Low,
        High,
        Full
    }
}