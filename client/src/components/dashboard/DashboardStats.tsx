import React from "react";
import { DashboardStatsDto } from "../../models/Dashboard/dashboardStatsDto";
import styles from "./dashboard.module.css";

interface Props {
  stats: DashboardStatsDto;
}

const DashboardStats: React.FC<Props> = ({ stats }) => (
  <section className={styles.section}>
    <h3>Statistics</h3>
    <ul className={styles.statsList}>
      <li>Total Plants: {stats.totalPlants}</li>
      <li>Healthy Plants: {stats.healthyPlants}</li>
      <li>Needs Attention: {stats.plantsNeedingAttention}</li>
      <li>Upcoming Tasks: {stats.upcomingCareTasks}</li>
    </ul>
  </section>
);

export default DashboardStats;
