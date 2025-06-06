﻿// PlantCareBuddy.Domain/Entities/Reminder.cs
using PlantCareBuddy.Domain.Enums;
using PlantCareBuddy.Domain.ValueObjects;
using System;

namespace PlantCareBuddy.Domain.Entities
{
    public class Reminder
    {
        public Guid Id { get; private set; }
        public Guid PlantId { get; private set; }
        public Plant Plant { get; private set; }

        public ReminderType Type { get; private set; }
        public string Title { get; private set; }
        public string Description { get; private set; }
        public DateTime DueDate { get; private set; }

        // Recurrence
        public RecurrencePattern? Recurrence { get; private set; }

        // Completion
        public bool IsCompleted { get; private set; }
        public DateTime? CompletedDate { get; private set; }

        // Audit
        public DateTime CreatedAt { get; private set; }
        public DateTime? UpdatedAt { get; private set; }

        private Reminder() { } // For EF

        public static Reminder Create(
            Guid plantId,
            ReminderType type,
            string title,
            string description,
            DateTime dueDate,
            RecurrencePattern recurrence)
        {
            if (string.IsNullOrWhiteSpace(title))
                throw new ArgumentException("Title is required.");

            if (dueDate < DateTime.UtcNow.Date)
                throw new ArgumentException("DueDate cannot be in the past.");

            return new Reminder
            {
                Id = Guid.NewGuid(),
                PlantId = plantId,
                Type = type,
                Title = title,
                Description = description,
                DueDate = dueDate,
                Recurrence = recurrence,
                CreatedAt = DateTime.UtcNow,
                IsCompleted = false
            };
        }

        public void MarkAsCompleted()
        {
            IsCompleted = true;
            CompletedDate = DateTime.UtcNow;
            UpdatedAt = DateTime.UtcNow;
        }

        public void ReactivateForNextOccurrence(DateTime nextDueDate)
        {
            IsCompleted = false;
            CompletedDate = null;
            DueDate = nextDueDate;
            UpdatedAt = DateTime.UtcNow;
        }

        public void UpdateTitle(string title)
        {
            if (string.IsNullOrWhiteSpace(title))
                throw new ArgumentException("Title is required.");
            Title = title;
            UpdatedAt = DateTime.UtcNow;
        }

        public void UpdateDescription(string description)
        {
            Description = description;
            UpdatedAt = DateTime.UtcNow;
        }

        public void UpdateDueDate(DateTime dueDate)
        {
            if (dueDate < DateTime.UtcNow.Date)
                throw new ArgumentException("DueDate cannot be in the past.");
            DueDate = dueDate;
            UpdatedAt = DateTime.UtcNow;
        }

        public void UpdateRecurrence(RecurrencePattern? recurrence)
        {
            Recurrence = recurrence;
            UpdatedAt = DateTime.UtcNow;
        }
    }
}