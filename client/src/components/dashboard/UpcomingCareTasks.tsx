import React from "react";
import { UpcomingCareDto } from "../../models/Dashboard/upcomingCareDto";
import styles from "./dashboard.module.css";

interface Props {
  tasks: UpcomingCareDto[];
}

const UpcomingCareTasks: React.FC<Props> = ({ tasks }) => (
  <section className={styles.section}>
    <h3>Upcoming Care Tasks</h3>
    {tasks.length === 0 ? (
      <p>No upcoming care tasks.</p>
    ) : (
      <ul className={styles.careList}>
        {tasks.map(task => (
          <li key={task.plantId + task.careType + task.dueDate}>
            <strong>{task.plantName}</strong>: {task.careType} on {new Date(task.dueDate).toLocaleDateString()}
            {task.isOverdue && <span className={styles.overdue}> (Overdue)</span>}
            {task.strategyName && <> | Strategy: {task.strategyName}</>}
          </li>
        ))}
      </ul>
    )}
  </section>
);

export default UpcomingCareTasks;
