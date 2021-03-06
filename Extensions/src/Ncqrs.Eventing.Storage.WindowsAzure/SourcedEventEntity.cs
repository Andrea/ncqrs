﻿using System;
using System.Diagnostics.Contracts;
using Microsoft.WindowsAzure.StorageClient;
using Ncqrs.Eventing.Sourcing;

namespace Ncqrs.Eventing.Storage.WindowsAzure
{
    internal class SourcedEventEntity : TableServiceEntity
    {
        public long Sequence
        {
            get;
            set;
        }

        public SourcedEventEntity()
        {
            RowKey = Guid.NewGuid().ToString();
        }

        public static SourcedEventEntity FromEventSource(Ncqrs.Eventing.Sourcing.SourcedEvent evnt)
        {
            Contract.Requires<ArgumentNullException>(evnt != null, "The evnt cannot be null.");

            return new SourcedEventEntity
            {
                RowKey = evnt.EventIdentifier.ToString(), PartitionKey = evnt.EventSourceId.ToString(), Sequence = evnt.EventSequence
            };
        }
    }
}
