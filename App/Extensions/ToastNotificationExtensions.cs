﻿using System;
using Microsoft.Toolkit.Uwp.Notifications;

namespace Percentage.App.Extensions;

internal static class ToastNotificationExtensions
{
    private const string ActionArgumentKey = "action";
    private const string NotificationTypeArgumentKey = "notificationType";

    internal static Action GetActionArgument(this ToastArguments arguments)
    {
        return arguments.GetEnum<Action>(ActionArgumentKey);
    }

    internal static NotificationType GetNotificationTypeArgument(this ToastArguments arguments)
    {
        return arguments.GetEnum<NotificationType>(NotificationTypeArgumentKey);
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