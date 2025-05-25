import React from "react";
import { Reminder } from "../../models/Reminder/reminder";
import { markReminderAsComplete, deleteReminder } from "../../api/reminderService";
import styles from "./dashboard.module.css"; // Use your existing CSS module

interface RemindersViewProps {
  reminders: Reminder[];
  loading: boolean;
}

const RemindersView: React.FC<RemindersViewProps> = ({ reminders, loading }) => {
  const handleComplete = async (id: string) => {
    await markReminderAsComplete(id);
    // Refresh list
    window.location.reload(); // Temporary solution - we'll improve this later
  };

  const handleDelete = async (id: string) => {
    await deleteReminder(id);
    // Refresh list
    window.location.reload(); // Temporary solution - we'll improve this later
  };

  if (loading) return <div className={styles.loading}>Loading reminders...</div>;
  if (!reminders.length)
    return (
      <div className={styles.emptyState}>
        <h3>Upcoming Reminders</h3>
        <div>No reminders scheduled.</div>
      </div>
    );

  return (
    <div className={styles.section}>
      <h3 className={styles.sectionTitle}>Upcoming Reminders</h3>
      <ul className={styles.reminderList}>
        {reminders.map(reminder => (
          <li key={reminder.id} className={styles.reminderCard}>
            <div className={styles.reminderHeader}>
              <span className={styles.reminderTitle}>
                {reminder.title} 
              </span>
              <span className={styles.reminderDue}>
                Due: {new Date(reminder.dueDate).toLocaleDateString()}
                {!reminder.isCompleted && new Date(reminder.dueDate) < new Date() && (
                  <span className={styles.overdue}>Overdue</span>
                )}
              </span>
            </div>
            <div className={styles.reminderMeta}>
              {reminder.plantName && (
                <span className={styles.reminderPlantName}>
                  <strong>Plant:</strong> {reminder.plantName}
                </span>
              )}
              {reminder.recurrence && (
                <span className={styles.reminderRecurrence}>
                  | Repeats: {reminder.recurrence.type} (every {reminder.recurrence.interval})
                  {reminder.isCompleted && (
                    <span> | Next: {new Date(reminder.dueDate).toLocaleDateString()}</span>
                  )}
                </span>
              )}
            </div>
            <div className={styles.reminderDescription}>{reminder.description}</div>
            <div className={styles.reminderActions}>
              {reminder.isCompleted ? (
                <span className={styles.completed}>✔ Completed</span>
              ) : (
                <>
                  <button
                    className={styles.completeButton}
                    onClick={() => handleComplete(reminder.id)}
                  >
                    Mark as Complete
                  </button>
                  <button
                    className={styles.deleteButton}
                    onClick={() => handleDelete(reminder.id)}
                  >
                    Delete
                  </button>
                </>
              )}
            </div>
          </li>
        ))}
      </ul>
    </div>
  );
};

export default RemindersView;
