﻿using System;

using Hangfire.Client;
using Hangfire.MetaExtensions.Plugins;

using JetBrains.Annotations;

namespace Hangfire.MetaExtensions
{
    public static class MetaClientExtensions
    {
        public static IBackgroundJobClient AddOrUpdateMeta<T>([NotNull] this IBackgroundJobClient client, [NotNull] string key, [NotNull] T value)
        {
            if (client == null)
            {
                throw new ArgumentNullException(nameof(client));
            }
            if (key == null)
            {
                throw new ArgumentNullException(nameof(key));
            }
            if (value == null)
            {
                throw new ArgumentNullException(nameof(value));
            }

            ConsumableThreadStorage<Action<CreatingContext>>.Add(context => context.SetJobParameter(key, value));

            return client;
        }

        public static IBackgroundJobClient UseQueue([NotNull] this IBackgroundJobClient client, [NotNull] string queueName)
        {
            if (client == null)
            {
                throw new ArgumentNullException(nameof(client));
            }
            if (queueName == null)
            {
                throw new ArgumentNullException(nameof(queueName));
            }

            client.AddOrUpdateMeta(DynamicQueue.DynamicQueueKey, queueName);

            return client;
        }
    }
}